using LanceServer.Core.Workspace;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Document;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;

namespace LanceServer.RequestHandler.DiagnosticHandler;

public class DocumentDiagnosticHandler : DocumentDiagnosticHandlerBase
{
    private DocumentDiagnosticLogic _documentDiagnosticLogic;
    private IWorkspace _workspace;

    public DocumentDiagnosticHandler(IWorkspace workspace)
    {
        _workspace = workspace;
        _documentDiagnosticLogic = new DocumentDiagnosticLogic();
    }

    protected override DiagnosticsRegistrationOptions CreateRegistrationOptions(DiagnosticClientCapabilities capability, ClientCapabilities clientCapabilities)
    {
        return new DiagnosticsRegistrationOptions
        {
            DocumentSelector = new TextDocumentSelector(),
            InterFileDependencies = true,
            WorkDoneProgress = true,
            WorkspaceDiagnostics = false
        };
    }

    public override Task<RelatedDocumentDiagnosticReport> Handle(DocumentDiagnosticParams request, CancellationToken cancellationToken)
    {
        var report = _documentDiagnosticLogic.HandleRequest(_workspace.GetSymbolUseExtractedDocument(request.TextDocument.Uri.ToUri()), _workspace);
        var fullReport = new RelatedFullDocumentDiagnosticReport { Items = report.ToArray() };
        return Task.FromResult<RelatedDocumentDiagnosticReport>(fullReport);
    }
}