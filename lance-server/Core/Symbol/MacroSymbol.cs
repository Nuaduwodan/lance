using Range = LspTypes.Range;

namespace LanceServer.Core.Symbol;

/// <summary>
/// Represents a macro symbol
/// </summary>
public class MacroSymbol : ISymbol
{
    /// <inheritdoc/>
    public string Identifier { get; }

    /// <inheritdoc/>
    public SymbolType Type { get; }

    /// <inheritdoc/>
    public Uri SourceDocument { get; }
        
    /// <inheritdoc/>
    public Range SymbolRange { get; }
        
    /// <inheritdoc/>
    public Range IdentifierRange { get; }
        
    /// <inheritdoc/>
    public string Description => $"{(isGlobal?"global":"local")} macro{(isGlobal?" in "+Path.GetFileName(SourceDocument.LocalPath):"")}";
        
    /// <inheritdoc/>
    public string Code => $"define {Identifier} as {_value}";
        
    /// <inheritdoc/>
    public string Documentation { get; }
        
    private readonly string _value;
    private readonly bool isGlobal;
        
    public MacroSymbol(string identifier, Uri sourceDocument, Range symbolRange, Range identifierRange, string value, bool isGlobal, string documentation = "")
    {
        Identifier = identifier;
        SourceDocument = sourceDocument;
        SymbolRange = symbolRange;
        IdentifierRange = identifierRange;
        _value = value;
        this.isGlobal = isGlobal;
        Documentation = documentation;
        Type = SymbolType.Macro;
    }
}