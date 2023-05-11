namespace LanceServer.Core.Configuration.DataModel;

public class FileExtensionConfiguration
{
    public string[] FileExtensions { get; }

    public FileExtensionConfiguration(string[] fileExtensions)
    {
        FileExtensions = fileExtensions;
    }
}