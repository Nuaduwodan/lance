using Range = LspTypes.Range;

namespace LanceServer.Core.Symbol;

/// <summary>
/// Represents a label symbol which can be used as target by goto commands
/// </summary>
public class LabelSymbol : ISymbol
{
    /// <inheritdoc/>
    public string Identifier { get; }
    
    /// <inheritdoc/>
    public Uri SourceDocument { get; }
    
    /// <inheritdoc/>
    public Range SymbolRange { get; }
    
    /// <inheritdoc/>
    public Range IdentifierRange { get; }
    
    /// <inheritdoc/>
    public string Description => $"label";
    
    /// <inheritdoc/>
    public string Code => $"{Identifier}:";
    
    /// <inheritdoc/>
    public string Documentation { get; }

    /// <summary>
    /// Instantiates a new <see cref="LabelSymbol"/>
    /// </summary>
    public LabelSymbol(string identifier, Uri sourceDocument, Range symbolRange, Range identifierRange, string documentation = "")
    {
        Identifier = identifier;
        SourceDocument = sourceDocument;
        SymbolRange = symbolRange;
        IdentifierRange = identifierRange;
        Documentation = documentation;
    }
}