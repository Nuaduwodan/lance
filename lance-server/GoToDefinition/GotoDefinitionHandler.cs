using LanceServer.Core.Workspace;
using LspTypes;

namespace LanceServer.GoToDefinition;

public class GotoDefinitionHandler : IGotoDefinitionHandler
{
    public LocationLink[] HandleRequest(SymbolUseExtractedDocument document, TypeDefinitionParams requestParams, IWorkspace workspace)
    {
        if (document.SymbolUseTable.TryGetSymbol(requestParams.Position, out var symbolUse) &&
            workspace.TryGetSymbol(symbolUse.Identifier.ToLower(), document.Information.Uri, out var symbol))
        {
            var locationLink = new LocationLink()
            {
                OriginSelectionRange = symbolUse.Range,
                TargetUri = FileUtil.UriToUriString(symbol.SourceDocument),
                TargetRange = symbol.SymbolRange,
                TargetSelectionRange = symbol.IdentifierRange
            };

            return new[] { locationLink };
        }

        return new LocationLink[]{};
    }
}