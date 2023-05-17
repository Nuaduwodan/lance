using LanceServer.Core.Document;
using LanceServer.Core.Workspace;
using LspTypes;

namespace LanceServer.RequestHandler.Hover;

/// <summary>
/// Handles hover requests and returns the respective data to be displayed
/// </summary>
public interface IHoverHandler
{
    /// <summary>
    /// Handles the hover request.
    /// </summary>
    /// <param name="document">The document with the necessary symbol information</param>
    /// <param name="position">The hover position</param>
    /// <param name="workspace">The workspace</param>
    /// <returns>The hover response <see cref="LspTypes.Hover"/></returns>
    public LspTypes.Hover HandleRequest(LanguageTokenExtractedDocument document, Position position, IWorkspace workspace);
}