using LanceServer.Core.Configuration;
using LanceServer.Core.Workspace;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Document;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;

namespace LanceServer.RequestHandler.HoverHandler;

public class HoverHandler : HoverHandlerBase
{
    private HoverLogic _hoverLogic;
    private IWorkspace _workspace;

    public HoverHandler(IWorkspace workspace, IConfigurationManager configurationManager)
    {
        _hoverLogic = new HoverLogic(configurationManager);
        _workspace = workspace;
    }

    protected override HoverRegistrationOptions CreateRegistrationOptions(HoverCapability capability, ClientCapabilities clientCapabilities)
    {
        return new HoverRegistrationOptions();
    }

    public override Task<Hover?> Handle(HoverParams request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_hoverLogic.HandleRequest(_workspace.GetSymbolUseExtractedDocument(request.TextDocument.Uri.ToUri()), request.Position, _workspace))!;
    }
}