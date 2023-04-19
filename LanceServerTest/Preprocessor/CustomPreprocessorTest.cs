﻿using LanceServer.Core.Configuration;
using LanceServer.Core.Configuration.DataModel;
using LanceServer.Core.Workspace;
using LanceServer.Preprocessor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LanceServerTest.Preprocessor;

[TestClass]
public class CustomPreprocessorTest
{
    private IConfigurationManager _configurationManagerMock;
        
    [TestInitialize]
    public void Init()
    {
        var configurationManagerMock = new Mock<IConfigurationManager>();
            
        var customPreprocessorConfiguration = new CustomPreprocessorConfiguration();
        customPreprocessorConfiguration.PlaceholderType = PlaceholderType.RegEx;
        customPreprocessorConfiguration.FileExtensions = new[]{ ".tpl" };
        customPreprocessorConfiguration.Placeholders = new []{ "([^\"])<[a-zA-Z0-9\\.]+>" };

        configurationManagerMock.Setup(m => m.CustomPreprocessorConfiguration)
            .Returns(customPreprocessorConfiguration);

        _configurationManagerMock = configurationManagerMock.Object;
    }
        
    [TestMethod]
    public void FilterTest_Empty()
    {
        // Arrange
        var preprocessor = new PlaceholderPreprocessor(_configurationManagerMock);
        var code = "";
        var document = new PreprocessedDocument(new DocumentInformation(new Uri("file:///testfile.tpl"), ".tpl", code), code, new Placeholders(new Dictionary<string, string>()), "");

        var expectedResult = code;

        // Act
        var actualResult = preprocessor.Filter(document);
            
        // Assert
        Assert.AreEqual(expectedResult, actualResult.Code);
    }
        
    [TestMethod]
    public void FilterTest_Identical()
    {
        // Arrange
        var preprocessor = new PlaceholderPreprocessor(_configurationManagerMock);
        var code = 
            @"proc testProcedure(int testparam)

                define definedMacro as 42
                def int declaredVariable
                def real definedVariable = 2.3

                if (definedMacro > definedVariable) or (testparam < 0)
                    declaredVariable = 7
                endif

                ret
                endproc";
        var document = new PreprocessedDocument(new DocumentInformation(new Uri("file:///testfile.tpl"), ".tpl", code), code, new Placeholders(new Dictionary<string, string>()), "");

        var expectedResult = code;

        // Act
        var actualResult = preprocessor.Filter(document);
            
        // Assert
        Assert.AreEqual(expectedResult, actualResult.Code);
    }
        
    [TestMethod]
    public void FilterTest_Replace()
    {
        // Arrange
        var preprocessor = new PlaceholderPreprocessor(_configurationManagerMock);
        var code = 
            @"proc testProcedure(int testparam)

                define definedMacro as <P.TheAnswer>
                def int declaredVariable
                def real definedVariable = 2.3

                if (definedMacro > definedVariable) or (testparam < 0)
                    declaredVariable = 7
                endif

                ret
                endproc";
        var document = new PreprocessedDocument(new DocumentInformation(new Uri("file:///testfile.tpl"), ".tpl", code), code, new Placeholders(new Dictionary<string, string>()), "");

        var expectedResult = 
            @"proc testProcedure(int testparam)

                define definedMacro as _P_TheAnswer_
                def int declaredVariable
                def real definedVariable = 2.3

                if (definedMacro > definedVariable) or (testparam < 0)
                    declaredVariable = 7
                endif

                ret
                endproc";

        // Act
        var actualResult = preprocessor.Filter(document);
            
        // Assert
        Assert.AreEqual(expectedResult, actualResult.Code);
    }
        
    [TestMethod]
    public void FilterTest_IdenticalInString()
    {
        // Arrange
        var preprocessor = new PlaceholderPreprocessor(_configurationManagerMock);
        var code = 
            @"proc testProcedure(int testparam)

                define definedMacro as ""<P.TheAnswer>""
                def int declaredVariable
                def real definedVariable = 2.3

                if (definedMacro > definedVariable) or (testparam < 0)
                    declaredVariable = 7
                endif

                ret
                endproc";
        var document = new PreprocessedDocument(new DocumentInformation(new Uri("file:///testfile.tpl"), ".tpl", code), code, new Placeholders(new Dictionary<string, string>()), "");

        var expectedResult = code;

        // Act
        var actualResult = preprocessor.Filter(document);
            
        // Assert
        Assert.AreEqual(expectedResult, actualResult.Code);
    }
        
    [TestMethod]
    public void FilterTest_ReplaceInSymbolName()
    {
        // Arrange
        var preprocessor = new PlaceholderPreprocessor(_configurationManagerMock);
        var code = 
            @"proc <InstanceName>Procedure(int testparam)

                define definedMacro as 42
                def int declaredVariable
                def real definedVariable = 2.3

                if (definedMacro > definedVariable) or (testparam < 0)
                    declaredVariable = 7
                endif

                ret
                endproc";
        var document = new PreprocessedDocument(new DocumentInformation(new Uri("file:///testfile.tpl"), ".tpl", code), code, new Placeholders(new Dictionary<string, string>()), "");

        var expectedResult = 
            @"proc _InstanceName_Procedure(int testparam)

                define definedMacro as 42
                def int declaredVariable
                def real definedVariable = 2.3

                if (definedMacro > definedVariable) or (testparam < 0)
                    declaredVariable = 7
                endif

                ret
                endproc";

        // Act
        var actualResult = preprocessor.Filter(document);
            
        // Assert
        Assert.AreEqual(expectedResult, actualResult.Code);
    }
        
    [TestMethod]
    public void FilterTest_NonMatchingFileEnding()
    {
        // Arrange
        var preprocessor = new PlaceholderPreprocessor(_configurationManagerMock);
        var code = 
            @"proc testProcedure(int testparam)

                define definedMacro as <P.TheAnswer>
                def int declaredVariable
                def real definedVariable = 2.3

                if (definedMacro > definedVariable) or (testparam < 0)
                    declaredVariable = 7
                endif

                ret
                endproc";
        var document = new PreprocessedDocument(new DocumentInformation(new Uri("file:///testfile.spf"), ".spf", code), code, new Placeholders(new Dictionary<string, string>()), "");

        var expectedResult = code;

        // Act
        var actualResult = preprocessor.Filter(document);
            
        // Assert
        Assert.AreEqual(expectedResult, actualResult.Code);
    }
}