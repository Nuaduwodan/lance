using LspTypes;

namespace LanceServer.Core.SymbolTable
{
    public class VariableSymbol : Symbol
    {
        protected readonly CompositeDataType CompositeDataType;
        protected readonly int[] ArraySize;
        private const string ArraySizeDelimiter = ", ";

        public VariableSymbol(string identifier, SymbolType symbolType, Uri sourceDocument, Position position, CompositeDataType compositeDataType, int[] arraySize,
            string description = "") : base(identifier, symbolType, sourceDocument, position, description)
        {
            CompositeDataType = compositeDataType;
            ArraySize = arraySize;
        }

        public override string GetCode()
        {
            var arraySizeString = string.Empty;
            if (ArraySize?.Length >= 1)
            {
                arraySizeString = $"[{string.Join(ArraySizeDelimiter, ArraySize)}]";
            }
            
            return $"def {CompositeDataType} {Identifier}{arraySizeString}";
        }
    }
}