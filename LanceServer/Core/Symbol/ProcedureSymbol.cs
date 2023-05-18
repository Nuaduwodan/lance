using Range = LspTypes.Range;

namespace LanceServer.Core.Symbol;

/// <summary>
/// A symbol representing a function, having zero or more <see cref="ParameterSymbol"/>s
/// </summary>
public class ProcedureSymbol : AbstractSymbol
{
    /// <inheritdoc/>
    public override string Identifier { get; }

    /// <inheritdoc/>
    public override Uri SourceDocument { get; }
        
    /// <inheritdoc/>
    public override Range SymbolRange { get; }
        
    /// <inheritdoc/>
    public override Range IdentifierRange { get; }
        
    /// <inheritdoc/>
    public override string Description => $"procedure in {Path.GetFileName(SourceDocument.LocalPath)}";
        
    /// <inheritdoc/>
    public override string Code => $"proc {Identifier}({string.Join(ParameterDelimiter, Parameters.Select(p => p.Code))})";
        
    /// <inheritdoc/>
    public override string Documentation { get; }
    
    /// <summary>
    /// True if the procedure needs an extern declaration, False otherwise.
    /// </summary>
    public bool NeedsExternDeclaration { get; }

    /// <summary>
    /// The parameters of this procedure
    /// </summary>
    /// <seealso cref="ProcedureSymbol"/>
    public readonly ParameterSymbol[] Parameters;
    
    private const string ParameterDelimiter = ", ";

    public ProcedureSymbol(string identifier, Uri sourceDocument, Range symbolRange, Range identifierRange, ParameterSymbol[] parameters, bool needsExternDeclaration = false, string documentation = "")
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