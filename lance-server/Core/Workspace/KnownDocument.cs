namespace LanceServer.Core.Workspace;

public class KnownDocument
{
    public Uri Uri { get; }
    public string FileEnding { get; }
    
    public KnownDocument(Uri uri)
    {
        Uri = uri;
        FileEnding = Path.GetExtension(uri.LocalPath);
    }
}