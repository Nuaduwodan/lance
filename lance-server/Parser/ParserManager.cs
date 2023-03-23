using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using LanceServer.Core.SymbolTable;
using LanceServer.Core.Workspace;

namespace LanceServer.Parser;

public class ParserManager
{
    private CommonTokenStream Tokenize(Document document)
    {
        ICharStream stream = CharStreams.fromString(document.Content);
        ITokenSource lexer = new SinumerikNCLexer(stream);
        return new CommonTokenStream(lexer);
    }

    public IParseTree Parse(Document document)
    {
        var parser = new SinumerikNCParser(Tokenize(document));
        IParseTree tree = parser.file();
        return tree;
    }

    public List<ISymbol> GetSymbolTableForDocument(Document document)
    {
        if (document.State < DocumentState.Parsed)
        {
            throw new ArgumentException();
        }
        
        var walker = new ParseTreeWalker();
        var symbolListener = new SymbolListener(document.Uri);
        walker.Walk(symbolListener, document.Tree);

        return symbolListener.SymbolTable;
    }
}