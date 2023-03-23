namespace LanceServer.Core.SymbolTable
{
    /// <summary>
    /// The data type of a symbol with an optional length if the type is string
    /// </summary>
    public class CompositeDataType
    {
        private readonly DataType _dataType;
        private readonly string _length;

        public CompositeDataType(DataType dataType, string length = "")
        {
            _length = length;
            _dataType = dataType;
        }

        public override string ToString()
        {
            string length = string.Empty;

            if (_dataType == DataType.String)
            {
                length = $"[{_length}]";
            }
            
            return $"{_dataType.ToString().ToLowerInvariant()}{length}";
        }
    }
}