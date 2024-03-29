﻿using LanceServer.Core.Document;
using LanceServer.Core.Symbol;
using LspTypes;

namespace LanceServer.Core.Workspace;

/// <summary>
/// Represents a workspace with multiple folders with multiple files which belong together into one project.
/// </summary>
public interface IWorkspace
{
    /// <summary>
    /// Returns the document with a symbol usage table.
    /// </summary>
    /// <param name="uri">The URI of the document</param>
    /// <returns>A <see cref="LanguageTokenExtractedDocument"/></returns>
    public LanguageTokenExtractedDocument GetSymbolUseExtractedDocument(Uri uri);
    
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
    /// Returns the document with the raw content.
    /// </summary>
    /// <param name="uri">The URI of the document</param>
    /// <returns>A <see cref="ReadDocument"/></returns>
    public ReadDocument GetReadDocument(Uri uri);

    /// <summary>
    /// Returns the base document with just location and type information from cache or creates it.
    /// </summary>
    /// <param name="uri">The URI of the document</param>
    /// <returns>A <see cref="Document"/></returns>
    public Document.Document GetDocument(Uri uri);

    /// <summary>
    /// Loads all the files of a workspace to be able to provide project wide insights.
    /// </summary>
    public Task InitWorkspaceAsync(Progress<WorkDoneProgressReport> progress);

    /// <summary>
    /// Returns a list of found symbols
    /// </summary>
    /// <param name="symbolName">The name of the symbol to be found.</param>
    /// <param name="documentOfReference">The URI of the document where the symbol is used.</param>
    public IEnumerable<AbstractSymbol> GetSymbols(string symbolName, Uri documentOfReference);

    /// <summary>
    /// Updates the raw content of the specified document.
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="newContent"></param>
    public void UpdateDocumentContent(Uri uri, string newContent);

    /// <summary>
    /// Returns true if the workspace is initialised.
    /// </summary>
    public bool IsWorkspaceInitialised { get; }

    /// <summary>
    /// Returns the global symbol table.
    /// </summary>
    public GlobalSymbolTable GlobalSymbolTable { get; }
}