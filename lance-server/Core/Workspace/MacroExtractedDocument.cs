namespace LanceServer.Core.Workspace;

public class MacroExtractedDocument : PlaceholderPreprocessedDocument
{
    //todo macrotable
    public string MacroTable;
    public MacroExtractedDocument(DocumentInformation information, string code, Placeholders placeholders, string macroTable) 
        : base(information, code, placeholders)
    {
        MacroTable = macroTable;
    }
    
    public MacroExtractedDocument(PlaceholderPreprocessedDocument placeholderPreprocessedDocument, string macroTable) 
        : this(placeholderPreprocessedDocument.Information, placeholderPreprocessedDocument.Code, placeholderPreprocessedDocument.Placeholders, macroTable)
    {
    }
}