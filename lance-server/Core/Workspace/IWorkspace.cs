using LanceServer.Core.SymbolTable;

namespace LanceServer.Core.Workspace;

public interface IWorkspace
{
    /// <summary>
    /// Returns the document with a parse tree and symbol table from memory or creates it.
    /// </summary>
    /// <param name="uri"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public SymbolisedDocument GetSymbolisedDocument(Uri uri);

    /// <summary>
    /// Returns the document with a parse tree from memory or creates it.
    /// </summary>
    /// <param name="uri">The URI of the document</param>
    public ParsedDocument GetParsedDocument(Uri uri);

    /// <summary>
    /// Returns the document with the preprocessed code..
    /// </summary>
    /// <param name="uri">The URI of the document</param>
    public PreprocessedDocument GetPreprocessedDocument(Uri uri);

    /// <summary>
    /// Returns the document from memory or creates it.
    /// </summary>
    /// <param name="uri">The URI of the document</param>
    public ReadDocument GetReadDocument(Uri uri);

    /// <summary>
    /// Adds the document to this workspace, returns true if successfully added or false if a document with that URI already exists.
    /// </summary>
    /// <param name="documentUri">The URI of the document</param>
    public KnownDocument GetKnownDocument(Uri documentUri);

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