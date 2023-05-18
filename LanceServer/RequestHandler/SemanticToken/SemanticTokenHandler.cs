﻿using LanceServer.Core.Document;
using LanceServer.Core.Symbol;
using LanceServer.Core.Workspace;
using LspTypes;

namespace LanceServer.RequestHandler.SemanticToken;

/// <summary>
/// Class responsible for handling semantic token requests
/// </summary>
public class SemanticTokenHandler : ISemanticTokenHandler
{
    public SemanticTokens ProcessRequest(LanguageTokenExtractedDocument document, IWorkspace workspace)
    {
        var localSymbols = document.SymbolTable.GetAll();
        var globalSymbols = workspace.GetGlobalSymbolsOfDocument(document.Information.Uri);
        var symbolUses = document.SymbolUseTable.GetAll();

        var semanticTokens = localSymbols.Select(CreateSemanticToken).ToList();

        semanticTokens.AddRange(globalSymbols.Select(CreateSemanticToken));
        
        foreach (var symbolUse in symbolUses)
        {
            semanticTokens.AddRange(workspace.GetSymbols(symbolUse.Identifier, document.Information.Uri).Select(CreateSemanticToken));
        }

        var orderedSemanticTokens = semanticTokens.OrderBy(symbolUse => symbolUse.Line).ThenBy(symbolUse => symbolUse.StartCharacter);
        
        var tokenData = new SemanticTokenData();
        uint previousLine = 0;
        uint previousCharacter = 0;
        foreach (var semanticToken in orderedSemanticTokens)
        {
            var deltaLine = semanticToken.Line - previousLine;
            
            if (deltaLine > 0)
            {
                previousLine = semanticToken.Line;
                previousCharacter = 0;
            }

            var deltaCharacter = semanticToken.StartCharacter - previousCharacter;
            
            previousCharacter = semanticToken.StartCharacter;
            
            tokenData.AddElement(new SemanticTokenDataElement(deltaLine, deltaCharacter, semanticToken.Length, semanticToken.Type, semanticToken.Modifiers));
        }
                    
        return new SemanticTokens
        {
            Data = tokenData.ToDataFormat()
        };
    }

    private SemanticToken CreateSemanticToken(AbstractSymbol symbol)
    {
        var startCharacter = symbol.IdentifierRange.Start.Character;
        return new SemanticToken(symbol.IdentifierRange.Start.Line, startCharacter, symbol.IdentifierRange.End.Character - startCharacter, TransformType(symbol), GetModifiers(symbol));
    }

    private uint GetModifiers(AbstractSymbol symbol)
    {
        return 0;
    }

    /// <summary>
    /// Maps the symbol type to a type as defined by the LSP.
    /// </summary>
    private uint TransformType(AbstractSymbol symbol)
    {
        switch (symbol)
        {
            case MacroSymbol:
                return (uint)SemanticTokenTypeHelper.SemanticTokenType.Macro;
            case ProcedureSymbol:
                return (uint)SemanticTokenTypeHelper.SemanticTokenType.Function;
            case ParameterSymbol:
                return (uint)SemanticTokenTypeHelper.SemanticTokenType.Parameter;
            case LabelSymbol:
            case BlockNumberSymbol:
                return (uint)SemanticTokenTypeHelper.SemanticTokenType.Decorator;
            case VariableSymbol:
                return (uint)SemanticTokenTypeHelper.SemanticTokenType.Variable;
            default:
                throw new NotImplementedException();
        }
    }
}