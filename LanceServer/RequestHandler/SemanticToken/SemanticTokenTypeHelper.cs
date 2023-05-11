namespace LanceServer.RequestHandler.SemanticToken;

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

    public static string[] GetTypes()
    {
        return Enum.GetNames<SemanticTokenType>().Select(t =>Char.ToLowerInvariant(t[0]) + t.Substring(1)).ToArray();
    }
}