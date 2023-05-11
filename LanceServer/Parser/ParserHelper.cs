using Antlr4.Runtime;
using LspTypes;
using Range = LspTypes.Range;

namespace LanceServer.Parser;

public static class ParserHelper
{
    public static Range GetRangeBetweenTokens(IToken startToken, IToken endToken)
    {
        var lineStart = (uint)startToken.Line - 1;
        var characterStart = (uint)startToken.Column;
        var lineEnd = (uint)endToken.Line - 1;
        var characterEnd = (uint)endToken.Column + (uint)endToken.Text.Length;
        return new Range { Start = new Position(lineStart, characterStart), End = new Position(lineEnd, characterEnd) };
    }

    public static Range GetRangeForToken(IToken identifierToken)
    {
        var line = (uint)identifierToken.Line - 1;
        var characterStart = (uint)identifierToken.Column;
        var characterEnd = characterStart + (uint)identifierToken.Text.Length;
        return new Range { Start = new Position(line, characterStart), End = new Position(line, characterEnd)};
    }
}