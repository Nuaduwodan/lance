using Range = LspTypes.Range;

namespace LanceServer.Core.Symbol;

public class ProcedureUse : ISymbolUse
{
    public string Identifier { get; }
    public Range Range { get; }
    public Uri SourceDocument { get; }
    
    public bool NeedsExternDeclaration { get; }
    
    public ProcedureUse(string identifier, Range range, Uri sourceDocument, bool needsExternDeclaration = true)
    {
        Identifier = identifier;
        Range = range;
        SourceDocument = sourceDocument;
        NeedsExternDeclaration = needsExternDeclaration;
    }
}