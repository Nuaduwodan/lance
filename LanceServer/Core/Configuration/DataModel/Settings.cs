using System.Runtime.Serialization;
using Newtonsoft.Json;

#pragma warning disable CS8618

namespace LanceServer.Core.Configuration.DataModel;

/// <summary>
/// The configuration object
/// </summary>
[DataContract]
public class Settings
{
    [DataMember(Name = "lance")]
    [JsonProperty(Required = Required.Always)]
    public ServerConfiguration Lance { get; set; }
}