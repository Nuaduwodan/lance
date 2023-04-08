namespace LanceServer.Core.Workspace;

public class PreprocessedDocument : ReadDocument
{
    public string Code { get; }
    
    public PreprocessedDocument(Uri uri, string rawContent, string code, bool isGlobalFile = false, bool isSubProcedure = false, bool procedureNeedsDeclaration = true, string encoding = "utf8") 
        : base(uri, rawContent, isGlobalFile, isSubProcedure, procedureNeedsDeclaration, encoding)
    {
        Code = code;
    }
    
    public PreprocessedDocument(ReadDocument readDocument, string code) 
        : this(readDocument.Uri, readDocument.RawContent, code, readDocument.IsGlobalFile, readDocument.IsSubProcedure, readDocument.ProcedureNeedsDeclaration, readDocument.Encoding)
    {
    }
}