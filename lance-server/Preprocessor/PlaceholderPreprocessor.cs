using System.Text.RegularExpressions;
using LanceServer.Core.Configuration;
using LanceServer.Core.Workspace;

namespace LanceServer.Preprocessor;

public class PlaceholderPreprocessor : IPlaceholderPreprocessor
{
    private IConfigurationManager _configurationManager;

    public PlaceholderPreprocessor(IConfigurationManager configurationManager)
    {
        _configurationManager = configurationManager;
    }
        
    public PlaceholderPreprocessedDocument Filter(Document document)
    {
        var preprocessorConfiguration = _configurationManager.CustomPreprocessorConfiguration;

        var placeholders = new Dictionary<string, string>();
            
        if(!preprocessorConfiguration.FileExtensions.Contains(document.Information.FileExtension))
        {
            return new PlaceholderPreprocessedDocument(document, document.RawContent, new Placeholders(placeholders));
        }

        var result = document.RawContent;
        foreach (var placeholder in preprocessorConfiguration.Placeholders)
        {
            var pattern = placeholder;
            if (preprocessorConfiguration.PlaceholderType == PlaceholderType.String)
            {
                pattern = Regex.Escape(placeholder);
            }

            pattern = "^(.*)("+pattern+")(.*)$";    
            var matches = Regex.Matches(result, pattern, RegexOptions.Multiline).Select(match => match);
            foreach (var match in matches)
            {
                var value = match.Groups[2].Value.Substring(1);
                if (IsAloneOnLine(match))
                {
                    result = Regex.Replace(result, value, "");
                }
                else
                {
                    var processedMatch = Regex.Replace(value, "[^a-zA-Z0-9_]", "_");
                    result = Regex.Replace(result, value, processedMatch);
                    placeholders.TryAdd(processedMatch, value);
                }
            }
        }

        placeholders = placeholders.OrderByDescending(pair => pair.Key.Length).ToDictionary(pair => pair.Key, pair => pair.Value);
            
        return new PlaceholderPreprocessedDocument(document, result, new Placeholders(placeholders));
    }

    private bool IsAloneOnLine(Match match)
    {
        return String.IsNullOrEmpty(match.Groups[1].Value.Trim()) && String.IsNullOrEmpty(match.Groups[^2].Value.Trim());
    }
}