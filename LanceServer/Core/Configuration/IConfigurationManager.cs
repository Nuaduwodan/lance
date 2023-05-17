using LanceServer.Core.Configuration.DataModel;
using LspTypes;

namespace LanceServer.Core.Configuration;

/// <summary>
/// The configuration manager
/// </summary>
public interface IConfigurationManager
{
    /// <summary>
    /// Returns the <see cref="SymbolTableConfiguration"/>
    /// </summary>
    SymbolTableConfiguration SymbolTableConfiguration { get; }
    
    /// <summary>
    /// Returns the <see cref="FileExtensionConfiguration"/>
    /// </summary>
    FileExtensionConfiguration FileExtensionConfiguration { get; }
    
    /// <summary>
    /// Returns the <see cref="DocumentationConfiguration"/>
    /// </summary>
    DocumentationConfiguration DocumentationConfiguration { get; }
    
    /// <summary>
    /// Returns the <see cref="CustomPreprocessorConfiguration"/>
    /// </summary>
    CustomPreprocessorConfiguration CustomPreprocessorConfiguration { get; }
    
    /// <summary>
    /// Returns the <see cref="Uri"/>s of the folders belonging to the workspace.
    /// </summary>
    Uri[] WorkspaceFolders { get; }
    
    /// <summary>
    /// Returns the capabilities of the client defined by the language server protocol client implementation
    /// </summary>
    ClientCapabilities ClientCapabilities { get; set; }
    
    /// <summary>
    /// Extracts the different settings out of the <see cref="ServerConfiguration"/> object.
    /// </summary>
    public void ExtractConfiguration(ServerConfiguration configuration);
    
    /// <summary>
    /// Sets the folders belonging to this workspace.
    /// </summary>
    public void SetWorkspaceFolders(WorkspaceFolder[]? workspaceFolders);
}