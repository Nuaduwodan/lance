namespace LanceServer.RequestHandler.SemanticToken;

public static class SemanticTokenModifierHelper
{
    public enum SemanticTokenModifier
    {
        Declaration,
        Definition,
        Readonly
    }

    public static string[] GetModifiers()
    {
        return Enum.GetNames<SemanticTokenModifier>().Select(t => char.ToLower(t[0]) + t[1..]).ToArray();
    }
}