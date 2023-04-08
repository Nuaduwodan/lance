using System.Data.SqlTypes;
using LspTypes;

namespace LanceServer.Core.SymbolTable;

public class ErrorSymbol : ISymbol
{
    public string Identifier { get; } = string.Empty;
    public Uri SourceDocument { get; } = null;
    public Position Position { get; } = null;
    public string Description { get; }
    public string Code => string.Empty;
    public string Documentation => string.Empty;

    public ErrorSymbol(string description)
    {
        Description = description;
    }
}