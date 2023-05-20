using LanceServer.Core.Document;
using LanceServer.Core.Workspace;
using LspTypes;

namespace LanceServer.RequestHandler.SemanticToken;

/// <summary>
/// Class responsible for handling semantic token requests
/// </summary>
public interface ISemanticTokenHandler
{
    /// <summary>
    /// Handles the semantic token request.
    /// </summary>
    SemanticTokens ProcessRequest(LanguageTokenExtractedDocument document, IWorkspace workspace);
}