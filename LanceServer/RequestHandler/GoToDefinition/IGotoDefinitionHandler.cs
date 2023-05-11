using LanceServer.Core.Document;
using LanceServer.Core.Workspace;
using LspTypes;

namespace LanceServer.RequestHandler.GoToDefinition;

/// <summary>
/// Handles goto definition requests
/// </summary>
public interface IGotoDefinitionHandler
{
    /// <summary>
    /// Handle a goto definition request
    /// </summary>
    /// <param name="document">The document with the necessary symbol information</param>
    /// <param name="position">The position where the goto is requested</param>
    /// <param name="workspace">The workspace</param>
    LocationLink[] HandleRequest(LanguageTokenExtractedDocument document, Position position, IWorkspace workspace);
}