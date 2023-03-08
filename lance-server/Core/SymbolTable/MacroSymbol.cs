using LspTypes;

namespace LanceServer.Core.SymbolTable
{
    public class MacroSymbol : Symbol
    {
        private readonly string _value;
        
        public MacroSymbol(string identifier, SymbolType symbolType, Uri sourceDocument, Position position, string value, string description = "") 
            : base(identifier, symbolType, sourceDocument, position, description)
        {
            _value = value;
        }
        
        public override string GetCode()
        {
            return $"define {Identifier} as {_value}";
        }
    }
}