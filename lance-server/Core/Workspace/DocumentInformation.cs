namespace LanceServer.Core.Workspace;

public class DocumentInformation
{
    public Uri Uri { get; }
    public string FileEnding { get; }
    public string RawContent { get; }
    public bool IsGlobalFile { get; }
    public bool IsSubProcedure { get; }
    public bool ProcedureNeedsDeclaration { get; }
    public string Encoding { get; }
    
    public DocumentInformation(Uri uri, string fileEnding, string rawContent, bool isGlobalFile = false, bool isSubProcedure = false, bool procedureNeedsDeclaration = true, string encoding = "utf8")
    {
        Uri = uri;
        FileEnding = fileEnding;
        RawContent = rawContent;
        IsGlobalFile = isGlobalFile;
        IsSubProcedure = isSubProcedure;
        ProcedureNeedsDeclaration = procedureNeedsDeclaration;
        Encoding = encoding;
    }
}