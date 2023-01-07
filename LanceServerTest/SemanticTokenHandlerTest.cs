using LanceServer.Handlers;
using LspTypes;

namespace LanceServerTest;

public class SemanticTokenHandlerTest
{
    [Theory]
    [InlineData(SinumerikNCLexer.COMMENT, (int)SemanticTokenTypes.Comment)]
    public void TransformType(int grammarTokenType, int expectedLspTokenType)
    {
        // Arrange
        var tokenHandler = new SemanticTokenHandler();

        // Act
        var actualLspTokenType = tokenHandler.TransformType(grammarTokenType);

        // Assert
        Assert.Equal(expectedLspTokenType, actualLspTokenType);
    }
}