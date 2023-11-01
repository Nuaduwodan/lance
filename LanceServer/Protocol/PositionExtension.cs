using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using Range = OmniSharp.Extensions.LanguageServer.Protocol.Models.Range;

namespace LanceServer.Protocol;

/// <summary>
/// Extends the Position of the language server protocol
/// </summary>
public static class PositionExtension
{
    /// <summary>
    /// Checks whether or not a <see cref="Position"/> is inside a <see cref="System.Range"/>.
    /// </summary>
    /// <returns>True if the position is inside, false otherwise.</returns>
    public static bool IsInRange(this Position position, Range range)
    {
        return range.Start <= position && position <= range.End;
    }
}