using LspTypes;

namespace LanceServer.Core.SymbolTable
{
    /// <summary>
    /// A symbol representing a function, having zero or more <see cref="ParameterSymbol"/>s
    /// </summary>
    public class ProcedureSymbol : ISymbol
    {
        public string Identifier { get; }
        public Uri SourceDocument { get; }
        public Position Position { get; }
        public string Description { get; }
        
        private readonly ParameterSymbol[] _parameters;
        private const string ParameterDelimiter = ", ";

        public ProcedureSymbol(string identifier, Uri sourceDocument, Position position, ParameterSymbol[] parameters, string description = "")
        {
            Identifier = identifier;
            SourceDocument = sourceDocument;
            Position = position;
            _parameters = parameters;
            Description = description;
        }

        public string GetCode()
        {
            var parameters = string.Join(ParameterDelimiter, _parameters.Select(p => p.GetCode()));
            return $"proc {Identifier}({parameters})";
        }

    }
}
