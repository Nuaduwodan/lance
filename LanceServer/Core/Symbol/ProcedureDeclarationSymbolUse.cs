using Range = LspTypes.Range;

namespace LanceServer.Core.Symbol;

/// <summary>
/// Represents a procedure declaration for a procedure symbol
/// </summary>
public class ProcedureDeclarationSymbolUse : ISymbolUse
{
    /// <inheritdoc />
    public string Identifier { get; }
    
    /// <inheritdoc />
    public Range Range { get; }
    
    /// <inheritdoc />
    public Uri SourceDocument { get; }

    public ProcedureDeclarationSymbolUse(string identifier, Range range, Uri sourceDocument)
    {
        Identifier = identifier;
        Range = range;
        SourceDocument = sourceDocument;
    }
}