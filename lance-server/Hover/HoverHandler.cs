﻿using System.Text;
using Antlr4.Runtime.Tree;
using LanceServer.Core.Configuration;
using LanceServer.Core.Symbol;
using LanceServer.Core.Workspace;
using LspTypes;
using Range = LspTypes.Range;

namespace LanceServer.Hover
{
    /// <inheritdoc cref="IHoverHandler"/>
    public class HoverHandler : IHoverHandler
    {
        private ConfigurationManager _configurationManager;
        private readonly ParseTreeWalker _walker = new ParseTreeWalker();

        /// <summary>
        /// Instantiates a new <see cref="HoverHandler"/>
        /// </summary>
        public HoverHandler(ConfigurationManager configurationManager)
        {
            _configurationManager = configurationManager;
        }

        /// <inheritdoc/>
        public LspTypes.Hover HandleRequest(SymbolUseExtractedDocument document, HoverParams requestParams, IWorkspace workspace)
        {
            var position = requestParams.Position;
            if (document.SymbolUseTable.TryGetSymbol(position, out var symbolUse))
            {
                var symbol = workspace.GetSymbol(symbolUse.Identifier.ToLower(), document.Information.Uri);
            
                return new LspTypes.Hover()
                {
                    Contents = new SumType<string, MarkedString, MarkedString[], MarkupContent>(new MarkupContent()
                    {
                        Kind = MarkupKind.Markdown, Value = CreateMarkdownString(symbol)
                    }),
                    Range = symbolUse.Position
                };
            }
            return new LspTypes.Hover();
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