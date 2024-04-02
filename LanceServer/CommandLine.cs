using LanceServer.Core.Workspace;
using LanceServer.RequestHandler.DiagnosticHandler;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;

namespace LanceServer;

/// <summary>
/// Provides the interface and defines the workflow for a command line use of the language server.
/// </summary>
public class CommandLine
{
    private readonly Workspace _workspace;
    private readonly DocumentDiagnosticLogic _documentDiagnosticLogic;

    public CommandLine(Workspace workspace, DocumentDiagnosticLogic documentDiagnosticLogic)
    {
        _workspace = workspace;
        _documentDiagnosticLogic = documentDiagnosticLogic;
    }

    /// <summary>
    /// Processes all files in the workspace and prints a report for the requested severity levels.
    /// </summary>
    /// <param name="directories">The directories to be processed.</param>
    /// <param name="printLevel">The minimum severity of problems to be printed.</param>
    /// <param name="reportLevel">The minimum severity of problems to be counted as problem.</param>
    /// <returns>The number of problems to report.</returns>
    public int ProcessFiles(DirectoryInfo[] directories, DiagnosticSeverity printLevel, DiagnosticSeverity reportLevel)
    {
        var workspaceFolders = directories.Select(directory => new WorkspaceFolder { Uri = new Uri(directory.FullName) }).ToList();
        
        var progressToken = new Progress<WorkDoneProgressReport>();
        progressToken.ProgressChanged += ReportProgress();
        
        _workspace.InitWorkspace(progressToken, workspaceFolders);

        var documentUris = _workspace.GetAllDocumentUris();
        var diagnostics = new List<KeyValuePair<Uri, Diagnostic>>();

        foreach (var uri in documentUris)
        {
            var document = _workspace.GetSymbolUseExtractedDocument(uri);
            var diagnosticReport = _documentDiagnosticLogic.HandleRequest(document, _workspace);
            foreach (var diagnostic in diagnosticReport)
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