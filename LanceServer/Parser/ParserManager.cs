using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using LanceServer.Core.Document;
using LanceServer.Core.Symbol;
using LspTypes;
using Range = LspTypes.Range;

namespace LanceServer.Parser;

/// <inheritdoc cref="IParserManager"/>
public class ParserManager : IParserManager
{
    /// <inheritdoc/>
    public ParserResult Parse(PreprocessedDocument document)
    {
        var errorListener = new ErrorListener();
        var parser = new SinumerikNCParser(Tokenize(document, errorListener));
        parser.RemoveErrorListeners();
        parser.AddErrorListener(errorListener);
        IParseTree tree = parser.file();
        return new ParserResult(tree, errorListener.Diagnostics);
    }

    /// <inheritdoc/>
    public IEnumerable<AbstractSymbol> GetSymbolTableForDocument(ParsedDocument document)
    {
        var walker = new ParseTreeWalker();
        var symbolListener = new SymbolListener(document);
        walker.Walk(symbolListener, document.ParseTree);

        var symbolTable = symbolListener.SymbolTable;
        return AddProcedureSymbolIfNeeded(document, symbolTable);
    }

    /// <inheritdoc/>
    public IEnumerable<AbstractSymbolUse> GetSymbolUseForDocument(SymbolisedDocument document)
    {
        var walker = new ParseTreeWalker();
        var symbolUseListener = new SymbolUseListener(document);
        walker.Walk(symbolUseListener, document.ParseTree);

        return symbolUseListener.SymbolUseTable;
    }

    /// <inheritdoc/>
    public IEnumerable<LanguageToken> GetLanguageTokensForDocument(SymbolisedDocument document)
    {
        var walker = new ParseTreeWalker();
        var languageTokenListener = new LanguageTokenListener();
        walker.Walk(languageTokenListener, document.ParseTree);

        return languageTokenListener.LanguageTokens;
    }
    
    private CommonTokenStream Tokenize(PreprocessedDocument document, ErrorListener errorListener)
    {
        ICharStream stream = CharStreams.fromString(document.Code);
        SinumerikNCLexer lexer = new SinumerikNCLexer(stream);
        lexer.RemoveErrorListeners();
        lexer.AddErrorListener(errorListener);
        return new CommonTokenStream(lexer);
    }

    private IList<AbstractSymbol> AddProcedureSymbolIfNeeded(ParsedDocument document, IList<AbstractSymbol> symbolTable)
    {
        var fileName = Path.GetFileNameWithoutExtension(document.Information.Uri.LocalPath);
        if (!symbolTable.Any(symbol => symbol is ProcedureSymbol) && document.Information.DocumentType is DocumentType.SubProcedure or DocumentType.ManufacturerSubProcedure)
        {
            var emptyRange = new Range { Start = new Position(0, 0), End = new Position(0, 0) };
            symbolTable.Add(new ProcedureSymbol(fileName, document.Information.Uri, emptyRange, emptyRange, Array.Empty<ParameterSymbol>()));
        }

        return symbolTable;
    }
}