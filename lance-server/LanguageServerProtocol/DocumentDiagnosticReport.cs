using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace LanceServer.LanguageServerProtocol;

[DataContract]
public class DocumentDiagnosticReport
{
    [DataMember(Name = "kind")]
    [JsonProperty(Required = Required.Always)]
    public DocumentDiagnosticReportKind Kind = DocumentDiagnosticReportKind.Full;

    [DataMember(Name = "resultId")]
    [JsonProperty(Required = Required.Default)]
    public string? ResultId { get; set; }

    [DataMember(Name = "partialResultToken")]
    [JsonProperty(Required = Required.Always)]
    public LspTypes.Diagnostic[] Items { get; set; }
}