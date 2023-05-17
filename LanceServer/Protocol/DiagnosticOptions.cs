using System.Runtime.Serialization;
using LspTypes;
using Newtonsoft.Json;

namespace LanceServer.Protocol;

/// <summary>
/// Diagnostic options.
///
/// @since 3.17.0
/// </summary>
[DataContract]
public class DiagnosticOptions : WorkDoneProgressOptions
{
	/// <summary>
	/// An optional identifier under which the diagnostics are managed by the client.
	/// </summary>
	[DataMember(Name = "identifier")]
	[JsonProperty(Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Ignore)]
	public string? Identifier { get; set; }
	
	/// <summary>
	/// Whether the language has inter file dependencies meaning that editing code in one file can result in a different diagnostic set in another file.
	/// Inter file dependencies are common for most programming languages and typically uncommon for linters.
	/// </summary>
	[DataMember(Name = "interFileDependencies")]
	[JsonProperty(Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Ignore)]
	public bool? InterFileDependencies { get; set; }
	
	/// <summary>
	/// The server provides support for workspace diagnostics as well.
	/// </summary>
	[DataMember(Name = "workspaceDiagnostics")]
	[JsonProperty(Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Ignore)]
	public bool? WorkspaceDiagnostics { get; set; }
}