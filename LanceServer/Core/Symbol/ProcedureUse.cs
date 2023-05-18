using Range = LspTypes.Range;

namespace LanceServer.Core.Symbol;

/// <summary>
/// A call to a procedure symbol
/// </summary>
public class ProcedureUse : ISymbolUse
{
    /// <inheritdoc />
    public string Identifier { get; }
    
    /// <inheritdoc />
    public Range Range { get; }
    
    /// <inheritdoc />
    public Uri SourceDocument { get; }

    /// <inheritdoc />
    public bool ReferencesSymbol(string reference)
    {
        return Identifier.Equals(reference, StringComparison.OrdinalIgnoreCase);
    }

    public ProcedureUse(string identifier, Range range, Uri sourceDocument)
    {
        Identifier = identifier;
        Range = range;
        SourceDocument = sourceDocument;
    }
}