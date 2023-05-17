using Antlr4.Runtime.Tree;
using LanceServer.Core.Symbol;
using LspTypes;

namespace LanceServer.Core.Document;

/// <summary>
/// A document with the in it defined symbols
/// </summary>
public class SymbolisedDocument : ParsedDocument
{
    /// <summary>
    /// The table with the local defined symbols
    /// </summary>
    public SymbolTable SymbolTable { get; }

    /// <summary>
    /// Instantiates a new <see cref="SymbolisedDocument"/>
    /// </summary>
    public SymbolisedDocument(IDocumentInformation information, string rawContent, string code, PlaceholderTable placeholderTable, string macroTable, IParseTree parseTree, IList<Diagnostic> parserDiagnostics, SymbolTable symbolTable) 
        : base(information, rawContent, code, placeholderTable, macroTable, parseTree, parserDiagnostics)
    {
        SymbolTable = symbolTable;
    }

    /// <summary>
    /// Instantiates a new <see cref="SymbolisedDocument"/>
    /// </summary>
    public SymbolisedDocument(ParsedDocument parsedDocument, SymbolTable symbolTable) 
        : this(parsedDocument.Information, parsedDocument.RawContent, parsedDocument.Code, parsedDocument.PlaceholderTable, parsedDocument.MacroTable, parsedDocument.ParseTree, parsedDocument.ParserDiagnostics, symbolTable)
    {
    }
}