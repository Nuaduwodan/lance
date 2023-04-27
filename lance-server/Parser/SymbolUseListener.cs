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
        base.ExitUserVariableAssignment(context);
        AddTokenIfNotPlaceholder(context.NAME().Symbol);
    }

    public override void ExitArrayVariableAssignment(SinumerikNCParser.ArrayVariableAssignmentContext context)
    {
        base.ExitArrayVariableAssignment(context);
        AddTokenIfNotPlaceholder(context.NAME().Symbol);
    }

    public override void ExitVariableUse(SinumerikNCParser.VariableUseContext context)
    {
        base.ExitVariableUse(context);
        AddTokenIfNotPlaceholder(context.NAME().Symbol);
    }

    public override void ExitMacroUse(SinumerikNCParser.MacroUseContext context)
    {
        base.ExitMacroUse(context);
        foreach (var name in context.NAME())
        {
            AddTokenIfNotPlaceholder(name.Symbol);
        }
    }

    public override void ExitOwnProcedure(SinumerikNCParser.OwnProcedureContext context)
    {
        base.ExitOwnProcedure(context);
        AddTokenIfNotPlaceholder(context.NAME().Symbol);
    }

    public override void ExitProcedureDeclaration(SinumerikNCParser.ProcedureDeclarationContext context)
    {
        base.ExitProcedureDeclaration(context);
        AddTokenIfNotPlaceholder(context.NAME().Symbol);
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
        SymbolUseTable.Add(new SymbolUse(token.Text, new Range{Start = new Position(line, characterStart), End = new Position(line, characterEnd)}, _document.Information.Uri));
    }
}