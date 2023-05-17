using System.Runtime.Serialization;

namespace LanceServer.Protocol;

[DataContract]
public static class DocumentDiagnosticReportKind
{
    public const string Full = "full";
    public const string Unchanged = "unchanged";
}