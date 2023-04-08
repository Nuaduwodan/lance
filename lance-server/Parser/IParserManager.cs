using Antlr4.Runtime.Tree;
using LanceServer.Core.SymbolTable;
using LanceServer.Core.Workspace;

namespace LanceServer.Parser;

public interface IParserManager
{
    IParseTree Parse(PreprocessedDocument document);
    List<ISymbol> GetSymbolTableForDocument(ParsedDocument document);
}