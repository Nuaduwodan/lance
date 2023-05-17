using Range = LspTypes.Range;

namespace LanceServer.Core.Symbol;

/// <summary>
/// Represents a not found symbol
/// </summary>
public class ErrorSymbol : ISymbol
{
    /// <inheritdoc/>
    public string Identifier => string.Empty;

    /// <inheritdoc/>
    public Uri SourceDocument => null!;

    /// <inheritdoc/>
    public Range SymbolRange => null!;

    /// <inheritdoc/>
    public Range IdentifierRange => null!;

    /// <inheritdoc/>
    public string Description => string.Empty;
    
    /// <inheritdoc/>
    public string Code => string.Empty;
    
    /// <inheritdoc/>
    public string Documentation { get; }

    /// <summary>
    /// Instantiates a new <see cref="ErrorSymbol"/>
    /// </summary>
    /// <param name="documentation">A human readable description of the problem.</param>
    public ErrorSymbol(string documentation)
    {
        Documentation = documentation;
    }
}