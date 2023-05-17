using Antlr4.Runtime.Tree;
using LanceServer.Core.Symbol;
using LspTypes;

namespace LanceServer.Core.Document;

public class SymbolisedDocument : ParsedDocument
{
    public SymbolTable SymbolTable { get; }

    public SymbolisedDocument(IDocumentInformation information, string rawContent, string code, Placeholders placeholders, string macroTable, IParseTree tree, IList<Diagnostic> parserDiagnostics, SymbolTable symbolTable) 
        : base(information, rawContent, code, placeholders, macroTable, tree, parserDiagnostics)
    {
        SymbolTable = symbolTable;
    }

    public SymbolisedDocument(ParsedDocument parsedDocument, SymbolTable symbolTable) 
        : this(parsedDocument.Information, parsedDocument.RawContent, parsedDocument.Code, parsedDocument.Placeholders, parsedDocument.MacroTable, parsedDocument.Tree, parsedDocument.ParserDiagnostics, symbolTable)
    {
    }
}