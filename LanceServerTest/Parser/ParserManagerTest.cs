using Antlr4.Runtime.Tree;
using LanceServer.Core.Document;
using LanceServer.Core.Workspace;
using LanceServer.Parser;
using LanceServerTest.Core.Workspace;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LanceServerTest.Parser;

[TestClass]
public class ParserManagerTest
{
    public class SimpleParseTreeWalker
    {
        public List<string> Elements { get; } = new();
            
        public SimpleParseTreeWalker(IParseTree parseTree)
        {
            InitializeRecursive(parseTree);
        }

        void InitializeRecursive(IParseTree currentPosition)
        {
            if (currentPosition.ChildCount == 0)
            {
                Elements.Add(currentPosition.GetText());
            }
            else
            {
                for (var i = 0;i < currentPosition.ChildCount;i++)
                {
                    InitializeRecursive(currentPosition.GetChild(i));
                }
            }
        }
    }
        
    [TestMethod]
    public void ParseEmptyDocument()
    {
        // Arrange
        var expectedText = "<EOF>";

        var code ="";
        var document = new PreprocessedDocument(new DocumentInformationMock(new Uri("file:///testfile.spf"), ".spf", DocumentType.SubProcedure), code, code, new Placeholders(new Dictionary<string, string>()), "");
        var parserManager = new ParserManager();

        // Act
        var actualParseTree = parserManager.Parse(document).ParseTree;

        // Assert
        Assert.AreEqual(expectedText, actualParseTree.GetText());
    }

    [TestMethod]
    public void ParseSimpleProcedure()
    {
        // Arrange
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
        var document = new PreprocessedDocument(new DocumentInformationMock(new Uri("file:///testfile.spf"), ".spf", DocumentType.SubProcedure), code, code, new Placeholders(new Dictionary<string, string>()), "");
        var parserManager = new ParserManager();

        // Act
        var actualParseTree = parserManager.Parse(document).ParseTree;

        // Assert
        var treeWalker = new SimpleParseTreeWalker(actualParseTree);
        var elementPosition = 0;
            
        Assert.AreEqual("proc", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual("testProcedure", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual("(", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual("int", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual("testparam", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual(")", treeWalker.Elements[elementPosition++]);
        elementPosition++; //newline
        elementPosition++; //newline
        Assert.AreEqual("define", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual("definedMacro", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual("as", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual("42", treeWalker.Elements[elementPosition++]);
        elementPosition++; //newline
        Assert.AreEqual("def", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual("", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual("int", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual("", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual("declaredVariable", treeWalker.Elements[elementPosition++]);
        elementPosition++; //newline
        Assert.AreEqual("def", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual("", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual("real", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual("", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual("definedVariable", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual("=", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual("2.3", treeWalker.Elements[elementPosition++]);
        elementPosition++; //newline
        elementPosition++; //newline
        Assert.AreEqual("if", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual("(", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual("definedMacro", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual(">", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual("definedVariable", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual(")", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual("or", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual("(", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual("testparam", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual("<", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual("0", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual(")", treeWalker.Elements[elementPosition++]);
        elementPosition++; //newline
        Assert.AreEqual("declaredVariable", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual("=", treeWalker.Elements[elementPosition++]);
        Assert.AreEqual("7", treeWalker.Elements[elementPosition++]);
        elementPosition++; //newline
        Assert.AreEqual("endif", treeWalker.Elements[elementPosition++]);
        elementPosition++; //newline
        elementPosition++; //newline
        Assert.AreEqual("ret", treeWalker.Elements[elementPosition++]);
        elementPosition++; //newline
        Assert.AreEqual("endproc", treeWalker.Elements[elementPosition++]);
    }

    [TestMethod]
    public void SymbolTable_SimpleProcedure()
    {
        // Arrange
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
        var preprocessedDocument = new PreprocessedDocument(new DocumentInformationMock(new Uri("file:///testfile.spf"), ".spf", DocumentType.SubProcedure), code, code, new Placeholders(new Dictionary<string, string>()), "");
        var parserManager = new ParserManager();
        var parserResult = parserManager.Parse(preprocessedDocument);
        var document = new ParsedDocument(preprocessedDocument, parserResult.ParseTree, parserResult.Diagnostics);

        // Act
        var actualSymbols = parserManager.GetSymbolTableForDocument(document).ToList();

        // Assert
        var symbolPosition = 0;
            
        Assert.AreEqual("testparam", actualSymbols[symbolPosition++].Identifier);
        Assert.AreEqual("testProcedure", actualSymbols[symbolPosition++].Identifier);
        Assert.AreEqual("definedMacro", actualSymbols[symbolPosition++].Identifier);
        Assert.AreEqual("declaredVariable", actualSymbols[symbolPosition++].Identifier);
        Assert.AreEqual("definedVariable", actualSymbols[symbolPosition++].Identifier);
    }

    [TestMethod]
    public void ParsingErrorTest()
    {
        // Arrange
        var code = 
            @"proc testProcedure(int testparam)

                define definedMacro as 42
                def int declaredVariable
                def real definedVariable = 2.3

                if (definedMacro > definedVariable) or (testparam < 0)
                    declaredVariable = 7
                endif

                ret
                endproc
                ";
        var preprocessedDocument = new PreprocessedDocument(new DocumentInformationMock(new Uri("file:///testfile.spf"), ".spf", DocumentType.SubProcedure), code, code, new Placeholders(new Dictionary<string, string>()), "");
        var parserManager = new ParserManager();
        var parserResult = parserManager.Parse(preprocessedDocument);
        var document = new ParsedDocument(preprocessedDocument, parserResult.ParseTree, parserResult.Diagnostics);

        // Act
        var actualSymbols = parserManager.GetSymbolTableForDocument(document).ToList();

        // Assert
        var symbolPosition = 0;
            
        Assert.AreEqual("testparam", actualSymbols[symbolPosition++].Identifier);
        Assert.AreEqual("testProcedure", actualSymbols[symbolPosition++].Identifier);
        Assert.AreEqual("definedMacro", actualSymbols[symbolPosition++].Identifier);
        Assert.AreEqual("declaredVariable", actualSymbols[symbolPosition++].Identifier);
        Assert.AreEqual("definedVariable", actualSymbols[symbolPosition++].Identifier);
    }
}