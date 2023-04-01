using System.Diagnostics.CodeAnalysis;
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
        private string _rawContent = String.Empty;

        public string RawContent
        {
            get
            {
                return _rawContent;
            }
            set
            {
                if (_rawContent == value)
                {
                    return;
                }
                State = DocumentState.Read;
                _rawContent = value;
            }
        }
        
        private string _code = String.Empty;

        public string Code
        {
            get
            {
                return _code;
            }
            set
            {
                if (_code == value)
                {
                    return;
                }
                State = DocumentState.Preprocessed;
                _code = value;
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
                State = DocumentState.Parsed;
                _tree = value;
            }
        }
        
        public string Encoding { get; }
        public string FileEnding { get; }
        public Uri Uri { get; }
        
        private Dictionary<string, ISymbol> _symbols = new();

        public bool TryGetSymbol(string symbolName, [MaybeNullWhen(false)] out ISymbol symbol)
        {
            return _symbols.TryGetValue(symbolName, out symbol);
        }

        public Document(Uri uri, string encoding = "utf8")
        {
            Uri = uri;
            FileEnding = Path.GetExtension(uri.LocalPath);
            Encoding = encoding;
            State = DocumentState.Known;
        }

        public bool AddSymbol(ISymbol symbol)
        {
            if (_symbols.ContainsKey(symbol.Identifier))
            {
                return false;
            }
            _symbols.Add(symbol.Identifier, symbol);
            return true;
        }

        public void DeleteAllSymbols()
        {
            _symbols = new Dictionary<string, ISymbol>();
            if (State >= DocumentState.Visited)
            {
                State = DocumentState.Parsed;
            }
        }

        public void SetVisited()
        {
            State = DocumentState.Visited;
        }
    }
}