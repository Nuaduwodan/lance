namespace LanceServer.Core.Workspace;

public class Placeholders
{
    private Dictionary<string, string> _placeholders;

    public Placeholders(Dictionary<string, string> placeholders)
    {
        _placeholders = placeholders;
    }
    
    public bool IsPlaceholder(string placeholder)
    {
        return _placeholders.ContainsKey(placeholder);
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