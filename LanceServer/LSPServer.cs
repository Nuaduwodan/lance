using LanceServer.Core.Configuration;
using LanceServer.Core.Workspace;
using LanceServer.Parser;
using LanceServer.Preprocessor;
using LanceServer.RequestHandler.DiagnosticHandler;
using LanceServer.RequestHandler.GoToDefinition;
using LanceServer.RequestHandler.HoverHandler;
using LanceServer.RequestHandler.SemanticToken;
using Microsoft.Extensions.DependencyInjection;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using OmniSharp.Extensions.LanguageServer.Server;

namespace LanceServer;

/// <summary>
/// The remote procedure call interface to work as a language server as defined by the language server protocol.
/// </summary>
class LSPServer : IDisposable
{
    private ILanguageServer _server;
    private Stream _receivingStream;
    private Stream _sendingStream;

    public LSPServer(Stream receivingStream, Stream sendingStream)
    {
        _receivingStream = receivingStream;
        _sendingStream = sendingStream;
    }

    public async Task RunAsync()
    {
        _server = await LanguageServer.From(
            options =>
            {
                options
                    .WithInput(_receivingStream)
                    .WithOutput(_sendingStream)
                    .WithServices(RegisterServices)
                    .WithHandler<WorkspaceHandler>()
                    .WithHandler<ConfigurationHandler>()
                    .WithHandler<SemanticTokensHandler>()
                    .WithHandler<HoverHandler>()
                    .WithHandler<DefinitionHandler>();

                //.WithHandler<DocumentDiagnosticHandler>(); // currently not working
            });
        await _server.WaitForExit.ConfigureAwait(false);
    }

    private static void RegisterServices(IServiceCollection services)
    {
        services
            .AddSingleton<IConfigurationManager, ConfigurationManager>()
            .AddSingleton<IParserManager, ParserManager>()
            .AddSingleton<IPlaceholderPreprocessor, PlaceholderPreprocessor>()
            .AddSingleton<IWorkspace, Workspace>();
    }

    public void Dispose()
    {
        _server.Dispose();
    }
}