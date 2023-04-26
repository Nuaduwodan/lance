using System.Runtime.Serialization;

namespace LanceServer.LanguageServerProtocol;

[DataContract]
public enum DocumentDiagnosticReportKind
{
    [EnumMember(Value = "full")]
    Full,
    [EnumMember(Value = "unchanged")]
    Unchanged
}