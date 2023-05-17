using LanceServer.Core.Document;

namespace LanceServer.Preprocessor;

public interface IPlaceholderPreprocessor
{
    public PlaceholderPreprocessedDocument Filter(ReadDocument document);
}