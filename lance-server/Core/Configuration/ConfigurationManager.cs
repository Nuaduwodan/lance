using LanceServer.Core.Configuration.DataModel;
using LanceServer.Core.Workspace;
using LspTypes;

namespace LanceServer.Core.Configuration;

public class ConfigurationManager : IConfigurationManager
{
    public SymbolTableConfiguration? SymbolTableConfiguration { get; private set; }
    public FileExtensionConfiguration? FileExtensionConfiguration { get; private set; }
    public DocumentationConfiguration? DocumentationConfiguration { get; private set; }
    public CustomPreprocessorConfiguration? CustomPreprocessorConfiguration { get; private set; }
    public Uri[] WorkspaceFolders { get; private set; } = Array.Empty<Uri>();

    public void ExtractConfiguration(ServerConfiguration configuration)
    {
        SymbolTableConfiguration = configuration.SymbolTableConfiguration;
        CustomPreprocessorConfiguration = configuration.PlaceholderPreprocessor;
        
        var fileExtensions = SymbolTableConfiguration.DefinitionFileExtensions.ToList();
        fileExtensions.AddRange(SymbolTableConfiguration.SubProcedureFileExtensions);
        fileExtensions.AddRange(SymbolTableConfiguration.MainProcedureFileExtensions);
        FileExtensionConfiguration = new FileExtensionConfiguration(fileExtensions.ToArray());
    }

    public void Initialize(InitializeParams initializeParams)
    {
        WorkspaceFolders = initializeParams.WorkspaceFolders.Select(folder => FileUtil.UriStringToUri(folder.Uri)).ToArray();
    }
}