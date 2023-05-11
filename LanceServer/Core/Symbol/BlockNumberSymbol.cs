using Range = LspTypes.Range;

namespace LanceServer.Core.Symbol;

public class BlockNumberSymbol : ISymbol
{
    public string Identifier { get; }
    public SymbolType Type { get; }
    public Uri SourceDocument { get; }
    public Range SymbolRange { get; }
    public Range IdentifierRange => SymbolRange;
    public string Description => $"block number on line {SymbolRange.Start.Line + 1}";
    public string Code => Identifier;
    public string Documentation { get; }
    
    public BlockNumberSymbol(string identifier, Uri sourceDocument, Range symbolRange, string documentation = "")
    {
        Identifier = identifier;
        SourceDocument = sourceDocument;
        SymbolRange = symbolRange;
        Documentation = documentation;

        Type = SymbolType.BlockNumber;
    }
}