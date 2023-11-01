using OmniSharp.Extensions.LanguageServer.Protocol.Models;

namespace LanceServer.RequestHandler.SemanticToken;

/// <summary>
/// The token with the necessary information for the <see cref="SemanticTokens"/> response.
/// </summary>
/// <param name="Line">The line of the token.</param>
/// <param name="StartCharacter">The start character of the token.</param>
/// <param name="Length">The length of the token.</param>
/// <param name="Type">The type of the token.</param>
/// <param name="Modifiers">The modifiers of the token.</param>
public readonly record struct SemanticToken(int Line, int StartCharacter, int Length, int Type, int Modifiers);
