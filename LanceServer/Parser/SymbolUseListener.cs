using Antlr4.Runtime;
using LanceServer.Core.Document;
using LanceServer.Core.Symbol;

namespace LanceServer.Parser;

public class SymbolUseListener : SinumerikNCBaseListener
{
    public IList<AbstractSymbolUse> SymbolUseTable { get; } = new List<AbstractSymbolUse>();
    private readonly PlaceholderPreprocessedDocument _document;
    
    public SymbolUseListener(SymbolisedDocument document)
    {
        _document = document;
    }
    
    public override void ExitUserVariableAssignment(SinumerikNCParser.UserVariableAssignmentContext context)
    {
        AddTokenIfNotPlaceholder(context.NAME().Symbol);
    }

    public override void ExitArrayVariableAssignment(SinumerikNCParser.ArrayVariableAssignmentContext context)
    {
        AddTokenIfNotPlaceholder(context.NAME().Symbol);
    }

    public override void ExitVariableUse(SinumerikNCParser.VariableUseContext context)
    {
        AddTokenIfNotPlaceholder(context.NAME().Symbol);
    }

    public override void ExitMacroUse(SinumerikNCParser.MacroUseContext context)
    {
        AddTokenIfNotPlaceholder(context.NAME().Symbol);
    }

    public override void ExitOwnProcedure(SinumerikNCParser.OwnProcedureContext context)
    {
        var token = context.NAME().Symbol;
        if (_document.PlaceholderTable.ContainsPlaceholder(token.Text))
        {
            return;
        }

        var arguments = context.arguments() != null ? context.arguments().expression().Select(_ => new ProcedureUseArgument()).ToArray() : Array.Empty<ProcedureUseArgument>();

        SymbolUseTable.Add(new ProcedureUse(token.Text, ParserHelper.GetRangeForToken(token), _document.Information.Uri, arguments));
    }

    public override void ExitProcedureDeclaration(SinumerikNCParser.ProcedureDeclarationContext context)
    {
        var token = context.NAME().Symbol;
        if (_document.PlaceholderTable.ContainsPlaceholder(token.Text))
        {
            return;
        }

        var arguments = context.parameterDeclarations() != null ? context.parameterDeclarations().parameterDeclaration().Select(_ => new ProcedureUseArgument()).ToArray() : Array.Empty<ProcedureUseArgument>();
        
        SymbolUseTable.Add(new DeclarationProcedureUse(token.Text, ParserHelper.GetRangeForToken(token), _document.Information.Uri, arguments));
    }

    public override void ExitGotoLabel(SinumerikNCParser.GotoLabelContext context)
    {
        AddTokenIfNotPlaceholder(context.NAME().Symbol);
    }

    public override void ExitGotoBlock(SinumerikNCParser.GotoBlockContext context)
    {
        SymbolUseTable.Add(new SymbolUse(context.GetText(), ParserHelper.GetRangeBetweenTokens(context.Start, context.Stop), _document.Information.Uri));
    }

    private void AddTokenIfNotPlaceholder(IToken token)
    {
        if (_document.PlaceholderTable.ContainsPlaceholder(token.Text))
        {
            return;
        }
        
        SymbolUseTable.Add(new SymbolUse(token.Text, ParserHelper.GetRangeForToken(token), _document.Information.Uri));
    }
}