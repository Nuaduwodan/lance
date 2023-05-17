namespace LanceServer.Core.Document;

/// <summary>
/// NOT IMPLEMENTED
/// Same as <see cref="PlaceholderPreprocessedDocument"/>
/// </summary>
public class MacroExtractedDocument : PlaceholderPreprocessedDocument
{
    //todo macrotable
    /// <summary>
    /// The table containing all macros defined in this document.
    /// </summary>
    public string MacroTable;
    
    /// <summary>
    /// Creates a new instance of <see cref="MacroExtractedDocument"/>
    /// </summary>
    public MacroExtractedDocument(IDocumentInformation information, string rawContent, string code, PlaceholderTable placeholderTable, string macroTable) 
        : base(information, rawContent, code, placeholderTable)
    {
        MacroTable = macroTable;
    }
    
    /// <summary>
    /// Creates a new instance of <see cref="MacroExtractedDocument"/>
    /// </summary>
    public MacroExtractedDocument(PlaceholderPreprocessedDocument placeholderPreprocessedDocument, string macroTable) 
        : this(placeholderPreprocessedDocument.Information, placeholderPreprocessedDocument.RawContent, placeholderPreprocessedDocument.Code, placeholderPreprocessedDocument.PlaceholderTable, macroTable)
    {
    }
}