namespace LanceServer.Core.Document;

public class PlaceholderPreprocessedDocument : ReadDocument
{
    public string Code { get; }
    public Placeholders Placeholders { get; }

    public PlaceholderPreprocessedDocument(IDocumentInformation information, string rawContent, string code, Placeholders placeholders) 
        : base(information, rawContent)
    {
        Code = code;
        Placeholders = placeholders;
    }
    
    public PlaceholderPreprocessedDocument(ReadDocument readDocument, string code, Placeholders placeholders) 
        : this(readDocument.Information, readDocument.RawContent, code, placeholders)
    {
    }
}