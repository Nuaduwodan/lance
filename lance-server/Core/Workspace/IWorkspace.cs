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
    Document GetDocumentWithUpdatedSymbolTable(Uri uri);

    /// <summary>
    /// Returns the document with a parse tree from memory or creates it.
    /// </summary>
    /// <param name="uri">The URI of the document</param>
    Document GetDocumentWithParseTree(Uri uri);

    Document GetPreprocessedDocument(Uri uri);

    /// <summary>
    /// Returns the document from memory or creates it.
    /// </summary>
    /// <param name="uri">The URI of the document</param>
    Document GetDocument(Uri uri);

    /// <summary>
    /// Returns true if this workspace has a document with that URI.
    /// </summary>
    /// <param name="uri">The URI of the document</param>
    bool HasDocument(Uri uri);

    /// <summary>
    /// Adds the document to this workspace, returns true if successfully added or false if a document with that URI already exists.
    /// </summary>
    /// <param name="documentUri">The URI of the document</param>
    bool AddDocument(Uri documentUri);

    //Task InitWorkspaceAsync();
    void InitWorkspace(IEnumerable<Uri> workspaceFolders);

    /// <summary>
    /// Returns the symbol.
    /// </summary>
    /// <param name="symbolName">The name of the symbol</param>
    /// <param name="documentOfReference">The URI of the document where the symbol is used</param>
    ISymbol GetSymbol(string symbolName, Uri documentOfReference);
}