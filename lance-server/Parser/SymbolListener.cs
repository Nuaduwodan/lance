using LanceServer.Core.SymbolTable;
using Position = LspTypes.Position;

namespace LanceServer.Parser
{
    public class SymbolListener : SinumerikNCBaseListener
    {
        public List<ISymbol> SymbolTable { get; } = new();
        public Uri Document { get; }

        private List<ParameterSymbol> _parameters = new();

        public SymbolListener(Uri documentUri)
        {
            Document = documentUri;
        }

        /// <inheritdoc/>
        public override void EnterProcedureDefinition(SinumerikNCParser.ProcedureDefinitionContext context)
        {
            base.EnterProcedureDefinition(context);
            _parameters = new List<ParameterSymbol>();
        }

        /// <inheritdoc/>
        public override void ExitParameterDefinitionByReference(SinumerikNCParser.ParameterDefinitionByReferenceContext context)
        {
            base.ExitParameterDefinitionByReference(context);
            var symbol = new ParameterSymbol(context.NAME().GetText(), Document, new Position((uint)context.Start.Line, (uint)context.Start.Column),
                GetCompositeDataType(context.type()), GetArrayDeclaration(context.arrayDeclaration()),true);
            _parameters.Add(symbol);
            SymbolTable.Add(symbol);
        }
        
        /// <inheritdoc/>
        public override void ExitParameterDefinitionByValue(SinumerikNCParser.ParameterDefinitionByValueContext context)
        {
            base.ExitParameterDefinitionByValue(context);
            var symbol = new ParameterSymbol(context.NAME().GetText(), Document, new Position((uint)context.Start.Line, (uint)context.Start.Column),
                GetCompositeDataType(context.type()), Array.Empty<string>());
            _parameters.Add(symbol);
            SymbolTable.Add(symbol);
        }

        /// <inheritdoc/>
        public override void ExitProcedureDefinition(SinumerikNCParser.ProcedureDefinitionContext context)
        {
            base.ExitProcedureDefinition(context);
            var symbol = new ProcedureSymbol(context.NAME().GetText(), Document, new Position((uint)context.Start.Line, (uint)context.Start.Column), _parameters.ToArray());
            SymbolTable.Add(symbol);
        }

        public override void ExitProcedureDeclaration(SinumerikNCParser.ProcedureDeclarationContext context)
        {
            base.ExitProcedureDeclaration(context);
            //TODO 
        }

        /// <inheritdoc/>
        public override void ExitMacroDeclaration(SinumerikNCParser.MacroDeclarationContext context)
        {
            base.ExitMacroDeclaration(context);
            var symbol = new MacroSymbol(context.NAME().GetText(), Document, new Position((uint)context.Start.Line, (uint)context.Start.Column), context.macroValue().GetText());
            SymbolTable.Add(symbol);
        }

        /// <inheritdoc/>
        public override void ExitVariableDeclaration(SinumerikNCParser.VariableDeclarationContext context)
        {
            base.ExitVariableDeclaration(context);
            var type = GetCompositeDataType(context.type());
            foreach (var variable in context.variableAssignment())
            {
                var symbol = new VariableSymbol(variable.NAME().GetText(), Document, new Position((uint)context.Start.Line, (uint)context.Start.Column), type, GetArrayDefinition(variable.arrayDefinition()));
                SymbolTable.Add(symbol);
            }
        }

        private CompositeDataType GetCompositeDataType(SinumerikNCParser.TypeContext typeContext)
        {
            var type = GetDataType(typeContext);
            string length = "";
            if (type == DataType.String)
            {
                length = typeContext.expression().GetText();
            }
            return new CompositeDataType(type, length);
        }

        private DataType GetDataType(SinumerikNCParser.TypeContext typeContext)
        {
            bool ignoreCase = true;
            if(typeContext.GetText().StartsWith(DataType.String.ToString(),StringComparison.InvariantCultureIgnoreCase))
            {
                return DataType.String;
            }

            return Enum.Parse<DataType>(typeContext.GetText(), ignoreCase);
        }

        private string[] GetArrayDeclaration(SinumerikNCParser.ArrayDeclarationContext? arrayContext)
        {
            if (arrayContext == null)
            {
                return Array.Empty<string>();
            }

            var size = arrayContext.arrayDeclarationDimension().Length + 1;
            var dimensions = new string[size];
            dimensions[0] = arrayContext.expression().GetText();
            for (int i = 1; i < size; i++)
            {
                dimensions[i] = arrayContext.arrayDeclarationDimension()[i - 1].expression().GetText();
            }

            return dimensions;
        }

        private string[] GetArrayDefinition(SinumerikNCParser.ArrayDefinitionContext? arrayContext)
        {
            if (arrayContext == null)
            {
                return Array.Empty<string>();
            }

            return arrayContext.expression().Select(expression => expression.GetText()).ToArray();
        }
    }
}