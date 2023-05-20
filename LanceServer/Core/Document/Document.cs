namespace LanceServer.Core.Document;

/// <summary>
/// A document with just the information about its location and type
/// </summary>
public class Document
{
    /// <summary>
    /// Some general Document information
    /// </summary>
    public IDocumentInformation Information { get; }
    
    /// <summary>
    /// Instantiates a new instance of <see cref="Document"/>
    /// </summary>
    public Document(IDocumentInformation documentInformation)
    {
        Information = documentInformation;
    }
}