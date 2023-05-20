namespace LanceServer.RequestHandler.SemanticToken;

/// <summary>
/// Defines the semantic token types used by this language server.
/// To extend see the <a href="https://microsoft.github.io/language-server-protocol/specifications/lsp/3.17/specification/#textDocument_semanticTokens">LSP specification</a>
/// </summary>
public static class SemanticTokenTypeHelper
{
    public enum SemanticTokenType
    {
        Type,
        Parameter,
        Variable,
        Property,
        Function,
        Macro,
        Keyword,
        Modifier,
        Comment,
        String,
        Number,
        Operator,
        Decorator
    }

    /// <summary>
    /// Returns all used types as a string array.
    /// </summary>
    public static string[] GetTypes()
    {
        return Enum.GetNames<SemanticTokenType>().Select(t => char.ToLower(t[0]) + t[1..]).ToArray();
    }
}