namespace LanceServer.SemanticToken;

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
        return Enum.GetNames<SemanticTokenModifier>().Select(t =>Char.ToLowerInvariant(t[0]) + t.Substring(1)).ToArray();
    }
}