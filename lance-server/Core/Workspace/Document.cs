namespace LanceServer.Core.Workspace;

public class Document
{
    public DocumentInformation Information { get; }
    
    public Document(DocumentInformation documentInformation)
    {
        Information = documentInformation;
    }
}