using LanceServer.Core.Configuration;
using LanceServer.Core.Workspace;
using LanceServer.RequestHandler.Diagnostic;
using LspTypes;

namespace LanceServer;

public class CommandLine
{
    private readonly Workspace _workspace;
    private readonly DiagnosticHandler _diagnosticHandler;

    public CommandLine(Workspace workspace, DiagnosticHandler diagnosticHandler)
    {
        _workspace = workspace;
        _diagnosticHandler = diagnosticHandler;
    }

    public int ProcessFiles()
    {
        var progressToken = new Progress<WorkDoneProgressReport>();
        _workspace.InitWorkspace(progressToken);

        var documentUris = _workspace.GetAllDocumentUris();

        var errors = 0;
        foreach (var uri in documentUris)
        {
            var document = _workspace.GetSymbolUseExtractedDocument(uri);
            var diagnosticReport = _diagnosticHandler.HandleRequest(document, _workspace);
            foreach (var diagnostic in diagnosticReport.Items)
            {
                if (diagnostic.Severity == DiagnosticSeverity.Error)
                {
                    errors++;
                }
                Console.Out.WriteLine($"{diagnostic.Severity} {uri.LocalPath} {diagnostic.Range.Start.Line}:{diagnostic.Range.Start.Character} {diagnostic.Message}");
            }
        }

        return errors;
    }
}