using System.Collections.Immutable;

namespace LanceServer.RequestHandler.SemanticToken;

/// <summary>
/// Represents the data for the semantic tokens and provides a method to convert to the LSP specific data structure.
/// </summary>
public class SemanticTokenData
{
    private List<SemanticTokenDataElement> _data = new();
    
    /// <summary>
    /// Adds a new <see cref="SemanticTokenDataElement"/> to this list.
    /// </summary>
    /// <param name="element">The new <see cref="SemanticTokenDataElement"/>.</param>
    public void AddElement(SemanticTokenDataElement element)
    {
        _data.Add(element);
    }
    
    /// <summary>
    /// Converts and returns this list of <see cref="SemanticTokenDataElement"/>s in the structure defined by the LSP.
    /// See <a href="https://microsoft.github.io/language-server-protocol/specifications/lsp/3.17/specification/#textDocument_semanticTokens">LSP specification</a>
    /// </summary>
    public ImmutableArray<int> ToDataFormat()
    {
        ImmutableArray<int> intData = _data.Select(element => new[]
        {
            element.DeltaLine, 
            element.DeltaChar, 
            element.Length, 
            element.TokenType,
            element.TokenModifiers
        }).SelectMany(e => e).ToImmutableArray();
        return intData;
    }
}