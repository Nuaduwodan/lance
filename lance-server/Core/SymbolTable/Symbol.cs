using LspTypes;

namespace LanceServer.Core.SymbolTable
{
    public class Symbol
    {
        public readonly string Identifier;
        public readonly string Description;
        public readonly Uri SourceDocument;
        private readonly SymbolType _symbolType;
        private readonly Position _position;

        public Symbol(string description) : this(String.Empty, SymbolType.None, null, null, description)
        {
        }
        
        public Symbol(string identifier, SymbolType symbolType, Uri sourceDocument, Position position, string description = "")
        {
            Description = description;
            Identifier = identifier;
            _symbolType = symbolType;
            SourceDocument = sourceDocument;
            _position = position;
        }
        
        public virtual string GetCode()
        {
            switch (_symbolType)
            {
                case SymbolType.Function:
                    return ((FunctionSymbol)this).GetCode();
                case SymbolType.Macro:
                    return ((MacroSymbol)this).GetCode();
                case SymbolType.Parameter:
                    return ((ParameterSymbol)this).GetCode();
                case SymbolType.Variable:
                    return ((VariableSymbol)this).GetCode();
                case SymbolType.None:
                default:
                    return String.Empty;
            }
        }
    }
}