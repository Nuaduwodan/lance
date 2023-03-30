namespace LanceServer.Core.Configuration;

public class FileEndingConfiguration
{
    public string[] FileEndings { get; }

    public FileEndingConfiguration(string[] fileEndings)
    {
        FileEndings = fileEndings;
    }
}