using LspTypes;

namespace LanceServer.Core.SymbolTable
{
    /// <summary>
    /// A symbol representing a function, having zero or more <see cref="ParameterSymbol"/>s
    /// </summary>
    public class FunctionSymbol : Symbol
    {
        private ParameterSymbol[] _parameters;
        private const string ParameterDelimiter = ", ";

        public FunctionSymbol(string identifier, SymbolType symbolType, Uri sourceDocument, Position position, ParameterSymbol[] parameters, string description = "") 
            : base(identifier, symbolType, sourceDocument, position, description)
        {
        }

        public override string GetCode()
        {
            var parameters = string.Join(ParameterDelimiter, _parameters.Select(p => p.GetCode()));
            return $"proc {Identifier}({parameters})";
        }

    }
}
