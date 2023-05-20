using LanceServer.Core.Symbol;

namespace LanceServer.Core.Workspace;

/// <summary>
/// The thread save symbol table for global symbols.
/// </summary>
public class GlobalSymbolTable
{
    private List<AbstractSymbol> _globalSymbols;

    private static readonly object SymbolTableLock = new();

    public GlobalSymbolTable()
    {
        _globalSymbols = new List<AbstractSymbol>();
    }

    /// <summary>
    /// Adds a new global symbol to the table.
    /// </summary>
    public void AddSymbol(AbstractSymbol newSymbol)
    {
        lock (SymbolTableLock)
        {
            _globalSymbols.Add(newSymbol);
        }
    }

    /// <summary>
    /// Returns all global symbols defined in the referenced document.
    /// </summary>
    public IEnumerable<AbstractSymbol> GetGlobalSymbolsOfDocument(Uri uri)
    {
        lock (SymbolTableLock)
        {
            return _globalSymbols.Where(symbol => symbol.SourceDocument == uri).ToList();
        }
    }

    /// <summary>
    /// Returns all global symbols.
    /// </summary>
    public IList<AbstractSymbol> GetGlobalSymbols(string symbolName)
    {
        lock (SymbolTableLock)
        {
            return _globalSymbols.Where(symbol => symbol.ReferencesSymbol(symbolName)).ToList();
        }
    }

    /// <summary>
    /// Deletes all global symbols defined in the referenced document.
    /// </summary>
    /// <param name="documentUri"></param>
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