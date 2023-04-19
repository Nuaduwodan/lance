using LanceServer.Core.Symbol;
using LanceServer.Core.Workspace;
using Position = LspTypes.Position;

namespace LanceServer.Parser
{
    public class SymbolListener : SinumerikNCBaseListener
    {
        public List<ISymbol> SymbolTable { get; } = new();
        private readonly PlaceholderPreprocessedDocument _document;

        private List<ParameterSymbol> _parameters = new();

        public SymbolListener(PlaceholderPreprocessedDocument document)
        {
            _document = document;
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
            var symbol = new ParameterSymbol(context.NAME().GetText(), _document.Information.Uri, new Position((uint)context.Start.Line, (uint)context.Start.Column),
                GetCompositeDataType(context.type()), GetArrayDeclaration(context.arrayDeclaration()),true);
            _parameters.Add(symbol);
            SymbolTable.Add(symbol);
        }
        
        /// <inheritdoc/>
        public override void ExitParameterDefinitionByValue(SinumerikNCParser.ParameterDefinitionByValueContext context)
        {
            base.ExitParameterDefinitionByValue(context);
            var symbol = new ParameterSymbol(context.NAME().GetText(), _document.Information.Uri, new Position((uint)context.Start.Line, (uint)context.Start.Column),
                GetCompositeDataType(context.type()), Array.Empty<string>());
            _parameters.Add(symbol);
            SymbolTable.Add(symbol);
        }

        /// <inheritdoc/>
        public override void ExitProcedureDefinitionHeader(SinumerikNCParser.ProcedureDefinitionHeaderContext context)
        {
            base.ExitProcedureDefinitionHeader(context);
            var symbol = new ProcedureSymbol(context.NAME().GetText(), _document.Information.Uri, new Position((uint)context.Start.Line, (uint)context.Start.Column), _parameters.ToArray());
            SymbolTable.Add(symbol);
        }

        /// <inheritdoc/>
        public override void ExitMacroDeclaration(SinumerikNCParser.MacroDeclarationContext context)
        {
            base.ExitMacroDeclaration(context);
            var symbol = new MacroSymbol(context.NAME().GetText(), _document.Information.Uri, new Position((uint)context.Start.Line, (uint)context.Start.Column), ReplacePlaceholder(context.macroValue().GetText()), _document.Information.IsGlobalFile);
            SymbolTable.Add(symbol);
        }

        /// <inheritdoc/>
        public override void ExitVariableDeclaration(SinumerikNCParser.VariableDeclarationContext context)
        {
            base.ExitVariableDeclaration(context);
            var type = GetCompositeDataType(context.type());
            foreach (var variable in context.variableNameDeclaration())
            {
                var symbol = new VariableSymbol(variable.NAME().GetText(), _document.Information.Uri, new Position((uint)context.Start.Line, (uint)context.Start.Column), type, GetArrayDefinition(variable.arrayDefinition()), _document.Information.IsGlobalFile);
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

            var dimensions = new string[arrayContext.COMMA().Length + 1];
            if (arrayContext.first != null)
            {
                dimensions[0] = arrayContext.first.GetText();
            }
            if (arrayContext.second != null)
            {
                dimensions[1] = arrayContext.second.GetText();
            }
            if (arrayContext.third != null)
            {
                dimensions[2] = arrayContext.third.GetText();
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

        private string ReplacePlaceholder(string textWithPlaceholder)
        {
            return _document.Placeholders.ReplacePlaceholder(textWithPlaceholder);
        }
    }
}