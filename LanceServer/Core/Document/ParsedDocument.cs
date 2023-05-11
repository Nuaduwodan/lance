using Antlr4.Runtime.Tree;
using LspTypes;

namespace LanceServer.Core.Document;

public class ParsedDocument : PreprocessedDocument
{
    public IParseTree Tree { get; }
    public IList<Diagnostic> Diagnostics { get; }
    
    public ParsedDocument(IDocumentInformation information, string rawContent, string code, Placeholders placeholders, string macroTable, IParseTree tree, IList<Diagnostic> diagnostics) 
        : base(information, rawContent, code, placeholders, macroTable)
    {
        Tree = tree;
        Diagnostics = diagnostics;
    }
    
    public ParsedDocument(PreprocessedDocument preprocessedDocument, IParseTree tree, IList<Diagnostic> diagnostics) 
        : this(preprocessedDocument.Information, preprocessedDocument.RawContent, preprocessedDocument.Code, preprocessedDocument.Placeholders, preprocessedDocument.MacroTable, tree, diagnostics)
    {
    }
}