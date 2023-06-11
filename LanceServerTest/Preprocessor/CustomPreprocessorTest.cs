using LanceServer.Core.Configuration;
using LanceServer.Core.Configuration.DataModel;
using LanceServer.Core.Document;
using LanceServer.Preprocessor;
using LanceServerTest.Core.Workspace;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LanceServerTest.Preprocessor;

[TestClass]
public class CustomPreprocessorTest
{
    private IConfigurationManager _configurationManagerMock = null!;
    private CustomPreprocessorConfiguration _customPreprocessorConfiguration = null!;
        
    [TestInitialize]
    public void Init()
    {
        var configurationManagerMock = new Mock<IConfigurationManager>();

        configurationManagerMock.Setup(m => m.CustomPreprocessorConfiguration)
            .Returns(() => _customPreprocessorConfiguration);

        _configurationManagerMock = configurationManagerMock.Object;
    }
        
    [TestMethod]
    public void FilterTest_Empty()
    {
        // Arrange
        _customPreprocessorConfiguration = new CustomPreprocessorConfiguration
        {
            PlaceholderType = PlaceholderType.RegEx,
            FileExtensions = new[] { ".tpl" },
            Placeholders = new[] { "<[a-zA-Z0-9\\.]+>" }
        };
        var preprocessor = new PlaceholderPreprocessor(_configurationManagerMock);
        var code = "";
        var document = new PreprocessedDocument(new DocumentInformationMock(new Uri("file:///testfile.tpl"), ".tpl", DocumentType.SubProcedure), code, code, new PlaceholderTable(new Dictionary<string, string>()), "");

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
        _customPreprocessorConfiguration = new CustomPreprocessorConfiguration
        {
            PlaceholderType = PlaceholderType.RegEx,
            FileExtensions = new[] { ".tpl" },
            Placeholders = new[] { "<[a-zA-Z0-9\\.]+>" }
        };
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
        var document = new PreprocessedDocument(new DocumentInformationMock(new Uri("file:///testfile.tpl"), ".tpl", DocumentType.SubProcedure), code, code, new PlaceholderTable(new Dictionary<string, string>()), "");

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
        _customPreprocessorConfiguration = new CustomPreprocessorConfiguration
        {
            PlaceholderType = PlaceholderType.RegEx,
            FileExtensions = new[] { ".tpl" },
            Placeholders = new[] { "<[a-zA-Z0-9\\.]+>" }
        };
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
        var document = new PreprocessedDocument(new DocumentInformationMock(new Uri("file:///testfile.tpl"), ".tpl", DocumentType.SubProcedure), code, code, new PlaceholderTable(new Dictionary<string, string>()), "");

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
    public void FilterTest_ReplaceInString()
    {
        // Arrange
        _customPreprocessorConfiguration = new CustomPreprocessorConfiguration
        {
            PlaceholderType = PlaceholderType.RegEx,
            FileExtensions = new[] { ".tpl" },
            Placeholders = new[] { "<[a-zA-Z0-9\\.]+>" }
        };
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
        var document = new PreprocessedDocument(new DocumentInformationMock(new Uri("file:///testfile.tpl"), ".tpl", DocumentType.SubProcedure), code, code, new PlaceholderTable(new Dictionary<string, string>()), "");

        var expectedResult = 
            @"proc testProcedure(int testparam)

                define definedMacro as ""_P_TheAnswer_""
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
    public void FilterTest_ReplaceInSymbolName()
    {
        // Arrange
        _customPreprocessorConfiguration = new CustomPreprocessorConfiguration
        {
            PlaceholderType = PlaceholderType.RegEx,
            FileExtensions = new[] { ".tpl" },
            Placeholders = new[] { "<[a-zA-Z0-9\\.]+>" }
        };
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
        var document = new PreprocessedDocument(new DocumentInformationMock(new Uri("file:///testfile.tpl"), ".tpl", DocumentType.SubProcedure), code, code, new PlaceholderTable(new Dictionary<string, string>()), "");

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
    public void FilterTest_ReplaceMultipleInSymbolName()
    {
        // Arrange
        _customPreprocessorConfiguration = new CustomPreprocessorConfiguration
        {
            PlaceholderType = PlaceholderType.RegEx,
            FileExtensions = new[] { ".tpl" },
            Placeholders = new[] { "<[a-zA-Z0-9\\.]+>" }
        };
        var preprocessor = new PlaceholderPreprocessor(_configurationManagerMock);
        var code = 
            @"proc First<InstanceName>Procedure<Second.Part>End(int testparam)

                define definedMacro as 42
                def int declaredVariable
                def real definedVariable = 2.3

                if (definedMacro > definedVariable) or (testparam < 0)
                    declaredVariable = 7
                endif

                ret
                endproc";
        var document = new PreprocessedDocument(new DocumentInformationMock(new Uri("file:///testfile.tpl"), ".tpl", DocumentType.SubProcedure), code, code, new PlaceholderTable(new Dictionary<string, string>()), "");

        var expectedResult = 
            @"proc First_InstanceName_Procedure_Second_Part_End(int testparam)

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
    public void FilterTest_MultiplePlaceholders_ReplaceMultiple()
    {
        // Arrange
        _customPreprocessorConfiguration = new CustomPreprocessorConfiguration
        {
            PlaceholderType = PlaceholderType.RegEx,
            FileExtensions = new[] { ".tpl" },
            Placeholders = new[] { "<InstanceName>", "Second\\.Part" }
        };
        var preprocessor = new PlaceholderPreprocessor(_configurationManagerMock);
        var code = 
            @"proc First<InstanceName>Procedure<Second.Part>End(int testparam)

                define definedMacro as 42
                def int declaredVariable
                def real definedVariable = 2.3

                if (definedMacro > definedVariable) or (testparam < 0)
                    declaredVariable = 7
                endif

                ret
                endproc";
        var document = new PreprocessedDocument(new DocumentInformationMock(new Uri("file:///testfile.tpl"), ".tpl", DocumentType.SubProcedure), code, code, new PlaceholderTable(new Dictionary<string, string>()), "");

        var expectedResult = 
            @"proc First_InstanceName_Procedure_Second_Part_End(int testparam)

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
    public void FilterTest_ReplaceAloneOnLine()
    {
        // Arrange
        _customPreprocessorConfiguration = new CustomPreprocessorConfiguration
        {
            PlaceholderType = PlaceholderType.RegEx,
            FileExtensions = new[] { ".tpl" },
            Placeholders = new[] { "<[a-zA-Z0-9\\.]+>" }
        };
        var preprocessor = new PlaceholderPreprocessor(_configurationManagerMock);
        var code = 
            @"proc Procedure(int testparam)

                <Alone.On.Line>

                define definedMacro as 42
                def int declaredVariable
                def real definedVariable = 2.3

                if (definedMacro > definedVariable) or (testparam < 0)
                    declaredVariable = 7
                endif

                ret
                endproc";
        var document = new PreprocessedDocument(new DocumentInformationMock(new Uri("file:///testfile.tpl"), ".tpl", DocumentType.SubProcedure), code, code, new PlaceholderTable(new Dictionary<string, string>()), "");

        var expectedResult = 
            @"proc Procedure(int testparam)

                

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
    public void FilterTest_ReplaceInSymbolNameWhileSameAloneOnLine()
    {
        // Arrange
        _customPreprocessorConfiguration = new CustomPreprocessorConfiguration
        {
            PlaceholderType = PlaceholderType.RegEx,
            FileExtensions = new[] { ".tpl" },
            Placeholders = new[] { "<[a-zA-Z0-9\\.]+>" }
        };
        var preprocessor = new PlaceholderPreprocessor(_configurationManagerMock);
        var code = 
            @"proc Procedure<InstanceName>(int testparam)

                <InstanceName>

                define definedMacro as 42
                def int declaredVariable
                def real definedVariable = 2.3

                if (definedMacro > definedVariable) or (testparam < 0)
                    declaredVariable = 7
                endif

                ret
                endproc";
        var document = new PreprocessedDocument(new DocumentInformationMock(new Uri("file:///testfile.tpl"), ".tpl", DocumentType.SubProcedure), code, code, new PlaceholderTable(new Dictionary<string, string>()), "");

        var expectedResult = 
            @"proc Procedure_InstanceName_(int testparam)

                

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
        Assert.IsTrue(actualResult.PlaceholderTable.ContainedPlaceholder(expectedResult));
    }
        
    [TestMethod]
    public void FilterTest_NonMatchingFileEnding()
    {
        // Arrange
        _customPreprocessorConfiguration = new CustomPreprocessorConfiguration
        {
            PlaceholderType = PlaceholderType.RegEx,
            FileExtensions = new[] { ".tpl" },
            Placeholders = new[] { "<[a-zA-Z0-9\\.]+>" }
        };
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
        var document = new PreprocessedDocument(new DocumentInformationMock(new Uri("file:///testfile.spf"), ".spf", DocumentType.SubProcedure), code, code, new PlaceholderTable(new Dictionary<string, string>()), "");

        var expectedResult = code;

        // Act
        var actualResult = preprocessor.Filter(document);
            
        // Assert
        Assert.AreEqual(expectedResult, actualResult.Code);
    }

    [TestMethod]
    public void FilterTest_DifferentExtensionCapitalisation_Replace()
    {
        // Arrange
        _customPreprocessorConfiguration = new CustomPreprocessorConfiguration
        {
            PlaceholderType = PlaceholderType.RegEx,
            FileExtensions = new[] { ".tpl" },
            Placeholders = new[] { "<[a-zA-Z0-9\\.]+>" }
        };
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
        var document = new PreprocessedDocument(new DocumentInformationMock(new Uri("file:///testfile.TPL"), ".TPL", DocumentType.SubProcedure), code, code, new PlaceholderTable(new Dictionary<string, string>()), "");

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
    public void FilterTest_CodeSnippet_Replace()
    {
        // Arrange
        _customPreprocessorConfiguration = new CustomPreprocessorConfiguration
        {
            PlaceholderType = PlaceholderType.RegEx,
            FileExtensions = new[] { ".tpl" },
            Placeholders = new[] { "mod[ 012\\+]" }
        };
        var preprocessor = new PlaceholderPreprocessor(_configurationManagerMock);
        var code = 
            @"proc testProcedure(int testparam)

                define definedMacro as mod 10 + 2
                def int declaredVariable
                def real definedVariable = 2.3

                if (definedMacro > definedVariable) or (testparam < 0)
                    declaredVariable = 7
                endif

                ret
                endproc";
        var document = new PreprocessedDocument(new DocumentInformationMock(new Uri("file:///testfile.tpl"), ".tpl", DocumentType.SubProcedure), code, code, new PlaceholderTable(new Dictionary<string, string>()), "");

        var expectedResult = 
            @"proc testProcedure(int testparam)

                define definedMacro as mod_10___2
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
}