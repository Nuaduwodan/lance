using Antlr4.Runtime.Tree;
using LspTypes;

namespace LanceServer.Core.Document;

/// <summary>
/// A parsed document with a parse tree
/// </summary>
public class ParsedDocument : PreprocessedDocument
{
    /// <summary>
    /// The parse tree
    /// </summary>
    public IParseTree ParseTree { get; }
    
    /// <summary>
    /// The diagnostics generated while parsing the code
    /// </summary>
    public IList<Diagnostic> ParserDiagnostics { get; }
    
    /// <summary>
    /// Instantiates a new <see cref="ParsedDocument"/>
    /// </summary>
    public ParsedDocument(IDocumentInformation information, string rawContent, string code, PlaceholderTable placeholderTable, string macroTable, IParseTree parseTree, IList<Diagnostic> parserDiagnostics) 
        : base(information, rawContent, code, placeholderTable, macroTable)
    {
        ParseTree = parseTree;
        ParserDiagnostics = parserDiagnostics;
    }
    
    /// <summary>
    /// Instantiates a new <see cref="ParsedDocument"/>
    /// </summary>
    public ParsedDocument(PreprocessedDocument preprocessedDocument, IParseTree parseTree, IList<Diagnostic> diagnostics) 
        : this(preprocessedDocument.Information, preprocessedDocument.RawContent, preprocessedDocument.Code, preprocessedDocument.PlaceholderTable, preprocessedDocument.MacroTable, parseTree, diagnostics)
    {
    }
}