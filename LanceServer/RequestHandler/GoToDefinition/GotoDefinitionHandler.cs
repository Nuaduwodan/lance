using LanceServer.Core.Document;
using LanceServer.Core.Workspace;
using LspTypes;

namespace LanceServer.RequestHandler.GoToDefinition;

public class GotoDefinitionHandler : IGotoDefinitionHandler
{
    public LocationLink[] HandleRequest(LanguageTokenExtractedDocument document, Position position, IWorkspace workspace)
    {
        if (!document.SymbolUseTable.TryGetSymbol(position, out var symbolUse))
        {
            return Array.Empty<LocationLink>();
        }

        var locationLinks = workspace.GetSymbols(symbolUse.Identifier, document.Information.Uri).Select(symbol => new LocationLink
        {
            OriginSelectionRange = symbolUse.Range,
            TargetUri = FileUtil.UriToUriString(symbol.SourceDocument),
            TargetRange = symbol.SymbolRange,
            TargetSelectionRange = symbol.IdentifierRange
        }).ToArray();

        return locationLinks;
    }
}