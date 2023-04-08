using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LanceServer.Preprocessor
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PlaceholderType
    {
        String,
        RegEx
    }
}