using LanceServer.Core.Workspace;
using LspTypes;

namespace LanceServer.Hover;

public interface IHoverHandler
{
    LspTypes.Hover ProcessRequest(Document document, HoverParams requestParams, IWorkspace workspace);
}