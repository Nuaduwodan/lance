using Antlr4.Runtime.Tree;
using LanceServer.Core.Symbol;

namespace LanceServer.Core.Document;

public class SymbolisedDocument : ParsedDocument
{
    public SymbolTable SymbolTable { get; }

    public SymbolisedDocument(IDocumentInformation information, string rawContent, string code, Placeholders placeholders, string macroTable, IParseTree tree, SymbolTable symbolTable) 
        : base(information, rawContent, code, placeholders, macroTable, tree)
    {
        SymbolTable = symbolTable;
    }

    public SymbolisedDocument(ParsedDocument parsedDocument, SymbolTable symbolTable) 
        : this(parsedDocument.Information, parsedDocument.RawContent, parsedDocument.Code, parsedDocument.Placeholders, parsedDocument.MacroTable, parsedDocument.Tree, symbolTable)
    {
    }
}