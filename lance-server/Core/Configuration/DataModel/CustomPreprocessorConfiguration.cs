using System.Runtime.Serialization;
using LanceServer.Preprocessor;
using Newtonsoft.Json;

namespace LanceServer.Core.Configuration.DataModel
{
    [DataContract]
    public class CustomPreprocessorConfiguration
    {
        [DataMember(Name = "keyType")]
        [JsonProperty(Required = Required.Always)]
        public KeyType KeyType { get; set; }
        
        [DataMember(Name = "fileEndings")]
        [JsonProperty(Required = Required.Always)]
        public string[] FileEndings { get; set; }

        [DataMember(Name = "placeholders")]
        [JsonProperty(Required = Required.Always)]
        public string[] Placeholders { get; set; }
    }
}