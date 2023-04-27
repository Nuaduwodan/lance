using LanceServer.Core.Symbol;
using LanceServer.Core.Workspace;
using LanceServer.LanguageServerProtocol;
using LspTypes;

namespace LanceServer.Diagnostic;

public class DiagnosticHandler : IDiagnosticHandler
{
    public DocumentDiagnosticReport HandleRequest(SymbolUseExtractedDocument document, DocumentDiagnosticParams requestParams, IWorkspace workspace)
    {
        var diagnostics = new List<LspTypes.Diagnostic>();
        
        var symbolUses = document.SymbolUseTable.GetAll();
        foreach (var symbolUse in symbolUses)
        {
            if (workspace.TryGetSymbol(symbolUse.Identifier, document.Information.Uri, out var symbol))
            {
                if (symbolUse.Identifier != symbol.Identifier)
                {
                    diagnostics.Add(new LspTypes.Diagnostic
                    {
                        Range = symbolUse.Range,
                        Severity = DiagnosticSeverity.Warning,
                        Source = "Lance",
                        RelatedInformation = new[]{new DiagnosticRelatedInformation
                        {
                            Location = new Location
                            {
                                Range = symbol.IdentifierRange,
                                Uri = symbol.SourceDocument.LocalPath
                            },
                            Message = $"{symbol.Identifier} has not the same capitalisation as at least one of its uses."
                        }},
                        Message = $"{symbolUse.Identifier} has not the same capitalisation as ```{symbol.Code}```."
                    });
                }
            }
            else
            {
                diagnostics.Add(new LspTypes.Diagnostic
                {
                    Range = symbolUse.Range,
                    Severity = DiagnosticSeverity.Error,
                    Source = "Lance",
                    Message = $"Cannot resolve symbol {symbolUse.Identifier}."
                });
            }
        }

        var symbols = document.SymbolTable.GetAll().ExceptBy(document.SymbolUseTable.GetAll().Select(symbolUse => symbolUse.Identifier.ToLower()), symbol => symbol.Identifier);

        foreach (var symbol in symbols)
        {
            if (!symbolUses.Any(use => use.Identifier.Equals(symbol.Identifier, StringComparison.OrdinalIgnoreCase)))
            {
                diagnostics.Add(new LspTypes.Diagnostic
                {
                    Range = symbol.IdentifierRange,
                    Severity = DiagnosticSeverity.Information,
                    Source = "Lance",
                    Message = $"The symbol {symbol.Identifier} has currently no uses.",
                    Tags = new [] { DiagnosticTag.Unnecessary }
                });
            }
        }

        //todo file name length check
        //todo symbol name length check
        //todo check missing extern declaration and matching parameters
        //todo check if all scopes are closed again
        
        return new DocumentDiagnosticReport{Items = diagnostics.ToArray()};
    }
}