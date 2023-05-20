using LspTypes;
using Range = LspTypes.Range;

namespace LanceServer.Protocol;

/// <summary>
/// Extends the Position of the language server protocol
/// </summary>
public static class PositionExtension
{
    /// <summary>
    /// Checks whether or not a <see cref="Position"/> is inside a <see cref="Range"/>.
    /// </summary>
    /// <returns>True if the position is inside, false otherwise.</returns>
    public static bool IsInRange(this Position position, Range range)
    {
        if (position.Line < range.Start.Line || range.End.Line < position.Line)
        {
            return false;
        }
        
        if (position.Character < range.Start.Character || range.End.Character < position.Character)
        {
            return false;
        }

        return true;
    }
}