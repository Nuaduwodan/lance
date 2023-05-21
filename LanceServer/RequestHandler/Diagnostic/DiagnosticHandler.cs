using LanceServer.Core.Document;
using LanceServer.Core.Symbol;
using LanceServer.Core.Workspace;
using LanceServer.Protocol;

namespace LanceServer.RequestHandler.Diagnostic;

/// <inheritdoc />
public class DiagnosticHandler : IDiagnosticHandler
{
    /// <inheritdoc />
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
                    diagnostics.Add(DiagnosticMessage.SymbolHasDifferentCapitalisation(symbolUse, referencedSymbols.First()));
                }

                if (referencedSymbols.First() is ProcedureSymbol procedureSymbol)
                {
                    if (symbolUse is ProcedureUse procedureUse)
                    {
                        if (procedureSymbol.MayNeedExternDeclaration
                            && procedureUse.Arguments.Any()
                            && !symbolUses.Any(symbolUse2 => symbolUse2 is DeclarationProcedureUse && symbolUse2.IsReferencedBy(symbolUse.Identifier)))
                        {
                            diagnostics.Add(DiagnosticMessage.MissingExtern(procedureUse));
                        }

                        if (!procedureSymbol.ArgumentsMatchParameters(procedureUse.Arguments))
                        {
                            diagnostics.Add(DiagnosticMessage.ParameterMismatch(procedureUse, procedureSymbol));
                        }
                    }
                    else if (symbolUse is DeclarationProcedureUse declarationUse)
                    {
                        if (!procedureSymbol.MayNeedExternDeclaration || !symbolUses.Any(symbolUse2 => symbolUse2.IsReferencedBy(declarationUse.Identifier)))
                        {
                            diagnostics.Add(DiagnosticMessage.UnnecessaryExtern(symbolUse));
                        }

                        if (!procedureSymbol.ArgumentsMatchParameters(declarationUse.Arguments))
                        {
                            diagnostics.Add(DiagnosticMessage.ParameterMismatch(declarationUse, procedureSymbol));
                        }
                    }
                    else
                    {
                        if (!procedureSymbol.ArgumentsMatchParameters(Array.Empty<ProcedureUseArgument>()))
                        {
                            diagnostics.Add(DiagnosticMessage.ParameterMismatch(symbolUse, procedureSymbol));
                        }
                    }
                }
            }
            else
            {
                diagnostics.Add(DiagnosticMessage.CannotResolveSymbol(symbolUse));
            }
        }

        var localSymbols = document.SymbolTable.GetAll();

        foreach (var symbol in localSymbols)
        {
            if (!symbolUses.Any(use => use.IsReferencedBy(symbol.Identifier)))
            {
                diagnostics.Add(DiagnosticMessage.SymbolHasNoUse(symbol));
            }

            if (symbol.Identifier.Length > DiagnosticMessage.MaxSymbolIdentifierLength)
            {
                diagnostics.Add(DiagnosticMessage.SymbolTooLong(symbol));
            }
        }

        var filename = Path.GetFileNameWithoutExtension(document.Information.Uri.LocalPath);
        if (filename.Length > DiagnosticMessage.MaxFileNameLength)
        {
            diagnostics.Add(DiagnosticMessage.FilenameTooLong(filename));
        }
        
        var globalSymbols = workspace.GlobalSymbolTable.GetGlobalSymbolsOfDocument(document.Information.Uri);

        foreach (var globalSymbol in globalSymbols)
        {
            var duplicateSymbols = workspace.GlobalSymbolTable.GetGlobalSymbols(globalSymbol.Identifier).Where(duplicate => !duplicate.Equals(globalSymbol)).ToList();
            if (duplicateSymbols.Count >= 1)
            {
                diagnostics.Add(DiagnosticMessage.GlobalSymbolHasDuplicates(globalSymbol, duplicateSymbols));
            }
            
            if (globalSymbol is ProcedureSymbol procedureSymbol && filename != procedureSymbol.Identifier)
            {
                diagnostics.Add(DiagnosticMessage.ProcedureFileNameMismatch(procedureSymbol, filename));
            }
        }
        
        return new DocumentDiagnosticReport { Items = diagnostics.ToArray() };
    }
}