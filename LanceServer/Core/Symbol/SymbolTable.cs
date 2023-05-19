using System.Diagnostics.CodeAnalysis;

namespace LanceServer.Core.Symbol;

/// <summary>
/// The symbol table containing all local symbols
/// </summary>
public class SymbolTable
{
    private Dictionary<string, AbstractSymbol> _symbols = new();

    /// <summary>
    /// Attempts to add the symbol to the table.
    /// </summary>
    /// <returns>True if the symbol was added successfully, false otherwise.</returns>
    public bool TryAddSymbol(AbstractSymbol symbol)
    {
        return _symbols.TryAdd(symbol.Identifier.ToLower(), symbol);
    }

    /// <summary>
    /// Tries to get the symbol referenced by the symbol name.
    /// </summary>
    /// <returns>True if the symbol was found, false otherwise.</returns>
    public bool TryGetSymbol(string symbolName, [MaybeNullWhen(false)] out AbstractSymbol symbol)
    {
        return _symbols.TryGetValue(symbolName.ToLower(), out symbol);
    }

    /// <summary>
    /// Gets all symbols in this symbol table.
    /// </summary>
    public IEnumerable<AbstractSymbol> GetAll()
    {
        return _symbols.Select(pair => pair.Value);
    }
}