using Antlr4.Runtime;
using LspTypes;
using Range = LspTypes.Range;

namespace LanceServer.Parser;

public class ErrorListener : BaseErrorListener, IAntlrErrorListener<int>
{
    public IList<Diagnostic> Diagnostics = new List<Diagnostic>();
    
    public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
    {
        // Console.Error.WriteLine($"SyntaxError line: {line}, char: {charPositionInLine}, msg: {msg}, symbol: {offendingSymbol}, exception: {e}");
        Diagnostics.Add(new Diagnostic
        {
            Range = ParserHelper.GetRangeForToken(offendingSymbol),
            Severity = DiagnosticSeverity.Error,
            Source = "Lance",
            Message = msg
        });
    }

    public void SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
    {
        // Console.Error.WriteLine($"SyntaxError line: {line}, char: {charPositionInLine}, msg: {msg}, symbol: {offendingSymbol}, exception: {e}");
        var characterEnd = charPositionInLine + (uint)e.Message.Length;
        var range = new Range { Start = new Position((uint)line, (uint)charPositionInLine), End = new Position((uint)line, (uint)characterEnd)};
        Diagnostics.Add(new Diagnostic
        {
            Range = range,
            Severity = DiagnosticSeverity.Error,
            Source = "Lance",
            Message = $"\"{e.Message}\" is not recognized as an element of the sinumerik one nc language"
        });
    }
    
    
}