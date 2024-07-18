using LanceServer.Core.Configuration.DataModel;
using MediatR;
using Microsoft.Extensions.Configuration;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using OmniSharp.Extensions.LanguageServer.Protocol.Workspace;

namespace LanceServer.Core.Configuration;

public class ConfigurationHandler : IDidChangeConfigurationHandler
{
    IConfigurationManager _configurationManager;

    public ConfigurationHandler(IConfigurationManager configurationManager)
    {
        _configurationManager = configurationManager;
    }

    public Task OnInitialize(ILanguageServer server, InitializeParams request, CancellationToken cancellationToken)
    {
        //_configurationManager.SetWorkspaceFolders(server.WorkspaceFolderManager.CurrentWorkspaceFolders.ToArray());
        return Task.CompletedTask;
    }

    public Task<Unit> Handle(DidChangeConfigurationParams request, CancellationToken cancellationToken)
    {
        var dictionary = request.Settings.ToObject<Dictionary<string, object>>().ToDictionary(k => k.Key, v => v.Value?.ToString());

        // Create ConfigurationBuilder and add Dictionary
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(dictionary)
            .Build();
        _configurationManager.ExtractConfiguration(config);
        return Unit.Task;
    }

    public void SetCapability(DidChangeConfigurationCapability capability, ClientCapabilities clientCapabilities)
    {
    }
}