using System.Diagnostics.CodeAnalysis;
using Antlr4.Runtime.Tree;
using LanceServer.Core.SymbolTable;

namespace LanceServer.Core.Workspace;

public class SymbolisedDocument : ParsedDocument
{
    private Dictionary<string, ISymbol> _symbols;

    public SymbolisedDocument(IEnumerable<ISymbol> symbols, IParseTree tree, Uri uri, string rawContent, string code, bool isGlobalFile = false, bool isSubProcedure = false, bool procedureNeedsDeclaration = true, string encoding = "utf8") 
        : base(tree, uri, rawContent, code, isGlobalFile, isSubProcedure, procedureNeedsDeclaration, encoding)
    {
        _symbols = symbols.ToDictionary(symbol => symbol.Identifier.ToLower(), symbol => symbol);
    }

    public SymbolisedDocument(ParsedDocument parsedDocument, IEnumerable<ISymbol> symbols) 
        : this(symbols, parsedDocument.Tree, parsedDocument.Uri, parsedDocument.RawContent, parsedDocument.Code, parsedDocument.IsGlobalFile, parsedDocument.IsSubProcedure, parsedDocument.ProcedureNeedsDeclaration, parsedDocument.Encoding)
    {
    }

    public bool TryGetSymbol(string symbolName, [MaybeNullWhen(false)] out ISymbol symbol)
    {
        return _symbols.TryGetValue(symbolName.ToLower(), out symbol);
    }
}