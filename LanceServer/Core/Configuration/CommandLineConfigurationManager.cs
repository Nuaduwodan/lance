using LanceServer.Core.Configuration.DataModel;
using Microsoft.Extensions.Configuration;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;

namespace LanceServer.Core.Configuration;

public class CommandLineConfigurationManager : IConfigurationManager
{
    private readonly IConfigurationRoot _configuration;

    public CommandLineConfigurationManager(IConfigurationRoot configuration)
    {
        _configuration = configuration;
    }

    public SymbolTableConfiguration GetSymbolTableConfiguration()
    {
        throw new NotImplementedException();
    }

    public FileExtensionConfiguration GetFileExtensionConfiguration()
    {
        throw new NotImplementedException();
    }

    public DocumentationConfiguration GetDocumentationConfiguration()
    {
        throw new NotImplementedException();
    }

    public CustomPreprocessorConfiguration GetCustomPreprocessorConfiguration()
    {
        throw new NotImplementedException();
    }

    public ClientCapabilities ClientCapabilities { get; set; }
}