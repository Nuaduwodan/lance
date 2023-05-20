using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LanceServer.Core.Configuration;

/// <summary>
/// The method of how the configured placeholders should be interpreted.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum PlaceholderType
{
    String,
    RegEx
}