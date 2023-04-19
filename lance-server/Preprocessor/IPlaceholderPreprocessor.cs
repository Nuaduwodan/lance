using LanceServer.Core.Workspace;

namespace LanceServer.Preprocessor;

public interface IPlaceholderPreprocessor
{
    public PlaceholderPreprocessedDocument Filter(Document document);
}