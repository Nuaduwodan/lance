using LanceServer.Core.Document;
using LanceServer.Core.Symbol;
using LanceServer.Core.Workspace;
using LanceServer.Protocol;
using LspTypes;
using Range = LspTypes.Range;

namespace LanceServer.RequestHandler.Diagnostic;

public class DiagnosticHandler : IDiagnosticHandler
{
    private const int MaxFileNameLength = 24;
    private const int MaxAxisNameLength = 8;
    private const int MaxSymbolIdentifierLength = 31;
    
    public DocumentDiagnosticReport HandleRequest(LanguageTokenExtractedDocument document, IWorkspace workspace)
    {
        var diagnostics = new List<LspTypes.Diagnostic>();
        
        diagnostics.AddRange(document.ParserDiagnostics);
        
        var symbolUses = document.SymbolUseTable.GetAll();
        foreach (var symbolUse in symbolUses)
        {
            var referencedSymbols = workspace.GetSymbols(symbolUse.Identifier, document.Information.Uri).ToList();
            if (referencedSymbols.Any())
            {
                if (symbolUse.Identifier != referencedSymbols.First().Identifier)
                {
                    diagnostics.Add(SymbolHasDifferentCapitalisation(symbolUse, referencedSymbols.First()));
                }

                if (referencedSymbols.First() is ProcedureSymbol procedureSymbol)
                {
                    if (procedureSymbol.NeedsExternDeclaration 
                        && symbolUse is ProcedureUse procedureUse 
                        && !symbolUses.Any(symbolUse2 => symbolUse2 is DeclarationProcedureUse && symbolUse2.ReferencesSymbol(symbolUse.Identifier)))
                    {
                        if (!symbolUses.Any(symbolUse2 => symbolUse2 is DeclarationProcedureUse 
                                                          && symbolUse2.Identifier.Equals(symbolUse.Identifier, StringComparison.OrdinalIgnoreCase)))
                        {
                            diagnostics.Add(MissingExtern(procedureUse));
                        }
                    }
                    else if (!procedureSymbol.NeedsExternDeclaration && symbolUse is DeclarationProcedureUse)
                    {
                        diagnostics.Add(UnnecessaryExtern(symbolUse));
                    }

                    //todo check argument count
                }
            }
            else
            {
                diagnostics.Add(CannotResolveSymbol(symbolUse));
            }
        }

        var localSymbols = document.SymbolTable.GetAll();

        foreach (var symbol in localSymbols)
        {
            if (!symbolUses.Any(use => use.ReferencesSymbol(symbol.Identifier)))
            {
                diagnostics.Add(SymbolHasNoUse(symbol));
            }

            if (symbol.Identifier.Length > MaxSymbolIdentifierLength)
            {
                diagnostics.Add(SymbolTooLong(symbol));
            }
        }

        var filename = Path.GetFileNameWithoutExtension(document.Information.Uri.LocalPath);
        if (filename.Length > MaxFileNameLength)
        {
            diagnostics.Add(FilenameTooLong(filename));
        }

        //todo check matching parameters
        //todo check if all scopes are closed again
        
        return new DocumentDiagnosticReport { Items = diagnostics.ToArray() };
    }

    private static LspTypes.Diagnostic SymbolHasDifferentCapitalisation(AbstractSymbolUse symbolUse, AbstractSymbol symbol)
    {
        return new LspTypes.Diagnostic
        {
            Code = symbolUse.Identifier,
            Range = symbolUse.Range,
            Severity = DiagnosticSeverity.Warning,
            Source = "Lance",
            RelatedInformation = new[]
            {
                new DiagnosticRelatedInformation
                {
                    Location = new Location
                    {
                        Range = symbol.IdentifierRange,
                        Uri = symbol.SourceDocument.AbsolutePath
                    },
                    Message = $"{symbol.Identifier} has not the same capitalisation as at least one of its uses."
                }
            },
            Message = $"{symbolUse.Identifier} has not the same capitalisation as its definition: {symbol.Identifier}."
        };
    }

    private static LspTypes.Diagnostic UnnecessaryExtern(AbstractSymbolUse symbolUse)
    {
        return new LspTypes.Diagnostic
        {
            Range = symbolUse.Range,
            Severity = DiagnosticSeverity.Warning,
            Source = "Lance",
            Message = $"Unnecessary extern declaration for procedure {symbolUse.Identifier}."
        };
    }

    private static LspTypes.Diagnostic CannotResolveSymbol(AbstractSymbolUse symbolUse)
    {
        return new LspTypes.Diagnostic
        {
            Range = symbolUse.Range,
            Severity = DiagnosticSeverity.Error,
            Source = "Lance",
            Message = $"Cannot resolve symbol {symbolUse.Identifier}."
        };
    }

    private static LspTypes.Diagnostic SymbolHasNoUse(AbstractSymbol symbol)
    {
        var severity = symbol is LabelSymbol or BlockNumberSymbol ? DiagnosticSeverity.Hint : DiagnosticSeverity.Warning;
        return new LspTypes.Diagnostic
        {
            Range = symbol.IdentifierRange,
            Severity = severity,
            Source = "Lance",
            Message = $"The symbol {symbol.Identifier} has currently no uses.",
            Tags = new[] { DiagnosticTag.Unnecessary }
        };
    }

    private static LspTypes.Diagnostic SymbolTooLong(AbstractSymbol symbol)
    {
        return new LspTypes.Diagnostic
        {
            Range = symbol.IdentifierRange,
            Severity = DiagnosticSeverity.Error,
            Source = "Lance",
            Message = $"The symbol {symbol.Identifier} is {symbol.Identifier.Length - MaxSymbolIdentifierLength} characters longer than the maximum allowed length of {MaxSymbolIdentifierLength}."
        };
    }

    private static LspTypes.Diagnostic FilenameTooLong(string filename)
    {
        return new LspTypes.Diagnostic
        {
            Range = new Range { Start = new Position(0, 0), End = new Position(0, 0) },
            Severity = DiagnosticSeverity.Error,
            Source = "Lance",
            Message = $"The filename of the file {filename} is {filename.Length - MaxFileNameLength} characters longer than the maximum allowed length of {MaxFileNameLength}."
        };
    }

    private static LspTypes.Diagnostic MissingExtern(AbstractSymbolUse symbolUse)
    {
        return new LspTypes.Diagnostic
        {
            Range = symbolUse.Range,
            Severity = DiagnosticSeverity.Error,
            Source = "Lance",
            Message = $"Missing extern declaration for procedure {symbolUse.Identifier}."
        };
    }
}