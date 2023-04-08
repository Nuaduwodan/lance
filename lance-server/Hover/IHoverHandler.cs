using LanceServer.Core.Workspace;
using LspTypes;

namespace LanceServer.Hover;

public interface IHoverHandler
{
    LspTypes.Hover ProcessRequest(SymbolisedDocument document, HoverParams requestParams, IWorkspace workspace);
}