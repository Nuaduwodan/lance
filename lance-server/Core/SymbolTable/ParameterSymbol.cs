using LspTypes;

namespace LanceServer.Core.SymbolTable
{
    public class ParameterSymbol : VariableSymbol
    {
        private readonly bool _outVar;

        public ParameterSymbol(string identifier, SymbolType symbolType, Uri sourceDocument, Position position, CompositeDataType compositeDataType, int[] arraySize, bool outVar = false,
            string description = "") : base(identifier, symbolType, sourceDocument, position, compositeDataType, arraySize, description)
        {
            _outVar = outVar;
        }

        public override string GetCode()
        {
            string var = string.Empty;

            if (_outVar)
            {
                var = "var ";
            }

            return $"{var}{CompositeDataType} {Identifier}{ArraySize}";
        }
    }
}