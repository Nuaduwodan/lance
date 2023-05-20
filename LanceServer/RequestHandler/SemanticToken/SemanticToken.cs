using LspTypes;

namespace LanceServer.RequestHandler.SemanticToken;

/// <summary>
/// The token with the necessary information for the <see cref="SemanticTokens"/> response.
/// </summary>
/// <param name="Line">The line of the token.</param>
/// <param name="StartCharacter">The start character of the token.</param>
/// <param name="Length">The length of the token.</param>
/// <param name="Type">The type of the token.</param>
/// <param name="Modifiers">The modifiers of the token.</param>
public record SemanticToken(uint Line, uint StartCharacter, uint Length, uint Type, uint Modifiers)
{
    public readonly uint Line = Line;
    public readonly uint StartCharacter = StartCharacter;
    public readonly uint Length = Length;
    public readonly uint Type = Type;
    public readonly uint Modifiers = Modifiers;
}