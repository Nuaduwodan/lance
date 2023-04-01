namespace LanceServer.Core.Configuration.DataModel
{
    public class FileEndingConfiguration
    {
        public string[] FileEndings { get; }

        public FileEndingConfiguration(string[] fileEndings)
        {
            FileEndings = fileEndings;
        }
    }
}