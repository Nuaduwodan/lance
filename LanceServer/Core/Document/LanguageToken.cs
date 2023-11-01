using Range = OmniSharp.Extensions.LanguageServer.Protocol.Models.Range;

namespace LanceServer.Core.Document;

/// <summary>
/// A token or part of code belonging to the language sinumerik one nc
/// </summary>
public class LanguageToken
{
    /// <summary>
    /// The code
    /// </summary>
    public string Code { get; }
    
    /// <summary>
    /// The range of the code
    /// </summary>
    public Range Range { get; }
    
    /// <summary>
    /// Instantiates a new <see cref="LanguageToken"/>
    /// </summary>
    public LanguageToken(string code, Range range)
    {
        Code = code.ToLower();
        Range = range;
    }
}