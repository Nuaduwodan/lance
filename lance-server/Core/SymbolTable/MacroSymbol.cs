using LspTypes;

namespace LanceServer.Core.SymbolTable
{
    public class MacroSymbol : ISymbol
    {
        public string Identifier { get; }
        public Uri SourceDocument { get; }
        public Position Position { get; }
        public string Description { get; }
        private readonly string _value;
        
        public MacroSymbol(string identifier, Uri sourceDocument, Position position, string value, string description = "")
        {
            Identifier = identifier;
            SourceDocument = sourceDocument;
            Position = position;
            _value = value;
            Description = description;
        }


        public string GetCode()
        {
            return $"define {Identifier} as {_value}";
        }
    }
}