using LanceServer.Core.Workspace;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Document;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;

namespace LanceServer.RequestHandler.GoToDefinition;

public class DefinitionHandler : DefinitionHandlerBase
{
    private GotoDefinitionLogic _gotoDefinitionLogic;
    private readonly IWorkspace _workspace;

    public DefinitionHandler(IWorkspace workspace)
    {
        _workspace = workspace;
        _gotoDefinitionLogic = new GotoDefinitionLogic();
    }
    
    protected override DefinitionRegistrationOptions CreateRegistrationOptions(DefinitionCapability capability, ClientCapabilities clientCapabilities)
    {
        return new DefinitionRegistrationOptions();
    }

    public override Task<LocationOrLocationLinks?> Handle(DefinitionParams request, CancellationToken cancellationToken)
    {
        var locationLinks = _gotoDefinitionLogic.HandleRequest(_workspace.GetSymbolUseExtractedDocument(request.TextDocument.Uri.ToUri()), request.Position, _workspace);
        
        var result = new LocationOrLocationLinks(locationLinks.Select(link => new LocationOrLocationLink(link)));
        return Task.FromResult(result)!;
    }
}