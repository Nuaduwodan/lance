using Antlr4.Runtime;
using LanceServer.RequestHandler.Diagnostic;
using LspTypes;
using Range = LspTypes.Range;

namespace LanceServer.Parser;

public class ErrorListener : BaseErrorListener, IAntlrErrorListener<int>
{
    public IList<Diagnostic> Diagnostics = new List<Diagnostic>();
    
    public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
    {
        Diagnostics.Add(DiagnosticMessage.ParsingError(ParserHelper.GetRangeForToken(offendingSymbol), msg));
    }

    public void SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
    {
        var characterEnd = charPositionInLine + (uint)e.Message.Length;
        var range = new Range { Start = new Position((uint)line, (uint)charPositionInLine), End = new Position((uint)line, (uint)characterEnd) };
        Diagnostics.Add(DiagnosticMessage.LexingError(range, e.Message));
    }
}