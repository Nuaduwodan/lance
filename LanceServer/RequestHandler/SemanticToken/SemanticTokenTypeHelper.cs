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
        return Enum.GetNames<SemanticTokenType>().Select(t => char.ToLower(t[0]) + t[1..]).ToArray();
    }
}