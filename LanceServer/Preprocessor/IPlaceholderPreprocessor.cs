using LanceServer.Core.Configuration.DataModel;
using LanceServer.Core.Document;

namespace LanceServer.Preprocessor;

/// <summary>
/// The placeholder preprocessor to process documents with language foreign tokens.
/// </summary>
public interface IPlaceholderPreprocessor
{
    /// <summary>
    /// Filters out placeholders as configured in <see cref="CustomPreprocessorConfiguration"/> and replaces them with a temporary token.
    /// </summary>
    /// <param name="document">The document to be filtered.</param>
    public PlaceholderPreprocessedDocument Filter(ReadDocument document);
}