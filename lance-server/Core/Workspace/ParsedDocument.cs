using Antlr4.Runtime.Tree;

namespace LanceServer.Core.Workspace;

public class ParsedDocument : PreprocessedDocument
{
    public IParseTree Tree { get; }
    
    public ParsedDocument(IParseTree tree, Uri uri, string rawContent, string code, bool isGlobalFile = false, bool isSubProcedure = false, bool procedureNeedsDeclaration = true, string encoding = "utf8") 
        : base(uri, rawContent, code, isGlobalFile, isSubProcedure, procedureNeedsDeclaration, encoding)
    {
        Tree = tree;
    }
    
    public ParsedDocument(PreprocessedDocument preprocessedDocument, IParseTree tree) 
        : this(tree, preprocessedDocument.Uri, preprocessedDocument.RawContent, preprocessedDocument.Code, preprocessedDocument.IsGlobalFile, preprocessedDocument.IsSubProcedure, preprocessedDocument.ProcedureNeedsDeclaration, preprocessedDocument.Encoding)
    {
    }
}