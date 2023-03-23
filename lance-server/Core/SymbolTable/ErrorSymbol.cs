using System.Data.SqlTypes;
using LspTypes;

namespace LanceServer.Core.SymbolTable;

public class ErrorSymbol : ISymbol
{
    public string Identifier { get; } = string.Empty;
    public Uri SourceDocument { get; } = null;
    public Position Position { get; } = null;
    public string Description { get; }
    
    public ErrorSymbol(string description)
    {
        Description = description;
    }
    public string GetCode()
    {
        return string.Empty;
    }
}