﻿using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using LanceServer.Core.Symbol;
using LanceServer.Core.Workspace;
using LspTypes;
using Range = LspTypes.Range;

namespace LanceServer.Parser;

/// <inheritdoc cref="IParserManager"/>
public class ParserManager : IParserManager
{
    private CommonTokenStream Tokenize(PreprocessedDocument document)
    {
        ICharStream stream = CharStreams.fromString(document.Code);
        ITokenSource lexer = new SinumerikNCLexer(stream);
        return new CommonTokenStream(lexer);
    }

    /// <inheritdoc/>
    public IParseTree Parse(PreprocessedDocument document)
    {
        var parser = new SinumerikNCParser(Tokenize(document));
        IParseTree tree = parser.file();
        return tree;
    }

    /// <inheritdoc/>
    public IEnumerable<ISymbol> GetSymbolTableForDocument(ParsedDocument document)
    {
        var walker = new ParseTreeWalker();
        var symbolListener = new SymbolListener(document);
        walker.Walk(symbolListener, document.Tree);

        var symbolTable = symbolListener.SymbolTable;
        var fileName = Path.GetFileNameWithoutExtension(document.Information.Uri.LocalPath);
        if (!symbolTable.Any(symbol => symbol.Identifier.Equals(fileName, StringComparison.InvariantCultureIgnoreCase)) && document.Information.IsSubProcedure)
        {
            var emptyRange = new Range(){Start = new Position(0, 0), End = new Position(0, 0)};
            symbolTable.Add(new ProcedureSymbol(fileName, document.Information.Uri, emptyRange, emptyRange, Array.Empty<ParameterSymbol>()));
        }

        return symbolTable;
    }

    /// <inheritdoc/>
    public IEnumerable<SymbolUse> GetSymbolUseForDocument(SymbolisedDocument document)
    {
        var walker = new ParseTreeWalker();
        var symbolUseListener = new SymbolUseListener(document);
        walker.Walk(symbolUseListener, document.Tree);

        return symbolUseListener.SymbolUseTable;
    }
}