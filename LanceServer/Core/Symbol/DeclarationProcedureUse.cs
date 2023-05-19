using Range = LspTypes.Range;

namespace LanceServer.Core.Symbol;

/// <summary>
/// A procedure declaration for a procedure symbol
/// </summary>
public class DeclarationProcedureUse : AbstractSymbolUse
{
    /// <inheritdoc />
    public override string Identifier { get; }
    
    /// <inheritdoc />
    public override Range Range { get; }
    
    /// <inheritdoc />
    public override Uri SourceDocument { get; }

    public DeclarationProcedureUse(string identifier, Range range, Uri sourceDocument)
    {
        Identifier = identifier;
        Range = range;
        SourceDocument = sourceDocument;
    }
}