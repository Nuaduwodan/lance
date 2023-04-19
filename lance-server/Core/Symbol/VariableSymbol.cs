using LspTypes;

namespace LanceServer.Core.Symbol
{
    public class VariableSymbol : ISymbol
    {
        public string Identifier { get; }
        public Uri SourceDocument { get; }
        public Position Position { get; }
        public string Description => $"{(isGlobal?"global":"local")} variable";
        public string Code => GetCode();
        public string Documentation { get; }

        private readonly CompositeDataType _compositeDataType;
        private readonly string[] _arraySize;
        private readonly bool isGlobal;
        
        private const string ArraySizeDelimiter = ", ";

        public VariableSymbol(string identifier, Uri sourceDocument, Position position, CompositeDataType compositeDataType, string[] arraySize, bool isGlobal,
            string documentation = "")
        {
            Identifier = identifier;
            SourceDocument = sourceDocument;
            Position = position;
            _compositeDataType = compositeDataType;
            _arraySize = arraySize;
            this.isGlobal = isGlobal;
            Documentation = documentation;
        }

        private string GetCode()
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