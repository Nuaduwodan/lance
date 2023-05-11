using System.Runtime.Serialization;
using LspTypes;
using Newtonsoft.Json;

namespace LanceServer.LanguageServerProtocol;

[DataContract]
public class DiagnosticRegistrationOptions : DiagnosticOptions, ITextDocumentRegistrationOptions, IStaticRegistrationOptions
{
    /// <inheritdoc />
    [DataMember(Name = "documentSelector")]
    [JsonProperty(Required = Required.Always)]
    public DocumentFilter[]? DocumentSelector { get; set; }

    /// <inheritdoc />
    [DataMember(Name = "id")]
    [JsonProperty(Required = Required.Default)]
    public string? Id { get; set; }
}