namespace LanceServer.Core.Workspace;

public class PlaceholderPreprocessedDocument : Document
{
    public string Code { get; }
    public Placeholders Placeholders { get; }

    public PlaceholderPreprocessedDocument(IDocumentInformation information, string rawContent, string code, Placeholders placeholders) 
        : base(information, rawContent)
    {
        Code = code;
        Placeholders = placeholders;
    }
    
    public PlaceholderPreprocessedDocument(Document document, string code, Placeholders placeholders) 
        : this(document.Information, document.RawContent, code, placeholders)
    {
    }
}