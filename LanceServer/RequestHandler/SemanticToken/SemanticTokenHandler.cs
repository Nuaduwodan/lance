using LanceServer.Core.Document;
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

        var semanticTokens = new List<SemanticToken>();

        foreach (var symbol in localSymbols)
        {
            var startCharacter = symbol.IdentifierRange.Start.Character;
            semanticTokens.Add(new SemanticToken(symbol.IdentifierRange.Start.Line, startCharacter, symbol.IdentifierRange.End.Character - startCharacter, TransformType(symbol), GetModifiers(symbol)));
        }
        
        foreach (var symbol in globalSymbols)
        {
            var startCharacter = symbol.IdentifierRange.Start.Character;
            semanticTokens.Add(new SemanticToken(symbol.IdentifierRange.Start.Line, startCharacter, symbol.IdentifierRange.End.Character - startCharacter, TransformType(symbol), GetModifiers(symbol)));
        }
        
        foreach (var symbolUse in symbolUses)
        {
            if (workspace.TryGetSymbol(symbolUse.Identifier, document.Information.Uri, out var symbol))
            {
                var startCharacter = symbolUse.Range.Start.Character;
                semanticTokens.Add(new SemanticToken(symbolUse.Range.Start.Line, startCharacter, symbolUse.Range.End.Character - startCharacter, TransformType(symbol), GetModifiers(symbol)));
            }
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

    private uint GetModifiers(ISymbol symbol)
    {
        return 0;
    }

    /// <summary>
    /// Maps the symbol type to a type as defined by the LSP.
    /// </summary>
    private uint TransformType(ISymbol symbol)
    {
        switch (symbol.Type)
        {
            case SymbolType.Variable:
                return (uint) SemanticTokenTypeHelper.SemanticTokenType.Variable;
            case SymbolType.Macro:
                return (uint)SemanticTokenTypeHelper.SemanticTokenType.Macro;
            case SymbolType.Procedure:
                return (uint)SemanticTokenTypeHelper.SemanticTokenType.Function;
            case SymbolType.Parameter:
                return (uint)SemanticTokenTypeHelper.SemanticTokenType.Parameter;
            case SymbolType.Label:
                return (uint)SemanticTokenTypeHelper.SemanticTokenType.Decorator;
            case SymbolType.BlockNumber:
                return (uint)SemanticTokenTypeHelper.SemanticTokenType.Decorator;
            case SymbolType.Error:
            default:
                return (uint)SemanticTokenTypeHelper.SemanticTokenType.Variable;
        }
    }
}