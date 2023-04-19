namespace LanceServer.Core.Workspace;

public class PreprocessedDocument : MacroExtractedDocument
{
    public PreprocessedDocument(DocumentInformation information, string code, Placeholders placeholders, string macroTable) 
        : base(information, code, placeholders, macroTable)
    {
    }
    
    public PreprocessedDocument(MacroExtractedDocument macroExtractedDocument, string code) 
        : this(macroExtractedDocument.Information, code, macroExtractedDocument.Placeholders, macroExtractedDocument.MacroTable)
    {
    } 
}