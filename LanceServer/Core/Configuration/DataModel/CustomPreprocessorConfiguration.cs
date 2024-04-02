namespace LanceServer.Core.Configuration.DataModel;

/// <summary>
/// The placeholder preprocessor configuration
/// </summary>
public record CustomPreprocessorConfiguration(PlaceholderType PlaceholderType, string[] FileExtensions, string[] Placeholders);