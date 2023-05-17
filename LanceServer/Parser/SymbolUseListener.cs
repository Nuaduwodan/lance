using Antlr4.Runtime;
using LanceServer.Core.Document;
using LanceServer.Core.Symbol;

namespace LanceServer.Parser;

public class SymbolUseListener : SinumerikNCBaseListener
{
    public List<ISymbolUse> SymbolUseTable { get; } = new();
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
        foreach (var name in context.NAME())
        {
            AddTokenIfNotPlaceholder(name.Symbol);
        }
    }

    public override void ExitOwnProcedure(SinumerikNCParser.OwnProcedureContext context)
    {
        AddTokenIfNotPlaceholder(context.NAME().Symbol);
    }

    public override void ExitProcedureDeclaration(SinumerikNCParser.ProcedureDeclarationContext context)
    {
        var token = context.NAME().Symbol;
        if (_document.Placeholders.ContainsPlaceholder(token.Text))
        {
            return;
        }

        SymbolUseTable.Add(new ProcedureDeclarationSymbolUse(token.Text, ParserHelper.GetRangeForToken(token), _document.Information.Uri));
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
        if (_document.Placeholders.ContainsPlaceholder(token.Text))
        {
            return;
        }
        
        SymbolUseTable.Add(new SymbolUse(token.Text, ParserHelper.GetRangeForToken(token), _document.Information.Uri));
    }
}