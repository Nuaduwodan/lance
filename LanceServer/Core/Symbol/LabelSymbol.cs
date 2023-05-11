using Range = LspTypes.Range;

namespace LanceServer.Core.Symbol;

public class LabelSymbol : ISymbol
{
    public string Identifier { get; }
    public SymbolType Type { get; }
    public Uri SourceDocument { get; }
    public Range SymbolRange { get; }
    public Range IdentifierRange { get; }
    public string Description => $"label";
    public string Code => $"{Identifier}:";
    public string Documentation { get; }
    
    public LabelSymbol(string identifier, Uri sourceDocument, Range symbolRange, Range identifierRange, string documentation = "")
    {
        Identifier = identifier;
        SourceDocument = sourceDocument;
        SymbolRange = symbolRange;
        IdentifierRange = identifierRange;
        Documentation = documentation;

        Type = SymbolType.Label;
    }
}