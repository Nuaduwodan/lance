using System.Runtime.Serialization;
using LanceServer.Preprocessor;
using Newtonsoft.Json;

namespace LanceServer.Core.Configuration.DataModel
{
    [DataContract]
    public class CustomPreprocessorConfiguration
    {
        [DataMember(Name = "placeholderType")]
        [JsonProperty(Required = Required.Always)]
        public PlaceholderType PlaceholderType { get; set; }
        
        [DataMember(Name = "fileExtensions")]
        [JsonProperty(Required = Required.Always)]
        public string[] FileExtensions { get; set; }

        [DataMember(Name = "placeholders")]
        [JsonProperty(Required = Required.Always)]
        public string[] Placeholders { get; set; }
    }
}