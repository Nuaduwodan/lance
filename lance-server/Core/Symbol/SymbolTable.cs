using System.Diagnostics.CodeAnalysis;

namespace LanceServer.Core.Symbol;

public class SymbolTable
{
    private Dictionary<string, ISymbol> _symbols;

    public SymbolTable(IEnumerable<ISymbol> symbols)
    {
        _symbols = symbols.ToDictionary(symbol => symbol.Identifier.ToLower(), symbol => symbol);
    }

    public bool TryGetSymbol(string symbolName, [MaybeNullWhen(false)] out ISymbol symbol)
    {
        return _symbols.TryGetValue(symbolName.ToLower(), out symbol);
    }
}