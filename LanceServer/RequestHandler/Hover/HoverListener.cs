using Antlr4.Runtime;
using LspTypes;

namespace LanceServer.RequestHandler.Hover;

public class HoverListener : SinumerikNCBaseListener
{
    public IToken? Token;

    private readonly Position _position;

    public HoverListener(Position position)
    {
        _position = position;
    }

    public override void ExitVariableNameDeclaration(SinumerikNCParser.VariableNameDeclarationContext context)
    {
        if (Token != null)
        {
            return;
        }
        
        SetTokenIfPositionMatches(context.NAME().Symbol);
    }
    
    public override void ExitUserVariableAssignment(SinumerikNCParser.UserVariableAssignmentContext context)
    {
        if (Token != null)
        {
            return;
        }
        
        SetTokenIfPositionMatches(context.NAME().Symbol);
    }

    public override void ExitArrayVariableAssignment(SinumerikNCParser.ArrayVariableAssignmentContext context)
    {
        if (Token != null)
        {
            return;
        }
        
        SetTokenIfPositionMatches(context.NAME().Symbol);
    }

    public override void ExitVariableUse(SinumerikNCParser.VariableUseContext context)
    {
        if (Token != null)
        {
            return;
        }
        
        SetTokenIfPositionMatches(context.NAME().Symbol);
    }

    public override void ExitMacroDeclaration(SinumerikNCParser.MacroDeclarationContext context)
    {
        if (Token != null)
        {
            return;
        }
        
        SetTokenIfPositionMatches(context.NAME().Symbol);
    }

    public override void ExitMacroUse(SinumerikNCParser.MacroUseContext context)
    {
        if (Token != null)
        {
            return;
        }

        SetTokenIfPositionMatches(context.NAME().Symbol);
    }

    public override void ExitOwnProcedure(SinumerikNCParser.OwnProcedureContext context)
    {
        if (Token != null)
        {
            return;
        }
        
        SetTokenIfPositionMatches(context.NAME().Symbol);
    }

    private void SetTokenIfPositionMatches(IToken token)
    {
        if (token.Line == _position.Line && token.Column <= _position.Character && token.Column + token.Text.Length >= _position.Character)
        {
            Token = token;
        }
    }
}