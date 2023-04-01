using System.Text.RegularExpressions;
using LanceServer.Core.Configuration;
using LanceServer.Core.Workspace;

namespace LanceServer.Preprocessor
{
    public class CustomPreprocessor : ICustomPreprocessor
    {
        private IConfigurationManager _configurationManager;

        public CustomPreprocessor(IConfigurationManager configurationManager)
        {
            _configurationManager = configurationManager;
        }
        
        public string Filter(Document document)
        {
            var preprocessorConfiguration = _configurationManager.CustomPreprocessorConfiguration;
            if(!preprocessorConfiguration.FileEndings.Contains(document.FileEnding))
            {
                return document.RawContent;
            }

            var result = document.RawContent;
            foreach (var placeholder in preprocessorConfiguration.Placeholders)
            {
                var pattern = placeholder;
                if (preprocessorConfiguration.KeyType == KeyType.String)
                {
                    pattern = Regex.Escape(placeholder);
                }
                
                var matches = Regex.Matches(result, pattern).Select(match => match.Value);
                foreach (var match in matches)
                {
                    var processedMatch = Regex.Replace(match, "[^a-zA-Z0-9_ ]", "_");
                    result = Regex.Replace(result, match, processedMatch);
                }
            }
            
            return result;
        }
    }
}