using Range = LspTypes.Range;

namespace LanceServer.Core.Symbol;

/// <summary>
/// A macro symbol
/// </summary>
public class MacroSymbol : AbstractSymbol
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
    public override string Description => $"{(_isGlobal ? "global" : "local")} macro{(_isGlobal ? " in " + Path.GetFileName(SourceDocument.LocalPath) : "")}";
        
    /// <inheritdoc/>
    public override string Code => $"define {Identifier} as {_value}";
        
    /// <inheritdoc/>
    public override string Documentation { get; }
        
    /// <summary>
    /// The value the macro resolves to.
    /// </summary>
    private readonly string _value;
    
    /// <summary>
    /// Whether or not the macro is global
    /// </summary>
    private readonly bool _isGlobal;
        
    public MacroSymbol(string identifier, Uri sourceDocument, Range symbolRange, Range identifierRange, string value, bool isGlobal, string documentation = "")
    {
        Identifier = identifier;
        SourceDocument = sourceDocument;
        SymbolRange = symbolRange;
        IdentifierRange = identifierRange;
        _value = value;
        _isGlobal = isGlobal;
        Documentation = documentation;
    }
}