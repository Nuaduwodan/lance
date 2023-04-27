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

    private readonly object _documentsLock = new ();
    private readonly object _globalSymbolsLock = new ();

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
        lock (_documentsLock)
        {
            _documents[uri] = newSymbolUseExtractedDocument;
        }
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

        if (parsedDocument.Information.DocumentType is DocumentType.Definition or DocumentType.MainProcedure)
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
        lock (_documentsLock)
        {
            _documents[uri] = newSymbolisedDocument;
        }
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
        lock (_documentsLock)
        {
            _documents[uri] = newParsedDocument;
        }
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
        lock (_documentsLock)
        {
            _documents[uri] = newPreprocessedDocument;
        }
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
        lock (_documentsLock)
        {
            _documents[uri] = newMacroExtractedDocument;
        }
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
        lock (_documentsLock)
        {
            _documents[uri] = newPlaceholderPreprocessedDocument;
        }
        return newPlaceholderPreprocessedDocument;
    }

    /// <inheritdoc/>
    public Document GetDocument(Uri uri)
    {
        lock (_documentsLock)
        {
            if (_documents.TryGetValue(uri, out var document))
            {
                return document;
            }
        }

        var config = _configurationManager.SymbolTableConfiguration;
        var content = FileUtil.ReadContent(uri);

        var documentInformation = new DocumentInformation(uri, config);
        var newDocument = new Document(documentInformation, content);
        lock (_documentsLock)
        {
            _documents.Add(uri, newDocument);
        }
        return newDocument;
    }

    /// <inheritdoc />
    public bool HasDocument(Uri uri)
    {
        lock (_documentsLock)
        {
            return _documents.ContainsKey(uri);
        }
    }
    
    /// <inheritdoc />
    public async Task InitWorkspaceAsync()
    {
        await Task.Run(InitWorkspace);
    }

    public void InitWorkspace()
    {
        var workspaceFolders = _configurationManager.WorkspaceFolders;
        var symbolTableConfig = _configurationManager.SymbolTableConfiguration;
        var fileExtensions = _configurationManager.FileExtensionConfiguration.FileExtensions;
        
        var documentUris = new List<Uri>();
        foreach (var workspaceFolder in workspaceFolders)
        {
            foreach (var fileExtension in fileExtensions)
            {
                documentUris.AddRange(FileUtil.GetFilesInDirectory(workspaceFolder, "*"+fileExtension));
            }
        }
        
        documentUris = documentUris.OrderByDescending(uri => symbolTableConfig.DefinitionFileExtensions.Contains(FileUtil.FileExtensionFromUri(uri))).ToList();
        
        //todo convert uris to documents

        var maxCount = documentUris.Count;
        var currentCount = 0;
        foreach (var documentUri in documentUris)
        {
            GetSymbolisedDocument(documentUri);
            currentCount++;
            Console.Error.WriteLine($"Initialised Document {currentCount}/{maxCount} : {Path.GetFileName(documentUri.LocalPath)}");
        }
    }

    /// <inheritdoc />
    public bool TryGetSymbol(string symbolName, Uri documentOfReference, [MaybeNullWhen(false)] out ISymbol symbol)
    {
        if (GetSymbolisedDocument(documentOfReference).SymbolTable.TryGetSymbol(symbolName, out symbol))
        {
            return true;
        }

        lock (_globalSymbolsLock)
        {
            if (_globalSymbols.TryGetValue(symbolName.ToLower(), out symbol))
            {
                return true;
            }
        }

        return false;
    }

    /// <inheritdoc />
    public IEnumerable<ISymbol> GetGlobalSymbolsOfDocument(Uri uri)
    {
        lock (_globalSymbolsLock)
        {
            return _globalSymbols.Where(pair => pair.Value.SourceDocument == uri).Select(pair => pair.Value);
        }
    }

    /// <inheritdoc />
    public void UpdateDocumentContent(Uri uri, string newContent)
    {
        var document = GetDocument(uri);
        if (document.RawContent != newContent)
        {
            var documentInformation = new DocumentInformation(document.Information.Uri, document.Information.DocumentType, document.Information.Encoding);
            _documents[uri] = new Document(documentInformation, newContent);
        }

        GetSymbolisedDocument(uri);
    }

    private bool AddSymbol(ISymbol symbol)
    {
        lock (_globalSymbolsLock)
        {
            if (_globalSymbols.ContainsKey(symbol.Identifier.ToLower()))
            {
                return false;
            }
            _globalSymbols.Add(symbol.Identifier.ToLower(), symbol);
            return true;
        }
    }

    private void DeleteGlobalSymbolsOfFile(Uri documentUri)
    {
        lock (_globalSymbolsLock)
        {
            _globalSymbols = _globalSymbols.Where(pair => pair.Value.SourceDocument != documentUri).ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }
}