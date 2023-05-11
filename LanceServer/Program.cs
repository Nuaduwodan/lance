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
using Newtonsoft.Json;
using StreamJsonRpc;
using Command = System.CommandLine.Command;

namespace LanceServer;

class Program
{
    private static void Main(string[] args)
    {
        var rootCommand = new RootCommand("Lance Server: Language appliance for numerical control code. There is an extension mode and a command line mode.");

        var languageServerCommand = new Command("language-server", "The mode to run with an extension of an editor supporting the language server protocol");
        languageServerCommand.AddAlias("ls");
        var commandLineCommand = new Command("command-line", "The mode to run in the command line, take arguments and print a report");
        commandLineCommand.AddAlias("cl");
        
        var config = new Option<FileInfo>(
            name: "--config-file",
            description: "specify the path to the json config file");
        config.AddAlias("-c");
        config.IsRequired = true;
        
        var folders = new Option<Uri[]>(
            name: "--workspace-folders",
            description: "provide a list of folders which will be processed");
        folders.AddAlias("-w");
        folders.IsRequired = true;

        rootCommand.AddCommand(languageServerCommand);
        rootCommand.AddCommand(commandLineCommand);
        commandLineCommand.AddOption(config);
        commandLineCommand.AddOption(folders);

        languageServerCommand.SetHandler(() => StartLanguageServerAsync().Wait());
        commandLineCommand.SetHandler(StartCommandLine, folders, config);

        rootCommand.Invoke(args);
    }

    private static async Task StartLanguageServerAsync()
    {
        Stream receivingStream = new StreamSplitter(Console.OpenStandardInput(), new StreamLog("editor"), StreamSplitter.StreamOwnership.OwnNone);
        Stream sendingStream = new StreamSplitter(Console.OpenStandardOutput(), new StreamLog("server"), StreamSplitter.StreamOwnership.OwnNone);
        var formatter = new JsonMessageFormatter();
        var rpcMessageHandler = new HeaderDelimitedMessageHandler(sendingStream, receivingStream, formatter);
        var jsonRpc = new JsonRpc(rpcMessageHandler);

        var appDir = Path.Join(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "documentation.json");
        var documentation = JsonConvert.DeserializeObject<DocumentationConfiguration>(FileUtil.ReadFileContent(appDir)) ?? throw new FileNotFoundException(appDir + " not found");
        var config = new ConfigurationManager(documentation);
        var parser = new ParserManager();
        var customPreprocessor = new PlaceholderPreprocessor(config);
        var workspace = new Workspace(parser, customPreprocessor, config);
        var semanticTokenHandler = new SemanticTokenHandler();
        var hoverHandler = new HoverHandler(config);
        var gotoDefinitionHandler = new GotoDefinitionHandler();
        var diagnosticHandler = new DiagnosticHandler();
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

    private static void StartCommandLine(Uri[] uris, FileInfo configFileInfo)
    {
        var appDir = Path.Join(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "documentation.json");
        var documentation = JsonConvert.DeserializeObject<DocumentationConfiguration>(FileUtil.ReadFileContent(appDir)) ?? throw new FileNotFoundException(appDir + " not found");
        var configFile = JsonConvert.DeserializeObject<ServerConfiguration>(FileUtil.ReadFileContent(configFileInfo.Name)) ?? throw new FileNotFoundException(configFileInfo.Name + " not found");
        var config = new ConfigurationManager(documentation, uris, configFile);
        var parser = new ParserManager();
        var customPreprocessor = new PlaceholderPreprocessor(config);
        var workspace = new Workspace(parser, customPreprocessor, config);
        var diagnosticHandler = new DiagnosticHandler();

        var commandLine = new CommandLine(workspace, diagnosticHandler);

        Environment.ExitCode = commandLine.ProcessFiles();
    }
}