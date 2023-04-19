namespace LanceServer.Core.Workspace;

public class PlaceholderPreprocessedDocument : Document
{
    public string Code { get; }
    public Placeholders Placeholders { get; }

    public PlaceholderPreprocessedDocument(DocumentInformation information, string code, Placeholders placeholders) 
        : base(information)
    {
        Code = code;
        Placeholders = placeholders;
    }
    
    public PlaceholderPreprocessedDocument(Document document, string code, Placeholders placeholders) 
        : this(document.Information, code, placeholders)
    {
    }
}