namespace LanceServer.Protocol;

public static class Method
{
    public const string TextDocumentDiagnostic = "textDocument/diagnostic";
    public const string WorkspaceConfiguration = "workspace/configuration";
    public const string WorkspaceDiagnosticRefresh = "workspace/diagnostic/refresh";
    public const string WorkspaceSemanticTokensRefresh = "workspace/semanticTokens/refresh";
    public const string WindowWorkDoneProgressCreate = "window/workDoneProgress/create";
}