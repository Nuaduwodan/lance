using Antlr4.Runtime.Tree;
using LanceServer.Core.SymbolTable;

namespace LanceServer.Core.Workspace
{
    /// <summary>
    /// Represents a single file and is used to cache data about it.
    /// </summary>
    public class Document
    {
        private string _content;

        public bool Changed
        {
            get;
            set;
        }
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                if (_content == value)
                {
                    return;
                }
                Changed = true;
                _content = value;
            }
        }

        private IParseTree _tree;

        public IParseTree Tree
        {
            get
            {
                return _tree;
            }
            set
            {
                if (_tree != value)
                {
                    _tree = value;
                }
                Changed = false;
            }
        }
        
        public string Encoding { get; set; }
        public Uri Uri { get; private set; }
        
        private Dictionary<string, Symbol> _symbols = new();

        public Symbol GetSymbol(string symbolName)
        {
            return _symbols[symbolName];
        }

        public Document(Uri uri, string encoding = "utf8")
        {
            Uri = uri;
            Encoding = encoding;
            Changed = false;
        }

        public void AddSymbol(Symbol symbol)
        {
            _symbols.Add(symbol.Identifier, symbol);
        }
    }
}