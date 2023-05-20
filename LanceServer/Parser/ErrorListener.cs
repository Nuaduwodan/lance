using Antlr4.Runtime;
using LanceServer.RequestHandler.Diagnostic;
using LspTypes;
using Range = LspTypes.Range;

namespace LanceServer.Parser;

/// <summary>
/// Error listener for the sinumerik nc parser.
/// </summary>
public class ErrorListener : BaseErrorListener, IAntlrErrorListener<int>
{
    /// <summary>
    /// The diagnostics generated while parsing.
    /// </summary>
    public IList<Diagnostic> Diagnostics = new List<Diagnostic>();
    
    /// <inheritdoc />
    public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
    {
        Diagnostics.Add(DiagnosticMessage.ParsingError(ParserHelper.GetRangeForToken(offendingSymbol), msg));
    }

    /// <inheritdoc />
    public void SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
    {
        var token = msg.Split('\'')[1];
        var characterEnd = charPositionInLine + token.Length;
        var range = new Range { Start = new Position((uint)line, (uint)charPositionInLine), End = new Position((uint)line, (uint)characterEnd) };
        Diagnostics.Add(DiagnosticMessage.LexingError(range, token));
    }
}