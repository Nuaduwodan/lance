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

    public bool IsUpdated { get; private set; } = false;
    
    public void ExtractConfiguration(ServerConfiguration configuration)
    {
        SymbolTableConfiguration = configuration.Customization.SymbolTableConfiguration;
        CustomPreprocessorConfiguration = configuration.Customization.PlaceholderPreprocessor;
        
        var fileExtensions = SymbolTableConfiguration.GlobalFileExtensions.ToList();
        fileExtensions.AddRange(SymbolTableConfiguration.SubProcedureFileExtensions);
        fileExtensions.Add(".mpf");
        FileExtensionConfiguration = new FileExtensionConfiguration(fileExtensions.ToArray());
        
        IsUpdated = true;
    }

    public void Initialize(InitializeParams initializeParams)
    {
        WorkspaceFolders = initializeParams.WorkspaceFolders.Select(folder => FileUtil.UriStringToUri(folder.Uri)).ToArray();
    }
}