using Antlr4.Runtime.Tree;
using LanceServer.Core.SymbolTable;

namespace LanceServer.Core.Workspace
{
    /// <summary>
    /// Represents a single file and is used to cache data about it.
    /// </summary>
    public class Document
    {
        public DocumentState State { get; private set; }
        private string _content;

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
                State = DocumentState.Read;
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
                if (_tree == value)
                {
                    return;
                }
                State = DocumentState.Parsed;
                _tree = value;
            }
        }
        
        public string Encoding { get; }
        public Uri Uri { get; }
        
        private Dictionary<string, ISymbol> _symbols = new();

        public ISymbol GetSymbol(string symbolName)
        {
            return _symbols[symbolName];
        }

        public Document(Uri uri, string encoding = "utf8")
        {
            Uri = uri;
            Encoding = encoding;
            State = DocumentState.Known;
        }

        public void AddSymbol(ISymbol symbol)
        {
            _symbols.Add(symbol.Identifier, symbol);
        }

        public void SetVisited()
        {
            State = DocumentState.Visited;
        }
    }
}