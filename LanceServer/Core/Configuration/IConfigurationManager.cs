using LanceServer.Core.Configuration.DataModel;

namespace LanceServer.Core.Configuration;

/// <summary>
/// The configuration manager
/// </summary>
public interface IConfigurationManager
{
    /// <summary>
    /// Returns the <see cref="SymbolTableConfiguration"/>
    /// </summary>
    public SymbolTableConfiguration GetSymbolTableConfiguration();

    /// <summary>
    /// Returns the <see cref="FileExtensionConfiguration"/>
    /// </summary>
    public FileExtensionConfiguration GetFileExtensionConfiguration();

    /// <summary>
    /// Returns the <see cref="DocumentationConfiguration"/>
    /// </summary>
    public DocumentationConfiguration GetDocumentationConfiguration();

    /// <summary>
    /// Returns the <see cref="CustomPreprocessorConfiguration"/>
    /// </summary>
    public CustomPreprocessorConfiguration GetCustomPreprocessorConfiguration();
}