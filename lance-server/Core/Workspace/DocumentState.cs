namespace LanceServer.Core.Workspace;

/// <summary>
/// Describes the state a document is in
/// </summary>
public enum DocumentState
{
    /// <summary>
    /// The State of a document when it is known to exist.
    /// </summary>
    Known,
    
    /// <summary>
    /// The State of a document when it just contains the actual file contents.
    /// </summary>
    Read,
    
    /// <summary>
    /// The State of a document after its content has been pre processed.
    /// </summary>
    Preprocessed,
    
    /// <summary>
    /// The State of a document after its content has been parsed.
    /// </summary>
    Parsed,
    
    /// <summary>
    /// The State of a document after its parse tree has been visited and the symbol tables have been updated.
    /// </summary>
    Visited
}