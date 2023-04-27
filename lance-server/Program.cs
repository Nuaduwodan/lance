using LanceServer.Core.Configuration;
using LanceServer.Core.Stream;
using LanceServer.Core.Workspace;
using LanceServer.Diagnostic;
using LanceServer.GoToDefinition;
using LanceServer.Hover;
using LanceServer.Parser;
using LanceServer.Preprocessor;
using LanceServer.SemanticToken;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using StreamJsonRpc;

namespace LanceServer;

class Program
{
    private static void Main(string[] args) => MainAsync(args).Wait();

    private static async Task MainAsync(string[] args)
    {
        Stream receivingStream = new StreamSplitter(Console.OpenStandardInput(), new StreamLog("editor"), StreamSplitter.StreamOwnership.OwnNone);
        Stream sendingStream = new StreamSplitter(Console.OpenStandardOutput(), new StreamLog("server"), StreamSplitter.StreamOwnership.OwnNone);
        var formatter = new JsonMessageFormatter();
        var rpcMessageHandler = new HeaderDelimitedMessageHandler(sendingStream, receivingStream, formatter);
        var jsonRpc = new JsonRpc(rpcMessageHandler);
        
        var config = new ConfigurationManager();
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
}