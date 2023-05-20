using LanceServer.Core.Configuration.DataModel;
using LanceServer.Core.Workspace;
using LspTypes;

namespace LanceServer.Core.Configuration;

/// <inheritdoc />
public class ConfigurationManager : IConfigurationManager
{
    /// <inheritdoc />
    public SymbolTableConfiguration SymbolTableConfiguration { get; } = new();
    
    /// <inheritdoc />
    public FileExtensionConfiguration FileExtensionConfiguration { get; private set; } = new(Array.Empty<string>());
    
    /// <inheritdoc />
    public DocumentationConfiguration DocumentationConfiguration { get; }
    
    /// <inheritdoc />
    public CustomPreprocessorConfiguration CustomPreprocessorConfiguration { get; } = new();
    
    /// <inheritdoc />
    public Uri[] WorkspaceFolders { get; private set; } = Array.Empty<Uri>();
    
    /// <inheritdoc />
    public ClientCapabilities ClientCapabilities { get; set; } = new();

    /// <summary>
    /// Instantiates a new <see cref="ConfigurationManager"/>
    /// This constructor is used for the <see cref="LSPServer"/>
    /// </summary>
    public ConfigurationManager(DocumentationConfiguration documentationConfiguration)
    {
        DocumentationConfiguration = documentationConfiguration;
    }
    
    /// <summary>
    /// Instantiates a new <see cref="ConfigurationManager"/>
    /// This constructor is used for the <see cref="CommandLine"/>
    /// </summary>
    public ConfigurationManager(DocumentationConfiguration documentationConfiguration, Uri[] uris, ServerConfiguration serverConfiguration) : this(documentationConfiguration)
    {
        ExtractConfiguration(serverConfiguration);
        WorkspaceFolders = uris;
    }

    /// <inheritdoc />
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

    /// <inheritdoc />
    public void SetWorkspaceFolders(WorkspaceFolder[]? workspaceFolders)
    {
        workspaceFolders ??= Array.Empty<WorkspaceFolder>();
        WorkspaceFolders = workspaceFolders.Select(folder => FileUtil.UriStringToUri(folder.Uri)).ToArray();
    }
}