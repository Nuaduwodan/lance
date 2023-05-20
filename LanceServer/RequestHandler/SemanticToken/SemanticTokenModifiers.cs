namespace LanceServer.RequestHandler.SemanticToken;

/// <summary>
/// Defines the semantic token modifiers used by this language server.
/// To extend see the <a href="https://microsoft.github.io/language-server-protocol/specifications/lsp/3.17/specification/#textDocument_semanticTokens">LSP specification</a>
/// </summary>
public static class SemanticTokenModifiers
{
    public enum SemanticTokenModifier
    {
        Declaration,
        Definition,
        Readonly
    }

    /// <summary>
    /// Returns all used modifiers as a string array.
    /// </summary>
    public static string[] GetModifiers()
    {
        return Enum.GetNames<SemanticTokenModifier>().Select(t => char.ToLower(t[0]) + t[1..]).ToArray();
    }
}