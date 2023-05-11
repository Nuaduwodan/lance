using LanceServer.Core.Document;
using LanceServer.Core.Workspace;

namespace LanceServer.Preprocessor;

public interface IPlaceholderPreprocessor
{
    public PlaceholderPreprocessedDocument Filter(ReadDocument document);
}