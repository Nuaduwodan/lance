using System.Runtime.Serialization;
using Newtonsoft.Json;

#pragma warning disable CS8618

namespace LanceServer.Core.Configuration.DataModel;

[DataContract]
public class ServerConfiguration
{
    [DataMember(Name = "maxNumberOfProblems")]
    [JsonProperty(Required = Required.Always)]
    public int MaxNumberOfProblems { get; set; }
        
    [DataMember(Name = "trace")]
    [JsonProperty(Required = Required.Always)]
    public bool Trace { get; set; }
        
    [DataMember(Name = "placeholderPreprocessor")]
    [JsonProperty(Required = Required.Always)]
    public CustomPreprocessorConfiguration PlaceholderPreprocessor { get; set; }
        
    [DataMember(Name = "symbols")]
    [JsonProperty(Required = Required.Always)]
    public SymbolTableConfiguration SymbolTableConfiguration { get; set; }
}