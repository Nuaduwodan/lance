using System.Diagnostics.CodeAnalysis;

namespace LanceServer.Core.Symbol;

/// <summary>
/// The symbol table containing all local symbols
/// </summary>
public class SymbolTable
{
    private Dictionary<string, ISymbol> _symbols = new();

    public bool AddSymbol(ISymbol symbol)
    {
        return _symbols.TryAdd(symbol.Identifier.ToLower(), symbol);
    }

    public bool TryGetSymbol(string symbolName, [MaybeNullWhen(false)] out ISymbol symbol)
    {
        return _symbols.TryGetValue(symbolName.ToLower(), out symbol);
    }

    public IEnumerable<ISymbol> GetAll()
    {
        return _symbols.Select(pair => pair.Value);
    }
}