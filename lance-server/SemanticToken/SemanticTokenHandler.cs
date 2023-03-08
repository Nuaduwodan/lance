using Antlr4.Runtime;
using LanceServer.Core.Workspace;
using LspTypes;

namespace LanceServer.SemanticToken
{
    /// <summary>
    /// Class responsible for handling semantic token requests
    /// </summary>
    public class SemanticTokenHandler
    {

        public SemanticTokens ProcessRequest(Document document, DocumentSymbolParams requestParams, CommonTokenStream tokens)
        {
            SemanticTokenData tokenData = new SemanticTokenData();
            
            int previousLine = 0;
            int previousStartChar = 0;
            while (tokens.LA(1) != IntStreamConstants.EOF)
            {
                tokens.Consume();
                var token = tokens.LT(0);
                int deltaLine = token.Line - previousLine;
                int deltaChar = token.StartIndex - previousStartChar;
                int length = token.StopIndex - token.StartIndex + 1;
                int type = TransformType(token.Type);
                tokenData.AddElement(new SemanticTokenDataElement(deltaLine, deltaChar, length, type, 0));
                previousLine = token.Line;
                previousStartChar = token.StartIndex;
            }
                    
            return new SemanticTokens
            {
                Data = tokenData.ToDataFormat()
            };
        }
        
        /// <summary>
        /// Maps the token type as defined by the grammar to a type as defined by the LSP.
        /// <seealso cref="SinumerikNCLexer"/>
        /// </summary>
        /// <param name="tokenType">The token type as defined by the grammar.</param>
        public int TransformType(int tokenType)
        {
            switch (tokenType)
            {
                case SinumerikNCLexer.COMMENT:
                    return (int) SemanticTokenTypeHelper.SemanticTokenType.Comment;
                case SinumerikNCLexer.PROC:
                case SinumerikNCLexer.AUXILIARY_FUNCTION:
                case SinumerikNCLexer.ADDITIONAL_FUNCTION:
                case SinumerikNCLexer.PREPARATORY_FUNCTION:
                    return (int) SemanticTokenTypeHelper.SemanticTokenType.Keyword;
                case SinumerikNCLexer.INT_TYPE:
                case SinumerikNCLexer.AXIS_TYPE:
                case SinumerikNCLexer.BOOL_TYPE:
                case SinumerikNCLexer.CHAR_TYPE:
                case SinumerikNCLexer.REAL_TYPE:
                case SinumerikNCLexer.FRAME_TYPE:
                case SinumerikNCLexer.STRING_TYPE:
                    return (int) SemanticTokenTypeHelper.SemanticTokenType.Type;
                case SinumerikNCLexer.BLOCK_NUMBER:
                    return (int) SemanticTokenTypeHelper.SemanticTokenType.Decorator;
                case SinumerikNCLexer.REAL:
                case SinumerikNCLexer.INT:
                    return (int) SemanticTokenTypeHelper.SemanticTokenType.Number;
                case SinumerikNCLexer.ADD:
                case SinumerikNCLexer.SUB:
                    return (int) SemanticTokenTypeHelper.SemanticTokenType.Operator;
                // Names should be separated into their purposes
                case SinumerikNCLexer.PROGRAM_NAME_SIMPLE:
                case SinumerikNCLexer.PROGRAM_NAME_EXTENDED:
                    return (int) SemanticTokenTypeHelper.SemanticTokenType.Variable;
                default:
                    return (int) SemanticTokenTypeHelper.SemanticTokenType.Decorator;
            }
        }
    }
}

