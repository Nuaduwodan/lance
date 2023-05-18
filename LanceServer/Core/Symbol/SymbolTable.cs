using System.Diagnostics.CodeAnalysis;

namespace LanceServer.Core.Symbol;

/// <summary>
/// The symbol table containing all local symbols
/// </summary>
public class SymbolTable
{
    private Dictionary<string, AbstractSymbol> _symbols = new();

    public bool AddSymbol(AbstractSymbol symbol)
    {
        return _symbols.TryAdd(symbol.Identifier.ToLower(), symbol);
    }

    public bool TryGetSymbol(string symbolName, [MaybeNullWhen(false)] out AbstractSymbol symbol)
    {
        return _symbols.TryGetValue(symbolName.ToLower(), out symbol);
    }

    public IEnumerable<AbstractSymbol> GetAll()
    {
        return _symbols.Select(pair => pair.Value);
    }
}