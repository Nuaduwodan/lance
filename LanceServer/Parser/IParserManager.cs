﻿using LanceServer.Core.Document;
using LanceServer.Core.Symbol;

namespace LanceServer.Parser;

/// <summary>
/// Provides the interface to the parser generated by antlr
/// </summary>
public interface IParserManager
{
    /// <summary>
    /// Parses the provided document and returns the parser result.
    /// </summary>
    /// <param name="document">The document to be parsed</param>
    ParserResult Parse(PreprocessedDocument document);
    
    /// <summary>
    /// Visits the parse tree of the provided document to generate a list of all defined symbols
    /// </summary>
    /// <param name="document">The document to be visited</param>
    /// <returns>A list of all defined symbols</returns>
    IEnumerable<AbstractSymbol> GetSymbolTableForDocument(ParsedDocument document);
    
    /// <summary>
    /// Visits the parse tree of the provided document to generate a list of all symbol uses
    /// </summary>
    /// <param name="document">The document to be visited</param>
    /// <returns>A list of all symbol uses</returns>
    IEnumerable<AbstractSymbolUse> GetSymbolUseForDocument(SymbolisedDocument document);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    IEnumerable<LanguageToken> GetLanguageTokensForDocument(SymbolisedDocument document);
}