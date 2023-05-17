using System.Text.RegularExpressions;
using LanceServer.Core.Configuration;
using LanceServer.Core.Document;

namespace LanceServer.Preprocessor;

public class PlaceholderPreprocessor : IPlaceholderPreprocessor
{
    private IConfigurationManager _configurationManager;

    public PlaceholderPreprocessor(IConfigurationManager configurationManager)
    {
        _configurationManager = configurationManager;
    }
        
    public PlaceholderPreprocessedDocument Filter(ReadDocument document)
    {
        var preprocessorConfiguration = _configurationManager.CustomPreprocessorConfiguration;

        var placeholders = new Dictionary<string, string>();
            
        if (!preprocessorConfiguration.FileExtensions.Contains(document.Information.FileExtension))
        {
            return new PlaceholderPreprocessedDocument(document, document.RawContent, new PlaceholderTable(placeholders));
        }

        var result = document.RawContent;
        foreach (var placeholder in preprocessorConfiguration.Placeholders)
        {
            var pattern = placeholder;
            if (preprocessorConfiguration.PlaceholderType == PlaceholderType.String)
            {
                pattern = Regex.Escape(placeholder);
            }
 
            var matches = Regex.Matches(result, pattern, RegexOptions.Multiline).Select(match => match.Value);
            foreach (var match in matches)
            {
                if (IsAloneOnLine(result, match))
                {
                    result = Regex.Replace(result, match, "");
                }
                else
                {
                    var processedMatch = Regex.Replace(match, "[^a-zA-Z0-9_]", "_");
                    result = Regex.Replace(result, match, processedMatch);
                    placeholders.TryAdd(processedMatch, match);
                }
            }
        }

        placeholders = placeholders.OrderByDescending(pair => pair.Key.Length).ToDictionary(pair => pair.Key, pair => pair.Value);
            
        return new PlaceholderPreprocessedDocument(document, result, new PlaceholderTable(placeholders));
    }

    private bool IsAloneOnLine(string text, string match)
    {
        return Regex.Match(text, "^\\s*" + match + "\\s*$", RegexOptions.Multiline).Value.Trim() == match;
    }
}