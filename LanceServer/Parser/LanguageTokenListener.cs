using Antlr4.Runtime.Tree;
using LanceServer.Core.Document;

namespace LanceServer.Parser;

/// <summary>
/// The listener to look for tokens that belong to the sinumerik one nc language.
/// </summary>
public class LanguageTokenListener : SinumerikNCBaseListener
{
    /// <summary>
    /// The list with all found language tokens.
    /// </summary>
    public IList<LanguageToken> LanguageTokens { get; } = new List<LanguageToken>();
    
    /// <summary>
    /// Is called for every terminal in the code.
    /// If it is a "relevant" terminal it gets added to the language token list.
    /// </summary>
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

    /// <summary>
    /// Is called for every g code occurrence in the code.
    /// If the number of the g code can be determined here, it gets added to the language token list.
    /// </summary>
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

    /// <summary>
    /// Is called for every m code occurrence in the code.
    /// If the number of the m code can be determined here, it gets added to the language token list.
    /// </summary>
    public override void ExitMCode(SinumerikNCParser.MCodeContext context)
    {
        if (context.exception != null) return;
        
        var code = context.MCODE().GetText();
        var range = ParserHelper.GetRangeForToken(context.Start);
        
        string number;
        var codeAssignment = context.codeAssignment();
        var codeAssignmentParameterized = context.codeAssignmentParameterized();
        if (codeAssignment != null)
        {
            if (codeAssignment.exception != null) return;
            
            if (codeAssignment.intUnsigned() != null) // M3
            {
                number = codeAssignment.intUnsigned().GetText();
                range = ParserHelper.GetRangeFromStartToEndToken(context.Start, codeAssignment.intUnsigned().Stop);
            }
            else // M = 3
            {
                number = codeAssignment.codeAssignmentExpression().expression().GetText();
            }
        }
        else // M1 = 3
        {
            if (codeAssignmentParameterized.exception != null) return;
            
            number = codeAssignmentParameterized.codeAssignmentExpression().expression().GetText();
        }

        if (!short.TryParse(number, out _)) return;
        
        code += number.TrimStart('0').PadLeft(1, '0');
        LanguageTokens.Add(new LanguageToken(code, range));
    }
}