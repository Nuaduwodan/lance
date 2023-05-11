using LanceServer.Core.Configuration;
using LanceServer.Core.Configuration.DataModel;
using LanceServer.Preprocessor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LanceServerTest.Core.Configuration;

[TestClass]
public class ConfigurationManagerTest
{

    [TestMethod]
    public void ExtractConfigurationTest()
    {
        // Arrange
        var expectedCustomPreprocessorConfiguration = new CustomPreprocessorConfiguration();
        expectedCustomPreprocessorConfiguration.PlaceholderType = PlaceholderType.RegEx;
        expectedCustomPreprocessorConfiguration.FileExtensions = new[]{ ".tpl" };
        expectedCustomPreprocessorConfiguration.Placeholders = new []{ "proc <InstanceName>", "([^\"])<[a-zA-Z0-9\\.]+>" };
        var expectedSymbolTableConfiguration = new SymbolTableConfiguration() { ManufacturerCyclesDirectories = new[]{ "asdf" }, DefinitionFileExtensions = new[]{ "asdf" }, SubProcedureFileExtensions = new[]{ "asdf" }, MainProcedureFileExtensions = new[]{ "asdf" } };
            
        var configuration = new ServerConfiguration();
        configuration.Trace = true;
        configuration.MaxNumberOfProblems = 100;
        configuration.SymbolTableConfiguration = expectedSymbolTableConfiguration;
        configuration.PlaceholderPreprocessor = expectedCustomPreprocessorConfiguration;
            
        var configurationManager = new ConfigurationManager(new DocumentationConfiguration());
            
        // Act
        configurationManager.ExtractConfiguration(configuration);
        var actualSymbolTableConfiguration = configurationManager.SymbolTableConfiguration;
        var actualCustomPreprocessorConfiguration = configurationManager.CustomPreprocessorConfiguration;

        // Assert
        CollectionAssert.AreEqual(expectedSymbolTableConfiguration.ManufacturerCyclesDirectories, actualSymbolTableConfiguration.ManufacturerCyclesDirectories);
        CollectionAssert.AreEqual(expectedSymbolTableConfiguration.DefinitionFileExtensions, actualSymbolTableConfiguration.DefinitionFileExtensions);
        Assert.AreEqual(expectedCustomPreprocessorConfiguration.PlaceholderType, actualCustomPreprocessorConfiguration.PlaceholderType);
        CollectionAssert.AreEqual(expectedCustomPreprocessorConfiguration.FileExtensions, actualCustomPreprocessorConfiguration.FileExtensions);
        CollectionAssert.AreEqual(expectedCustomPreprocessorConfiguration.Placeholders, actualCustomPreprocessorConfiguration.Placeholders);
    }
        
}