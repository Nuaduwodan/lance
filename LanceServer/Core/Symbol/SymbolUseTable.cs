using System.Diagnostics.CodeAnalysis;
using LspTypes;
using Range = LspTypes.Range;

namespace LanceServer.Core.Symbol;

/// <summary>
/// Represents a list of all symbol uses.
/// </summary>
public class SymbolUseTable
{
    private IList<ISymbolUse> _symbolUses;

    public SymbolUseTable(IList<ISymbolUse> symbolUses)
    {
        _symbolUses = symbolUses;
    }

    public bool TryGetSymbol(Position position, [MaybeNullWhen(false)] out ISymbolUse symbol)
    {
        symbol = null;
        if (_symbolUses.Any(symbolUse => IsInRange(symbolUse.Range, position)))
        {
            symbol = _symbolUses.First(symbolUse => IsInRange(symbolUse.Range, position));
            return true;
        }

        return false;
    }

    private bool IsInRange(Range range, Position position)
    {
        if (position.Line < range.Start.Line || range.End.Line < position.Line)
        {
            return false;
        }
        
        if (position.Character < range.Start.Character || range.End.Character < position.Character)
        {
            return false;
        }

        return true;
    }

    public IList<ISymbolUse> GetAll()
    {
        return _symbolUses;
    }
}