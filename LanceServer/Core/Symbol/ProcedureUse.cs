using Range = LspTypes.Range;

namespace LanceServer.Core.Symbol;

/// <summary>
/// A call to a procedure symbol
/// </summary>
public class ProcedureUse : AbstractSymbolUse
{
    /// <inheritdoc />
    public override string Identifier { get; }
    
    /// <inheritdoc />
    public override Range Range { get; }
    
    /// <inheritdoc />
    public override Uri SourceDocument { get; }
    
    public ProcedureUse(string identifier, Range range, Uri sourceDocument)
    {
        Identifier = identifier;
        Range = range;
        SourceDocument = sourceDocument;
    }
}