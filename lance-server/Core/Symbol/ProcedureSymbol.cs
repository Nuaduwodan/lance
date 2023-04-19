using LspTypes;

namespace LanceServer.Core.Symbol
{
    /// <summary>
    /// A symbol representing a function, having zero or more <see cref="ParameterSymbol"/>s
    /// </summary>
    public class ProcedureSymbol : ISymbol
    {
        public string Identifier { get; }
        public Uri SourceDocument { get; }
        public Position Position { get; }
        public string Description => $"procedure in {Path.GetFileName(SourceDocument.LocalPath)}";
        public string Code => $"proc {Identifier}({string.Join(ParameterDelimiter, _parameters.Select(p => p.Code))})";
        public string Documentation { get; }

        private readonly ParameterSymbol[] _parameters;
        private const string ParameterDelimiter = ", ";

        public ProcedureSymbol(string identifier, Uri sourceDocument, Position position, ParameterSymbol[] parameters, string documentation = "")
        {
            Identifier = identifier;
            SourceDocument = sourceDocument;
            Position = position;
            _parameters = parameters;
            Documentation = documentation;
        }

    }
}
