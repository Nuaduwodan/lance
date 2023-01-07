using Antlr4.Runtime;

namespace LanceServerTest;

public class SinumerikNCLexerTest
{
    [Fact]
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
        Assert.Equal(expectedTokenCount, actualTokenList.Count());
    }
    
    [Fact]
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
        Assert.Equal(expectedTokenCount, actualTokenList.Count());
    }
    
    [Fact]
    public void Procedure()
    {
        // Arrange
        int expectedTokenCount = 4;

        String code = "proc procedure\n\nendproc";
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
        Assert.Equal(expectedTokenCount, actualTokenList.Count());
    }
}