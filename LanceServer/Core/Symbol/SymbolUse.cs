using Range = LspTypes.Range;

namespace LanceServer.Core.Symbol;

/// <inheritdoc />
public class SymbolUse : ISymbolUse
{
    /// <inheritdoc />
    public string Identifier { get; }

    /// <inheritdoc />
    public Range Range { get; }

    /// <inheritdoc />
    public Uri SourceDocument { get; }

    /// <summary>
    /// Creates a new symbol use
    /// </summary>
    /// <param name="identifier">The identifier of the symbol used</param>
    /// <param name="range">The position of the usage</param>
    /// <param name="sourceDocument">The source document of the usage</param>
    public SymbolUse(string identifier, Range range, Uri sourceDocument)
    {
        Identifier = identifier;
        Range = range;
        SourceDocument = sourceDocument;
    }
}