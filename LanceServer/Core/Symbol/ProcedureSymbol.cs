using Range = LspTypes.Range;

namespace LanceServer.Core.Symbol;

/// <summary>
/// A symbol representing a function, having zero or more <see cref="ParameterSymbol"/>s
/// </summary>
public class ProcedureSymbol : ISymbol
{
    /// <inheritdoc/>
    public string Identifier { get; }

    /// <inheritdoc/>
    public Uri SourceDocument { get; }
        
    /// <inheritdoc/>
    public Range SymbolRange { get; }
        
    /// <inheritdoc/>
    public Range IdentifierRange { get; }
        
    /// <inheritdoc/>
    public string Description => $"procedure in {Path.GetFileName(SourceDocument.LocalPath)}";
        
    /// <inheritdoc/>
    public string Code => $"proc {Identifier}({string.Join(ParameterDelimiter, Parameters.Select(p => p.Code))})";
        
    /// <inheritdoc/>
    public string Documentation { get; }
    
    /// <summary>
    /// True if the procedure needs an extern declaration, False otherwise.
    /// </summary>
    public bool NeedsExternDeclaration { get; }

    public readonly ParameterSymbol[] Parameters;
    private const string ParameterDelimiter = ", ";

    public ProcedureSymbol(string identifier, Uri sourceDocument, Range symbolRange, Range identifierRange, ParameterSymbol[] parameters, bool needsExternDeclaration = true, string documentation = "")
    {
        Identifier = identifier;
        SourceDocument = sourceDocument;
        SymbolRange = symbolRange;
        IdentifierRange = identifierRange;
        Parameters = parameters;
        NeedsExternDeclaration = needsExternDeclaration;
        Documentation = documentation;
    }
}