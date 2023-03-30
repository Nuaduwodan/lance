using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace LanceServer.Core.Configuration
{
    [DataContract]
    public class ConfigurationResult
    {
        [DataMember(Name = "maxNumberOfProblems")]
        [JsonProperty(Required = Required.Always)]
        public int MaxNumberOfProblems { get; set; }
    }
}