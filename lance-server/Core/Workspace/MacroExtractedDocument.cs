namespace LanceServer.Core.Workspace;

public class MacroExtractedDocument : PlaceholderPreprocessedDocument
{
    //todo macrotable
    public string MacroTable;
    public MacroExtractedDocument(IDocumentInformation information, string rawContent, string code, Placeholders placeholders, string macroTable) 
        : base(information, rawContent, code, placeholders)
    {
        MacroTable = macroTable;
    }
    
    public MacroExtractedDocument(PlaceholderPreprocessedDocument placeholderPreprocessedDocument, string macroTable) 
        : this(placeholderPreprocessedDocument.Information, placeholderPreprocessedDocument.RawContent, placeholderPreprocessedDocument.Code, placeholderPreprocessedDocument.Placeholders, macroTable)
    {
    }
}