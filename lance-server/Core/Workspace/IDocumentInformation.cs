namespace LanceServer.Core.Workspace;

public interface IDocumentInformation
{
    Uri Uri { get; }
    string FileExtension { get; }
    DocumentType DocumentType { get; }
    string Encoding { get; }
}