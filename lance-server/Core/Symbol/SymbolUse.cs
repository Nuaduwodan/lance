using LspTypes;
using Range = LspTypes.Range;

namespace LanceServer.Core.Symbol
{
    /// <summary>
    /// Represents a use of a symbol
    /// </summary>
    public class SymbolUse
    {
        public string Identifier { get; }

        public Range Position { get; }

        public Uri SourceDocument { get; }

        /// <summary>
        /// Creates a new symbol use
        /// </summary>
        /// <param name="identifier">The identifier of the symbol used</param>
        /// <param name="position">The position of the usage</param>
        /// <param name="sourceDocument">The source document of the usage</param>
        public SymbolUse(string identifier, Range position, Uri sourceDocument)
        {
            Identifier = identifier;
            Position = position;
            SourceDocument = sourceDocument;
        }
    }
}