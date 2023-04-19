using LspTypes;

namespace LanceServer.Core.Symbol
{
    public class ParameterSymbol : ISymbol
    {        
        public string Identifier { get; }
        public Uri SourceDocument { get; }
        public Position Position { get; }
        
        public string Description => $"parameter by {(_outVar?"reference":"value")}";
        public string Code => GetCode();
        public string Documentation { get; }

        private const string ArraySizeDelimiter = ", ";
        private readonly CompositeDataType _compositeDataType;
        private readonly string[] _arraySize;
        private readonly bool _outVar;

        public ParameterSymbol(string identifier, Uri sourceDocument, Position position, CompositeDataType compositeDataType, string[] arraySize, bool outVar = false,
            string documentation = "")
        {
            Identifier = identifier;
            SourceDocument = sourceDocument;
            Position = position;
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
}