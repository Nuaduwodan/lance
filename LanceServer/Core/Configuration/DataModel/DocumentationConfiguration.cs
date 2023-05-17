namespace LanceServer.Core.Configuration.DataModel;

/// <summary>
/// The language token documentation
/// </summary>
public class DocumentationConfiguration
{
    public TokenDocumentation[] LanguageTokens { get; set; } = Array.Empty<TokenDocumentation>();
}