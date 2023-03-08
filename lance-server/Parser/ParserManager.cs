using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using LanceServer.Core.Workspace;

namespace LanceServer.Parser;

public class ParserManager
{
    public CommonTokenStream Tokenize(Document document)
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
}