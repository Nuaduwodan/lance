﻿using Antlr4.Runtime;
using LanceServer.Core.Symbol;
using LanceServer.Core.Workspace;
using Position = LspTypes.Position;
using Range = LspTypes.Range;

namespace LanceServer.Parser;

public class SymbolListener : SinumerikNCBaseListener
{
    public List<ISymbol> SymbolTable { get; } = new();
    private readonly PlaceholderPreprocessedDocument _document;

    private List<ParameterSymbol> _parameters = new();

    public SymbolListener(PlaceholderPreprocessedDocument document)
    {
        _document = document;
    }

    /// <inheritdoc/>
    public override void EnterProcedureDefinition(SinumerikNCParser.ProcedureDefinitionContext context)
    {
        base.EnterProcedureDefinition(context);
        _parameters = new List<ParameterSymbol>();
    }

    /// <inheritdoc/>
    public override void ExitParameterDefinitionByReference(SinumerikNCParser.ParameterDefinitionByReferenceContext context)
    {
        base.ExitParameterDefinitionByReference(context);
        var identifier = context.NAME().GetText();
        var uri = _document.Information.Uri;
        var symbolRange = GetSymbolRange(context.Start, context.Stop);
        var identifierRange = GetIdentifierRange(context.NAME().Symbol);
        var dataType = GetCompositeDataType(context.type());
        var arrayDeclaration = GetArrayDeclaration(context.arrayDeclaration());
        var symbol = new ParameterSymbol(identifier, uri, symbolRange, identifierRange, dataType, arrayDeclaration,true);
        _parameters.Add(symbol);
        SymbolTable.Add(symbol);
    }
        
    /// <inheritdoc/>
    public override void ExitParameterDefinitionByValue(SinumerikNCParser.ParameterDefinitionByValueContext context)
    {
        base.ExitParameterDefinitionByValue(context);
        var identifier = context.NAME().GetText();
        var uri = _document.Information.Uri;
        var symbolRange = GetSymbolRange(context.Start, context.Stop);
        var identifierRange = GetIdentifierRange(context.NAME().Symbol);
        var dataType = GetCompositeDataType(context.type());
        var symbol = new ParameterSymbol(identifier, uri, symbolRange, identifierRange, dataType, Array.Empty<string>(), false);
        _parameters.Add(symbol);
        SymbolTable.Add(symbol);
    }

    /// <inheritdoc/>
    public override void ExitProcedureDefinitionHeader(SinumerikNCParser.ProcedureDefinitionHeaderContext context)
    {
        base.ExitProcedureDefinitionHeader(context);
        var identifier = context.NAME().GetText();
        var uri = _document.Information.Uri;
        var symbolRange = GetSymbolRange(context.Start, context.Stop);
        var identifierRange = GetIdentifierRange(context.NAME().Symbol);
        var symbol = new ProcedureSymbol(identifier, uri, symbolRange, identifierRange, _parameters.ToArray());
        SymbolTable.Add(symbol);
    }

    /// <inheritdoc/>
    public override void ExitMacroDeclaration(SinumerikNCParser.MacroDeclarationContext context)
    {
        base.ExitMacroDeclaration(context);
        var identifier = context.NAME().GetText();
        var uri = _document.Information.Uri;
        var symbolRange = GetSymbolRange(context.Start, context.Stop);
        var identifierRange = GetIdentifierRange(context.NAME().Symbol);
        var value = ReplacePlaceholder(context.macroValue().GetText());
        var isGlobal = _document.Information.IsGlobalFile;
        var symbol = new MacroSymbol(identifier, uri, symbolRange, identifierRange, value, isGlobal);
        SymbolTable.Add(symbol);
    }

    /// <inheritdoc/>
    public override void ExitVariableDeclaration(SinumerikNCParser.VariableDeclarationContext context)
    {
        base.ExitVariableDeclaration(context);
        var uri = _document.Information.Uri;
        var symbolRange = GetSymbolRange(context.Start, context.Stop);
        var type = GetCompositeDataType(context.type());
        var isGlobal = _document.Information.IsGlobalFile;
        foreach (var variable in context.variableNameDeclaration())
        {
            var identifier = variable.NAME().GetText();
            var identifierRange = GetIdentifierRange(variable.NAME().Symbol);
            var arrayDefinition = GetArrayDefinition(variable.arrayDefinition());
            var symbol = new VariableSymbol(identifier, uri, symbolRange, identifierRange, type, arrayDefinition, isGlobal);
            SymbolTable.Add(symbol);
        }
    }

    private CompositeDataType GetCompositeDataType(SinumerikNCParser.TypeContext typeContext)
    {
        var type = GetDataType(typeContext);
        string length = "";
        if (type == DataType.String)
        {
            length = typeContext.expression().GetText();
        }
        return new CompositeDataType(type, length);
    }

    private DataType GetDataType(SinumerikNCParser.TypeContext typeContext)
    {
        bool ignoreCase = true;
        if(typeContext.GetText().StartsWith(DataType.String.ToString(),StringComparison.InvariantCultureIgnoreCase))
        {
            return DataType.String;
        }

        return Enum.Parse<DataType>(typeContext.GetText(), ignoreCase);
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
        return new Range() { Start = new Position((uint)startToken.Line, (uint)startToken.Column), End = new Position((uint)endToken.Line, (uint)endToken.Column) };
    }

    private Range GetIdentifierRange(IToken identifierToken)
    {
        var line = (uint)identifierToken.Line;
        var start = (uint)identifierToken.Column;
        var end = start + (uint)identifierToken.Text.Length;
        return new Range() { Start = new Position(line, start), End = new Position(line, end)};
    }
}