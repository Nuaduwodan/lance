using System.Diagnostics.CodeAnalysis;

namespace LanceServer.Core.Symbol;

public class SymbolTable
{
    private Dictionary<string, ISymbol> _symbols;

    public SymbolTable(Dictionary<string, ISymbol> symbols)
    {
        _symbols = symbols;
    }

    public bool TryGetSymbol(string symbolName, [MaybeNullWhen(false)] out ISymbol symbol)
    {
        return _symbols.TryGetValue(symbolName.ToLower(), out symbol);
    }
}