using Antlr4.Runtime;

namespace LanceServer.Parser;

public class ErrorListener : BaseErrorListener, IAntlrErrorListener<int>
{
    public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
    {
        Console.Error.WriteLine($"SyntaxError line: {line}, char: {charPositionInLine}, msg: {msg}, symbol: {offendingSymbol}, exception: {e}");
    }

    public void SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
    {
        Console.Error.WriteLine($"SyntaxError line: {line}, char: {charPositionInLine}, msg: {msg}, symbol: {offendingSymbol}, exception: {e}");
    }
    
    
}