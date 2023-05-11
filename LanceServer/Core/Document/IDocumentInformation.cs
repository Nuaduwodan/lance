namespace LanceServer.Core.Document;

/// <summary>
/// Represents some general information about the location and its type
/// </summary>
public interface IDocumentInformation
{
    /// <summary>
    /// The uri of the document
    /// </summary>
    Uri Uri { get; }
    
    /// <summary>
    /// The file extension of the document
    /// </summary>
    string FileExtension { get; }
    
    /// <summary>
    /// The <see cref="DocumentType"/> of the document
    /// </summary>
    DocumentType DocumentType { get; }
    
    /// <summary>
    /// The encoding of the document
    /// </summary>
    string Encoding { get; }
}