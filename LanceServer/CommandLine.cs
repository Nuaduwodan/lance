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
        progressToken.ProgressChanged += ReportProgress();
        
        _workspace.InitWorkspace(progressToken);

        var documentUris = _workspace.GetAllDocumentUris();
        var diagnostics = new List<KeyValuePair<Uri, Diagnostic>>();

        foreach (var uri in documentUris)
        {
            var document = _workspace.GetSymbolUseExtractedDocument(uri);
            var diagnosticReport = _diagnosticHandler.HandleRequest(document, _workspace);
            foreach (var diagnostic in diagnosticReport.Items)
            {
                diagnostics.Add(new KeyValuePair<Uri, Diagnostic>(uri, diagnostic));
            }
        }

        diagnostics = diagnostics.Where(diagnostic => diagnostic.Value.Severity <= DiagnosticSeverity.Warning)
            .OrderBy(diagnostic => diagnostic.Value.Severity).ToList();
        
        var errors = 0;
        foreach (var diagnostic in diagnostics)
        {
            if (diagnostic.Value.Severity == DiagnosticSeverity.Error)
            {
                errors++;
            }
            Console.Out.WriteLine($"{diagnostic.Value.Severity} {diagnostic.Key.LocalPath} {diagnostic.Value.Range.Start.Line+1}:{diagnostic.Value.Range.Start.Character+1} {diagnostic.Value.Message}");
        }

        return errors;
    }

    private static EventHandler<WorkDoneProgressReport> ReportProgress()
    {
        var lastLength = 0;
        return (_, workDoneReport) =>
        {
            var updateText = $"\rParsing files {workDoneReport.Percentage}%: {workDoneReport.Message}";
            var currentLength = updateText.Length;
            var paddingLength = lastLength - currentLength;
            lastLength = currentLength;
            var padding = paddingLength > 0 ? new string(' ', paddingLength) : "";
            Console.Out.Write(updateText + "{0}", padding);
        };
    }
}