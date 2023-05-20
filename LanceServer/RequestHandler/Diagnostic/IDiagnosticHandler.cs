using LanceServer.Core.Document;
using LanceServer.Core.Workspace;
using LanceServer.Protocol;

namespace LanceServer.RequestHandler.Diagnostic;

/// <summary>
/// Handles diagnostic requests
/// </summary>
public interface IDiagnosticHandler
{
    /// <summary>
    /// Handle a diagnostic request
    /// </summary>
    /// <param name="document">The document with the necessary symbol information</param>
    /// <param name="workspace">The workspace</param>
    /// <returns>The diagnostic report to be sent back.</returns>
    DocumentDiagnosticReport HandleRequest(LanguageTokenExtractedDocument document, IWorkspace workspace);
}