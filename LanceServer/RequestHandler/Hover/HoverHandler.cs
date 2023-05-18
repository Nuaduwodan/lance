using System.Text;
using LanceServer.Core.Configuration;
using LanceServer.Core.Document;
using LanceServer.Core.Symbol;
using LanceServer.Core.Workspace;
using LspTypes;
using Range = LspTypes.Range;

namespace LanceServer.RequestHandler.Hover;

/// <inheritdoc cref="IHoverHandler"/>
public class HoverHandler : IHoverHandler
{
    private IConfigurationManager _configurationManager;

    /// <summary>
    /// Instantiates a new <see cref="HoverHandler"/>
    /// </summary>
    public HoverHandler(IConfigurationManager configurationManager)
    {
        _configurationManager = configurationManager;
    }

    /// <inheritdoc/>
    public LspTypes.Hover HandleRequest(LanguageTokenExtractedDocument document, Position position, IWorkspace workspace)
    {
        var hover = new LspTypes.Hover();
        
        if (document.SymbolUseTable.TryGetSymbol(position, out var symbolUse))
        {
            var symbols = workspace.GetSymbols(symbolUse.Identifier, document.Information.Uri).ToList();
            var value = symbols.Any()
                ? CreateMarkdownString(symbols.First())
                : EscapeMarkdown($"Cannot resolve symbol '{symbolUse.Identifier}'");
            hover = CreateHover(value, symbolUse.Range);
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

    private LspTypes.Hover CreateHover(string value, Range range)
    {
        return new LspTypes.Hover()
        {
            Contents = new SumType<string, MarkedString, MarkedString[], MarkupContent>(new MarkupContent()
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