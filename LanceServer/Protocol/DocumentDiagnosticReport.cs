using System.Runtime.Serialization;
using Newtonsoft.Json;

#pragma warning disable CS8618

namespace LanceServer.Protocol;

[DataContract]
public class DocumentDiagnosticReport
{
    [DataMember(Name = "kind")]
    [JsonProperty(Required = Required.Always)]
    public string Kind = DocumentDiagnosticReportKind.Full;

    [DataMember(Name = "items")]
    [JsonProperty(Required = Required.Always)]
    public LspTypes.Diagnostic[] Items { get; set; }
}