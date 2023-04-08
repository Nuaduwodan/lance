using LanceServer.Core.Workspace;
using LspTypes;

namespace LanceServer.SemanticToken;

public interface ISemanticTokenHandler
{
    SemanticTokens ProcessRequest(ParsedDocument document, DocumentSymbolParams requestParams);

    /// <summary>
    /// Maps the token type as defined by the grammar to a type as defined by the LSP.
    /// <seealso cref="SinumerikNCLexer"/>
    /// </summary>
    /// <param name="tokenType">The token type as defined by the grammar.</param>
    int TransformType(int tokenType);
}