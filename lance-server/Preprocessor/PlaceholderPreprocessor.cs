using System.Text.RegularExpressions;
using LanceServer.Core.Configuration;
using LanceServer.Core.Workspace;

namespace LanceServer.Preprocessor
{
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
            
            if(!preprocessorConfiguration.FileExtensions.Contains(document.Information.FileEnding))
            {
                return new PlaceholderPreprocessedDocument(document, document.Information.RawContent, new Placeholders(placeholders));
            }

            var result = document.Information.RawContent;
            foreach (var placeholder in preprocessorConfiguration.Placeholders)
            {
                var pattern = placeholder;
                if (preprocessorConfiguration.PlaceholderType == PlaceholderType.String)
                {
                    pattern = Regex.Escape(placeholder);
                }
                
                var matches = Regex.Matches(result, pattern).Select(match => match.Value);
                foreach (var match in matches)
                {
                    var processedMatch = Regex.Replace(match, "[^a-zA-Z0-9_ ]", "_");
                    result = Regex.Replace(result, match, processedMatch);
                    placeholders.Add(processedMatch, match);
                }
            }
            
            return new PlaceholderPreprocessedDocument(document, result, new Placeholders(placeholders));
        }
    }
}