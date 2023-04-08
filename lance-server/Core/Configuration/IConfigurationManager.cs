using LanceServer.Core.Configuration.DataModel;
using LspTypes;

namespace LanceServer.Core.Configuration;

public interface IConfigurationManager
{
    SymbolTableConfiguration? SymbolTableConfiguration { get; }
    FileEndingConfiguration? FileEndingConfiguration { get; }
    DocumentationConfiguration? DocumentationConfiguration { get; }
    CustomPreprocessorConfiguration? CustomPreprocessorConfiguration { get; }
    Uri[] WorkspaceFolders { get; }
    void ExtractConfiguration(ConfigurationParameters configurationParameters);
    void Initialize(InitializeParams initializeParams);
}