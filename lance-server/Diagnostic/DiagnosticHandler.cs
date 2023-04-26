using LanceServer.Core.Workspace;
using LanceServer.LanguageServerProtocol;
using LspTypes;

namespace LanceServer.Diagnostic;

public class DiagnosticHandler : IDiagnosticHandler
{
    public DocumentDiagnosticReport HandleRequest(SymbolUseExtractedDocument document, DocumentDiagnosticParams requestParams, IWorkspace workspace)
    {
        var diagnostics = new List<LspTypes.Diagnostic>();
        
        foreach (var symbolUse in document.SymbolUseTable.GetAll())
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
                        Message = $"{symbolUse.Identifier} has not the same capitalisation as {symbol.Identifier}."
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

        var unusedSymbols = document.SymbolTable.GetAll().ExceptBy(document.SymbolUseTable.GetAll().Select(symbolUse => symbolUse.Identifier.ToLower()), symbol => symbol.Identifier);

        foreach (var unusedSymbol in unusedSymbols)
        {
            diagnostics.Add(new LspTypes.Diagnostic
            {
                Range = unusedSymbol.SymbolRange,
                Severity = DiagnosticSeverity.Warning,
                Source = "Lance",
                Message = $"The symbol {unusedSymbol.Identifier} has currently no uses."
            });
        }

        return new DocumentDiagnosticReport{Items = diagnostics.ToArray()};
    }
}