namespace LanceServer.Core.Workspace;

public class Document
{
    public IDocumentInformation Information { get; }
    public string RawContent { get; }
    
    public Document(IDocumentInformation documentInformation, string rawContent)
    {
        Information = documentInformation;
        RawContent = rawContent;
    }
}