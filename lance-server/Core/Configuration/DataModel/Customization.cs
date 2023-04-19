using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace LanceServer.Core.Configuration.DataModel;

[DataContract]
public class Customization
{
    [DataMember(Name = "placeholderPreprocessor")]
    [JsonProperty(Required = Required.Always)]
    public CustomPreprocessorConfiguration PlaceholderPreprocessor { get; set; }
        
    [DataMember(Name = "globalSymbol")]
    [JsonProperty(Required = Required.Always)]
    public SymbolTableConfiguration SymbolTableConfiguration { get; set; }
}