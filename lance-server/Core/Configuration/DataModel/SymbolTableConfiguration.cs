using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace LanceServer.Core.Configuration.DataModel
{
    [DataContract]
    public class SymbolTableConfiguration
    {
        [DataMember(Name = "fileEndings")]
        [JsonProperty(Required = Required.Always)]
        public string[] GlobalFileEndings { get; set; }
        
        [DataMember(Name = "subProcedureFileEndings")]
        [JsonProperty(Required = Required.Always)]
        public string[] SubProcedureFileEndings { get; set; }
        
        [DataMember(Name = "folderNames")]
        [JsonProperty(Required = Required.Always)]
        public string[] GlobalDirectories { get; set; }
    }
}