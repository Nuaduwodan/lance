using LanceServer.Core.Document;
using LanceServer.Core.Workspace;
using LspTypes;

namespace LanceServer.RequestHandler.SemanticToken;

public interface ISemanticTokenHandler
{
    SemanticTokens ProcessRequest(LanguageTokenExtractedDocument document, IWorkspace workspace);
}