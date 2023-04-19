using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using LanceServer.Core.Symbol;
using LanceServer.Core.Workspace;
using LspTypes;

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

    public IParseTree Parse(PreprocessedDocument document)
    {
        var parser = new SinumerikNCParser(Tokenize(document));
        IParseTree tree = parser.file();
        return tree;
    }

    public List<ISymbol> GetSymbolTableForDocument(ParsedDocument document)
    {
        var walker = new ParseTreeWalker();
        var symbolListener = new SymbolListener(document);
        walker.Walk(symbolListener, document.Tree);

        var symbolTable = symbolListener.SymbolTable;
        var fileName = Path.GetFileNameWithoutExtension(document.Information.Uri.LocalPath);
        if (!symbolTable.Any(symbol => symbol.Identifier.Equals(fileName, StringComparison.InvariantCultureIgnoreCase)) && document.Information.IsSubProcedure)
        {
            symbolTable.Add(new ProcedureSymbol(fileName, document.Information.Uri, new Position(0, 0), Array.Empty<ParameterSymbol>()));
        }

        return symbolTable;
    }
}