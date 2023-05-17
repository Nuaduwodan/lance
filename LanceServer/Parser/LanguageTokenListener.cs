using Antlr4.Runtime.Tree;
using LanceServer.Core.Document;

namespace LanceServer.Parser;

public class LanguageTokenListener : SinumerikNCBaseListener
{
    public List<LanguageToken> LanguageTokens { get; } = new();

    public override void VisitTerminal(ITerminalNode node)
    {
        var type = node.Symbol.Type;
        if (type is >= SinumerikNCLexer.WHILE and <= SinumerikNCLexer.CALL_MODAL_OFF 
            or >= SinumerikNCLexer.DIV and <= SinumerikNCLexer.MOD
            or >= SinumerikNCLexer.NOT and <= SinumerikNCLexer.WRTPR
            or >= SinumerikNCLexer.ADIS and <= SinumerikNCLexer.WALIMON
            or >= SinumerikNCLexer.BLOCK_NUMBER and <= SinumerikNCLexer.TU)
        {
            var code = node.GetText();
            var range = ParserHelper.GetRangeForToken(node.Symbol);
            LanguageTokens.Add(new LanguageToken(code, range));
        }
    }

    public override void ExitGCode(SinumerikNCParser.GCodeContext context)
    {
        if (context.exception != null) return;
        
        var code = context.GCODE().GetText();
        var codeAssignment = context.codeAssignment();
        var number = codeAssignment.intUnsigned() != null ? codeAssignment.intUnsigned().GetText() : codeAssignment.codeAssignmentExpression().expression().GetText();
        code += number.TrimStart('0').PadLeft(1, '0');
        var range = ParserHelper.GetRangeForToken(context.Start);
        LanguageTokens.Add(new LanguageToken(code, range));
    }

    public override void ExitMCode(SinumerikNCParser.MCodeContext context)
    {
        if (context.exception != null) return;
        
        var code = context.MCODE().GetText();
        var range = ParserHelper.GetRangeForToken(context.Start);
        var codeAssignment = context.codeAssignment();
        string number;
        
        if (codeAssignment != null)
        {
            if (codeAssignment.intUnsigned() != null) // M3
            {
                number = codeAssignment.intUnsigned().GetText();
                range = ParserHelper.GetRangeBetweenTokens(context.Start, codeAssignment.intUnsigned().Stop);
            }
            else // M = 3
            {
                number = codeAssignment.codeAssignmentExpression().expression().GetText();
            }
        }
        else // M1 = 3
        {
            var codeAssignmentExpression = context.codeAssignmentParameterized().codeAssignmentExpression();
            number = codeAssignmentExpression.expression().GetText();
        }
        
        code += number.TrimStart('0').PadLeft(1, '0');
        LanguageTokens.Add(new LanguageToken(code, range));
    }
}