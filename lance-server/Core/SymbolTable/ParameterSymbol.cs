using LspTypes;

namespace LanceServer.Core.SymbolTable
{
    public class ParameterSymbol : ISymbol
    {        
        public string Identifier { get; }
        public Uri SourceDocument { get; }
        public Position Position { get; }
        public string Description { get; }
        
        private const string ArraySizeDelimiter = ", ";
        private readonly CompositeDataType _compositeDataType;
        private readonly string[] _arraySize;
        private readonly bool _outVar;

        public ParameterSymbol(string identifier, Uri sourceDocument, Position position, CompositeDataType compositeDataType, string[] arraySize, bool outVar = false,
            string description = "")
        {
            Identifier = identifier;
            SourceDocument = sourceDocument;
            Position = position;
            _compositeDataType = compositeDataType;
            _arraySize = arraySize;
            _outVar = outVar;
            Description = description;
        }



        public string GetCode()
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