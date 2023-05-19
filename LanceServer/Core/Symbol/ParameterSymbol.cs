using Range = LspTypes.Range;

namespace LanceServer.Core.Symbol;

/// <summary>
/// A parameter symbol
/// </summary>
public class ParameterSymbol : AbstractSymbol
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
    public override string Description => $"parameter by {(_isReferenceValue ? "reference" : "value")}";
        
    /// <inheritdoc/>
    public override string Code => GetCode();
        
    /// <inheritdoc/>
    public override string Documentation { get; }
    
    public bool IsOptional { get; }

    private const string ArraySizeDelimiter = ", ";
    private readonly CompositeDataType _compositeDataType;
    private readonly string[] _arraySize;
    private readonly bool _isReferenceValue;

    public ParameterSymbol(string identifier,
        Uri sourceDocument,
        Range symbolRange,
        Range identifierRange,
        CompositeDataType compositeDataType,
        string[] arraySize,
        bool isReferenceValue = false,
        bool isOptional = false,
        string documentation = "")
    {
        if (isReferenceValue && isOptional)
        {
            throw new ArgumentException("parameter by reference can not be optional");
        }
        
        Identifier = identifier;
        SourceDocument = sourceDocument;
        SymbolRange = symbolRange;
        IdentifierRange = identifierRange;
        _compositeDataType = compositeDataType;
        _arraySize = arraySize;
        _isReferenceValue = isReferenceValue;
        IsOptional = isOptional;
        Documentation = documentation;
    }

    private string GetCode()
    {
        string var = string.Empty;

        if (_isReferenceValue)
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