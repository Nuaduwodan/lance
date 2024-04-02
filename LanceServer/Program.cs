using System.CommandLine;
using System.Diagnostics;
using System.Reflection;
using LanceServer.Core.Configuration;
using LanceServer.Core.Stream;
using LanceServer.Core.Workspace;
using LanceServer.Parser;
using LanceServer.Preprocessor;
using LanceServer.RequestHandler.DiagnosticHandler;
using Microsoft.Extensions.Configuration;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
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
        var waitForDebugger = false;
        
        while (waitForDebugger && !Debugger.IsAttached)
        {
            Thread.Sleep(100);
        }
        
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

    private static Task StartLanguageServerAsync()
    {
        Stream receivingStream = new StreamSplitter(Console.OpenStandardInput(), new StreamLog("editor"), StreamSplitter.StreamOwnership.OwnNone);
        Stream sendingStream = new StreamSplitter(Console.OpenStandardOutput(), new StreamLog("server"), StreamSplitter.StreamOwnership.OwnNone);
        var server = new LSPServer(receivingStream, sendingStream);
        return server.RunAsync();
    }
    
    private static void StartCommandLine(DirectoryInfo[] directories, FileInfo configFileInfo, DiagnosticSeverity printLevel, DiagnosticSeverity reportLevel)
    {
        if (directories.Length == 0)
        {
            Console.Out.WriteLine("No directory provided.");
            return;
        }

        var serverConfigPath = configFileInfo.Name;
        var configuration = new ConfigurationBuilder()
            .AddJsonFile(serverConfigPath)
            .Build();

        var config = new CommandLineConfigurationManager(configuration);
        var parser = new ParserManager();
        var customPreprocessor = new PlaceholderPreprocessor(config);
        var workspace = new Workspace(parser, customPreprocessor, config);
        var diagnosticHandler = new DocumentDiagnosticLogic();

        var commandLine = new CommandLine(workspace, diagnosticHandler);

        Environment.ExitCode = commandLine.ProcessFiles(directories, printLevel, reportLevel);
    }
}