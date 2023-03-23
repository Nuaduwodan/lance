using LspTypes;

namespace LanceServer.Core.SymbolTable
{
    public class VariableSymbol : ISymbol
    {
        public string Identifier { get; }
        public Uri SourceDocument { get; }
        public Position Position { get; }
        public string Description { get; }
        
        private readonly CompositeDataType _compositeDataType;
        private readonly string[] _arraySize;
        
        private const string ArraySizeDelimiter = ", ";

        public VariableSymbol(string identifier, Uri sourceDocument, Position position, CompositeDataType compositeDataType, string[] arraySize,
            string description = "")
        {
            Identifier = identifier;
            SourceDocument = sourceDocument;
            Position = position;
            _compositeDataType = compositeDataType;
            _arraySize = arraySize;
            Description = description;
        }

        public string GetCode()
        {
            var arraySizeString = string.Empty;
            if (_arraySize?.Length >= 1)
            {
                arraySizeString = $"[{string.Join(ArraySizeDelimiter, _arraySize)}]";
            }
            
            return $"def {_compositeDataType} {Identifier}{arraySizeString}";
        }
    }
}