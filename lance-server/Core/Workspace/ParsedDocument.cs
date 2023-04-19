using Antlr4.Runtime.Tree;

namespace LanceServer.Core.Workspace;

public class ParsedDocument : PreprocessedDocument
{
    public IParseTree Tree { get; }
    
    public ParsedDocument(DocumentInformation information, string code, Placeholders placeholders, string macroTable, IParseTree tree) 
        : base(information, code, placeholders, macroTable)
    {
        Tree = tree;
    }
    
    public ParsedDocument(PreprocessedDocument preprocessedDocument, IParseTree tree) 
        : this(preprocessedDocument.Information, preprocessedDocument.Code, preprocessedDocument.Placeholders, preprocessedDocument.MacroTable, tree)
    {
    }
}