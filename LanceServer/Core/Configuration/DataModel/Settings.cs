using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace LanceServer.Core.Configuration.DataModel;

[DataContract]
public class Settings
{
    [DataMember(Name = "lance")]
    [JsonProperty(Required = Required.Always)]
    public ServerConfiguration Lance { get; set; }
}