using LanceServer.Core.Configuration;
using LanceServer.Core.Stream;
using LanceServer.Core.Workspace;
using LanceServer.Hover;
using LanceServer.Parser;
using LanceServer.Preprocessor;
using LanceServer.SemanticToken;

namespace LanceServer
{
    class Program
    {
        private static void Main(string[] args) => MainAsync(args).Wait();

        private static async Task MainAsync(string[] args)
        {
            Stream stdin = new StreamSplitter(Console.OpenStandardInput(), new StreamLog("editor"), StreamSplitter.StreamOwnership.OwnNone);
            Stream stdout = new StreamSplitter(Console.OpenStandardOutput(), new StreamLog("server"), StreamSplitter.StreamOwnership.OwnNone);
            var config = new ConfigurationManager();
            var parser = new ParserManager();
            var customPreprocessor = new CustomPreprocessor(config);
            var workspace = new Workspace(parser, customPreprocessor, config);
            var semanticTokenHandler = new SemanticTokenHandler();
            var hoverHandler = new HoverHandler(config);
            var lsp = new LSPServer(
                stdout,
                stdin,
                workspace,
                config,
                semanticTokenHandler,
                hoverHandler
            );
            
            await Task.Delay(-1);
        }
    }
}