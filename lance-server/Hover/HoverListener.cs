using Antlr4.Runtime;
using LspTypes;

namespace LanceServer.Hover
{
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
            base.ExitVariableNameDeclaration(context);
            if (Token != null)
            {
                return;
            }
        
            SetTokenIfPositionMatches(context.NAME().Symbol);
        }
    
        public override void ExitUserVariableAssignment(SinumerikNCParser.UserVariableAssignmentContext context)
        {
            base.ExitUserVariableAssignment(context);
            if (Token != null)
            {
                return;
            }
        
            SetTokenIfPositionMatches(context.NAME().Symbol);
        }

        public override void ExitArrayVariableAssignment(SinumerikNCParser.ArrayVariableAssignmentContext context)
        {
            base.ExitArrayVariableAssignment(context);
            if (Token != null)
            {
                return;
            }
        
            SetTokenIfPositionMatches(context.NAME().Symbol);
        }

        public override void ExitVariableUse(SinumerikNCParser.VariableUseContext context)
        {
            base.ExitVariableUse(context);
            if (Token != null)
            {
                return;
            }
        
            SetTokenIfPositionMatches(context.NAME().Symbol);
        }

        public override void ExitMacroDeclaration(SinumerikNCParser.MacroDeclarationContext context)
        {
            base.ExitMacroDeclaration(context);
            if (Token != null)
            {
                return;
            }
        
            SetTokenIfPositionMatches(context.NAME().Symbol);
        
        }

        public override void ExitMacroUse(SinumerikNCParser.MacroUseContext context)
        {
            base.ExitMacroUse(context);
            if (Token != null)
            {
                return;
            }

            foreach (var name in context.NAME())
            {
                SetTokenIfPositionMatches(name.Symbol);
            }
        }

        public override void ExitOwnProcedure(SinumerikNCParser.OwnProcedureContext context)
        {
            base.ExitOwnProcedure(context);
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
}