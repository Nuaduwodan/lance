using LanceServer.Core.Configuration;
using LanceServer.Core.Configuration.DataModel;
using LanceServer.Preprocessor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LanceServerTest.Core.Configuration
{
    [TestClass]
    public class ConfigurationManagerTest
    {

        [TestMethod]
        public void ExtractConfigurationTest()
        {
            // Arrange
            var expectedCustomPreprocessorConfiguration = new CustomPreprocessorConfiguration();
            expectedCustomPreprocessorConfiguration.KeyType = KeyType.RegEx;
            expectedCustomPreprocessorConfiguration.FileEndings = new[]{ ".tpl" };
            expectedCustomPreprocessorConfiguration.Placeholders = new []{ "proc <InstanceName>", "([^\"])<[a-zA-Z0-9\\.]+>" };
            var expectedSymbolTableConfiguration = new SymbolTableConfiguration() { GlobalDirectories = new[]{ "asdf" }, GlobalFileEndings = new[]{ "asdf" } };
            
            var configurationParams = new ConfigurationParameters();
            configurationParams.Settings = new Settings();
            configurationParams.Settings.Lance = new ServerConfiguration();
            configurationParams.Settings.Lance.Trace = true;
            configurationParams.Settings.Lance.MaxNumberOfProblems = 100;
            configurationParams.Settings.Lance.Customization = new Customization();
            configurationParams.Settings.Lance.Customization.SymbolTableConfiguration = expectedSymbolTableConfiguration;
            configurationParams.Settings.Lance.Customization.PlaceholderPreprocessor = expectedCustomPreprocessorConfiguration;
            
            var configurationManager = new ConfigurationManager();
            
            // Act
            configurationManager.ExtractConfiguration(configurationParams);
            var actualSymbolTableConfiguration = configurationManager.SymbolTableConfiguration;
            var actualCustomPreprocessorConfiguration = configurationManager.CustomPreprocessorConfiguration;

            // Assert
            CollectionAssert.AreEqual(expectedSymbolTableConfiguration.GlobalDirectories, actualSymbolTableConfiguration.GlobalDirectories);
            CollectionAssert.AreEqual(expectedSymbolTableConfiguration.GlobalFileEndings, actualSymbolTableConfiguration.GlobalFileEndings);
            Assert.AreEqual(expectedCustomPreprocessorConfiguration.KeyType, actualCustomPreprocessorConfiguration.KeyType);
            CollectionAssert.AreEqual(expectedCustomPreprocessorConfiguration.FileEndings, actualCustomPreprocessorConfiguration.FileEndings);
            CollectionAssert.AreEqual(expectedCustomPreprocessorConfiguration.Placeholders, actualCustomPreprocessorConfiguration.Placeholders);
        }
        
    }
}