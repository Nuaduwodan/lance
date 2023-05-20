using Antlr4.Runtime.Tree;
using LanceServer.Core.Symbol;
using LspTypes;

namespace LanceServer.Core.Document;

/// <summary>
/// The document with the extracted language tokens and symbol usage
/// </summary>
public class LanguageTokenExtractedDocument : SymbolisedDocument
{
    /// <summary>
    /// The table with all symbol uses
    /// </summary>
    public SymbolUseTable SymbolUseTable { get; }
    
    /// <summary>
    /// The table with all language tokens
    /// </summary>
    public LanguageTokenTable LanguageTokenTable { get; }

    /// <summary>
    /// Instantiates a new instance of <see cref="LanguageTokenExtractedDocument"/>
    /// </summary>
    public LanguageTokenExtractedDocument(IDocumentInformation information, string rawContent, string code, PlaceholderTable placeholderTable, string macroTable, IParseTree parseTree, IList<Diagnostic> parserDiagnostics, SymbolTable symbolTable, SymbolUseTable symbolUseTable, LanguageTokenTable languageTokenTable) 
        : base(information, rawContent, code, placeholderTable, macroTable, parseTree, parserDiagnostics, symbolTable)
    {
        SymbolUseTable = symbolUseTable;
        LanguageTokenTable = languageTokenTable;
    }

    /// <summary>
    /// Instantiates a new instance of <see cref="LanguageTokenExtractedDocument"/>
    /// </summary>
    public LanguageTokenExtractedDocument(SymbolisedDocument symbolisedDocument, SymbolUseTable symbolUseTable, LanguageTokenTable languageTokenTable) 
        : this(symbolisedDocument.Information, symbolisedDocument.RawContent, symbolisedDocument.Code, symbolisedDocument.PlaceholderTable, symbolisedDocument.MacroTable, symbolisedDocument.ParseTree, symbolisedDocument.ParserDiagnostics, symbolisedDocument.SymbolTable, symbolUseTable, languageTokenTable)
    {
    }
}