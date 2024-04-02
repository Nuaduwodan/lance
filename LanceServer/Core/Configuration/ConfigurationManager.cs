using System.Reflection;
using LanceServer.Core.Configuration.DataModel;
using LanceServer.Core.Workspace;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;

namespace LanceServer.Core.Configuration;

/// <inheritdoc />
public class ConfigurationManager : IConfigurationManager
{
    private DocumentationConfiguration? _documentationConfiguration;
    private readonly ILanguageServerConfiguration _languageServerConfiguration;
    private const string DefinitionFileExtensionsKey = "definitionFileExtensions";
    private const string SubProcedureFileExtensionsKey = "subProcedureFileExtensions";
    private const string MainProcedureFileExtensionsKey = "mainProcedureFileExtensions";
    private const string ManufacturerCyclesDirectoriesKey = "manufacturerCyclesDirectories";
    private const string PlaceholderTypeKey = "placeholderType";
    private const string FileExtensionsKey = "fileExtensions";
    private const string PlaceholdersKey = "placeholders";

    public ConfigurationManager(ILanguageServerConfiguration languageServerConfiguration)
    {
        _languageServerConfiguration = languageServerConfiguration;
    }
    
    /// <inheritdoc />
    public SymbolTableConfiguration GetSymbolTableConfiguration()
    { 
        var config = _languageServerConfiguration.GetConfiguration(new ConfigurationItem { Section = "symbols" }).Result;
        return new SymbolTableConfiguration(
            config.GetSection(DefinitionFileExtensionsKey).Get<string[]>(),
            config.GetSection(SubProcedureFileExtensionsKey).Get<string[]>(),
            config.GetSection(MainProcedureFileExtensionsKey).Get<string[]>(),
            config.GetSection(ManufacturerCyclesDirectoriesKey).Get<string[]>());
    }

    /// <inheritdoc />
    public FileExtensionConfiguration GetFileExtensionConfiguration()
    {
        var config = _languageServerConfiguration.GetConfiguration(new ConfigurationItem { Section = "symbols" }).Result;
        return new FileExtensionConfiguration(
            config.GetSection(DefinitionFileExtensionsKey).Get<string[]>().Concat(
            config.GetSection(SubProcedureFileExtensionsKey).Get<string[]>()).Concat(
                config.GetSection(MainProcedureFileExtensionsKey).Get<string[]>()).ToArray());
    }

    /// <inheritdoc />
    public DocumentationConfiguration GetDocumentationConfiguration()
    {
        if (_documentationConfiguration != null)
        {
            return _documentationConfiguration;
        }

        var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var docConfigPath = Path.Join(basePath, "language_token_documentation.json");
        _documentationConfiguration = JsonConvert.DeserializeObject<DocumentationConfiguration>(FileUtil.ReadFileContent(docConfigPath)) 
                                      ?? throw new FileNotFoundException(docConfigPath + " not found");

        return _documentationConfiguration;
    }

    /// <inheritdoc />
    public CustomPreprocessorConfiguration GetCustomPreprocessorConfiguration()
    {
        var config = _languageServerConfiguration.GetConfiguration(new ConfigurationItem { Section = "placeholderPreprocessor" }).Result;
        return new CustomPreprocessorConfiguration(
            config.GetSection(PlaceholderTypeKey).Get<PlaceholderType>(), 
            config.GetSection(FileExtensionsKey).Get<string[]>(), 
            config.GetSection(PlaceholdersKey).Get<string[]>());
    }
}