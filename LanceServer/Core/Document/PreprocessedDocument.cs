namespace LanceServer.Core.Document;

/// <summary>
/// NOT IMPLEMENTED
/// Same as <see cref="T:LanceServer.Core.Document.PlaceholderPreprocessedDocument" />
/// </summary>
public class PreprocessedDocument : MacroExtractedDocument
{
    /// <summary>
    /// Instantiates a new <see cref="PreprocessedDocument"/>
    /// </summary>
    public PreprocessedDocument(IDocumentInformation information, string rawContent, string code, PlaceholderTable placeholderTable, string macroTable) 
        : base(information, rawContent, code, placeholderTable, macroTable)
    {
    }
    
    /// <summary>
    /// Instantiates a new <see cref="PreprocessedDocument"/>
    /// </summary>
    public PreprocessedDocument(MacroExtractedDocument macroExtractedDocument, string code) 
        : this(macroExtractedDocument.Information, macroExtractedDocument.RawContent, code, macroExtractedDocument.PlaceholderTable, macroExtractedDocument.MacroTable)
    {
    } 
}