using Antlr4.Runtime;
using LanceServer.Core.Symbol;
using LanceServer.Core.Workspace;
using Position = LspTypes.Position;
using Range = LspTypes.Range;

namespace LanceServer.Parser;

public class SymbolListener : SinumerikNCBaseListener
{
    public IList<ISymbol> SymbolTable { get; } = new List<ISymbol>();
    private readonly PlaceholderPreprocessedDocument _document;

    private IList<ParameterSymbol> _parameters = new List<ParameterSymbol>();

    public SymbolListener(PlaceholderPreprocessedDocument document)
    {
        _document = document;
    }

    /// <inheritdoc/>
    public override void EnterProcedureDefinition(SinumerikNCParser.ProcedureDefinitionContext context)
    {
        _parameters = new List<ParameterSymbol>();
    }

    /// <inheritdoc/>
    public override void ExitParameterDefinitionByReference(SinumerikNCParser.ParameterDefinitionByReferenceContext context)
    {
        var identifier = context.NAME().GetText();
        var uri = _document.Information.Uri;
        var symbolRange = GetSymbolRange(context.Start, context.Stop);
        var identifierRange = GetIdentifierRange(context.NAME().Symbol);
        var success = GetCompositeDataType(context.type(), out var dataType);
        var arrayDeclaration = GetArrayDeclaration(context.arrayDeclaration());

        if (!success || identifier.Length < 2)
        {
            return;
        }
        
        var symbol = new ParameterSymbol(identifier, uri, symbolRange, identifierRange, dataType, arrayDeclaration,true);
        _parameters.Add(symbol);
        SymbolTable.Add(symbol);
    }
        
    /// <inheritdoc/>
    public override void ExitParameterDefinitionByValue(SinumerikNCParser.ParameterDefinitionByValueContext context)
    {
        var identifier = context.NAME().GetText();
        var uri = _document.Information.Uri;
        var symbolRange = GetSymbolRange(context.Start, context.Stop);
        var identifierRange = GetIdentifierRange(context.NAME().Symbol);
        var success = GetCompositeDataType(context.type(), out var dataType);
        
        if (!success || identifier.Length < 2)
        {
            return;
        }
        
        var symbol = new ParameterSymbol(identifier, uri, symbolRange, identifierRange, dataType, Array.Empty<string>(), false);
        _parameters.Add(symbol);
        SymbolTable.Add(symbol);
    }

    /// <inheritdoc/>
    public override void ExitProcedureDefinitionHeader(SinumerikNCParser.ProcedureDefinitionHeaderContext context)
    {
        var identifier = ReplacePlaceholder(context.NAME().GetText());
        var uri = _document.Information.Uri;
        var symbolRange = GetSymbolRange(context.Start, context.Stop);
        var identifierRange = GetIdentifierRange(context.NAME().Symbol);
        var symbol = new ProcedureSymbol(identifier, uri, symbolRange, identifierRange, _parameters.ToArray());
        SymbolTable.Add(symbol);
    }

    /// <inheritdoc/>
    public override void ExitMacroDeclaration(SinumerikNCParser.MacroDeclarationContext context)
    {
        var identifier = context.NAME().GetText();
        var uri = _document.Information.Uri;
        var symbolRange = GetSymbolRange(context.Start, context.Stop);
        var identifierRange = GetIdentifierRange(context.NAME().Symbol);
        var value = ReplacePlaceholder(context.macroValue().GetText());
        var isGlobal = _document.Information.DocumentType is DocumentType.Definition or DocumentType.MainProcedure;
        var symbol = new MacroSymbol(identifier, uri, symbolRange, identifierRange, value, isGlobal);
        SymbolTable.Add(symbol);
    }

    /// <inheritdoc/>
    public override void ExitVariableDeclaration(SinumerikNCParser.VariableDeclarationContext context)
    {
        var uri = _document.Information.Uri;
        var symbolRange = GetSymbolRange(context.Start, context.Stop);
        var success = GetCompositeDataType(context.type(), out var dataType);
        var isGlobal = _document.Information.DocumentType is DocumentType.Definition or DocumentType.MainProcedure;
        
        if (!success)
        {
            return;
        }
        
        foreach (var variable in context.variableNameDeclaration())
        {
            var identifier = variable.NAME().GetText();
            var identifierRange = GetIdentifierRange(variable.NAME().Symbol);
            var arrayDefinition = GetArrayDefinition(variable.arrayDefinition());

            if (identifier.Length < 2)
            {
                continue;
            }
            
            var symbol = new VariableSymbol(identifier, uri, symbolRange, identifierRange, dataType, arrayDefinition, isGlobal);
            SymbolTable.Add(symbol);
        }
    }

    public override void ExitLabelDefinition(SinumerikNCParser.LabelDefinitionContext context)
    {
        var identifier = context.NAME().GetText();
        var uri = _document.Information.Uri;
        var symbolRange = GetSymbolRange(context.Start, context.Stop);
        var identifierRange = GetIdentifierRange(context.NAME().Symbol);
        var symbol = new LabelSymbol(identifier, uri, symbolRange, identifierRange);
        SymbolTable.Add(symbol);
    }

    public override void ExitBlockNumberDefinition(SinumerikNCParser.BlockNumberDefinitionContext context)
    {
        var identifier = context.GetText();
        var uri = _document.Information.Uri;
        var symbolRange = GetSymbolRange(context.Start, context.Stop);
        var symbol = new BlockNumberSymbol(identifier, uri, symbolRange);
        SymbolTable.Add(symbol);
    }

    private bool GetCompositeDataType(SinumerikNCParser.TypeContext typeContext, out CompositeDataType compositeDataType)
    {
        var success = GetDataType(typeContext, out var type);
        
        string length = "";
        if (type == DataType.String)
        {
            length = typeContext.expression().GetText();
        }
        compositeDataType = new CompositeDataType(type, length);
        return success;
    }

    private bool GetDataType(SinumerikNCParser.TypeContext typeContext, out DataType dataType)
    {
        bool ignoreCase = true;
        if(typeContext.GetText().StartsWith(DataType.String.ToString(),StringComparison.OrdinalIgnoreCase))
        {
            dataType = DataType.String;
            return true;
        }

        return Enum.TryParse(typeContext.GetText(), ignoreCase, out dataType);
    }

    private string[] GetArrayDeclaration(SinumerikNCParser.ArrayDeclarationContext? arrayContext)
    {
        if (arrayContext == null)
        {
            return Array.Empty<string>();
        }

        var dimensions = new string[arrayContext.COMMA().Length + 1];
        if (arrayContext.first != null)
        {
            dimensions[0] = arrayContext.first.GetText();
        }
        if (arrayContext.second != null)
        {
            dimensions[1] = arrayContext.second.GetText();
        }
        if (arrayContext.third != null)
        {
            dimensions[2] = arrayContext.third.GetText();
        }

        return dimensions;
    }

    private string[] GetArrayDefinition(SinumerikNCParser.ArrayDefinitionContext? arrayContext)
    {
        if (arrayContext == null)
        {
            return Array.Empty<string>();
        }

        return arrayContext.expression().Select(expression => expression.GetText()).ToArray();
    }

    private string ReplacePlaceholder(string textWithPlaceholder)
    {
        return _document.Placeholders.ReplacePlaceholder(textWithPlaceholder);
    }

    private Range GetSymbolRange(IToken startToken, IToken endToken)
    {
        var lineStart = (uint)startToken.Line - 1;
        var characterStart = (uint)startToken.Column;
        var lineEnd = (uint)endToken.Line - 1;
        var characterEnd = (uint)endToken.Column + (uint)endToken.Text.Length;
        return new Range { Start = new Position(lineStart, characterStart), End = new Position(lineEnd, characterEnd) };
    }

    private Range GetIdentifierRange(IToken identifierToken)
    {
        var line = (uint)identifierToken.Line - 1;
        var characterStart = (uint)identifierToken.Column;
        var characterEnd = characterStart + (uint)identifierToken.Text.Length;
        return new Range { Start = new Position(line, characterStart), End = new Position(line, characterEnd)};
    }
}