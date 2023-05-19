using Range = LspTypes.Range;

namespace LanceServer.Core.Symbol;

/// <summary>
/// A use of a symbol.
/// </summary>
public abstract class AbstractSymbolUse
{
    /// <summary>
    /// The identifier of the symbol which is used.
    /// </summary>
    public abstract string Identifier { get; }
    
    /// <summary>
    /// The range of the symbol use in the code.
    /// </summary>
    public abstract Range Range { get; }
    
    /// <summary>
    /// The uri of the file where the symbol is used.
    /// </summary>
    public abstract Uri SourceDocument { get; }

    /// <summary>
    /// Whether or not the given reference references this symbol use.
    /// </summary>
    /// <returns>True if the reference references this symbol use, False otherwise.</returns>
    public bool IsReferencedBy(string reference)
    {
        return Identifier.Equals(reference, StringComparison.OrdinalIgnoreCase);
    }
}