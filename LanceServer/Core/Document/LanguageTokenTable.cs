using System.Diagnostics.CodeAnalysis;
using LspTypes;

namespace LanceServer.Core.Document;

public class LanguageTokenTable
{
    private IList<LanguageToken> _languageTokens;

    public LanguageTokenTable(IList<LanguageToken> languageTokens)
    {
        _languageTokens = languageTokens;
    }

    public bool TryGetToken(Position position, [MaybeNullWhen(false)] out LanguageToken symbol)
    {
        symbol = null;
        if (_languageTokens.Any(languageToken => IsInRange(languageToken.Range, position)))
        {
            symbol = _languageTokens.First(languageToken => IsInRange(languageToken.Range, position));
            return true;
        }

        return false;
    }

    private bool IsInRange(LspTypes.Range range, Position position)
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
}