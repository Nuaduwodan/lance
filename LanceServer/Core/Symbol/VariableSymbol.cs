using Range = LspTypes.Range;

namespace LanceServer.Core.Symbol;

/// <summary>
/// The variable symbol
/// </summary>
public class VariableSymbol : ISymbol
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
    public string Description => $"{(_isGlobal ? "global" : "local")} variable{(_isGlobal ? " in " + Path.GetFileName(SourceDocument.LocalPath) : "")}";
        
    /// <inheritdoc/>
    public string Code => GetCode();
        
    /// <inheritdoc/>
    public string Documentation { get; }

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