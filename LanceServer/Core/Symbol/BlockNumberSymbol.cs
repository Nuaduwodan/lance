using Range = LspTypes.Range;

namespace LanceServer.Core.Symbol;

/// <summary>
/// A block number symbol
/// </summary>
public class BlockNumberSymbol : ISymbol
{
    /// <inheritdoc/>
    public string Identifier { get; }
    
    /// <inheritdoc/>
    public Uri SourceDocument { get; }
    
    /// <inheritdoc/>
    public Range SymbolRange { get; }
    
    /// <inheritdoc/>
    public Range IdentifierRange => SymbolRange;

    /// <inheritdoc/>
    public string Description => $"block number on line {SymbolRange.Start.Line + 1}";
    
    /// <inheritdoc/>
    public string Code => Identifier;
    
    /// <inheritdoc/>
    public string Documentation { get; }
    
    public BlockNumberSymbol(string identifier, Uri sourceDocument, Range symbolRange, string documentation = "")
    {
        Identifier = identifier;
        SourceDocument = sourceDocument;
        SymbolRange = symbolRange;
        Documentation = documentation;
    }
}