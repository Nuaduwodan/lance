namespace LanceServer.Core.Workspace;

public class PreprocessedDocument : MacroExtractedDocument
{
    public PreprocessedDocument(IDocumentInformation information, string rawContent, string code, Placeholders placeholders, string macroTable) 
        : base(information, rawContent, code, placeholders, macroTable)
    {
    }
    
    public PreprocessedDocument(MacroExtractedDocument macroExtractedDocument, string code) 
        : this(macroExtractedDocument.Information, macroExtractedDocument.RawContent, code, macroExtractedDocument.Placeholders, macroExtractedDocument.MacroTable)
    {
    } 
}