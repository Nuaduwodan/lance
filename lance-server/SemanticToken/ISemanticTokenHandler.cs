using LanceServer.Core.Workspace;
using LspTypes;

namespace LanceServer.SemanticToken;

public interface ISemanticTokenHandler
{
    SemanticTokens ProcessRequest(SymbolUseExtractedDocument document, DocumentSymbolParams requestParams, IWorkspace workspace);
}