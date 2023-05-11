using Antlr4.Runtime.Tree;
using LanceServer.Core.Document;
using LspTypes;
using Range = LspTypes.Range;

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
        code += codeAssignment.intUnsigned() != null ? codeAssignment.intUnsigned().GetText(): codeAssignment.codeAssignmentExpression().expression().GetText();
        var range = ParserHelper.GetRangeForToken(context.Start);
        LanguageTokens.Add(new LanguageToken(code, range));
    }

    public override void ExitMCode(SinumerikNCParser.MCodeContext context)
    {
        if (context.exception != null) return;
        
        var code = context.MCODE().GetText();
        var range = ParserHelper.GetRangeForToken(context.Start);
        var codeAssignment = context.codeAssignment();
        
        if (codeAssignment != null)
        {
            if (codeAssignment.intUnsigned() != null) // M3
            {
                code += codeAssignment.intUnsigned().GetText();
                range = ParserHelper.GetRangeBetweenTokens(context.Start, codeAssignment.intUnsigned().Stop);
            }
            else // M = 3
            {
                code += codeAssignment.codeAssignmentExpression().expression().GetText();
            }
        }
        else // M1 = 3
        {
            var codeAssignmentExpression = context.codeAssignmentParameterized().codeAssignmentExpression();
            code += codeAssignmentExpression.expression().GetText();
        }
        
        LanguageTokens.Add(new LanguageToken(code, range));
    }
}