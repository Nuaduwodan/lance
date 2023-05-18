using Range = LspTypes.Range;

namespace LanceServer.Core.Symbol;

/// <summary>
/// A procedure declaration for a procedure symbol
/// </summary>
public class DeclarationProcedureUse : ISymbolUse
{
    /// <inheritdoc />
    public string Identifier { get; }
    
    /// <inheritdoc />
    public Range Range { get; }
    
    /// <inheritdoc />
    public Uri SourceDocument { get; }

    public DeclarationProcedureUse(string identifier, Range range, Uri sourceDocument)
    {
        Identifier = identifier;
        Range = range;
        SourceDocument = sourceDocument;
    }
}