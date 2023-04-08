using LspTypes;

namespace LanceServer.Core.SymbolTable
{
    public class MacroSymbol : ISymbol
    {
        public string Identifier { get; }
        public Uri SourceDocument { get; }
        public Position Position { get; }
        public string Description => $"{(isGlobal?"global":"local")} macro";

        public string Code => $"define {Identifier} as {_value}";

        public string Documentation { get; }
        private readonly string _value;
        private readonly bool isGlobal;
        
        public MacroSymbol(string identifier, Uri sourceDocument, Position position, string value, bool isGlobal, string documentation = "")
        {
            Identifier = identifier;
            SourceDocument = sourceDocument;
            Position = position;
            _value = value;
            this.isGlobal = isGlobal;
            Documentation = documentation;
        }
    }
}