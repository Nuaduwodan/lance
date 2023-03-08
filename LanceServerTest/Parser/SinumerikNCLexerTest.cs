using Antlr4.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LanceServerTest.SemanticToken;

[TestClass]
public class SinumerikNCLexerTest
{
    [TestMethod]
    public void EmptyString()
    {
        // Arrange
        int expectedTokenCount = 1;

        String code = "";
        ICharStream stream = CharStreams.fromString(code);
        ITokenSource lexer = new SinumerikNCLexer(stream);
        CommonTokenStream tokens = new CommonTokenStream(lexer);

        // Act
        while (tokens.LA(1) != IntStreamConstants.EOF)
        {
            tokens.Consume();
        }
        var actualTokenList = tokens.GetTokens();

        // Assert
        Assert.AreEqual(expectedTokenCount, actualTokenList.Count);
        Assert.AreEqual(SinumerikNCLexer.Eof, actualTokenList[0].Type);
    }
    
    [TestMethod]
    public void Whitespace()
    {
        // Arrange
        int expectedTokenCount = 1;

        String code = "     ";
        ICharStream stream = CharStreams.fromString(code);
        ITokenSource lexer = new SinumerikNCLexer(stream);
        CommonTokenStream tokens = new CommonTokenStream(lexer);

        // Act
        while (tokens.LA(1) != IntStreamConstants.EOF)
        {
            tokens.Consume();
        }
        var actualTokenList = tokens.GetTokens();

        // Assert
        Assert.AreEqual(expectedTokenCount, actualTokenList.Count);
        Assert.AreEqual(SinumerikNCLexer.Eof, actualTokenList[0].Type);
    }
    
    [TestMethod]
    public void Procedure()
    {
        // Arrange
        int expectedTokenCount = 6;

        String code = "proc procedure()\n\nendproc";
        ICharStream stream = CharStreams.fromString(code);
        ITokenSource lexer = new SinumerikNCLexer(stream);
        CommonTokenStream tokens = new CommonTokenStream(lexer);

        // Act
        while (tokens.LA(1) != IntStreamConstants.EOF)
        {
            tokens.Consume();
        }
        var actualTokenList = tokens.GetTokens();

        // Assert
        Assert.AreEqual(expectedTokenCount, actualTokenList.Count);
        Assert.AreEqual(SinumerikNCLexer.PROC, actualTokenList[0].Type);
        Assert.AreEqual(SinumerikNCLexer.PROGRAM_NAME_SIMPLE, actualTokenList[1].Type);
        Assert.AreEqual(SinumerikNCLexer.PROC_END, actualTokenList[2].Type);
        Assert.AreEqual(SinumerikNCLexer.Eof, actualTokenList[3].Type);
    }
}