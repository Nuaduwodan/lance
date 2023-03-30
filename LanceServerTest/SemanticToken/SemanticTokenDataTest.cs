using LanceServer.SemanticToken;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LanceServerTest.SemanticToken;

[TestClass]
public class SemanticTokenDataTest
{
    [TestMethod]
    public void EmptyData()
    {
        // Arrange
        uint[] expectedResult = Array.Empty<uint>();
        
        var tokenData = new SemanticTokenData();
        
        // Act
        var actualResult = tokenData.ToDataFormat();
        
        // Assert
        Assert.AreEqual(expectedResult, actualResult);
    }
    
    [TestMethod]
    public void OneEntry()
    {
        // Arrange
        uint[] expectedResult = new[] { 1u, 1u, 1u, 1u, 0u };
        
        var tokenData = new SemanticTokenData();
        tokenData.AddElement(new SemanticTokenDataElement(1, 1, 1, 1, 0));
        
        // Act
        var actualResult = tokenData.ToDataFormat();
        
        // Assert
        CollectionAssert.AreEqual(expectedResult, actualResult);
    }
    
    [TestMethod]
    public void MultipleEntry()
    {
        // Arrange
        uint[] expectedResult = new[] { 1u, 1u, 1u, 1u, 0u, 1u, 1u, 1u, 1u, 0u, 1u, 1u, 1u, 1u, 0u };
        
        var tokenData = new SemanticTokenData();
        tokenData.AddElement(new SemanticTokenDataElement(1, 1, 1, 1, 0));
        tokenData.AddElement(new SemanticTokenDataElement(1, 1, 1, 1, 0));
        tokenData.AddElement(new SemanticTokenDataElement(1, 1, 1, 1, 0));
        
        // Act
        var actualResult = tokenData.ToDataFormat();
        
        // Assert
        CollectionAssert.AreEqual(expectedResult, actualResult);
    }
}