using LanceServer.Core.Document;
using LanceServer.Core.Workspace;

namespace LanceServerTest.Core.Workspace;

public class DocumentInformationMock : IDocumentInformation
{
    public Uri Uri { get; }
    public string FileExtension { get; }
    public DocumentType DocumentType { get; }
    public string Encoding { get; }
    
    public DocumentInformationMock(Uri uri, string fileExtension, DocumentType documentType, string encoding = "utf8")
    {
        Uri = uri;
        FileExtension = fileExtension;
        DocumentType = documentType;
        Encoding = encoding;
    }
}