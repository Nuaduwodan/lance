namespace LanceServer.Core.Document;

/// <summary>
/// The type of the document in the context of the sinumerik nc
/// </summary>
public enum DocumentType
{
    /// <summary>
    /// The type of definition files.
    /// </summary>
    Definition,
    
    /// <summary>
    /// The type of main procedure files.
    /// </summary>
    MainProcedure,
    
    /// <summary>
    /// the type of manufacturer sub procedure files.
    /// </summary>
    ManufacturerSubProcedure,
    
    /// <summary>
    /// The type of sub procedure files.
    /// </summary>
    SubProcedure
}