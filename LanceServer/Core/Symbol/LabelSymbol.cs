using Range = LspTypes.Range;

namespace LanceServer.Core.Symbol;

/// <summary>
/// A label symbol which can be used as target by goto commands
/// </summary>
public class LabelSymbol : AbstractSymbol
{
    /// <inheritdoc/>
    public override string Identifier { get; }
    
    /// <inheritdoc/>
    public override Uri SourceDocument { get; }
    
    /// <inheritdoc/>
    public override Range SymbolRange { get; }
    
    /// <inheritdoc/>
    public override Range IdentifierRange { get; }
    
    /// <inheritdoc/>
    public override string Description => $"label";
    
    /// <inheritdoc/>
    public override string Code => $"{Identifier}:";
    
    /// <inheritdoc/>
    public override string Documentation { get; }

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