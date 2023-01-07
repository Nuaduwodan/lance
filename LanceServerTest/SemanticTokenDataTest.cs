using LanceServer.DataModels;

namespace LanceServerTest;

public class SemanticTokenDataTest
{
    [Fact]
    public void EmptyData()
    {
        // Arrange
        int[] expectedResult = Array.Empty<int>();
        
        var tokenData = new SemanticTokenData();
        
        // Act
        var actualResult = tokenData.ToIntArray();
        
        // Assert
        Assert.Equal(expectedResult, actualResult);
    }
    
    [Fact]
    public void OneEntry()
    {
        // Arrange
        int[] expectedResult = new[] { 1, 1, 1, 1, 0 };
        
        var tokenData = new SemanticTokenData();
        tokenData.AddElement(new SemanticTokenDataElement(1, 1, 1, 1, 0));
        
        // Act
        var actualResult = tokenData.ToIntArray();
        
        // Assert
        Assert.Equal(expectedResult, actualResult);
    }
    
    [Fact]
    public void MultipleEntry()
    {
        // Arrange
        int[] expectedResult = new[] { 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0 };
        
        var tokenData = new SemanticTokenData();
        tokenData.AddElement(new SemanticTokenDataElement(1, 1, 1, 1, 0));
        tokenData.AddElement(new SemanticTokenDataElement(1, 1, 1, 1, 0));
        tokenData.AddElement(new SemanticTokenDataElement(1, 1, 1, 1, 0));
        
        // Act
        var actualResult = tokenData.ToIntArray();
        
        // Assert
        Assert.Equal(expectedResult, actualResult);
    }
}