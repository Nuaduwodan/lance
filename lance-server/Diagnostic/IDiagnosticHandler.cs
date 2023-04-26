using LanceServer.Core.Workspace;
using LanceServer.LanguageServerProtocol;
using LspTypes;

namespace LanceServer.Diagnostic;

/// <summary>
/// Handles diagnostic requests
/// </summary>
public interface IDiagnosticHandler
{
    /// <summary>
    /// Handle a diagnostic request
    /// </summary>
    /// <param name="document">The document with the necessary symbol information</param>
    /// <param name="requestParams">The diagnostic request parameters</param>
    /// <param name="workspace">The workspace</param>
    DocumentDiagnosticReport HandleRequest(SymbolUseExtractedDocument document, DocumentDiagnosticParams requestParams, IWorkspace workspace);
}