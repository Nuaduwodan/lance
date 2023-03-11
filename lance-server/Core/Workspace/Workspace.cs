using LanceServer.Core.Configuration;
using LanceServer.Core.SymbolTable;

namespace LanceServer.Core.Workspace
{
    public class Workspace
    {
        private Dictionary<Uri, Document> _documents = new();
        private Dictionary<string, Symbol> _globalSymbols = new();

        private readonly SymbolTableConfiguration _config;

        public Workspace(SymbolTableConfiguration config)
        {
            _config = config;
        }

        public Document GetDocument(Uri uri)
        {
            if (HasDocument(uri))
            {
                return _documents[uri];
            }

            var document = Util.ReadDocument(uri);
            _documents.Add(uri, document);
            return document;
        }

        public bool HasDocument(Uri uri)
        {
            return _documents.ContainsKey(uri);
        }

        public bool AddDocument(Uri uri)
        {
            if (HasDocument(uri))
            {
                return false;
            }

            GetDocument(uri);
            return true;
        }

        public Symbol GetSymbol(string symbolName, Uri documentOfReference)
        {
            var symbol = GetDocument(documentOfReference).GetSymbol(symbolName);

            if (symbol is not null)
            {
                return symbol;
            }
            
            symbol = _globalSymbols[symbolName];
            
            if (symbol is not null)
            {
                return symbol;
            }

            return new Symbol($"Cannot resolve symbol '{symbolName}'");
        }

        public void AddSymbol(Symbol symbol)
        {
            if (IsGlobalSymbol(symbol.SourceDocument))
            {
                _globalSymbols.Add(symbol.Identifier, symbol);
            }
            else
            {
                GetDocument(symbol.SourceDocument).AddSymbol(symbol);
            }
        }

        private bool IsGlobalSymbol(Uri sourceDocument)
        {
            var fileEnding = Path.GetExtension(sourceDocument.AbsolutePath);

            if (_config.GlobalFileEndings.Contains(fileEnding))
            {
                return true;
            }

            var directories = Path.GetDirectoryName(sourceDocument.AbsolutePath)!.Split(Path.DirectorySeparatorChar);

            if (_config.GlobalDirectories.Intersect(directories).Any())
            {
                return true;
            }
            
            return false;
        }
    }
}