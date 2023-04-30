using Antlr4.Runtime;
using LanceServer.Core.Symbol;
using LanceServer.Core.Workspace;
using LspTypes;
using Range = LspTypes.Range;

namespace LanceServer.Parser;

public class SymbolUseListener : SinumerikNCBaseListener
{
    public List<SymbolUse> SymbolUseTable { get; } = new();
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
        AddTokenIfNotPlaceholder(context.NAME().Symbol);
    }

    public override void ExitGotoLabel(SinumerikNCParser.GotoLabelContext context)
    {
        AddTokenIfNotPlaceholder(context.NAME().Symbol);
    }

    public override void ExitGotoBlock(SinumerikNCParser.GotoBlockContext context)
    {
        var text = context.GetText();
        var line = (uint)context.Start.Line - 1;
        var characterStart = (uint)context.Start.Column;
        var characterEnd = characterStart + (uint)text.Length;
        AddSymbolUse(text, line, characterStart, characterEnd);
    }

    private void AddTokenIfNotPlaceholder(IToken token)
    {
        if (_document.Placeholders.ContainsPlaceholder(token.Text))
        {
            return;
        }
        
        var line = (uint)token.Line - 1;
        var characterStart = (uint)token.Column;
        var characterEnd = characterStart + (uint)token.Text.Length;
        AddSymbolUse(token.Text, line, characterStart, characterEnd);
    }

    private void AddSymbolUse(string text, uint line, uint characterStart, uint characterEnd)
    {
        IToken token;
        SymbolUseTable.Add(new SymbolUse(text, new Range { Start = new Position(line, characterStart), End = new Position(line, characterEnd) }, _document.Information.Uri));
    }
}