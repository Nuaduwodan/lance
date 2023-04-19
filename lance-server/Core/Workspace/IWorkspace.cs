using LanceServer.Core.Symbol;

namespace LanceServer.Core.Workspace;

public interface IWorkspace
{
    /// <summary>
    /// Returns the document with a symbol table.
    /// </summary>
    /// <param name="uri">The URI of the document</param>
    public SymbolisedDocument GetSymbolisedDocument(Uri uri);

    /// <summary>
    /// Returns the document with a parse tree.
    /// </summary>
    /// <param name="uri">The URI of the document</param>
    public ParsedDocument GetParsedDocument(Uri uri);

    /// <summary>
    /// Returns the document with the preprocessed code.
    /// </summary>
    /// <param name="uri">The URI of the document</param>
    public PreprocessedDocument GetPreprocessedDocument(Uri uri);

    /// <summary>
    /// Returns the document with the extracted macro table code.
    /// </summary>
    /// <param name="uri">The URI of the document</param>
    public MacroExtractedDocument GetMacroExtractedDocument(Uri uri);

    /// <summary>
    /// Returns the document with the placeholder preprocessed code.
    /// </summary>
    /// <param name="uri">The URI of the document</param>
    public PlaceholderPreprocessedDocument GetPlaceholderPreprocessedDocument(Uri uri);

    /// <summary>
    /// Returns the document from cache or creates it.
    /// </summary>
    /// <param name="uri">The URI of the document</param>
    public Document GetDocument(Uri uri);

    /// <summary>
    /// Returns true if this workspace has a document with that URI.
    /// </summary>
    /// <param name="uri">The URI of the document</param>
    public bool HasDocument(Uri uri);

    /// <summary>
    /// Loads all the files of a workspace to be able to provide project wide insights.
    /// </summary>
    public void InitWorkspace();

    /// <summary>
    /// Returns the symbol.
    /// </summary>
    /// <param name="symbolName">The name of the symbol</param>
    /// <param name="documentOfReference">The URI of the document where the symbol is used</param>
    public ISymbol GetSymbol(string symbolName, Uri documentOfReference);

    public void UpdateDocumentContent(Uri uri, string newContent);
}