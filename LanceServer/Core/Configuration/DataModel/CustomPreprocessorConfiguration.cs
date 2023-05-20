using System.Runtime.Serialization;
using Newtonsoft.Json;

#pragma warning disable CS8618

namespace LanceServer.Core.Configuration.DataModel;

/// <summary>
/// The placeholder preprocessor configuration
/// </summary>
[DataContract]
public class CustomPreprocessorConfiguration
{
    [DataMember(Name = "placeholderType")]
    [JsonProperty(Required = Required.Always)]
    public PlaceholderType PlaceholderType { get; set; }
        
    [DataMember(Name = "fileExtensions")]
    [JsonProperty(Required = Required.Always)]
    public string[] FileExtensions { get; set; }

    [DataMember(Name = "placeholders")]
    [JsonProperty(Required = Required.Always)]
    public string[] Placeholders { get; set; }
}