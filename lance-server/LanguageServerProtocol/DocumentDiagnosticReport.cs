using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace LanceServer.LanguageServerProtocol;

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