using LanceServer.SemanticToken;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LanceServerTest.SemanticToken;

[TestClass]
public class SemanticTokenHandlerTest
{
    [DataTestMethod]
    [DataRow(SinumerikNCLexer.COMMENT, (int)SemanticTokenTypeHelper.SemanticTokenType.Comment)]
    public void TransformType(int grammarTokenType, int expectedLspTokenType)
    {
        // Arrange
        var tokenHandler = new SemanticTokenHandler();

        // Act
        var actualLspTokenType = tokenHandler.TransformType(grammarTokenType);

        // Assert
        Assert.AreEqual(expectedLspTokenType, actualLspTokenType);
    }
}