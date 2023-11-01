using LanceServer.Core.Document;
using LanceServer.Core.Workspace;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;

namespace LanceServer.RequestHandler.GoToDefinition;

/// <summary>
/// Handles goto definition requests
/// </summary>
public class GotoDefinitionLogic
{
    /// <summary>
    /// Handle a goto definition request
    /// </summary>
    /// <param name="document">The document with the necessary symbol information</param>
    /// <param name="position">The position where the goto is requested</param>
    /// <param name="workspace">The workspace</param>
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