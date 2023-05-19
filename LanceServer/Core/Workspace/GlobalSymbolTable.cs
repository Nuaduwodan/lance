using LanceServer.Core.Symbol;

namespace LanceServer.Core.Workspace;

public class GlobalSymbolTable
{
    private List<AbstractSymbol> _globalSymbols;

    private static readonly object SymbolTableLock = new();

    public GlobalSymbolTable()
    {
        _globalSymbols = new List<AbstractSymbol>();
    }

    public IEnumerable<AbstractSymbol> AddSymbol(AbstractSymbol newSymbol)
    {
        lock (SymbolTableLock)
        {
            var existingSymbols = _globalSymbols.Where(symbol => symbol.ReferencesSymbol(newSymbol.Identifier)).ToList();
            
            _globalSymbols.Add(newSymbol);
            return existingSymbols;
        }
    }

    public IEnumerable<AbstractSymbol> GetGlobalSymbolsOfDocument(Uri uri)
    {
        lock (SymbolTableLock)
        {
            return _globalSymbols.Where(symbol => symbol.SourceDocument == uri).ToList();
        }
    }

    public IList<AbstractSymbol> GetGlobalSymbols(string symbolName)
    {
        lock (SymbolTableLock)
        {
            return _globalSymbols.Where(symbol => symbol.ReferencesSymbol(symbolName)).ToList();
        }
    }

    public void DeleteGlobalSymbolsOfDocument(Uri documentUri)
    {
        lock (SymbolTableLock)
        {
            foreach (var symbol in GetGlobalSymbolsOfDocument(documentUri))
            {
                _globalSymbols.Remove(symbol);
            }
        }
    }
}