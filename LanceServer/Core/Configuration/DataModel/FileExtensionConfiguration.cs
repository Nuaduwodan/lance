namespace LanceServer.Core.Configuration.DataModel;

/// <summary>
/// The configuration about which file extensions are used
/// </summary>
public class FileExtensionConfiguration
{
    public string[] FileExtensions { get; }

    public FileExtensionConfiguration(string[] fileExtensions)
    {
        FileExtensions = fileExtensions;
    }
}