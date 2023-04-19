using LanceServer.Core.Configuration.DataModel;
using LanceServer.Core.Workspace;
using LspTypes;

namespace LanceServer.Core.Configuration;

public class ConfigurationManager : IConfigurationManager
{
    public SymbolTableConfiguration? SymbolTableConfiguration { get; private set; }
    public FileEndingConfiguration? FileEndingConfiguration { get; private set; }
    public DocumentationConfiguration? DocumentationConfiguration { get; private set; }
    public CustomPreprocessorConfiguration? CustomPreprocessorConfiguration { get; private set; }
    public Uri[] WorkspaceFolders { get; private set; } = Array.Empty<Uri>();

    public void ExtractConfiguration(ConfigurationParameters configurationParameters)
    {
        SymbolTableConfiguration = configurationParameters.Settings.Lance.Customization.SymbolTableConfiguration;
        CustomPreprocessorConfiguration = configurationParameters.Settings.Lance.Customization.PlaceholderPreprocessor;
        FileEndingConfiguration = new FileEndingConfiguration(new []{".def", ".spf", ".mpf"});
    }

    public void Initialize(InitializeParams initializeParams)
    {
        WorkspaceFolders = initializeParams.WorkspaceFolders.Select(folder => FileUtil.UriStringToUri(folder.Uri)).ToArray();
    }
}