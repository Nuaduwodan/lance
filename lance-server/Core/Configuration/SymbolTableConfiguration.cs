namespace LanceServer.Core.Configuration;

public class SymbolTableConfiguration
{
    public string[] GlobalFileEndings { get; }
    public string[] GlobalDirectories { get; }

    public SymbolTableConfiguration(string[] globalFileEndings, string[] globalDirectories)
    {
        GlobalFileEndings = globalFileEndings;
        GlobalDirectories = globalDirectories;
    }
}