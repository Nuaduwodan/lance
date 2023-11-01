using System.Text;
using LanceServer.Core.Configuration;
using LanceServer.Core.Document;
using LanceServer.Core.Symbol;
using LanceServer.Core.Workspace;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using Range = OmniSharp.Extensions.LanguageServer.Protocol.Models.Range;

namespace LanceServer.RequestHandler.HoverHandler;

/// <summary>
/// Handles hover requests and returns the respective data to be displayed
/// </summary>
public class HoverLogic
{
    private readonly IConfigurationManager _configurationManager;

    /// <summary>
    /// Instantiates a new <see cref="HoverLogic"/>
    /// </summary>
    public HoverLogic(IConfigurationManager configurationManager)
    {
        _configurationManager = configurationManager;
    }

    /// <summary>
    /// Handles the hover request.
    /// </summary>
    /// <param name="document">The document with the necessary symbol information</param>
    /// <param name="position">The hover position</param>
    /// <param name="workspace">The workspace</param>
    /// <returns>The hover response <see cref="Hover"/></returns>
    public Hover HandleRequest(LanguageTokenExtractedDocument document, Position position, IWorkspace workspace)
    {
        var hover = new Hover();
        
        if (document.SymbolUseTable.TryGetSymbol(position, out var symbolUse))
        {
            var symbols = workspace.GetSymbols(symbolUse.Identifier, document.Information.Uri).ToList();
            if (symbols.Any())
            {
                var hoverSymbol = symbols.First();
                if (symbolUse is ProcedureUse procedureUse && hoverSymbol is ProcedureSymbol)
                {
                    var betterHoverSymbol = symbols.Select(symbol => symbol as ProcedureSymbol).FirstOrDefault(symbol => symbol!.ArgumentsMatchParameters(procedureUse.Arguments));
                    hoverSymbol = betterHoverSymbol ?? hoverSymbol;
                }
                
                hover = CreateHover(CreateMarkdownString(hoverSymbol), symbolUse.Range);
            }
        }

        if (document.LanguageTokenTable.TryGetToken(position, out var languageToken))
        {
            var token = _configurationManager.DocumentationConfiguration.LanguageTokens.FirstOrDefault(token => token.Code == languageToken.Code);
            if (token != null)
            {
                hover = CreateHover(token.Description, languageToken.Range);
            }
        }

        return hover;
    }

    private Hover CreateHover(string value, Range range)
    {
        return new Hover()
        {
            Contents = new MarkedStringsOrMarkupContent(new MarkupContent()
            {
                Kind = MarkupKind.Markdown, Value = value
            }),
            Range = range
        };
    }

    private string CreateMarkdownString(AbstractSymbol symbol)
    {
        var markdownString = new StringBuilder();
        var description = symbol.Description;
        var code = symbol.Code;
        var documentation = symbol.Documentation;

        if (!string.IsNullOrEmpty(description))
        {
            markdownString.Append(description);
            markdownString.Append("\n");
        }
            
        if (!string.IsNullOrEmpty(code))
        {
            markdownString.Append("```sinumeriknc\n");
            markdownString.Append(code);
            markdownString.Append("\n```");
        }

        // if code and documentation are not empty
        if (!(string.IsNullOrEmpty(code) || string.IsNullOrEmpty(documentation)))
        {
            markdownString.Append("\n\n***\n\n");
        }
            
        if (!string.IsNullOrEmpty(documentation))
        {
            markdownString.Append(EscapeMarkdown(documentation));
        }

        return markdownString.ToString();
    }

    private string EscapeMarkdown(string text)
    {
        text = text.Replace("\\", "\\\\");
        text = text.Replace("*", "\\*");
        text = text.Replace("_", "\\_");
        text = text.Replace(".", "\\.");
        text = text.Replace("<", "\\<");
        text = text.Replace(">", "\\>");
        return text;
    }
}