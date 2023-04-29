﻿using LanceServer.Core.Symbol;
using LanceServer.Core.Workspace;
using LanceServer.LanguageServerProtocol;
using LspTypes;
using Range = LspTypes.Range;

namespace LanceServer.Diagnostic;

public class DiagnosticHandler : IDiagnosticHandler
{
    private const int MaxFileNameLength = 24;
    private const int MaxSymbolIdentifierLength = 31;
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
                        Message = $"{symbolUse.Identifier} has not the same capitalisation as its definition: {symbol.Identifier}."
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

        var symbols = document.SymbolTable.GetAll();

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

            if (symbol.Identifier.Length >= MaxSymbolIdentifierLength)
            {
                diagnostics.Add(new LspTypes.Diagnostic
                {
                    Range = symbol.IdentifierRange,
                    Severity = DiagnosticSeverity.Error,
                    Source = "Lance",
                    Message = $"The symbol {symbol.Identifier} is longer than the maximum allowed length of {MaxSymbolIdentifierLength}."
                });
            }
        }

        var filename = Path.GetFileName(document.Information.Uri.LocalPath);
        if (filename.Length >= MaxFileNameLength)
        {
            diagnostics.Add(new LspTypes.Diagnostic
            {
                Range = new Range{Start = new Position(0, 0), End = new Position(0, 0)},
                Severity = DiagnosticSeverity.Error,
                Source = "Lance",
                Message = $"The filename of the file {filename} is longer than the maximum allowed length of {MaxSymbolIdentifierLength}."
            });
        }
        
        //todo check missing extern declaration and matching parameters
        //todo check if all scopes are closed again
        
        return new DocumentDiagnosticReport{Items = diagnostics.ToArray()};
    }
}