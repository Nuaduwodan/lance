using Range = LspTypes.Range;

namespace LanceServer.Core.Symbol;

/// <summary>
/// The variable symbol
/// </summary>
public class VariableSymbol : AbstractSymbol
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
    public override string Description => $"{(_isGlobal ? "global" : "local")} variable{(_isGlobal ? " in " + Path.GetFileName(SourceDocument.LocalPath) : "")}";
        
    /// <inheritdoc/>
    public override string Code => GetCode();
        
    /// <inheritdoc/>
    public override string Documentation { get; }

    private readonly CompositeDataType _compositeDataType;
    private readonly string[] _arraySize;
    private readonly bool _isGlobal;
        
    private const string ArraySizeDelimiter = ", ";

    public VariableSymbol(string identifier, Uri sourceDocument, Range symbolRange, Range identifierRange, CompositeDataType compositeDataType, string[] arraySize, bool isGlobal,
        string documentation = "")
    {
        Identifier = identifier;
        SourceDocument = sourceDocument;
        SymbolRange = symbolRange;
        IdentifierRange = identifierRange;
        _compositeDataType = compositeDataType;
        _arraySize = arraySize;
        _isGlobal = isGlobal;
        Documentation = documentation;
    }

    private string GetCode()
    {
        var arraySizeString = string.Empty;
        if (_arraySize.Length >= 1)
        {
            arraySizeString = $"[{string.Join(ArraySizeDelimiter, _arraySize)}]";
        }
            
        return $"def {_compositeDataType} {Identifier}{arraySizeString}";
    }
}