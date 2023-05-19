namespace LanceServer.RequestHandler.SemanticToken;

public record SemanticToken(uint Line, uint StartCharacter, uint Length, uint Type, uint Modifiers)
{
    public readonly uint Line = Line;
    public readonly uint StartCharacter = StartCharacter;
    public readonly uint Length = Length;
    public readonly uint Type = Type;
    public readonly uint Modifiers = Modifiers;
}