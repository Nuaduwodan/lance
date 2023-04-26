namespace LanceServer.SemanticToken;

/// <summary>
/// Represents a semantic token with the information necessary for the editor.
/// </summary>
/// <param name="DeltaLine">The delta in line numbers between the last token or the start of the file.</param>
/// <param name="DeltaChar">The delta in char position between the last token or the start of the line.</param>
/// <param name="Length">The length of the token.</param>
/// <param name="TokenType">The type of the token as defined by the LSP.</param>
/// <param name="TokenModifiers">The token modifiers as defined by the LSP.</param>
public readonly record struct SemanticTokenDataElement(uint DeltaLine, uint DeltaChar, uint Length, uint TokenType, uint TokenModifiers);