namespace LanceServer.Core.Symbol;

/// <summary>
/// The data type of a symbol with an optional length if the type is a string
/// </summary>
public class CompositeDataType
{
    private readonly DataType _dataType;
    private readonly string _length;

    /// <summary>
    /// Instantiates a new <see cref="CompositeDataType"/>
    /// </summary>
    /// <param name="dataType">The data type</param>
    /// <param name="length">The expression of the length as a string if the data type is a string</param>
    public CompositeDataType(DataType dataType, string length = "")
    {
        _length = length;
        _dataType = dataType;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        string length = string.Empty;

        if (_dataType == DataType.String)
        {
            length = $"[{_length}]";
        }
            
        return $"{_dataType.ToString().ToLower()}{length}";
    }
}