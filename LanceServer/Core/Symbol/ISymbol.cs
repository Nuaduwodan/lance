using Range = LspTypes.Range;

namespace LanceServer.Core.Symbol;

/// <summary>
/// Represents a symbol.
/// </summary>
public interface ISymbol
{
    /// <summary>
    /// The identifier or name of the symbol.
    /// </summary>
    public string Identifier { get; }
    
    /// <summary>
    /// The type of the symbol.
    /// </summary>
    public SymbolType Type { get; }
    
    /// <summary>
    /// The URI of the document where the symbol is defined.
    /// </summary>
    public Uri SourceDocument { get; }
    
    /// <summary>
    /// The range from the beginning to the end of the whole symbol
    /// </summary>
    public Range SymbolRange { get; }
    
    /// <summary>
    /// The range from the beginning to the end of the identifier of the symbol
    /// </summary>
    public Range IdentifierRange { get; }
    
    /// <summary>
    /// The human readable description of the symbol
    /// </summary>
    public string Description { get; }
    
    /// <summary>
    /// The standardised code of the symbol
    /// </summary>
    public string Code { get; }
    
    /// <summary>
    /// The documentation of the symbol
    /// </summary>
    public string Documentation { get; }
}