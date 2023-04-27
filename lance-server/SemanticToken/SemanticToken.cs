namespace LanceServer.SemanticToken;

public class SemanticToken
{
    public uint Line;
    public uint StartCharacter;
    public uint Length;
    public uint Type;
    public uint Modifiers;

    public SemanticToken(uint line, uint startCharacter, uint length, uint type, uint modifiers)
    {
        Line = line;
        StartCharacter = startCharacter;
        Length = length;
        Type = type;
        Modifiers = modifiers;
    }
}