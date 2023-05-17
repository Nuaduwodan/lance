using System.Runtime.Serialization;
using LspTypes;
using Newtonsoft.Json;

#pragma warning disable CS8618

namespace LanceServer.Protocol;

[DataContract]
public class DocumentDiagnosticParams : WorkDoneProgressParams, IPartialResultParams
{
    [DataMember(Name = "partialResultToken")]
    [JsonProperty(Required = Required.Default)]
    public SumType<int, string> PartialResultToken { get; set; }
    
    [DataMember(Name = "textDocument")]
    [JsonProperty(Required = Required.Always)]
    public TextDocumentIdentifier TextDocument { get; set; }
    
    [DataMember(Name = "identifier")]
    [JsonProperty(Required = Required.Default)]
    public string? Identifier { get; set; }
    
    [DataMember(Name = "previousResultId")]
    [JsonProperty(Required = Required.Default)]
    public string? PreviousResultId { get; set; }
}