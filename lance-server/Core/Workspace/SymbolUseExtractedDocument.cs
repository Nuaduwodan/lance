using Antlr4.Runtime.Tree;
using LanceServer.Core.Symbol;

namespace LanceServer.Core.Workspace;

public class SymbolUseExtractedDocument : SymbolisedDocument
{
    public SymbolUseTable SymbolUseTable { get; }

    public SymbolUseExtractedDocument(IDocumentInformation information, string rawContent, string code, Placeholders placeholders, string macroTable, IParseTree tree, SymbolTable symbolTable, SymbolUseTable symbolUseTable) 
        : base(information, rawContent, code, placeholders, macroTable, tree, symbolTable)
    {
        SymbolUseTable = symbolUseTable;
    }

    public SymbolUseExtractedDocument(SymbolisedDocument symbolisedDocument, SymbolUseTable symbolUseTable) 
        : this(symbolisedDocument.Information, symbolisedDocument.RawContent, symbolisedDocument.Code, symbolisedDocument.Placeholders, symbolisedDocument.MacroTable, symbolisedDocument.Tree, symbolisedDocument.SymbolTable, symbolUseTable)
    {
    }
}