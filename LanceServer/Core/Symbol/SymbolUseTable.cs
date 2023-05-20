using System.Diagnostics.CodeAnalysis;
using LanceServer.Protocol;
using LspTypes;

namespace LanceServer.Core.Symbol;

/// <summary>
/// Represents a list of all symbol uses.
/// </summary>
public class SymbolUseTable
{
    private readonly IList<AbstractSymbolUse> _symbolUses;

    public SymbolUseTable(IList<AbstractSymbolUse> symbolUses)
    {
        _symbolUses = symbolUses;
    }

    /// <summary>
    /// Tries to get a symbol.
    /// </summary>
    /// <param name="position">The position of the symbol to be found.</param>
    /// <param name="symbol">The symbol if one was found.</param>
    /// <returns>True if a symbol was found, false otherwise.</returns>
    public bool TryGetSymbol(Position position, [MaybeNullWhen(false)] out AbstractSymbolUse symbol)
    {
        symbol = null;
        if (_symbolUses.Any(symbolUse => position.IsInRange(symbolUse.Range)))
        {
            symbol = _symbolUses.First(symbolUse => position.IsInRange(symbolUse.Range));
            return true;
        }

        return false;
    }

    public IList<AbstractSymbolUse> GetAll()
    {
        return _symbolUses;
    }
}