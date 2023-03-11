using System.Text;
using LanceServer.Core.Configuration;
using LanceServer.Core.Workspace;
using LspTypes;
using Range = LspTypes.Range;

namespace LanceServer.Hover
{
    /// <summary>
    /// Handles hover requests and returns the respective data to be displayed
    /// </summary>
    public class HoverHandler
    {
        private HoverVisitor visitor = new HoverVisitor();
        private DocumentationConfiguration _documentation;

        public HoverHandler(DocumentationConfiguration documentation)
        {
            _documentation = documentation;
        }

        public LspTypes.Hover ProcessRequest(Document document, HoverParams requestParams, Workspace workspace)
        {
            var token = visitor.Visit(document.Tree);

            if (token.Type != SinumerikNCLexer.NAME)
            {
                return new LspTypes.Hover();
            }
            
            var symbol = workspace.GetSymbol(token.Text, document.Uri);

            return new LspTypes.Hover()
            {
                Contents = new SumType<string, MarkedString, MarkedString[], MarkupContent>(new MarkupContent()
                {
                    Kind = MarkupKind.Markdown, Value = CreateMarkdownString(symbol.GetCode(), symbol.Description)
                }),
                Range = new Range()
                {
                    Start = new Position((uint)token.Line, (uint)token.Column),
                    End = new Position((uint)token.Line, (uint)(token.Column + token.Text.Length))
                }
            };
        }

        private string CreateMarkdownString(string code, string description = "")
        {
            var markdownString = new StringBuilder();
            if (!string.IsNullOrEmpty(code))
            {
                markdownString.Append("```sinumeriknc\n");
                markdownString.Append(code);
                markdownString.Append("\n```");
            }

            if (!(string.IsNullOrEmpty(code) || string.IsNullOrEmpty(description)))
            {
                markdownString.Append("\n\n***\n\n");
            }
            
            if (!string.IsNullOrEmpty(description))
            {
                markdownString.Append(description);
            }

            return markdownString.ToString();
        }
    }
}