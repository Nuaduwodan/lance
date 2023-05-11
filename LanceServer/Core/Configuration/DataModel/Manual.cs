using System.Diagnostics.CodeAnalysis;

namespace LanceServer.Core.Configuration.DataModel;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "IdentifierTypo")]
public enum Manual
{
    FMA,
    FMB,
    FMSA,
    FMSI,
    FMTE,
    FMTM,
    FMTR,
    PMMC,
    PMNC,
    None
}