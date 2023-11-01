using LanceServer.Core.Document;
using LanceServer.Core.Symbol;
using LanceServer.Core.Workspace;
using OmniSharp.Extensions.LanguageServer.Protocol.Document;
using Range = OmniSharp.Extensions.LanguageServer.Protocol.Models.Range;

namespace LanceServer.RequestHandler.SemanticToken;

/// <summary>
/// Class responsible for handling semantic token requests
/// </summary>
public class SemanticTokensLogic
{
    /// <summary>
    /// Handles the semantic token request.
    /// </summary>
    public void ProcessRequest(LanguageTokenExtractedDocument document, IWorkspace workspace, SemanticTokensBuilder builder)
    {
        var localSymbols = document.SymbolTable.GetAll();
        var globalSymbols = workspace.GlobalSymbolTable.GetGlobalSymbolsOfDocument(document.Information.Uri);
        var symbolUses = document.SymbolUseTable.GetAll();

        var semanticTokens = localSymbols.Select(CreateSemanticToken).ToList();

        semanticTokens.AddRange(globalSymbols.Select(CreateSemanticToken));
        
        foreach (var symbolUse in symbolUses)
        {
            semanticTokens.AddRange(workspace.GetSymbols(symbolUse.Identifier, document.Information.Uri).Select(symbol => CreateSemanticToken(symbolUse.Range, symbol)));
        }

        var orderedSemanticTokens = semanticTokens.Distinct().OrderBy(symbolUse => symbolUse.Line).ThenBy(symbolUse => symbolUse.StartCharacter);
        
        foreach (var token in orderedSemanticTokens)
        {
            builder.Push(token.Line, token.StartCharacter, token.Length, token.Type, token.Modifiers);
        }

        builder.Commit();
    }

    private SemanticToken CreateSemanticToken(AbstractSymbol symbol)
    {
        return CreateSemanticToken(symbol.IdentifierRange, symbol);
    }

    private SemanticToken CreateSemanticToken(Range range, AbstractSymbol symbol)
    {
        var startCharacter = range.Start.Character;
        return new SemanticToken(range.Start.Line, startCharacter, range.End.Character - startCharacter, TransformType(symbol), GetModifiers(symbol));
    }

    // ReSharper disable once UnusedParameter.Local
    private int GetModifiers(AbstractSymbol symbol)
    {
        return 0;
    }

    /// <summary>
    /// Maps the symbol type to a type as defined by the LSP.
    /// </summary>
    private int TransformType(AbstractSymbol symbol)
    {
        switch (symbol)
        {
            case MacroSymbol:
                return (int)SemanticTokenTypeHelper.SemanticTokenType.Macro;
            case ProcedureSymbol:
                return (int)SemanticTokenTypeHelper.SemanticTokenType.Function;
            case ParameterSymbol:
                return (int)SemanticTokenTypeHelper.SemanticTokenType.Parameter;
            case LabelSymbol:
            case BlockNumberSymbol:
                return (int)SemanticTokenTypeHelper.SemanticTokenType.Decorator;
            case VariableSymbol:
                return (int)SemanticTokenTypeHelper.SemanticTokenType.Variable;
            default:
                throw new NotImplementedException();
        }
    }
}