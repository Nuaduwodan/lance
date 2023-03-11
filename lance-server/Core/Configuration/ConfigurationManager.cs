namespace LanceServer.Core.Configuration;

public class ConfigurationManager
{
    private readonly object _initializationOptions;
    public ConfigurationManager(object initializationOptions)
    {
        _initializationOptions = initializationOptions;
    }

    public SymbolTableConfiguration GetSymbolTableConfiguration()
    {
        // Todo read config from _initializationOptions
        return new SymbolTableConfiguration(new []{"def"},new []{"cma.dir"});
    }

    public DocumentationConfiguration GetDocumentationConfiguration()
    {
        return new DocumentationConfiguration();
    }
}