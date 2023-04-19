namespace LanceServer.SemanticToken;

/// <summary>
/// Represents the data for the semantic tokens and provides a method to convert to the LSP specific data structure.
/// </summary>
public class SemanticTokenData
{
    private List<SemanticTokenDataElement> data = new();
    
    /// <summary>
    /// Adds a new <see cref="SemanticTokenDataElement"/> to this list.
    /// </summary>
    /// <param name="element">The new <see cref="SemanticTokenDataElement"/>.</param>
    public void AddElement(SemanticTokenDataElement element)
    {
        data.Add(element);
    }
    
    /// <summary>
    /// Converts and returns this list of <see cref="SemanticTokenDataElement"/>s in the structure defined by the LSP.
    /// See <a href="https://microsoft.github.io/language-server-protocol/specifications/lsp/3.17/specification/#textDocument_semanticTokens">LSP specification</a>
    /// </summary>
    public uint[] ToDataFormat()
    {
        var intData = data.Select(element => new uint[]{ Convert.ToUInt32(element.DeltaLine), Convert.ToUInt32(element.DeltaChar), Convert.ToUInt32(element.Length), Convert.ToUInt32(element.TokenType), Convert.ToUInt32(element.TokenModifiers) }).SelectMany(e => e).ToArray();
        return intData;
    }
}