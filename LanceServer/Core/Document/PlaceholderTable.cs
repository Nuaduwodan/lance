namespace LanceServer.Core.Document;

public class PlaceholderTable
{
    private IDictionary<string, string> _placeholders;

    public PlaceholderTable(IDictionary<string, string> placeholders)
    {
        _placeholders = placeholders;
    }
    
    public bool ContainsPlaceholder(string text)
    {
        return _placeholders.Any(placeholder => text.Contains(placeholder.Key));
    }
    
    public string ReplacePlaceholder(string placeholder)
    {
        foreach (var kvp in _placeholders)
        {
            placeholder = placeholder.Replace(kvp.Key, kvp.Value);
        }

        return placeholder;
    }
}