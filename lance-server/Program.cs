using LanceServer.Core.Configuration;
using LanceServer.Core.Stream;
using LanceServer.Core.Workspace;
using LanceServer.Hover;
using LanceServer.Parser;
using LanceServer.Preprocessor;
using LanceServer.SemanticToken;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LanceServer
{
    class Program
    {
        private static void Main(string[] args) => MainAsync(args).Wait();

        private static async Task MainAsync(string[] args)
        {
            /*var builder = Host.CreateDefaultBuilder(args);

            builder.ConfigureServices(services =>
                services.AddSingleton(LspServerFactory)
                    .AddSingleton<IConfigurationManager, ConfigurationManager>()
                    .AddSingleton<ICustomPreprocessor, CustomPreprocessor>()
                    .AddSingleton<IWorkspace, Workspace>()
                    .AddSingleton<IParserManager, ParserManager>()
                    .AddSingleton<IHoverHandler, HoverHandler>()
            );

            using var host = builder.Build();

            host.Run();
            */
            Stream stdin = new StreamSplitter(Console.OpenStandardInput(), new StreamLog("editor"), StreamSplitter.StreamOwnership.OwnNone);
            Stream stdout = new StreamSplitter(Console.OpenStandardOutput(), new StreamLog("server"), StreamSplitter.StreamOwnership.OwnNone);
            var config = new ConfigurationManager();
            var lsp = new LSPServer(
                stdout,
                stdin,
                new Workspace(new ParserManager(), new CustomPreprocessor(config),config),
                config,
                new SemanticTokenHandler(),
                new HoverHandler(config)
            );
            
            await Task.Delay(-1);
        }

        private static LSPServer LspServerFactory(IServiceProvider provider)
        {
            Stream stdin = new StreamSplitter(Console.OpenStandardInput(), new StreamLog("editor"), StreamSplitter.StreamOwnership.OwnNone);
            Stream stdout = new StreamSplitter(Console.OpenStandardOutput(), new StreamLog("server"), StreamSplitter.StreamOwnership.OwnNone);

            return new LSPServer(
                stdout,
                stdin,
                provider.GetService<IWorkspace>()!,
                provider.GetService<IConfigurationManager>()!,
                provider.GetService<ISemanticTokenHandler>()!,
                provider.GetService<IHoverHandler>()!
            );
        }
    }
}