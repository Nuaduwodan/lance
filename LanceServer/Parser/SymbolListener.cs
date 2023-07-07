using LanceServer.Core.Document;
using LanceServer.Core.Symbol;

namespace LanceServer.Parser;

/// <summary>
/// The listener looking for symbols in the code.
/// </summary>
public class SymbolListener : SinumerikNCBaseListener
{
    /// <summary>
    /// The list of all found symbols.
    /// </summary>
    public IList<AbstractSymbol> SymbolTable { get; } = new List<AbstractSymbol>();
    
    private readonly PlaceholderPreprocessedDocument _document;

    private IList<ParameterSymbol> _parameters = new List<ParameterSymbol>();

    public SymbolListener(PlaceholderPreprocessedDocument document)
    {
        _document = document;
    }

    /// <summary>
    /// Is called at the beginning of a procedure definition.
    /// Creates a new empty parameter list.
    /// </summary>
    public override void EnterProcedureDefinition(SinumerikNCParser.ProcedureDefinitionContext context)
    {
        _parameters = new List<ParameterSymbol>();
    }

    /// <summary>
    /// Is called at the end of a reference parameter definition.
    /// Creates a new <see cref="ParameterSymbol"/> and adds it to the symbol table and the parameter list.
    /// </summary>
    public override void ExitParameterDefinitionByReference(SinumerikNCParser.ParameterDefinitionByReferenceContext context)
    {
        var identifier = context.NAME()?.GetText() ?? string.Empty;
        var uri = _document.Information.Uri;
        var symbolRange = ParserHelper.GetRangeFromStartToEndToken(context.Start, context.Stop);
        var identifierRange = ParserHelper.GetRangeForToken(context.NAME().Symbol);
        var success = GetCompositeDataType(context.type(), out var dataType);
        var arrayDeclaration = GetArrayDeclaration(context.arrayDeclaration());

        if (!success || identifier.Length < 2)
        {
            return;
        }
        
        var symbol = new ParameterSymbol(identifier, uri, symbolRange, identifierRange, dataType, arrayDeclaration, true);
        _parameters.Add(symbol);
        SymbolTable.Add(symbol);
    }
        
    /// <summary>
    /// Is called at the end of a value parameter definition.
    /// Creates a new <see cref="ParameterSymbol"/> and adds it to the symbol table and the parameter list.
    /// </summary>
    public override void ExitParameterDefinitionByValue(SinumerikNCParser.ParameterDefinitionByValueContext context)
    {
        var identifier = context.NAME()?.GetText() ?? string.Empty;
        var uri = _document.Information.Uri;
        var symbolRange = ParserHelper.GetRangeFromStartToEndToken(context.Start, context.Stop);
        var identifierRange = ParserHelper.GetRangeForToken(context.NAME().Symbol);
        const bool IS_REFERENCE_VALUE = false;
        var isOptional = context.defaultValue != null;
        
        var success = GetCompositeDataType(context.type(), out var dataType);
        
        if (!success || identifier.Length < 2)
        {
            return;
        }
        
        var symbol = new ParameterSymbol(identifier, uri, symbolRange, identifierRange, dataType, Array.Empty<string>(), IS_REFERENCE_VALUE, isOptional);
        _parameters.Add(symbol);
        SymbolTable.Add(symbol);
    }

    /// <summary>
    /// Is called at the end of a procedure definition.
    /// Creates a new <see cref="ProcedureSymbol"/> and adds it to the symbol table.
    /// </summary>
    public override void ExitProcedureDefinitionHeader(SinumerikNCParser.ProcedureDefinitionHeaderContext context)
    {
        var identifier = ReplacePlaceholder(context.NAME()?.GetText() ?? string.Empty);
        var uri = _document.Information.Uri;
        var symbolRange = ParserHelper.GetRangeFromStartToEndToken(context.Start, context.Stop);
        var identifierRange = ParserHelper.GetRangeForToken(context.NAME().Symbol);
        
        if (identifier.Length < 2)
        {
            return;
        }

        var mayNeedExternDeclaration = _document.Information.DocumentType is not DocumentType.ManufacturerSubProcedure && _parameters.Any();
        var symbol = new ProcedureSymbol(identifier, uri, symbolRange, identifierRange, _parameters.ToArray(), mayNeedExternDeclaration);
        SymbolTable.Add(symbol);
    }

    /// <summary>
    /// Is called at the end of a macro definition.
    /// Creates a new <see cref="MacroSymbol"/> and adds it to the symbol table.
    /// </summary>
    public override void ExitMacroDeclaration(SinumerikNCParser.MacroDeclarationContext context)
    {
        var identifier = context.NAME()?.GetText() ?? string.Empty;
        var uri = _document.Information.Uri;
        var symbolRange = ParserHelper.GetRangeFromStartToEndToken(context.Start, context.Stop);
        var identifierRange = ParserHelper.GetRangeForToken(context.NAME().Symbol);
        var value = ReplacePlaceholder(context.macroValue().GetText());
        var isGlobal = _document.Information.DocumentType is DocumentType.Definition or DocumentType.MainProcedure;
        
        if (identifier.Length < 2)
        {
            return;
        }
        
        var symbol = new MacroSymbol(identifier, uri, symbolRange, identifierRange, value, isGlobal);
        SymbolTable.Add(symbol);
    }

    /// <summary>
    /// Is called at the end of a variable declaration.
    /// Creates a new <see cref="VariableSymbol"/> for every variable and adds them to the symbol table.
    /// </summary>
    public override void ExitVariableDeclaration(SinumerikNCParser.VariableDeclarationContext context)
    {
        var uri = _document.Information.Uri;
        var symbolRange = ParserHelper.GetRangeFromStartToEndToken(context.Start, context.Stop);
        var success = GetCompositeDataType(context.type(), out var dataType);
        var isGlobal = _document.Information.DocumentType is DocumentType.Definition or DocumentType.MainProcedure;
        
        if (!success)
        {
            return;
        }
        
        foreach (var variable in context.variableNameDeclaration() ?? Array.Empty<SinumerikNCParser.VariableNameDeclarationContext>())
        {
            if (variable.exception != null) return;
            
            var identifier = variable.NAME().GetText();
            var identifierRange = ParserHelper.GetRangeForToken(variable.NAME().Symbol);
            var arrayDefinition = GetArrayDefinition(variable.arrayDefinition());

            if (identifier.Length < 2)
            {
                continue;
            }
            
            var symbol = new VariableSymbol(identifier, uri, symbolRange, identifierRange, dataType, arrayDefinition, isGlobal);
            SymbolTable.Add(symbol);
        }
    }

    /// <summary>
    /// Is called at the end of a label definition.
    /// Creates a new <see cref="LabelSymbol"/> and adds it to the symbol table.
    /// </summary>
    public override void ExitLabelDefinition(SinumerikNCParser.LabelDefinitionContext context)
    {
        var identifier = context.NAME().GetText();
        var uri = _document.Information.Uri;
        var symbolRange = ParserHelper.GetRangeFromStartToEndToken(context.Start, context.Stop);
        var identifierRange = ParserHelper.GetRangeForToken(context.NAME().Symbol);
        var symbol = new LabelSymbol(identifier, uri, symbolRange, identifierRange);
        SymbolTable.Add(symbol);
    }

    /// <summary>
    /// Is called at the end of a label definition.
    /// Creates a new <see cref="BlockNumberSymbol"/> and adds it to the symbol table.
    /// </summary>
    public override void ExitBlockNumberDefinition(SinumerikNCParser.BlockNumberDefinitionContext context)
    {
        var identifier = context.GetText();
        var uri = _document.Information.Uri;
        var symbolRange = ParserHelper.GetRangeFromStartToEndToken(context.Start, context.Stop);
        
        if (identifier.Length < 2)
        {
            return;
        }
        
        var symbol = new BlockNumberSymbol(identifier, uri, symbolRange);
        SymbolTable.Add(symbol);
    }

    private bool GetCompositeDataType(SinumerikNCParser.TypeContext typeContext, out CompositeDataType compositeDataType)
    {
        var success = GetDataType(typeContext, out var type);
        
        var length = "";
        if (type == DataType.String)
        {
            length = typeContext.expression().GetText();
        }
        
        compositeDataType = new CompositeDataType(type, length);
        return success;
    }

    private bool GetDataType(SinumerikNCParser.TypeContext typeContext, out DataType dataType)
    {
        const bool IGNORE_CASE = true;
        if (typeContext.GetText().StartsWith(DataType.String.ToString(), StringComparison.OrdinalIgnoreCase))
        {
            dataType = DataType.String;
            return true;
        }

        return Enum.TryParse(typeContext.GetText(), IGNORE_CASE, out dataType);
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
        return _document.PlaceholderTable.ReplacePlaceholder(textWithPlaceholder);
    }
}