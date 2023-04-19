﻿using LanceServer.Core.Workspace;
using LspTypes;
using Range = LspTypes.Range;

namespace LanceServer.GoToDefinition
{
    public class GotoDefinitionHandler : IGotoDefinitionHandler
    {
        public LocationLink[] HandleRequest(SymbolUseExtractedDocument document, TypeDefinitionParams requestParams, IWorkspace workspace)
        {
            if (document.SymbolUseTable.TryGetSymbol(requestParams.Position, out var symbolUse))
            {
                var symbol = workspace.GetSymbol(symbolUse.Identifier.ToLower(), document.Information.Uri);
                var locationLink = new LocationLink()
                {
                    OriginSelectionRange = symbolUse.Position,
                    TargetUri = symbol.SourceDocument.LocalPath,
                    TargetRange = symbol.SymbolRange,
                    TargetSelectionRange = symbol.IdentifierRange
                };

                return new[] { locationLink };
            }

            return new LocationLink[]{};
        }
    }
}