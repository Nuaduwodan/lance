using LanceServer.Core.Configuration.DataModel;

namespace LanceServer.Core.Configuration;

public interface IConfigurationManager
{
    SymbolTableConfiguration? SymbolTableConfiguration { get; }
    FileEndingConfiguration? FileEndingConfiguration { get; }
    DocumentationConfiguration? DocumentationConfiguration { get; }
    CustomPreprocessorConfiguration? CustomPreprocessorConfiguration { get; }
    void ExtractConfiguration(ConfigurationParameters configurationParameters);
}