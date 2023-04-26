using LanceServer.Core.Workspace;
using LspTypes;

namespace LanceServer.GoToDefinition;

/// <summary>
/// Handles goto definition requests
/// </summary>
public interface IGotoDefinitionHandler
{
    /// <summary>
    /// Handle a goto definition request
    /// </summary>
    /// <param name="document">The document with the necessary symbol information</param>
    /// <param name="requestParams">The goto definition request parameters</param>
    /// <param name="workspace">The workspace</param>
    LocationLink[] HandleRequest(SymbolUseExtractedDocument document, TypeDefinitionParams requestParams, IWorkspace workspace);
}