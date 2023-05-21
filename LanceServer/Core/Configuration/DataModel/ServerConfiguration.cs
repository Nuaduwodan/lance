using System.Runtime.Serialization;
using Newtonsoft.Json;

#pragma warning disable CS8618

namespace LanceServer.Core.Configuration.DataModel;

/// <summary>
/// The configuration of the application
/// </summary>
[DataContract]
public class ServerConfiguration
{
    [DataMember(Name = "placeholderPreprocessor")]
    [JsonProperty(Required = Required.Always)]
    public CustomPreprocessorConfiguration PlaceholderPreprocessor { get; set; }
        
    [DataMember(Name = "symbols")]
    [JsonProperty(Required = Required.Always)]
    public SymbolTableConfiguration SymbolTableConfiguration { get; set; }
}