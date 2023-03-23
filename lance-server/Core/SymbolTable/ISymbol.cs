using LspTypes;

namespace LanceServer.Core.SymbolTable;

public interface ISymbol
{
    public string Identifier { get; }
    public Uri SourceDocument { get; }
    public Position Position { get; }
    public string Description { get; }
    public string GetCode();
}