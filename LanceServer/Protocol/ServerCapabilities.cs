using System.Runtime.Serialization;
using LspTypes;
using Newtonsoft.Json;

namespace LanceServer.Protocol;

[DataContract]
public class ServerCapabilities
{
    [DataMember(Name = "textDocumentSync")]
    [JsonProperty(Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public SumType<TextDocumentSyncOptions, TextDocumentSyncKind> TextDocumentSync { get; set; }

    [DataMember(Name = "completionProvider")]
    [JsonProperty(Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public CompletionOptions? CompletionProvider { get; set; }

    [DataMember(Name = "hoverProvider")]
    [JsonProperty(Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public SumType<bool, HoverOptions> HoverProvider { get; set; }

    [DataMember(Name = "signatureHelpProvider")]
    public SignatureHelpOptions? SignatureHelpProvider { get; set; }

    [DataMember(Name = "declarationProvider")]
    [JsonProperty(Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public SumType<bool, DeclarationOptions, DeclarationRegistrationOptions> DeclarationProvider { get; set; }

    [DataMember(Name = "definitionProvider")]
    [JsonProperty(Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public SumType<bool, DefinitionOptions> DefinitionProvider { get; set; }

    [DataMember(Name = "typeDefinitionProvider")]
    [JsonProperty(Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public SumType<bool, TypeDefinitionOptions, TypeDefinitionRegistrationOptions> TypeDefinitionProvider { get; set; }

    [DataMember(Name = "implementationProvider")]
    [JsonProperty(Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public SumType<bool, ImplementationOptions, ImplementationRegistrationOptions> ImplementationProvider { get; set; }

    [DataMember(Name = "referencesProvider")]
    [JsonProperty(Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public SumType<bool, ReferenceOptions> ReferencesProvider { get; set; }

    [DataMember(Name = "documentHighlightProvider")]
    [JsonProperty(Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public SumType<bool, DocumentHighlightOptions> DocumentHighlightProvider { get; set; }

    [DataMember(Name = "documentSymbolProvider")]
    [JsonProperty(Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public SumType<bool, DocumentSymbolOptions> DocumentSymbolProvider { get; set; }

    [DataMember(Name = "codeActionProvider")]
    [JsonProperty(Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public SumType<bool, CodeActionOptions> CodeActionProvider { get; set; }

    [DataMember(Name = "codeLensProvider")]
    public CodeLensOptions? CodeLensProvider { get; set; }

    [DataMember(Name = "documentLinkProvider")]
    public DocumentLinkOptions? DocumentLinkProvider { get; set; }

    [DataMember(Name = "colorProvider")]
    [JsonProperty(Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public SumType<bool, DocumentColorOptions, DocumentColorRegistrationOptions> ColorProvider { get; set; }

    [DataMember(Name = "documentFormattingProvider")]
    [JsonProperty(Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public SumType<bool, DocumentFormattingOptions> DocumentFormattingProvider { get; set; }

    [DataMember(Name = "documentRangeFormattingProvider")]
    [JsonProperty(Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public SumType<bool, DocumentRangeFormattingOptions> DocumentRangeFormattingProvider { get; set; }

    [DataMember(Name = "documentOnTypeFormattingProvider")]
    public DocumentOnTypeFormattingOptions? DocumentOnTypeFormattingProvider { get; set; }

    [DataMember(Name = "renameProvider")]
    [JsonProperty(Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public SumType<bool, RenameOptions> RenameProvider { get; set; }

    [DataMember(Name = "foldingRangeProvider")]
    [JsonProperty(Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public SumType<bool, FoldingRangeOptions, FoldingRangeRegistrationOptions> FoldingRangeProvider { get; set; }

    [DataMember(Name = "executeCommandProvider")]
    public ExecuteCommandOptions? ExecuteCommandProvider { get; set; }

    [DataMember(Name = "selectionRangeProvider")]
    [JsonProperty(Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public SumType<bool, SelectionRangeOptions, SelectionRangeRegistrationOptions> SelectionRangeProvider { get; set; }

    [DataMember(Name = "linkedEditingRangeProvider")]
    [JsonProperty(Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public SumType<bool, LinkedEditingRangeOptions, LinkedEditingRangeRegistrationOptions> LinkedEditingRangeProvider { get; set; }

    [DataMember(Name = "callHierarchyProvider")]
    [JsonProperty(Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public SumType<bool, CallHierarchyOptions, CallHierarchyRegistrationOptions> CallHierarchyProvider { get; set; }

    [DataMember(Name = "semanticTokensProvider")]
    [JsonProperty(Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public SumType<SemanticTokensOptions, SemanticTokensRegistrationOptions> SemanticTokensProvider { get; set; }

    [DataMember(Name = "monikerProvider")]
    [JsonProperty(Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public SumType<bool, MonikerOptions, MonikerRegistrationOptions> MonikerProvider { get; set; }

    [DataMember(Name = "diagnosticProvider")]
    [JsonProperty(Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public SumType<DiagnosticOptions, DiagnosticRegistrationOptions> DiagnosticProvider { get; set; }

    [DataMember(Name = "workspaceSymbolProvider")]
    [JsonProperty(Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public SumType<bool, WorkspaceSymbolOptions> WorkspaceSymbolProvider { get; set; }

    [DataMember(Name = "workspace")] 
    public _ServerCapabilities_Workspace? Workspace { get; set; }

    [DataMember(Name = "experimental")] 
    public object? Experimental { get; set; }
}