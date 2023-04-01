using Antlr4.Runtime.Tree;
using LanceServer.Core.SymbolTable;
using LanceServer.Core.Workspace;

namespace LanceServer.Parser;

public interface IParserManager
{
    IParseTree Parse(Document document);
    List<ISymbol> GetSymbolTableForDocument(Document document);
}