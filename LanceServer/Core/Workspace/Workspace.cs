﻿using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using LanceServer.Core.Configuration;
using LanceServer.Core.Document;
using LanceServer.Parser;
using LanceServer.Core.Symbol;
using LanceServer.Preprocessor;
using LspTypes;

namespace LanceServer.Core.Workspace;

/// <inheritdoc cref="IWorkspace"/>
public class Workspace : IWorkspace
{
    public bool IsWorkspaceInitialised { get; private set; }
        
    private IParserManager _parserManager;

    private IPlaceholderPreprocessor _placeholderPreprocessor;
    private readonly IConfigurationManager _configurationManager;

    private ConcurrentDictionary<Uri, Document.Document> _documents = new();
    private ConcurrentDictionary<string, ISymbol> _globalSymbols = new();
    private ConcurrentBag<ISymbolUse> _globalSymbolUses = new();

    public Workspace(IParserManager parserManager, IPlaceholderPreprocessor placeholderPreprocessor, IConfigurationManager configurationManager)
    {
        _parserManager = parserManager;
        _placeholderPreprocessor = placeholderPreprocessor;
        _configurationManager = configurationManager;
    }
        
    /// <inheritdoc/>
    public LanguageTokenExtractedDocument GetSymbolUseExtractedDocument(Uri uri)
    {
        var symbolisedDocument = GetSymbolisedDocument(uri);
        if (symbolisedDocument is LanguageTokenExtractedDocument symbolUseExtractedDocument)
        {
            return symbolUseExtractedDocument;
        }
            
        var symbolUseList = _parserManager.GetSymbolUseForDocument(symbolisedDocument);
        var languageTokens = _parserManager.GetLanguageTokensForDocument(symbolisedDocument);

        var symbolUseTable = new SymbolUseTable(symbolUseList);
        var languageTokenTable = new LanguageTokenTable(languageTokens);
        var newSymbolUseExtractedDocument = new LanguageTokenExtractedDocument(symbolisedDocument, symbolUseTable, languageTokenTable);
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

        if (parsedDocument.Information.DocumentType is DocumentType.Definition or DocumentType.MainProcedure)
        {
            globalSymbols.AddRange(symbolList.Where(symbol => symbol is VariableSymbol or MacroSymbol));
        }

        // todo remove when not needed
        symbolList = symbolList.Except(globalSymbols).ToList();
       
        foreach (var symbol in globalSymbols.Where(symbol => !_globalSymbols.TryAdd(symbol.Identifier.ToLower(), symbol)))
        {
            _globalSymbols.TryGetValue(symbol.Identifier.ToLower(), out var existingSymbol);
            parsedDocument.ParserDiagnostics.Add(new Diagnostic
            {
                Code = symbol.Identifier,
                Range = symbol.IdentifierRange,
                Message = $"A global symbol with the name {existingSymbol!.Identifier} is already defined.",
                Severity = DiagnosticSeverity.Warning
            });
        }

        var symbolTable = new SymbolTable();
        foreach (var symbol in symbolList.Where(symbol => !symbolTable.AddSymbol(symbol)))
        {
            symbolTable.TryGetSymbol(symbol.Identifier.ToLower(), out var existingSymbol);
            parsedDocument.ParserDiagnostics.Add(new Diagnostic
            {
                Code = symbol.Identifier,
                Range = symbol.IdentifierRange,
                Message = $"A local symbol with the name {existingSymbol!.Identifier} is already defined.",
                Severity = DiagnosticSeverity.Warning
            });
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

        var parserResult = _parserManager.Parse(preprocessedDocument);
        var newParsedDocument = new ParsedDocument(preprocessedDocument, parserResult.ParseTree, parserResult.Diagnostics);
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
        var readDocument = GetReadDocument(uri);
        if (readDocument is PlaceholderPreprocessedDocument placeholderPreprocessedDocument)
        {
            return placeholderPreprocessedDocument;
        }
            
        var newPlaceholderPreprocessedDocument = _placeholderPreprocessor.Filter(readDocument);
        _documents[uri] = newPlaceholderPreprocessedDocument;
        
        return newPlaceholderPreprocessedDocument;
    }

    public ReadDocument GetReadDocument(Uri uri)
    {
        var document = GetDocument(uri);
        if (document is ReadDocument readDocument)
        {
            return readDocument;
        }
        
        var content = FileUtil.ReadContent(uri);
        var newReadDocument = new ReadDocument(document, content);
        _documents[uri] = newReadDocument;
        
        return newReadDocument;
    }

    /// <inheritdoc/>
    public Document.Document GetDocument(Uri uri)
    {
        if (_documents.TryGetValue(uri, out var document))
        {
            return document;
        }

        var config = _configurationManager.SymbolTableConfiguration;

        var documentInformation = new DocumentInformation(uri, config);
        var newDocument = new Document.Document(documentInformation);
        _documents.TryAdd(uri, newDocument);
        
        return newDocument;
    }
    
    /// <inheritdoc />
    public async Task InitWorkspaceAsync(Progress<WorkDoneProgressReport> progress)
    {
        await Task.Run(() => InitWorkspace(progress));
    }

    public void InitWorkspace(IProgress<WorkDoneProgressReport> progress)
    {
        var workspaceFolders = _configurationManager.WorkspaceFolders;
        var fileExtensions = _configurationManager.FileExtensionConfiguration.FileExtensions;
        
        var documentUris = new List<Uri>();
        foreach (var workspaceFolder in workspaceFolders)
        {
            foreach (var fileExtension in fileExtensions)
            {
                documentUris.AddRange(FileUtil.GetFilesInDirectory(workspaceFolder, "*" + fileExtension));
            }
        }

        if (_documents.IsEmpty)
        {
            _documents = new ConcurrentDictionary<Uri, Document.Document>(20, documentUris.Count);
        }

        documentUris = documentUris.Select(GetDocument).OrderBy(document => document.Information.DocumentType).Select(document => document.Information.Uri).ToList();

        var maxCount = documentUris.Count;
        double currentCount = 0;
        
        Parallel.ForEach(documentUris, uri => 
        {
            GetSymbolisedDocument(uri);
            currentCount++;
            
            progress.Report(new WorkDoneProgressReport
            {
                Kind = "report",
                Message = $"{currentCount}/{maxCount} {uri.LocalPath}",
                Percentage = (uint)Math.Floor(currentCount / maxCount * 100)
            });
        });
        
        IsWorkspaceInitialised = true;
    }

    /// <inheritdoc />
    public bool TryGetSymbol(string symbolName, Uri documentOfReference, [MaybeNullWhen(false)] out ISymbol symbol)
    {
        return GetSymbolisedDocument(documentOfReference).SymbolTable.TryGetSymbol(symbolName, out symbol) 
               || _globalSymbols.TryGetValue(symbolName.ToLower(), out symbol);
    }

    /// <inheritdoc />
    public IEnumerable<ISymbol> GetGlobalSymbolsOfDocument(Uri uri)
    {
        return _globalSymbols.Where(pair => pair.Value.SourceDocument == uri).Select(pair => pair.Value);
    }

    /// <inheritdoc />
    public void UpdateDocumentContent(Uri uri, string newContent)
    {
        var readDocument = GetReadDocument(uri);
        if (readDocument.RawContent != newContent)
        {
            var documentInformation = new DocumentInformation(readDocument.Information.Uri, readDocument.Information.DocumentType, readDocument.Information.Encoding);
            readDocument = new ReadDocument(documentInformation, newContent);
        }
        
        _documents[uri] = readDocument;

        GetSymbolisedDocument(uri);
    }

    private void DeleteGlobalSymbolsOfFile(Uri documentUri)
    {
        foreach (var pair in _globalSymbols.Where(pair => pair.Value.SourceDocument == documentUri))
        {
            _globalSymbols.TryRemove(pair);
        }
    }

    public IEnumerable<Uri> GetAllDocumentUris()
    {
        return _documents.Select(pair => pair.Key);
    }
}