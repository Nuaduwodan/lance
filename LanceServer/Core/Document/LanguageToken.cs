namespace LanceServer.Core.Document;

public class LanguageToken
{
    public string Code { get; }
    
    public LspTypes.Range Range { get; }
    
    public LanguageToken(string code, LspTypes.Range range)
    {
        Code = code;
        Range = range;
    }
}