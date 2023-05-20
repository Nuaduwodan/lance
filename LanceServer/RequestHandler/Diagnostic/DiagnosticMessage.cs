using LanceServer.Core.Symbol;
using LanceServer.Core.Workspace;
using LspTypes;
using Range = LspTypes.Range;

namespace LanceServer.RequestHandler.Diagnostic;

public static class DiagnosticMessage
{
    /// <summary>
    /// The maximum file name length as defined by the manual
    /// </summary>
    public const int MaxFileNameLength = 24;
    
    /// <summary>
    /// The maximum axis name length as defined by the manual
    /// </summary>
    public const int MaxAxisNameLength = 8;
    
    /// <summary>
    /// The maximum symbol name length as defined by the manual
    /// </summary>
    public const int MaxSymbolIdentifierLength = 31;

    private const string DiagnosticSource = "Lance";
    
    public static LspTypes.Diagnostic SymbolHasDifferentCapitalisation(AbstractSymbolUse symbolUse, AbstractSymbol symbol)
    {
        return new LspTypes.Diagnostic
        {
            Code = symbolUse.Identifier,
            Range = symbolUse.Range,
            Severity = DiagnosticSeverity.Warning,
            Source = DiagnosticSource,
            RelatedInformation = new[]
            {
                new DiagnosticRelatedInformation
                {
                    Location = new Location
                    {
                        Range = symbol.IdentifierRange,
                        Uri = FileUtil.UriToUriString(symbol.SourceDocument)
                    },
                    Message = $"{symbol.Identifier} has not the same capitalisation as at least one of its uses."
                }
            },
            Message = $"{symbolUse.Identifier} has not the same capitalisation as its definition: {symbol.Identifier}."
        };
    }

    public static LspTypes.Diagnostic UnnecessaryExtern(AbstractSymbolUse symbolUse)
    {
        return new LspTypes.Diagnostic
        {
            Range = symbolUse.Range,
            Severity = DiagnosticSeverity.Warning,
            Source = DiagnosticSource,
            Message = $"Unnecessary extern declaration for procedure {symbolUse.Identifier}."
        };
    }

    public static LspTypes.Diagnostic CannotResolveSymbol(AbstractSymbolUse symbolUse)
    {
        return new LspTypes.Diagnostic
        {
            Range = symbolUse.Range,
            Severity = DiagnosticSeverity.Error,
            Source = DiagnosticSource,
            Message = $"Cannot resolve symbol {symbolUse.Identifier}."
        };
    }

    public static LspTypes.Diagnostic SymbolHasNoUse(AbstractSymbol symbol)
    {
        var severity = symbol switch
        {
            BlockNumberSymbol => DiagnosticSeverity.Hint,
            LabelSymbol => DiagnosticSeverity.Information,
            _ => DiagnosticSeverity.Warning
        };

        return new LspTypes.Diagnostic
        {
            Range = symbol.IdentifierRange,
            Severity = severity,
            Source = DiagnosticSource,
            Message = $"{symbol.Identifier} has currently no uses.",
            Tags = new[] { DiagnosticTag.Unnecessary }
        };
    }

    public static LspTypes.Diagnostic SymbolTooLong(AbstractSymbol symbol)
    {
        return new LspTypes.Diagnostic
        {
            Range = symbol.IdentifierRange,
            Severity = DiagnosticSeverity.Error,
            Source = DiagnosticSource,
            Message = $"{symbol.Identifier} is {symbol.Identifier.Length - MaxSymbolIdentifierLength} characters too long. Maximum allowed length: {MaxSymbolIdentifierLength}."
        };
    }

    public static LspTypes.Diagnostic FilenameTooLong(string filename)
    {
        return new LspTypes.Diagnostic
        {
            Range = new Range { Start = new Position(0, 0), End = new Position(0, 0) },
            Severity = DiagnosticSeverity.Error,
            Source = DiagnosticSource,
            Message = $"The filename {filename} is {filename.Length - MaxFileNameLength} characters too long. Maximum allowed length: {MaxFileNameLength}."
        };
    }

    public static LspTypes.Diagnostic MissingExtern(AbstractSymbolUse symbolUse)
    {
        return new LspTypes.Diagnostic
        {
            Range = symbolUse.Range,
            Severity = DiagnosticSeverity.Error,
            Source = DiagnosticSource,
            Message = $"Missing extern declaration for procedure {symbolUse.Identifier}."
        };
    }

    public static LspTypes.Diagnostic ProcedureFileNameMismatch(AbstractSymbol procedureSymbol, string filename)
    {
        return new LspTypes.Diagnostic
        {
            Range = procedureSymbol.IdentifierRange,
            Severity = DiagnosticSeverity.Warning,
            Source = DiagnosticSource,
            Message = $"Procedure name {procedureSymbol.Identifier} does not match file name {filename}."
        };
    }

    public static LspTypes.Diagnostic ParsingError(Range range, string message)
    {
        return new LspTypes.Diagnostic
        {
            Range = range,
            Severity = DiagnosticSeverity.Error,
            Source = DiagnosticSource,
            Message = message
        };
    }

    public static LspTypes.Diagnostic LexingError(Range range, string token)
    {
        return new LspTypes.Diagnostic
        {
            Range = range,
            Severity = DiagnosticSeverity.Error,
            Source = DiagnosticSource,
            Message = $"'{token}' is not recognized as an element of the sinumerik one nc language"
        };
    }

    public static LspTypes.Diagnostic ParameterMismatch(AbstractSymbolUse declarationUse, ProcedureSymbol procedureSymbol)
    {
        return new LspTypes.Diagnostic
        {
            Range = declarationUse.Range,
            Severity = DiagnosticSeverity.Warning,
            Source = DiagnosticSource,
            Message = $"Number of parameters does not match the number of expected parameters."
        };
    }

    public static LspTypes.Diagnostic LocalSymbolAlreadyExists(AbstractSymbol symbol, AbstractSymbol existingSymbol)
    {
        return new LspTypes.Diagnostic
        {
            Code = symbol.Identifier,
            Range = symbol.IdentifierRange,
            Message = $"A local symbol with the name {existingSymbol.Identifier} is already defined.",
            RelatedInformation = new[] { RelatedDuplicate(existingSymbol) },
            Severity = DiagnosticSeverity.Warning
        };
    }

    public static LspTypes.Diagnostic GlobalSymbolHasDuplicates(AbstractSymbol newSymbol, IList<AbstractSymbol> existingSymbols)
    {
        var relatedInformation = existingSymbols.Select(RelatedDuplicate).ToArray();

        var multiple = existingSymbols.Count > 1;
        
        return new LspTypes.Diagnostic
        {
            Code = newSymbol.Identifier,
            Range = newSymbol.IdentifierRange,
            Message = $"The global symbol {newSymbol.Identifier} has {(multiple ? existingSymbols.Count : "one")} global duplicate{(multiple ? "s" : "")}.",
            RelatedInformation = relatedInformation,
            Severity = DiagnosticSeverity.Information
        };
    }

    private static DiagnosticRelatedInformation RelatedDuplicate(AbstractSymbol existingSymbol)
    {
        return new DiagnosticRelatedInformation
        {
            Location = new Location
            {
                Range = existingSymbol.IdentifierRange,
                Uri = FileUtil.UriToUriString(existingSymbol.SourceDocument)
            },
            Message = $"{existingSymbol.Identifier} has at least one duplicate."
        };
    }
}