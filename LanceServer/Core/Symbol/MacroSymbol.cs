using Range = LspTypes.Range;

namespace LanceServer.Core.Symbol;

/// <summary>
/// A macro symbol
/// </summary>
public class MacroSymbol : ISymbol
{
    /// <inheritdoc/>
    public string Identifier { get; }

    /// <inheritdoc/>
    public Uri SourceDocument { get; }
        
    /// <inheritdoc/>
    public Range SymbolRange { get; }
        
    /// <inheritdoc/>
    public Range IdentifierRange { get; }

    /// <inheritdoc/>
    public string Description => $"{(_isGlobal ? "global" : "local")} macro{(_isGlobal ? " in " + Path.GetFileName(SourceDocument.LocalPath) : "")}";
        
    /// <inheritdoc/>
    public string Code => $"define {Identifier} as {_value}";
        
    /// <inheritdoc/>
    public string Documentation { get; }
        
    private readonly string _value;
    private readonly bool _isGlobal;
        
    public MacroSymbol(string identifier, Uri sourceDocument, Range symbolRange, Range identifierRange, string value, bool isGlobal, string documentation = "")
    {
        Identifier = identifier;
        SourceDocument = sourceDocument;
        SymbolRange = symbolRange;
        IdentifierRange = identifierRange;
        _value = value;
        _isGlobal = isGlobal;
        Documentation = documentation;
    }
}