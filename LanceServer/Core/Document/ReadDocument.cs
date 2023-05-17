namespace LanceServer.Core.Document;

/// <summary>
/// Represents a document that has been read
/// </summary>
public class ReadDocument : Document
{
    /// <summary>
    /// The raw content of the file
    /// </summary>
    public string RawContent { get; }
    
    /// <summary>
    /// Creates a new instance of <see cref="ReadDocument"/>
    /// </summary>
    public ReadDocument(IDocumentInformation documentInformation, string rawContent) : base(documentInformation)
    {
        RawContent = rawContent;
    }
    
    public ReadDocument(Document document, string code) 
        : this(document.Information, code)
    {
    }
}