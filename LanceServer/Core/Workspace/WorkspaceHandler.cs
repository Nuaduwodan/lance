using MediatR;
using OmniSharp.Extensions.LanguageServer.Protocol;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Document;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using OmniSharp.Extensions.LanguageServer.Protocol.Server.Capabilities;

namespace LanceServer.Core.Workspace;

public class WorkspaceHandler : IOnLanguageServerStarted, IDidOpenTextDocumentHandler, IDidChangeTextDocumentHandler
{
    IWorkspace _workspace;
    
    public WorkspaceHandler(IWorkspace workspace)
    {
        _workspace = workspace;
    }
    
    public Task<Unit> Handle(DidOpenTextDocumentParams request, CancellationToken cancellationToken)
    {
        _workspace.GetSymbolUseExtractedDocument(request.TextDocument.Uri.ToUri());
        return Unit.Task;
    }

    TextDocumentOpenRegistrationOptions IRegistration<TextDocumentOpenRegistrationOptions, TextSynchronizationCapability>.GetRegistrationOptions(TextSynchronizationCapability capability, ClientCapabilities clientCapabilities)
    {
        return new TextDocumentOpenRegistrationOptions();
    }

    public Task<Unit> Handle(DidChangeTextDocumentParams request, CancellationToken cancellationToken)
    {
        _workspace.UpdateDocumentContent(request.TextDocument.Uri.ToUri(), request.ContentChanges.Last().Text);
        return Unit.Task;
    }

    TextDocumentChangeRegistrationOptions IRegistration<TextDocumentChangeRegistrationOptions, TextSynchronizationCapability>.GetRegistrationOptions(TextSynchronizationCapability capability, ClientCapabilities clientCapabilities)
    {
        return new TextDocumentChangeRegistrationOptions { SyncKind = TextDocumentSyncKind.Full };
    }

    public async Task OnStarted(ILanguageServer server, CancellationToken cancellationToken)
    {
        var observer = await server.WorkDoneManager.Create(new WorkDoneProgressBegin { Title = "Parsing files" }).ConfigureAwait(false);
        var progress = new Progress<WorkDoneProgressReport>();
        progress.ProgressChanged += (_, report) =>
        {
            observer.OnNext(report.Message!, report.Percentage, report.Cancellable);
        };
                        
        await _workspace.InitWorkspaceAsync(progress, server.WorkspaceFolderManager.CurrentWorkspaceFolders).ConfigureAwait(false);
        
        server.SendNotification(new DiagnosticRefreshParams());
    }
}