using Range = LspTypes.Range;

namespace LanceServer.Core.Symbol;

/// <summary>
/// Represents a symbol.
/// </summary>
public abstract class AbstractSymbol : IEquatable<AbstractSymbol>
{
    /// <summary>
    /// The identifier or name of the symbol.
    /// </summary>
    public abstract string Identifier { get; }
    
    /// <summary>
    /// The URI of the document where the symbol is defined.
    /// </summary>
    public abstract Uri SourceDocument { get; }
    
    /// <summary>
    /// The range from the beginning to the end of the whole symbol
    /// </summary>
    public abstract Range SymbolRange { get; }
    
    /// <summary>
    /// The range from the beginning to the end of the identifier of the symbol
    /// </summary>
    public abstract Range IdentifierRange { get; }
    
    /// <summary>
    /// The human readable description of the symbol
    /// </summary>
    public abstract string Description { get; }
    
    /// <summary>
    /// The standardised code of the symbol
    /// </summary>
    public abstract string Code { get; }
    
    /// <summary>
    /// The documentation of the symbol
    /// </summary>
    public abstract string Documentation { get; }

    /// <summary>
    /// Whether or not the given reference references this symbol.
    /// </summary>
    /// <returns>True if the reference references this symbol, False otherwise.</returns>
    public bool ReferencesSymbol(string reference)
    {
        return Identifier.Equals(reference, StringComparison.OrdinalIgnoreCase);
    }

    public bool Equals(AbstractSymbol? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Identifier == other.Identifier && SourceDocument.Equals(other.SourceDocument) && IdentifierRange.Equals(other.IdentifierRange);
    }
}