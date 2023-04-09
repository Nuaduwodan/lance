using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace LanceServer.Core.Configuration.DataModel
{
    [DataContract]
    public class SymbolTableConfiguration
    {
        [DataMember(Name = "fileExtensions")]
        [JsonProperty(Required = Required.Always)]
        public string[] GlobalFileExtensions { get; set; }
        
        [DataMember(Name = "subProcedureFileExtensions")]
        [JsonProperty(Required = Required.Always)]
        public string[] SubProcedureFileExtensions { get; set; }
        
        [DataMember(Name = "folderNames")]
        [JsonProperty(Required = Required.Always)]
        public string[] GlobalDirectories { get; set; }
    }
}