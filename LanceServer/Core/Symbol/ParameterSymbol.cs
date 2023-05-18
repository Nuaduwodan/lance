using Range = LspTypes.Range;

namespace LanceServer.Core.Symbol;

/// <summary>
/// A parameter symbol
/// </summary>
public class ParameterSymbol : ISymbol
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
    public string Description => $"parameter by {(_outVar ? "reference" : "value")}";
        
    /// <inheritdoc/>
    public string Code => GetCode();
        
    /// <inheritdoc/>
    public string Documentation { get; }

    private const string ArraySizeDelimiter = ", ";
    private readonly CompositeDataType _compositeDataType;
    private readonly string[] _arraySize;
    private readonly bool _outVar;

    public ParameterSymbol(string identifier, Uri sourceDocument, Range symbolRange, Range identifierRange, CompositeDataType compositeDataType, string[] arraySize, bool outVar = false,
        string documentation = "")
    {
        Identifier = identifier;
        SourceDocument = sourceDocument;
        SymbolRange = symbolRange;
        IdentifierRange = identifierRange;
        _compositeDataType = compositeDataType;
        _arraySize = arraySize;
        _outVar = outVar;
        Documentation = documentation;
    }

    private string GetCode()
    {
        string var = string.Empty;

        if (_outVar)
        {
            var = "var ";
        }

        var arraySize = string.Empty;
        if (_arraySize.Length >= 1)
        {
            arraySize = $"[{string.Join(ArraySizeDelimiter, _arraySize)}]";
        }

        return $"{var}{_compositeDataType} {Identifier}{arraySize}";
    }
}