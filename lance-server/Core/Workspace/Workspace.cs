using System.Diagnostics.CodeAnalysis;
using LanceServer.Core.Configuration;
using LanceServer.Parser;
using LanceServer.Core.Symbol;
using LanceServer.Preprocessor;

namespace LanceServer.Core.Workspace;

/// <inheritdoc cref="IWorkspace"/>
public class Workspace : IWorkspace
{
        
    private IParserManager _parserManager;

    private IPlaceholderPreprocessor _placeholderPreprocessor;
    private readonly IConfigurationManager _configurationManager;

    private IEnumerable<Uri> _workspaceFolders;
        
    private Dictionary<Uri, Document> _documents = new();
    private Dictionary<string, ISymbol> _globalSymbols = new();

    public Workspace(IParserManager parserManager, IPlaceholderPreprocessor placeholderPreprocessor, IConfigurationManager configurationManager)
    {
        _parserManager = parserManager;
        _placeholderPreprocessor = placeholderPreprocessor;
        _configurationManager = configurationManager;
    }
        
    /// <inheritdoc/>
    public SymbolUseExtractedDocument GetSymbolUseExtractedDocument(Uri uri)
    {
        var symbolisedDocument = GetSymbolisedDocument(uri);
        if (symbolisedDocument is SymbolUseExtractedDocument symbolUseExtractedDocument)
        {
            return symbolUseExtractedDocument;
        }
            
        var symbolUseList = _parserManager.GetSymbolUseForDocument(symbolisedDocument);

        var symbolUseTable = new SymbolUseTable(symbolUseList);
        var newSymbolUseExtractedDocument = new SymbolUseExtractedDocument(symbolisedDocument, symbolUseTable);
        _documents[uri] = newSymbolUseExtractedDocument;
        return newSymbolUseExtractedDocument;
    }
        
    /// <inheritdoc/>
    public SymbolisedDocument GetSymbolisedDocument(Uri uri)
    {
        var parsedDocument = GetParsedDocument(uri);
        if (parsedDocument is SymbolisedDocument symbolisedDocument)
        {
            return symbolisedDocument;
        }
            
        var symbolList = _parserManager.GetSymbolTableForDocument(parsedDocument);
            
        //update symbol table
        DeleteGlobalSymbolsOfFile(uri);

        var globalSymbols = symbolList.Where(symbol => symbol is ProcedureSymbol).ToList();

        if (parsedDocument.Information.IsGlobalFile)
        {
            globalSymbols.AddRange(symbolList.Where(symbol => symbol is VariableSymbol or MacroSymbol));
        }

        var localSymbols = symbolList.Except(globalSymbols);
       
        foreach (var symbol in globalSymbols)
        {
            if (!AddSymbol(symbol))
            {
                //TODO create diagnostic
            }
        }

        var symbolTable = new SymbolTable();
        foreach (var symbol in localSymbols)
        {
            if (!symbolTable.AddSymbol(symbol))
            {
                //TODO create diagnostic
            }
        }
        
        var newSymbolisedDocument = new SymbolisedDocument(parsedDocument, symbolTable);
        _documents[uri] = newSymbolisedDocument;
        return newSymbolisedDocument;
    }

    /// <inheritdoc/>
    public ParsedDocument GetParsedDocument(Uri uri)
    {
        var preprocessedDocument = GetPreprocessedDocument(uri);
        if (preprocessedDocument is ParsedDocument parsedDocument)
        {
            return parsedDocument;
        }

        var newParsedDocument = new ParsedDocument(preprocessedDocument, _parserManager.Parse(preprocessedDocument));
        _documents[uri] = newParsedDocument;
        return newParsedDocument;
    }

    /// <inheritdoc/>
    public PreprocessedDocument GetPreprocessedDocument(Uri uri)
    {
        var document = GetMacroExtractedDocument(uri);
        if (document is PreprocessedDocument preprocessedDocument)
        {
            return preprocessedDocument;
        }
            
        var newPreprocessedDocument = new PreprocessedDocument(document, document.Code);
        _documents[uri] = newPreprocessedDocument;
        return newPreprocessedDocument;
    }

    /// <inheritdoc/>
    public MacroExtractedDocument GetMacroExtractedDocument(Uri uri)
    {
        var document = GetPlaceholderPreprocessedDocument(uri);
        if (document is MacroExtractedDocument macroExtractedDocument)
        {
            return macroExtractedDocument;
        }
            
        var newMacroExtractedDocument = new MacroExtractedDocument(document, string.Empty);
        _documents[uri] = newMacroExtractedDocument;
        return newMacroExtractedDocument;
    }

    /// <inheritdoc/>
    public PlaceholderPreprocessedDocument GetPlaceholderPreprocessedDocument(Uri uri)
    {
        var document = GetDocument(uri);
        if (document is PlaceholderPreprocessedDocument placeholderPreprocessedDocument)
        {
            return placeholderPreprocessedDocument;
        }
            
        var newPlaceholderPreprocessedDocument = _placeholderPreprocessor.Filter(document);
        _documents[uri] = newPlaceholderPreprocessedDocument;
        return newPlaceholderPreprocessedDocument;
    }

    /// <inheritdoc/>
    public Document GetDocument(Uri uri)
    {
        if (HasDocument(uri))
        {
            return _documents[uri];
        }

        var fileEnding = FileUtil.FileExtensionFromUri(uri);
        var config = _configurationManager.SymbolTableConfiguration;
            
        var content = FileUtil.ReadContent(uri);
        var isGlobalFile = config.GlobalFileExtensions.Contains(fileEnding);
        var isSubProcedure = config.SubProcedureFileExtensions.Contains(fileEnding);
            
        var directories = Path.GetDirectoryName(uri.LocalPath)!.Split(Path.DirectorySeparatorChar);
        var procedureNeedsDeclaration = config.GlobalDirectories.Intersect(directories).Any();

        var documentInformation = new DocumentInformation(uri, fileEnding, content, isGlobalFile, isSubProcedure, procedureNeedsDeclaration);
        var document = new Document(documentInformation);
        _documents.Add(uri, document);
        return document;
    }

    /// <inheritdoc />
    public bool HasDocument(Uri uri)
    {
        return _documents.ContainsKey(uri);
    }

    /*
    public async Task InitWorkspaceAsync()
    {
        await Task.Run(InitWorkspace);
    }
    */

    /// <inheritdoc />
    public void InitWorkspace()
    {
        var workspaceFolders = _configurationManager.WorkspaceFolders;
        var fileExtensions = _configurationManager.FileExtensionConfiguration.FileExtensions;
        
        var documentUris = new List<Uri>();
        foreach (var workspaceFolder in workspaceFolders)
        {
            foreach (var fileExtension in fileExtensions)
            {
                documentUris.AddRange(FileUtil.GetFilesInDirectory(workspaceFolder, "*"+fileExtension));
            }
        }
            
        //todo convert uris to documents
/*
        var defFiles = documentUris.Where(uri => symbolTableConfig.GlobalFileExtensions.Contains(FileUtil.FileExtensionFromUri(uri)));
        documentUris = documentUris.Except(defFiles).ToList();
        
        foreach (var defFile in defFiles)
        {
            GetSymbolisedDocument(defFile);
        }
*/      
        foreach (var documentUri in documentUris)
        {
            GetSymbolisedDocument(documentUri);
        }
    }

    /// <inheritdoc />
    public bool TryGetSymbol(string symbolName, Uri documentOfReference, [MaybeNullWhen(false)] out ISymbol symbol)
    {
        if (GetSymbolisedDocument(documentOfReference).SymbolTable.TryGetSymbol(symbolName, out symbol))
        {
            return true;
        }
        
        if (_globalSymbols.TryGetValue(symbolName.ToLower(), out symbol))
        {
            return true;
        }

        return false;
    }

    /// <inheritdoc />
    public IEnumerable<ISymbol> GetGlobalSymbolsOfDocument(Uri uri)
    {
        return _globalSymbols.Where(pair => pair.Value.SourceDocument == uri).Select(pair => pair.Value);
    }

    /// <inheritdoc />
    public void UpdateDocumentContent(Uri uri, string newContent)
    {
        var document = GetDocument(uri);
        if (document.Information.RawContent != newContent)
        {
            var documentInformation = new DocumentInformation(document.Information.Uri, document.Information.FileEnding, newContent, document.Information.IsGlobalFile, document.Information.IsSubProcedure, document.Information.ProcedureNeedsDeclaration, document.Information.Encoding);
            _documents[uri] = new Document(documentInformation);
        }

        GetSymbolisedDocument(uri);
    }

    private bool AddSymbol(ISymbol symbol)
    {
        if (_globalSymbols.ContainsKey(symbol.Identifier.ToLower()))
        {
            return false;
        }
        _globalSymbols.Add(symbol.Identifier.ToLower(), symbol);
        return true;
    }

    private void DeleteGlobalSymbolsOfFile(Uri documentUri)
    {
        _globalSymbols = _globalSymbols.Where(pair => pair.Value.SourceDocument != documentUri).ToDictionary(pair => pair.Key, pair => pair.Value);
    }
}