using Antlr4.Runtime;
using LanceServer.Core.Workspace;
using LspTypes;

namespace LanceServer.SemanticToken
{
    /// <summary>
    /// Class responsible for handling semantic token requests
    /// </summary>
    public class SemanticTokenHandler : ISemanticTokenHandler
    {

        public SemanticTokens ProcessRequest(ParsedDocument document, DocumentSymbolParams requestParams)
        {
            SemanticTokenData tokenData = new SemanticTokenData();
            
            //TODO use listener to find all usages of symbols and look them up in the symbol table
                    
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
                case SinumerikNCLexer.GCODE:
                case SinumerikNCLexer.MCODE:
                case SinumerikNCLexer.HCODE:
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
                case SinumerikNCLexer.REAL_UNSIGNED:
                case SinumerikNCLexer.INT_UNSIGNED:
                    return (int) SemanticTokenTypeHelper.SemanticTokenType.Number;
                case SinumerikNCLexer.ADD:
                case SinumerikNCLexer.SUB:
                    return (int) SemanticTokenTypeHelper.SemanticTokenType.Operator;
                // Names should be separated into their purposes
                case SinumerikNCLexer.NAME:
                    return (int) SemanticTokenTypeHelper.SemanticTokenType.Variable;
                default:
                    return (int) SemanticTokenTypeHelper.SemanticTokenType.Decorator;
            }
        }
    }
}

