using System.Diagnostics.CodeAnalysis;
using Antlr4.Runtime.Tree;
using LanceServer.Core.Symbol;

namespace LanceServer.Core.Workspace;

public class SymbolisedDocument : ParsedDocument
{
    public SymbolTable SymbolTable { get; }

    public SymbolisedDocument(DocumentInformation information, string code, Placeholders placeholders, string macroTable, IParseTree tree, SymbolTable symbolTable) 
        : base(information, code, placeholders, macroTable, tree)
    {
        SymbolTable = symbolTable;
    }

    public SymbolisedDocument(ParsedDocument parsedDocument, SymbolTable symbolTable) 
        : this(parsedDocument.Information, parsedDocument.Code, parsedDocument.Placeholders, parsedDocument.MacroTable, parsedDocument.Tree, symbolTable)
    {
    }
}