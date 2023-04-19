using Antlr4.Runtime.Tree;
using LanceServer.Core.Symbol;

namespace LanceServer.Core.Workspace;

public class SymbolUseExtractedDocument : SymbolisedDocument
{
    public SymbolUseTable SymbolUseTable { get; }

    public SymbolUseExtractedDocument(DocumentInformation information, string code, Placeholders placeholders, string macroTable, IParseTree tree, SymbolTable symbolTable, SymbolUseTable symbolUseTable) 
        : base(information, code, placeholders, macroTable, tree, symbolTable)
    {
        SymbolUseTable = symbolUseTable;
    }

    public SymbolUseExtractedDocument(SymbolisedDocument symbolisedDocument, SymbolUseTable symbolUseTable) 
        : this(symbolisedDocument.Information, symbolisedDocument.Code, symbolisedDocument.Placeholders, symbolisedDocument.MacroTable, symbolisedDocument.Tree, symbolisedDocument.SymbolTable, symbolUseTable)
    {
    }
}