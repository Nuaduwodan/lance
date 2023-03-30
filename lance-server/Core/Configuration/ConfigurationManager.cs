using LspTypes;

namespace LanceServer.Core.Configuration;

public class ConfigurationManager
{
    private readonly IConfigurationServer _server;
    public SymbolTableConfiguration? SymbolTableConfiguration { get; private set; }
    public FileEndingConfiguration? FileEndingConfiguration { get; private set; }
    public DocumentationConfiguration? DocumentationConfiguration { get; private set; }

    public ConfigurationManager(IConfigurationServer server)
    {
        _server = server;
    }

    public Task InitConfigurationAsync()
    {
        var requestParameters = new ConfigurationParams()
        {
            Items = new []{new ConfigurationItem{Section = "lanceServer"}}
        };
        
        return _server.GetConfigurationsAsync(requestParameters).ContinueWith(task => ExtractConfiguration(task.Result), TaskScheduler.Default);
    }

    public void ExtractConfiguration(ConfigurationResult configurationResult)
    {
        // Todo extract Configuration from ConfigurationResult
        SymbolTableConfiguration = new SymbolTableConfiguration(new []{".def"},new []{"cma.dir"});
        FileEndingConfiguration = new FileEndingConfiguration(new []{".def", ".spf", ".mpf"});
    }
}