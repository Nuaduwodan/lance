using LanceServer.Core.Configuration.DataModel;
using LspTypes;

namespace LanceServer.Core.Configuration;

public interface IConfigurationManager
{
    SymbolTableConfiguration SymbolTableConfiguration { get; }
    FileExtensionConfiguration FileExtensionConfiguration { get; }
    DocumentationConfiguration DocumentationConfiguration { get; }
    CustomPreprocessorConfiguration CustomPreprocessorConfiguration { get; }
    Uri[] WorkspaceFolders { get; }
    ClientCapabilities ClientCapabilities { get; set; }
    void ExtractConfiguration(ServerConfiguration configuration);
    void SetWorkspaceFolders(WorkspaceFolder[]? workspaceFolders);
}