using Range = LspTypes.Range;

namespace LanceServer.Core.Symbol;

/// <summary>
/// A block number symbol
/// </summary>
public class BlockNumberSymbol : AbstractSymbol
{
    /// <inheritdoc/>
    public override string Identifier { get; }
    
    /// <inheritdoc/>
    public override Uri SourceDocument { get; }
    
    /// <inheritdoc/>
    public override Range SymbolRange { get; }
    
    /// <inheritdoc/>
    public override Range IdentifierRange => SymbolRange;

    /// <inheritdoc/>
    public override string Description => $"block number on line {SymbolRange.Start.Line + 1}";
    
    /// <inheritdoc/>
    public override string Code => Identifier;
    
    /// <inheritdoc/>
    public override string Documentation { get; }
    
    public BlockNumberSymbol(string identifier, Uri sourceDocument, Range symbolRange, string documentation = "")
    {
        Identifier = identifier;
        SourceDocument = sourceDocument;
        SymbolRange = symbolRange;
        Documentation = documentation;
    }
}