using System.Text;
using System.Text.RegularExpressions;
using LanceServer.Core.Configuration;
using LanceServer.Core.Document;

namespace LanceServer.Preprocessor;

public class PlaceholderPreprocessor : IPlaceholderPreprocessor
{
    private readonly IConfigurationManager _configurationManager;

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

        var resultBuilder = new StringBuilder();
        
        var rawLines = Regex.Split(document.RawContent, @"(?<=[\n])");
        foreach (var placeholder in preprocessorConfiguration.Placeholders)
        {
            var pattern = placeholder;
            if (preprocessorConfiguration.PlaceholderType == PlaceholderType.String)
            {
                pattern = Regex.Escape(placeholder);
            }

            foreach (var rawLine in rawLines)
            {
                var matches = Regex.Matches(rawLine, pattern).Select(match => match.Value);
                var line = rawLine;
                foreach (var match in matches)
                {
                    if (rawLine.Trim() == match)
                    {
                        line = Regex.Replace(line, match, "");
                    }
                    else
                    {
                        var processedMatch = Regex.Replace(match, "[^a-zA-Z0-9_]", "_");
                        line = Regex.Replace(line, match, processedMatch);
                        placeholders.TryAdd(processedMatch, match);
                    }
                }

                resultBuilder.Append(line);
            }
        }

        placeholders = placeholders.OrderByDescending(pair => pair.Key.Length).ToDictionary(pair => pair.Key, pair => pair.Value);
            
        return new PlaceholderPreprocessedDocument(document, resultBuilder.ToString(), new PlaceholderTable(placeholders));
    }
}