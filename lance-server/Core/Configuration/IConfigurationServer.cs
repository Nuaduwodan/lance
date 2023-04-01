﻿using LanceServer.Core.Configuration.DataModel;
using LspTypes;

namespace LanceServer.Core.Configuration
{
    public interface IConfigurationServer
    {
        [StreamJsonRpc.JsonRpcMethod("workspace/configuration")]
        Task<ConfigurationParameters> GetConfigurationsAsync(LspTypes.ConfigurationParams configurationParams);
    }
}