namespace LanceServer.Core.Document;

/// <summary>
/// The table with all placeholders
/// </summary>
public class PlaceholderTable
{
    /// <summary>
    /// The placeholders in a &lt;replacement token, original placeholder&gt; dictionary.
    /// </summary>
    private readonly IDictionary<string, string> _placeholders;

    /// <summary>
    /// Instantiates a new <see cref="PlaceholderTable"/>
    /// </summary>
    public PlaceholderTable(IDictionary<string, string> placeholders)
    {
        _placeholders = placeholders;
    }
    
    /// <summary>
    /// Checks if some text contained a placeholder
    /// </summary>
    public bool ContainedPlaceholder(string text)
    {
        return _placeholders.Any(placeholder => text.Contains(placeholder.Key));
    }
    
    /// <summary>
    /// Replaces the replacement token in a text with the original placeholder
    /// </summary>
    /// <returns>The text with the original placeholder</returns>
    public string ReplacePlaceholder(string text)
    {
        foreach (var kvp in _placeholders)
        {
            text = text.Replace(kvp.Key, kvp.Value);
        }

        return text;
    }
}