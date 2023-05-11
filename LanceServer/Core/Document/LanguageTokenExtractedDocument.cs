using Antlr4.Runtime.Tree;
using LanceServer.Core.Symbol;
using LspTypes;

namespace LanceServer.Core.Document;

public class LanguageTokenExtractedDocument : SymbolisedDocument
{
    public SymbolUseTable SymbolUseTable { get; }
    public LanguageTokenTable LanguageTokenTable { get; }

    public LanguageTokenExtractedDocument(IDocumentInformation information, string rawContent, string code, Placeholders placeholders, string macroTable, IParseTree tree, IList<Diagnostic> diagnostics, SymbolTable symbolTable, SymbolUseTable symbolUseTable, LanguageTokenTable languageTokenTable) 
        : base(information, rawContent, code, placeholders, macroTable, tree, diagnostics, symbolTable)
    {
        SymbolUseTable = symbolUseTable;
        LanguageTokenTable = languageTokenTable;
    }

    public LanguageTokenExtractedDocument(SymbolisedDocument symbolisedDocument, SymbolUseTable symbolUseTable, LanguageTokenTable languageTokenTable) 
        : this(symbolisedDocument.Information, symbolisedDocument.RawContent, symbolisedDocument.Code, symbolisedDocument.Placeholders, symbolisedDocument.MacroTable, symbolisedDocument.Tree, symbolisedDocument.Diagnostics, symbolisedDocument.SymbolTable, symbolUseTable, languageTokenTable)
    {
    }
}