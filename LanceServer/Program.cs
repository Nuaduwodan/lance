using System.CommandLine;
using System.Reflection;
using LanceServer.Core.Configuration;
using LanceServer.Core.Configuration.DataModel;
using LanceServer.Core.Stream;
using LanceServer.Core.Workspace;
using LanceServer.Parser;
using LanceServer.Preprocessor;
using LanceServer.RequestHandler.Diagnostic;
using LanceServer.RequestHandler.GoToDefinition;
using LanceServer.RequestHandler.Hover;
using LanceServer.RequestHandler.SemanticToken;
using LspTypes;
using Newtonsoft.Json;
using StreamJsonRpc;
using Command = System.CommandLine.Command;

namespace LanceServer;

/// <summary>
/// The program class with the main method.
/// It decides in which mode the server should be started and starts it.
/// </summary>
internal static class Program
{
    /// <summary>
    /// Provides a robust command line interface to start the server in either the language server mode or the command line mode.
    /// </summary>
    private static void Main(string[] args)
    {
        var rootCommand = new RootCommand("Lance Server: Language appliance for numerical control code. There is an extension mode and a command line mode.");

        // language server mode
        var languageServerCommand = new Command("language-server", "The mode to run with an extension of an editor supporting the language server protocol");
        languageServerCommand.AddAlias("ls");
        rootCommand.AddCommand(languageServerCommand);

#pragma warning disable VSTHRD002
        languageServerCommand.SetHandler(() => StartLanguageServerAsync().Wait());
#pragma warning restore VSTHRD002

        // command line mode
        var commandLineCommand = new Command("command-line", "The mode to run in the command line, takes a configuration and folders belonging to the same project and prints a report");
        commandLineCommand.AddAlias("cl");
        rootCommand.AddCommand(commandLineCommand);
        
        var config = new Option<FileInfo>(
            name: "--config-file",
            description: "Specify the path to the json config file.", 
            getDefaultValue: GetDefaultConfig);
        config.AddAlias("-c");
        config.AddValidator(result =>
        {
            var fileInfo = result.GetValueForOption(config);
            if (!fileInfo!.Exists)
            {
                result.ErrorMessage = $"The file {fileInfo.FullName} does not exist.";
            }
        });
        commandLineCommand.AddOption(config);
        
        var folders = new Option<DirectoryInfo[]>(
            name: "--workspace-folders",
            description: "Provide a list of folders which will be processed.");
        folders.AddAlias("-w");
        folders.Arity = ArgumentArity.OneOrMore;
        folders.AllowMultipleArgumentsPerToken = true;
        folders.IsRequired = true;
        folders.AddValidator(result =>
        {
            foreach (var folder in result.GetValueForOption(folders)!.Where(folder => !folder.Exists))
            {
                result.ErrorMessage = $"The folder {folder.FullName} does not exist.";
                return;
            }
        });
        commandLineCommand.AddOption(folders);
        
        var printLevel = new Option<DiagnosticSeverity>(
            name: "--print-level",
            description: "The minimum severity level to be printed to standard out.",
            getDefaultValue: () => DiagnosticSeverity.Warning);
        printLevel.AddAlias("-p");
        commandLineCommand.AddOption(printLevel);
        
        var reportLevel = new Option<DiagnosticSeverity>(
            name: "--report-level",
            description: "The minimum severity level to be reported with the return code.",
            getDefaultValue: () => DiagnosticSeverity.Error);
        reportLevel.AddAlias("-r");
        commandLineCommand.AddOption(reportLevel);
        
        commandLineCommand.SetHandler(StartCommandLine, folders, config, printLevel, reportLevel);

        rootCommand.Invoke(args);
    }

    private static FileInfo GetDefaultConfig()
    {
        var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var defaultConfigPath = Path.Join(basePath, "config.json");
        return new FileInfo(defaultConfigPath);
    }

    private static async Task StartLanguageServerAsync()
    {
        Stream receivingStream = new StreamSplitter(Console.OpenStandardInput(), new StreamLog("editor"), StreamSplitter.StreamOwnership.OwnNone);
        Stream sendingStream = new StreamSplitter(Console.OpenStandardOutput(), new StreamLog("server"), StreamSplitter.StreamOwnership.OwnNone);
        var formatter = new JsonMessageFormatter();
        var rpcMessageHandler = new HeaderDelimitedMessageHandler(sendingStream, receivingStream, formatter);
        var jsonRpc = new JsonRpc(rpcMessageHandler);

        var docConfig = Path.Join(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "language_token_documentation.json");
        var documentation = JsonConvert.DeserializeObject<DocumentationConfiguration>(FileUtil.ReadFileContent(docConfig)) 
                            ?? throw new FileNotFoundException(docConfig + " not found");
        var config = new ConfigurationManager(documentation);
        var parser = new ParserManager();
        var customPreprocessor = new PlaceholderPreprocessor(config);
        var workspace = new Workspace(parser, customPreprocessor, config);
        var semanticTokenHandler = new SemanticTokenHandler();
        var hoverHandler = new HoverHandler(config);
        var gotoDefinitionHandler = new GotoDefinitionHandler();
        var diagnosticHandler = new DiagnosticHandler();
        
        // ReSharper disable once UnusedVariable
        var lsp = new LSPServer(
            jsonRpc,
            workspace,
            config,
            semanticTokenHandler,
            hoverHandler,
            gotoDefinitionHandler,
            diagnosticHandler
        );
            
        await Task.Delay(-1);
    }

    private static void StartCommandLine(DirectoryInfo[] directories, FileInfo configFileInfo, DiagnosticSeverity printLevel, DiagnosticSeverity reportLevel)
    {
        if (directories.Length == 0)
        {
            Console.Out.WriteLine("No directory provided.");
            return;
        }

        var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var docConfigPath = Path.Join(basePath, "language_token_documentation.json");
        var docConfig = JsonConvert.DeserializeObject<DocumentationConfiguration>(FileUtil.ReadFileContent(docConfigPath)) 
                        ?? throw new FileNotFoundException(docConfigPath + " not found");
        var serverConfigPath = configFileInfo.Name;
        var serverConfig = JsonConvert.DeserializeObject<ServerConfiguration>(FileUtil.ReadFileContent(serverConfigPath)) 
                           ?? throw new FileNotFoundException(serverConfigPath + " not found");

        var uris = directories.Select(directory => new Uri(directory.FullName)).ToArray();
        var config = new ConfigurationManager(docConfig, uris, serverConfig);
        var parser = new ParserManager();
        var customPreprocessor = new PlaceholderPreprocessor(config);
        var workspace = new Workspace(parser, customPreprocessor, config);
        var diagnosticHandler = new DiagnosticHandler();

        var commandLine = new CommandLine(workspace, diagnosticHandler);

        Environment.ExitCode = commandLine.ProcessFiles(printLevel, reportLevel);
    }
}