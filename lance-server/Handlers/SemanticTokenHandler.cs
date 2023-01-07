namespace LanceServer.Handlers
{
    using LspTypes;
    
    /// <summary>
    /// Class responsible for handling semantic token requests
    /// </summary>
    public class SemanticTokenHandler
    {
        
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
                    return (int) SemanticTokenTypes.Comment;
                case SinumerikNCLexer.PROCEDURE:
                case SinumerikNCLexer.AUXILIARY_FUNCTION:
                case SinumerikNCLexer.ADDITIONAL_FUNCTION:
                case SinumerikNCLexer.PREPARATORY_FUNCTION:
                    return (int) SemanticTokenTypes.Keyword;
                case SinumerikNCLexer.INT_TYPE:
                case SinumerikNCLexer.AXIS_TYPE:
                case SinumerikNCLexer.BOOL_TYPE:
                case SinumerikNCLexer.CHAR_TYPE:
                case SinumerikNCLexer.REAL_TYPE:
                case SinumerikNCLexer.FRAME_TYPE:
                case SinumerikNCLexer.STRING_TYPE:
                    return (int) SemanticTokenTypes.Type;
                case SinumerikNCLexer.BLOCK_NUMBER:
                    return (int) SemanticTokenTypes.Modifier;
                case SinumerikNCLexer.REAL:
                case SinumerikNCLexer.INT:
                    return (int) SemanticTokenTypes.Number;
                case SinumerikNCLexer.ADD:
                case SinumerikNCLexer.INC:
                case SinumerikNCLexer.SUB:
                case SinumerikNCLexer.DEC:
                    return (int) SemanticTokenTypes.Operator;
                // Names should be separated into their purposes
                case SinumerikNCLexer.PROGRAM_NAME_SIMPLE:
                case SinumerikNCLexer.PROGRAM_NAME_EXTENDED:
                    return (int) SemanticTokenTypes.Variable;
                default:
                    return (int) SemanticTokenTypes.Namespace;
            }
        }
    }
}

