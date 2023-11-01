using Antlr4.Runtime;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using Range = OmniSharp.Extensions.LanguageServer.Protocol.Models.Range;

namespace LanceServer.Parser;

/// <summary>
/// Provides some helper functions to convert parser specific objects.
/// </summary>
public static class ParserHelper
{
    /// <summary>
    /// Returns a new <see cref="Range"/> starting at the start of the <paramref name="startToken"/> and ending at the end of the <paramref name="endToken"/>.
    /// </summary>
    public static Range GetRangeFromStartToEndToken(IToken startToken, IToken endToken)
    {
        var lineStart = startToken.Line - 1;
        var characterStart = startToken.Column;
        var lineEnd = endToken.Line - 1;
        var characterEnd = endToken.Column + endToken.Text.Length;
        return new Range { Start = new Position(lineStart, characterStart), End = new Position(lineEnd, characterEnd) };
    }

    /// <summary>
    /// Returns a new <see cref="Range"/> starting at the start of the <paramref name="identifierToken"/> and ending at the end of the same.
    /// </summary>
    public static Range GetRangeForToken(IToken identifierToken)
    {
        return GetRangeFromStartToEndToken(identifierToken, identifierToken);
    }
}