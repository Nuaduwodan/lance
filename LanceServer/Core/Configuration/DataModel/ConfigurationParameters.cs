using System.Runtime.Serialization;
using Newtonsoft.Json;

#pragma warning disable CS8618

namespace LanceServer.Core.Configuration.DataModel;

/// <summary>
/// The parameters for a configuration changed request
/// </summary>
[DataContract]
public class ConfigurationParameters
{
    [DataMember(Name = "settings")]
    [JsonProperty(Required = Required.Always)]
    public Settings Settings { get; set; }
}