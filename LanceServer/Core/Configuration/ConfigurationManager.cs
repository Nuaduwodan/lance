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
    private SymbolTableConfiguration? _symbolTableConfiguration;
    private FileExtensionConfiguration? _fileExtensionConfiguration;
    private CustomPreprocessorConfiguration? _customPreprocessorConfiguration;
    private readonly ILanguageServerConfiguration _languageServerConfiguration;
    private const string DefinitionFileExtensionsKey = "symbols.definitionFileExtensions";
    private const string SubProcedureFileExtensionsKey = "symbols.subProcedureFileExtensions";
    private const string MainProcedureFileExtensionsKey = "symbols.mainProcedureFileExtensions";
    private const string ManufacturerCyclesDirectoriesKey = "symbols.manufacturerCyclesDirectories";
    private const string PlaceholderTypeKey = "placeholderPreprocessor.placeholderType";
    private const string FileExtensionsKey = "placeholderPreprocessor.fileExtensions";
    private const string PlaceholdersKey = "placeholderPreprocessor.placeholders";

    public ConfigurationManager(ILanguageServerConfiguration languageServerConfiguration)
    {
        _languageServerConfiguration = languageServerConfiguration;
    }
    
    /// <inheritdoc />
    public SymbolTableConfiguration GetSymbolTableConfiguration()
    { 
        if (_symbolTableConfiguration == null)
        {
            var config = Task.Run(() => _languageServerConfiguration.GetConfiguration(new ConfigurationItem { Section = "lance" })).Result;
            _symbolTableConfiguration = ExtractSymbolTableConfiguration(config);
        }
        
        return _symbolTableConfiguration;
    }

    private SymbolTableConfiguration ExtractSymbolTableConfiguration(IConfiguration config)
    {
        return new SymbolTableConfiguration(
            config.GetSection(DefinitionFileExtensionsKey).Get<string[]>(),
            config.GetSection(SubProcedureFileExtensionsKey).Get<string[]>(),
            config.GetSection(MainProcedureFileExtensionsKey).Get<string[]>(),
            config.GetSection(ManufacturerCyclesDirectoriesKey).Get<string[]>());
    }

    /// <inheritdoc />
    public FileExtensionConfiguration GetFileExtensionConfiguration()
    {
        if (_fileExtensionConfiguration == null)
        {
            var config = Task.Run(() => _languageServerConfiguration.GetConfiguration(new ConfigurationItem { Section = "lance" })).Result;
            _fileExtensionConfiguration = ExtractFileExtensionConfiguration(config);
        }

        return _fileExtensionConfiguration;
    }

    private FileExtensionConfiguration ExtractFileExtensionConfiguration(IConfiguration config)
    {
        return new FileExtensionConfiguration(
            config.GetSection(DefinitionFileExtensionsKey).Get<string[]>().Concat(
                config.GetSection(SubProcedureFileExtensionsKey).Get<string[]>()).Concat(
                config.GetSection(MainProcedureFileExtensionsKey).Get<string[]>()).ToArray());
    }

    /// <inheritdoc />
    public DocumentationConfiguration GetDocumentationConfiguration()
    {
        if (_documentationConfiguration == null)
        {
            var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var docConfigPath = Path.Join(basePath, "language_token_documentation.json");
            _documentationConfiguration = JsonConvert.DeserializeObject<DocumentationConfiguration>(FileUtil.ReadFileContent(docConfigPath))
                                          ?? throw new FileNotFoundException(docConfigPath + " not found");
        }

        return _documentationConfiguration;
    }

    /// <inheritdoc />
    public CustomPreprocessorConfiguration GetCustomPreprocessorConfiguration()
    {
        if (_customPreprocessorConfiguration == null)
        {
            var config = Task.Run(() => _languageServerConfiguration.GetConfiguration(new ConfigurationItem { Section = "lance" })).Result;
            _customPreprocessorConfiguration = ExtractCustomPreprocessorConfiguration(config);
        }

        return _customPreprocessorConfiguration;
    }

    private CustomPreprocessorConfiguration ExtractCustomPreprocessorConfiguration(IConfiguration config)
    {
        return new CustomPreprocessorConfiguration(
            config.GetSection(PlaceholderTypeKey).Get<PlaceholderType>(),
            config.GetSection(FileExtensionsKey).Get<string[]>(),
            config.GetSection(PlaceholdersKey).Get<string[]>());
    }

    public void ExtractConfiguration(IConfigurationRoot config)
    {
        var lanceConfig = config.GetSection("lance");
        _symbolTableConfiguration = ExtractSymbolTableConfiguration(lanceConfig);
        _customPreprocessorConfiguration = ExtractCustomPreprocessorConfiguration(lanceConfig);
        _fileExtensionConfiguration = ExtractFileExtensionConfiguration(lanceConfig);
    }
}