using LanceServer.Core.Configuration.DataModel;
using LanceServer.Core.Workspace;
using LspTypes;

namespace LanceServer.Core.Configuration;

public class ConfigurationManager : IConfigurationManager
{
    public SymbolTableConfiguration SymbolTableConfiguration { get; private set; } = new();
    public FileExtensionConfiguration FileExtensionConfiguration { get; private set; } = new(Array.Empty<string>());
    public DocumentationConfiguration DocumentationConfiguration { get; private set; } = new();
    public CustomPreprocessorConfiguration CustomPreprocessorConfiguration { get; private set; } = new();
    public Uri[] WorkspaceFolders { get; private set; } = Array.Empty<Uri>();

    public void ExtractConfiguration(ServerConfiguration configuration)
    {
        SymbolTableConfiguration.DefinitionFileExtensions = configuration.SymbolTableConfiguration.DefinitionFileExtensions.Select(str => str.ToLower()).ToArray();
        SymbolTableConfiguration.MainProcedureFileExtensions = configuration.SymbolTableConfiguration.MainProcedureFileExtensions.Select(str => str.ToLower()).ToArray();
        SymbolTableConfiguration.SubProcedureFileExtensions = configuration.SymbolTableConfiguration.SubProcedureFileExtensions.Select(str => str.ToLower()).ToArray();
        SymbolTableConfiguration.ManufacturerCyclesDirectories = configuration.SymbolTableConfiguration.ManufacturerCyclesDirectories.Select(str => str.ToLower()).ToArray();
        
        CustomPreprocessorConfiguration.FileExtensions = configuration.PlaceholderPreprocessor.FileExtensions.Select(str => str.ToLower()).ToArray();
        CustomPreprocessorConfiguration.Placeholders = configuration.PlaceholderPreprocessor.Placeholders;
        CustomPreprocessorConfiguration.PlaceholderType = configuration.PlaceholderPreprocessor.PlaceholderType;
        
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