namespace LanceServer.Core.Workspace;

public class InitWorkspaceProgressReport
{
    public int MaxNumberOfFiles { get; set; }
    public int NumberOfProcessedFiles { get; set; }
    public int CurrentlyProcessingFile { get; set; }
}