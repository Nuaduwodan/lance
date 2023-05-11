using Newtonsoft.Json;

namespace LanceServer.Core.Configuration.DataModel;

public class TokenDocumentation
{
    [JsonRequired]
    public string Code { get; set; }
    
    [JsonRequired]
    public LanguageTokenType Type { get; set; }

    [JsonRequired]
    public string Description { get; set; }

    [JsonRequired]
    public ModalMode Modal { get; set; }

    [JsonRequired]
    public SubProcedureAvailability SubProcedure { get; set; }

    [JsonRequired]
    public SyncActionAvailability SyncAction { get; set; }

    [JsonRequired] 
    public Manual[] Manual { get; set; } = Array.Empty<Manual>();
}