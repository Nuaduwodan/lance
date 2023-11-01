using LanceServer.Core.Workspace;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Document;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;

namespace LanceServer.RequestHandler.SemanticToken;

public class SemanticTokensHandler : SemanticTokensHandlerBase
{
    private SemanticTokensRegistrationOptions _registrationOptions;
    private readonly IWorkspace _workspace;
    private SemanticTokensLogic _semanticTokensLogic;

    public SemanticTokensHandler(IWorkspace workspace)
    {
        _workspace = workspace;
        _semanticTokensLogic = new SemanticTokensLogic();
        _registrationOptions = new SemanticTokensRegistrationOptions
        {
            Full = true,
            Range = false,
            Legend = new SemanticTokensLegend()
        };
    }
    
    protected override SemanticTokensRegistrationOptions CreateRegistrationOptions(SemanticTokensCapability capability, ClientCapabilities clientCapabilities)
    {
        return _registrationOptions;
    }

    protected override Task Tokenize(SemanticTokensBuilder builder, ITextDocumentIdentifierParams identifier, CancellationToken cancellationToken)
    {
        var document = _workspace.GetSymbolUseExtractedDocument(identifier.TextDocument.Uri.ToUri());
        _semanticTokensLogic.ProcessRequest(document, _workspace, builder);
        return Task.CompletedTask;
    }

    protected override Task<SemanticTokensDocument> GetSemanticTokensDocument(ITextDocumentIdentifierParams @params, CancellationToken cancellationToken)
    {
        return Task.FromResult(new SemanticTokensDocument(_registrationOptions.Legend));
    }
}