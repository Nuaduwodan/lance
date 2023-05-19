using LanceServer.Core.Document;
using LanceServer.Core.Symbol;
using LanceServer.Core.Workspace;
using LanceServer.Protocol;

namespace LanceServer.RequestHandler.Diagnostic;

public class DiagnosticHandler : IDiagnosticHandler
{
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
                    if (procedureSymbol.MayNeedExternDeclaration 
                        && symbolUse is ProcedureUse procedureUse 
                        && !symbolUses.Any(symbolUse2 => symbolUse2 is DeclarationProcedureUse && symbolUse2.ReferencesSymbol(symbolUse.Identifier)))
                    {
                        if (!symbolUses.Any(symbolUse2 => symbolUse2 is DeclarationProcedureUse 
                                                          && symbolUse2.Identifier.Equals(symbolUse.Identifier, StringComparison.OrdinalIgnoreCase)))
                        {
                            diagnostics.Add(DiagnosticMessage.MissingExtern(procedureUse));
                        }
                    }
                    else if (!procedureSymbol.MayNeedExternDeclaration && symbolUse is DeclarationProcedureUse)
                    {
                        diagnostics.Add(DiagnosticMessage.UnnecessaryExtern(symbolUse));
                    }

                    //todo check argument count
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
            if (!symbolUses.Any(use => use.ReferencesSymbol(symbol.Identifier)))
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

        foreach (var symbol in globalSymbols)
        {
            if (symbol is ProcedureSymbol procedureSymbol && filename != procedureSymbol.Identifier)
            {
                diagnostics.Add(DiagnosticMessage.ProcedureFileNameMismatch(procedureSymbol, filename));
            }
        }

        //todo check matching parameters
        //todo check if all scopes are closed again
        
        return new DocumentDiagnosticReport { Items = diagnostics.ToArray() };
    }
}