using System.Diagnostics.CodeAnalysis;
using LanceServer.Protocol;
using LspTypes;

namespace LanceServer.Core.Document;

/// <summary>
/// The table with the language tokens
/// </summary>
public class LanguageTokenTable
{
    private IList<LanguageToken> _languageTokens;

    public LanguageTokenTable(IList<LanguageToken> languageTokens)
    {
        _languageTokens = languageTokens;
    }

    /// <summary>
    /// Tries to get a language token at a specific position.
    /// </summary>
    /// <param name="position">The position in the code.</param>
    /// <param name="languageToken">The requested language token if found.</param>
    /// <returns>True if a language token was found, false otherwise.</returns>
    public bool TryGetToken(Position position, [MaybeNullWhen(false)] out LanguageToken languageToken)
    {
        languageToken = null;
        if (_languageTokens.Any(token => position.IsInRange(token.Range)))
        {
            languageToken = _languageTokens.First(languageToken => position.IsInRange(languageToken.Range));
            return true;
        }

        return false;
    }
}