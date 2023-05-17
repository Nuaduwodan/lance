namespace LanceServer.Core.Document;

/// <summary>
/// A document with processed placeholders if applicable.
/// </summary>
public class PlaceholderPreprocessedDocument : ReadDocument
{
    /// <summary>
    /// The code with the processed placeholders
    /// </summary>
    public string Code { get; }
    
    /// <summary>
    /// The table with all placeholder processed in this document
    /// </summary>
    public PlaceholderTable PlaceholderTable { get; }

    /// <summary>
    /// Creates a new instance of <see cref="PlaceholderPreprocessedDocument"/>
    /// </summary>
    public PlaceholderPreprocessedDocument(IDocumentInformation information, string rawContent, string code, PlaceholderTable placeholderTable) 
        : base(information, rawContent)
    {
        Code = code;
        PlaceholderTable = placeholderTable;
    }
    
    /// <summary>
    /// Creates a new instance of <see cref="PlaceholderPreprocessedDocument"/>
    /// </summary>
    public PlaceholderPreprocessedDocument(ReadDocument readDocument, string code, PlaceholderTable placeholderTable) 
        : this(readDocument.Information, readDocument.RawContent, code, placeholderTable)
    {
    }
}