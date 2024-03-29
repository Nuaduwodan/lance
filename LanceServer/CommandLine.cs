using LanceServer.Core.Workspace;
using LanceServer.RequestHandler.Diagnostic;
using LspTypes;

namespace LanceServer;

/// <summary>
/// Provides the interface and defines the workflow for a command line use of the language server.
/// </summary>
public class CommandLine
{
    private readonly Workspace _workspace;
    private readonly DiagnosticHandler _diagnosticHandler;

    public CommandLine(Workspace workspace, DiagnosticHandler diagnosticHandler)
    {
        _workspace = workspace;
        _diagnosticHandler = diagnosticHandler;
    }

    /// <summary>
    /// Processes all files in the workspace and prints a report for the requested severity levels.
    /// </summary>
    /// <param name="printLevel">The minimum severity of problems to be printed.</param>
    /// <param name="reportLevel">The minimum severity of problems to be counted as problem.</param>
    /// <returns>The number of problems to report.</returns>
    public int ProcessFiles(DiagnosticSeverity printLevel, DiagnosticSeverity reportLevel)
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

        diagnostics = diagnostics.OrderBy(diagnostic => diagnostic.Value.Severity).ThenBy(diagnostic => diagnostic.Key.LocalPath).ToList();
        
        Console.Out.WriteLine();
        var errors = 0;
        foreach (var diagnostic in diagnostics)
        {
            if (diagnostic.Value.Severity <= reportLevel)
            {
                errors++;
            }
            
            if (diagnostic.Value.Severity <= printLevel)
            {
                Console.Out.WriteLine($"{diagnostic.Value.Severity} {diagnostic.Key.LocalPath} {diagnostic.Value.Range.Start.Line + 1}:{diagnostic.Value.Range.Start.Character + 1} {diagnostic.Value.Message}");
            }
        }
        
        Console.Out.WriteLine($"Total number of problems with a severity of {reportLevel} or higher is {errors}");
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