using LspTypes;

namespace LanceServer.Core.Configuration
{
    public interface IConfigurationServer
    {
        [StreamJsonRpc.JsonRpcMethod("workspace/configuration")]
        Task<ConfigurationResult> GetConfigurationsAsync(ConfigurationParams configurationParams);
    }
}