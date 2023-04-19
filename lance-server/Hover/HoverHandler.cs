using System.Text;
using Antlr4.Runtime.Tree;
using LanceServer.Core.Configuration;
using LanceServer.Core.Symbol;
using LanceServer.Core.Workspace;
using LanceServer.Parser;
using LspTypes;
using Range = LspTypes.Range;

namespace LanceServer.Hover
{
    /// <summary>
    /// Handles hover requests and returns the respective data to be displayed
    /// </summary>
    public class HoverHandler : IHoverHandler
    {
        private ConfigurationManager _configurationManager;
        private readonly ParseTreeWalker _walker = new ParseTreeWalker();

        public HoverHandler(ConfigurationManager configurationManager)
        {
            _configurationManager = configurationManager;
        }

        public LspTypes.Hover ProcessRequest(SymbolisedDocument document, HoverParams requestParams, IWorkspace workspace)
        {
            var position = requestParams.Position;
            position.Line++;
            var hoverListener = new HoverListener(requestParams.Position);
            _walker.Walk(hoverListener, document.Tree);

            var token = hoverListener.Token;

            if (token == null || document.Placeholders.IsPlaceholder(token.Text))
            {
                return new LspTypes.Hover();
            }
            
            var symbol = workspace.GetSymbol(token.Text, document.Information.Uri);

            var tokenStart = new Position((uint)token.Line - 1, (uint)token.Column);
            var tokenEnd = new Position(tokenStart.Line, tokenStart.Character + (uint)token.Text.Length);

            return new LspTypes.Hover()
            {
                Contents = new SumType<string, MarkedString, MarkedString[], MarkupContent>(new MarkupContent()
                {
                    Kind = MarkupKind.Markdown, Value = CreateMarkdownString(symbol)
                }),
                Range = new Range()
                {
                    Start = tokenStart,
                    End = tokenEnd
                }
            };
        }

        private string CreateMarkdownString(ISymbol symbol)
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
}