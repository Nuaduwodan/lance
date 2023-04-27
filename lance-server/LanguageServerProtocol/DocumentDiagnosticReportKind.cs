using System.Runtime.Serialization;

namespace LanceServer.LanguageServerProtocol;

[DataContract]
public class DocumentDiagnosticReportKind
{
    public const string Full = "full";
    public const string Unchanged = "unchanged";
}