namespace LanceServer.Core.Workspace;

public class ReadDocument : KnownDocument
{
    public string RawContent { get; }
    public bool IsGlobalFile { get; }
    public bool IsSubProcedure { get; }
    public bool ProcedureNeedsDeclaration { get; }
    public string Encoding { get; }
    
    public ReadDocument(Uri uri, string rawContent, bool isGlobalFile = false, bool isSubProcedure = false, bool procedureNeedsDeclaration = true, string encoding = "utf8") : base(uri)
    {
        RawContent = rawContent;
        IsGlobalFile = isGlobalFile;
        IsSubProcedure = isSubProcedure;
        ProcedureNeedsDeclaration = procedureNeedsDeclaration;
        Encoding = encoding;
    }

    public ReadDocument(KnownDocument knownDocument, string rawContent, bool isGlobalFile = false, bool isSubProcedure = false, bool procedureNeedsDeclaration = true, string encoding = "utf8") :
        this(knownDocument.Uri, rawContent, isGlobalFile, isSubProcedure, procedureNeedsDeclaration, encoding)
    {
    }
}