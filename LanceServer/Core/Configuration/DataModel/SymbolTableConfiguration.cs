using System.Runtime.Serialization;
using Newtonsoft.Json;

#pragma warning disable CS8618

namespace LanceServer.Core.Configuration.DataModel;

/// <summary>
/// The configuration for the scopes of symbols of different files
/// </summary>
[DataContract]
public class SymbolTableConfiguration
{
    [DataMember(Name = "definitionFileExtensions")]
    [JsonProperty(Required = Required.Always)]
    public string[] DefinitionFileExtensions { get; set; }
        
    [DataMember(Name = "subProcedureFileExtensions")]
    [JsonProperty(Required = Required.Always)]
    public string[] SubProcedureFileExtensions { get; set; }
        
    [DataMember(Name = "mainProcedureFileExtensions")]
    [JsonProperty(Required = Required.Always)]
    public string[] MainProcedureFileExtensions { get; set; }
        
    [DataMember(Name = "manufacturerCyclesDirectories")]
    [JsonProperty(Required = Required.Always)]
    public string[] ManufacturerCyclesDirectories { get; set; }
}