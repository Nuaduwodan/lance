using LanceServer.Core.Symbol;

namespace LanceServer.Core.Workspace
{
    /// <summary>
    /// Represents a workspace with multiple folders with multiple files which belong together into one project.
    /// </summary>
    public interface IWorkspace
    {
        /// <summary>
        /// Returns the document with a symbol usage table.
        /// </summary>
        /// <param name="uri">The URI of the document</param>
        /// <returns>A <see cref="SymbolUseExtractedDocument"/></returns>
        public SymbolUseExtractedDocument GetSymbolUseExtractedDocument(Uri uri);
    
        /// <summary>
        /// Returns the document with a symbol table.
        /// </summary>
        /// <param name="uri">The URI of the document</param>
        /// <returns>A <see cref="SymbolisedDocument"/></returns>
        public SymbolisedDocument GetSymbolisedDocument(Uri uri);

        /// <summary>
        /// Returns the document with a parse tree.
        /// </summary>
        /// <param name="uri">The URI of the document</param>
        /// <returns>A <see cref="ParsedDocument"/></returns>
        public ParsedDocument GetParsedDocument(Uri uri);

        /// <summary>
        /// Returns the document with the preprocessed code.
        /// </summary>
        /// <param name="uri">The URI of the document</param>
        /// <returns>A <see cref="PreprocessedDocument"/></returns>
        public PreprocessedDocument GetPreprocessedDocument(Uri uri);

        /// <summary>
        /// Returns the document with the extracted macro table code.
        /// </summary>
        /// <param name="uri">The URI of the document</param>
        /// <returns>A <see cref="MacroExtractedDocument"/></returns>
        public MacroExtractedDocument GetMacroExtractedDocument(Uri uri);

        /// <summary>
        /// Returns the document with the placeholder preprocessed code.
        /// </summary>
        /// <param name="uri">The URI of the document</param>
        /// <returns>A <see cref="PlaceholderPreprocessedDocument"/></returns>
        public PlaceholderPreprocessedDocument GetPlaceholderPreprocessedDocument(Uri uri);

        /// <summary>
        /// Returns the document from cache or creates it.
        /// </summary>
        /// <param name="uri">The URI of the document</param>
        /// <returns>A <see cref="Document"/></returns>
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
}