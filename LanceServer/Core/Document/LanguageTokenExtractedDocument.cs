using Antlr4.Runtime.Tree;
using LanceServer.Core.Symbol;
using LspTypes;

namespace LanceServer.Core.Document;

/// <summary>
/// The document with 
/// </summary>
public class LanguageTokenExtractedDocument : SymbolisedDocument
{
    public SymbolUseTable SymbolUseTable { get; }
    public LanguageTokenTable LanguageTokenTable { get; }

    public LanguageTokenExtractedDocument(IDocumentInformation information, string rawContent, string code, PlaceholderTable placeholderTable, string macroTable, IParseTree parseTree, IList<Diagnostic> parserDiagnostics, SymbolTable symbolTable, SymbolUseTable symbolUseTable, LanguageTokenTable languageTokenTable) 
        : base(information, rawContent, code, placeholderTable, macroTable, parseTree, parserDiagnostics, symbolTable)
    {
        SymbolUseTable = symbolUseTable;
        LanguageTokenTable = languageTokenTable;
    }

    public LanguageTokenExtractedDocument(SymbolisedDocument symbolisedDocument, SymbolUseTable symbolUseTable, LanguageTokenTable languageTokenTable) 
        : this(symbolisedDocument.Information, symbolisedDocument.RawContent, symbolisedDocument.Code, symbolisedDocument.PlaceholderTable, symbolisedDocument.MacroTable, symbolisedDocument.ParseTree, symbolisedDocument.ParserDiagnostics, symbolisedDocument.SymbolTable, symbolUseTable, languageTokenTable)
    {
    }
}