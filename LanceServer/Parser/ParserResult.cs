using Antlr4.Runtime.Tree;
using LspTypes;

namespace LanceServer.Parser;

/// <summary>
/// The result of the parser.
/// </summary>
public class ParserResult
{
    public IParseTree ParseTree { get; }
    
    public IList<Diagnostic> Diagnostics { get; }
    
    public ParserResult(IParseTree parseTree, IList<Diagnostic> diagnostics)
    {
        ParseTree = parseTree;
        Diagnostics = diagnostics;
    }
}