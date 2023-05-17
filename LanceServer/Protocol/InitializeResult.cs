using System.Runtime.Serialization;
using LspTypes;
using Newtonsoft.Json;

#pragma warning disable CS8618

namespace LanceServer.Protocol;

[DataContract]
public class InitializeResult
{
    [DataMember(Name = "capabilities")]
    [JsonProperty(Required = Required.Always)]
    public ServerCapabilities Capabilities { get; set; }

    [DataMember(Name = "serverInfo")]
    [JsonProperty(Required = Required.Default)]
    public _InitializeResults_ServerInfo? ServerInfo { get; set; }
}