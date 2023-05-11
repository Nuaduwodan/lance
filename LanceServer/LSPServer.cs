using LanceServer.Core.Configuration;
using LanceServer.Core.Configuration.DataModel;
using Newtonsoft.Json.Linq;
using StreamJsonRpc;
using LanceServer.Core.Workspace;
using LanceServer.LanguageServerProtocol;
using LanceServer.RequestHandler.Diagnostic;
using LanceServer.RequestHandler.GoToDefinition;
using LanceServer.RequestHandler.Hover;
using LanceServer.RequestHandler.SemanticToken;
using LspTypes;
using StreamJsonRpc.Protocol;
using InitializeResult = LanceServer.LanguageServerProtocol.InitializeResult;
using ServerCapabilities = LanceServer.LanguageServerProtocol.ServerCapabilities;

namespace LanceServer;

class LSPServer : IDisposable
{
    private const string InvalidParamsMessage = "params could not be parsed into type ";
    private const string TraceIn = "<-- ";
    private const string TraceOut = "--> ";
        
    private readonly JsonRpc _rpc;
        
    private readonly ManualResetEvent _disconnectEvent = new(false);
    private bool _trace = true;

    private bool _isDisposed;

    public event EventHandler Disconnected;
        
    /// <summary>
    /// The workspace containing all the files 
    /// </summary>
    private readonly IWorkspace _workspace;
    private readonly IConfigurationManager _configurationManager;
    private readonly ISemanticTokenHandler _semanticTokenHandler;
    private readonly IHoverHandler _hoverHandler;
    private readonly IGotoDefinitionHandler _gotoDefinitionHandler;
    private readonly IDiagnosticHandler _diagnosticHandler;

    public LSPServer(JsonRpc jsonRpc, IWorkspace workspace, IConfigurationManager configurationManager, ISemanticTokenHandler semanticTokenHandler, IHoverHandler hoverHandler, IGotoDefinitionHandler gotoDefinitionHandler, IDiagnosticHandler diagnosticHandler)
    {
        _rpc = jsonRpc;
        _rpc.AddLocalRpcTarget(this);
        _rpc.StartListening();
        _rpc.Disconnected += OnRpcDisconnected;
        
        _workspace = workspace;
        _configurationManager = configurationManager;
        _semanticTokenHandler = semanticTokenHandler;
        _hoverHandler = hoverHandler;
        _gotoDefinitionHandler = gotoDefinitionHandler;
        _diagnosticHandler = diagnosticHandler;
    }

    private void OnRpcDisconnected(object? sender, JsonRpcDisconnectedEventArgs e)
    {
        InternalExit();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed) return;

        if (disposing)
        {
            // free managed resources
            _disconnectEvent.Dispose();
        }

        _isDisposed = true;
    }

    public void WaitForExit()
    {
        _disconnectEvent.WaitOne();
    }

    ~LSPServer()
    {
        // Finalizer calls Dispose(false)
        Dispose(false);
    }

    private void InternalExit()
    {
        _disconnectEvent.Set();
        Disconnected?.Invoke(this, EventArgs.Empty);
        Environment.Exit(0);
    }

    /// <summary>
    /// Handles the initialize request
    /// </summary>
    [JsonRpcMethod(Methods.InitializeName)]
    public InitializeResult Initialize(int? processId, string? rootUri, ClientCapabilities capabilities, _InitializeParams_ClientInfo? clientInfo = null, string? locale = "utf-8", string? rootPath = null, object? initializationOptions = null, TraceValue trace = TraceValue.Off, WorkspaceFolder[]? workspaceFolders = null)
    {
        if (_trace)
        {
            Console.Error.WriteLine(TraceIn + nameof(Initialize));
        }

        InitializeResult result = new InitializeResult();
            
        try
        {
            _configurationManager.SetWorkspaceFolders(workspaceFolders);
            _configurationManager.ClientCapabilities = capabilities;
            ServerCapabilities serverCapabilities = new ServerCapabilities
            {
                TextDocumentSync = new TextDocumentSyncOptions
                {
                    OpenClose = true,
                    Change = TextDocumentSyncKind.Full,
                    Save = new SaveOptions
                    {
                        IncludeText = false
                    }
                },

                CompletionProvider = null,

                HoverProvider = true,

                SignatureHelpProvider = null,

                DefinitionProvider = true,

                TypeDefinitionProvider = false,

                ImplementationProvider = false,

                ReferencesProvider = new ReferenceOptions()
                {
                    WorkDoneProgress = true
                },

                DocumentHighlightProvider = false,

                DocumentSymbolProvider = false,

                CodeLensProvider = null,

                DocumentLinkProvider = null,

                DocumentFormattingProvider = false,

                DocumentRangeFormattingProvider = false,

                RenameProvider = false,

                FoldingRangeProvider = false,

                ExecuteCommandProvider = null,

                WorkspaceSymbolProvider = false,

                SemanticTokensProvider = new SemanticTokensOptions()
                {
                    Full = true,
                    Range = false,
                    Legend = new SemanticTokensLegend()
                    {
                        tokenTypes = SemanticTokenTypeHelper.GetTypes(),
                        tokenModifiers = SemanticTokenModifierHelper.GetModifiers()
                    }
                },
                DiagnosticProvider = new DiagnosticOptions{
                    InterFileDependencies = true,
                    WorkspaceDiagnostics = false
                }
            };

            result.Capabilities = serverCapabilities;
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception.ToString());
            throw new LocalRpcException(exception.Message, exception){ErrorCode = (int)JsonRpcErrorCode.InternalError};
        }
            
        if (_trace)
        {
            Console.Error.Write(TraceOut + nameof(InitializedAsync));
        }
            
        return result;
    }

    [JsonRpcMethod(Methods.InitializedName)]
    public async Task InitializedAsync()
    {
        if (_trace)
        {
            Console.Error.WriteLine(TraceIn + nameof(InitializedAsync));
        }
            
        try
        {
            var token = Guid.NewGuid().ToString();
            var progressReport = new Progress<WorkDoneProgressReport>();
            var shouldReport = _configurationManager.ClientCapabilities.Window.WorkDoneProgress == true && RequestWorkDoneProgressToken(token);
            
            if (shouldReport)
            {
                NotifyProgress(new WorkDoneProgressBegin
                            {
                                Kind = "begin",
                                Title = "Parsing files",
                                Message = "Parsing files",
                                Percentage = 0
                            }, token);
                progressReport.ProgressChanged += (_, workDoneReport)=>
                {
                    NotifyProgress(workDoneReport, token);
                };
            }
            
            _configurationManager.ExtractConfiguration(RequestConfiguration());
            await _workspace.InitWorkspaceAsync(progressReport);

            if (shouldReport)
            {
                NotifyProgress(new WorkDoneProgressEnd
                {
                    Kind = "end"
                }, token);
            }

            RequestDiagnosticRefresh();
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception.ToString());
            throw new LocalRpcException(exception.Message, exception){ErrorCode = (int)JsonRpcErrorCode.InternalError};
        }
            
        if (_trace)
        {
            Console.Error.Write(TraceOut + nameof(InitializedAsync));
        }
    }

    [JsonRpcMethod(Methods.ShutdownName)]
    public JToken Shutdown()
    {
        if (_trace)
        {
            Console.Error.WriteLine(TraceIn + nameof(Shutdown));
        }
            
        try
        {
            return new JObject();
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception.ToString());
            throw new LocalRpcException(exception.Message, exception){ErrorCode = (int)JsonRpcErrorCode.InternalError};
        }
    }

    [JsonRpcMethod(Methods.ExitName)]
    public void Exit()
    {
        if (_trace)
        {
            Console.Error.WriteLine(TraceIn + nameof(Exit));
        }
            
        try
        {
            InternalExit();
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception.ToString());
            throw new LocalRpcException(exception.Message, exception){ErrorCode = (int)JsonRpcErrorCode.InternalError};
        }
    }

    [JsonRpcMethod(Methods.WorkspaceDidChangeConfigurationName)]
    public void WorkspaceDidChangeConfiguration(Settings settings)
    {
        if (_trace)
        {
            Console.Error.WriteLine(TraceIn + nameof(WorkspaceDidChangeConfiguration));
        }

        try
        {
            _configurationManager.ExtractConfiguration(settings.Lance);
            _trace = settings.Lance.Trace;
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception.ToString());
            throw new LocalRpcException(exception.Message, exception){ErrorCode = (int)JsonRpcErrorCode.InternalError};
        }

        if (_trace)
        {
            Console.Error.Write(TraceOut + nameof(WorkspaceDidChangeConfiguration));
        }
    }

    public ServerConfiguration RequestConfiguration()
    {
        if (_trace)
        {
            Console.Error.WriteLine(TraceOut + nameof(RequestConfiguration));
        }

        ServerConfiguration result;
        
        try
        {
            result = Task.Run(async delegate
            {
                var response = await _rpc.InvokeWithParameterObjectAsync<ServerConfiguration[]>("workspace/configuration", new ConfigurationParams
                {
                    Items = new []{new ConfigurationItem
                    {
                        Section = "lance"
                    }}
                });
                return response[0];
            }).Result;
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception.ToString());
            throw new LocalRpcException(exception.Message, exception){ErrorCode = (int)JsonRpcErrorCode.InternalError};
        }

        if (_trace)
        {
            Console.Error.Write(TraceIn + nameof(RequestConfiguration));
        }

        return result;
    }
    
    public bool RequestDiagnosticRefresh()
    {
        if (_trace)
        {
            Console.Error.WriteLine(TraceOut + nameof(RequestDiagnosticRefresh));
        }

        var result = false;
        try
        {
            var task = Task.Run(async delegate
            {
                await _rpc.InvokeAsync("workspace/diagnostic/refresh");
            });

            task.Wait();
            result = task.IsCompletedSuccessfully;
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception.ToString());
        }

        if (_trace)
        {
            Console.Error.Write(TraceIn + nameof(RequestDiagnosticRefresh));
        }

        return result;
    }
    
    public bool RequestWorkDoneProgressToken(string token)
    {
        if (_trace)
        {
            Console.Error.WriteLine(TraceOut + nameof(RequestWorkDoneProgressToken));
        }

        bool result = false;
        
        try
        {
            var task = Task.Run(async delegate
            {
                await _rpc.InvokeWithParameterObjectAsync("window/workDoneProgress/create", new WorkDoneProgressCreateParams
                {
                    Token = token
                });
            });

            task.Wait();
            result = task.IsCompletedSuccessfully;
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception.ToString());
        }

        if (_trace)
        {
            Console.Error.Write(TraceIn + nameof(RequestWorkDoneProgressToken));
        }

        return result;
    }

    public void NotifyProgress<T>(T workDoneProgress, SumType<string, int> workDoneToken)
    {
        if (_trace)
        {
            Console.Error.WriteLine(TraceOut + nameof(NotifyProgress));
        }
        
        try
        {
            _rpc.NotifyWithParameterObjectAsync(Methods.ProgressNotificationName, new ProgressParams<T>()
            {
                Token = workDoneToken,
                Value = workDoneProgress
            });
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception.ToString());
        }

        if (_trace)
        {
            Console.Error.Write(TraceIn + nameof(NotifyProgress));
        }
    }
        
    [JsonRpcMethod(Methods.TextDocumentDidChangeName)]
    public void TextDocumentDidChange(VersionedTextDocumentIdentifier textDocument, TextDocumentContentChangeEvent[] contentChanges)
    {
        if (_trace)
        {
            Console.Error.WriteLine(TraceIn + nameof(TextDocumentDidChange));
        }
            
        var uri = FileUtil.UriStringToUri(textDocument.Uri);
            
        try
        {
            _workspace.UpdateDocumentContent(uri, contentChanges.Last().Text);
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception.ToString());
            throw new LocalRpcException(exception.Message, exception){ErrorCode = (int)JsonRpcErrorCode.InternalError};
        }

        if (_trace)
        {
            Console.Error.Write(TraceOut + nameof(TextDocumentDidChange));
        }
    }
        
    [JsonRpcMethod(Methods.TextDocumentDidOpenName)]
    public void TextDocumentDidOpen(TextDocumentItem textDocument)
    {
        if (_trace)
        {
            Console.Error.WriteLine(TraceIn + nameof(TextDocumentDidOpen));
        }
        
        var uri = FileUtil.UriStringToUri(textDocument.Uri);
            
        try
        {
            _workspace.GetSymbolisedDocument(uri);
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception.ToString());
            throw new LocalRpcException(exception.Message, exception){ErrorCode = (int)JsonRpcErrorCode.InternalError};
        }

        if (_trace)
        {
            Console.Error.Write(TraceOut + nameof(TextDocumentDidOpen));
        }
    }
        
    // Language Features
    
    [JsonRpcMethod(Method.TextDocumentDiagnostic)]
    public DocumentDiagnosticReport TextDocumentDiagnostic(TextDocumentIdentifier textDocument, string identifier = "", string previousResultId = "", SumType<string, int>? workDoneToken = null, SumType<string, int>? partialResultToken = null)
    {
        if (_trace)
        {
            Console.Error.WriteLine(TraceIn + nameof(TextDocumentDiagnostic));
        }
            
        var uri = FileUtil.UriStringToUri(textDocument.Uri);
        
        DocumentDiagnosticReport result;
        if (!_workspace.IsWorkspaceInitialised)
        {
            return null!;
        }
        
        try
        {
            var document = _workspace.GetSymbolUseExtractedDocument(uri);
            result = _diagnosticHandler.HandleRequest(document, _workspace);
        }
        catch (LocalRpcException)
        {
            throw;
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception.ToString());
            throw new LocalRpcException(exception.Message, exception) { ErrorCode = (int)JsonRpcErrorCode.InternalError };
        }

        if (_trace)
        {
            Console.Error.WriteLine(TraceOut + nameof(TextDocumentDiagnostic));
        }

        return result;
    }
    
    [JsonRpcMethod(Methods.TextDocumentDefinitionName)]
    public LocationLink[] TextDocumentDefinition(TextDocumentIdentifier textDocument, Position position, SumType<string, int>? workDoneToken = null, SumType<string, int>? partialResultToken = null)
    {
        if (_trace)
        {
            Console.Error.WriteLine(TraceIn + nameof(TextDocumentDefinition));
        }
        
        var uri = FileUtil.UriStringToUri(textDocument.Uri);
        LocationLink[] result = Array.Empty<LocationLink>();
            
        try
        {
            var document = _workspace.GetSymbolUseExtractedDocument(uri);
            result = _gotoDefinitionHandler.HandleRequest(document, position, _workspace);
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception.ToString());
            throw new LocalRpcException(exception.Message, exception){ErrorCode = (int)JsonRpcErrorCode.InternalError};
        }

        if (_trace)
        {
            Console.Error.Write(TraceOut + nameof(TextDocumentDefinition));
        }

        return result;
    }

    [JsonRpcMethod(Methods.TextDocumentSemanticTokensFull)]
    public SemanticTokens SemanticTokens(TextDocumentIdentifier textDocument, SumType<string, int>? workDoneToken = null, SumType<string, int>? partialResultToken = null)
    {
        if (_trace)
        {
            Console.Error.WriteLine(TraceIn + nameof(SemanticTokens));
        }

        var uri = FileUtil.UriStringToUri(textDocument.Uri);
        SemanticTokens result;
            
        try
        {
            var document = _workspace.GetSymbolUseExtractedDocument(uri);

            result = _semanticTokenHandler.ProcessRequest(document, _workspace);
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception.ToString());
            throw new LocalRpcException(exception.Message, exception){ErrorCode = (int)JsonRpcErrorCode.InternalError};
        }

        if (_trace)
        {
            Console.Error.Write(TraceOut + nameof(SemanticTokens));
            Console.Error.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
        }

        return result;
    }

    [JsonRpcMethod(Methods.TextDocumentHoverName)]
    public Hover Hover(TextDocumentIdentifier textDocument, Position position, SumType<string, int>? workDoneToken = null)
    {
        if (_trace)
        {
            Console.Error.WriteLine(TraceIn + nameof(Hover));
        }
        
        var uri = FileUtil.UriStringToUri(textDocument.Uri);
        Hover result;
            
        try
        {
            var document = _workspace.GetSymbolUseExtractedDocument(uri);

            result = _hoverHandler.HandleRequest(document, position, _workspace);
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception.ToString());
            throw new LocalRpcException(exception.Message, exception){ErrorCode = (int)JsonRpcErrorCode.InternalError};
        }

        if (_trace)
        {
            Console.Error.Write(TraceOut + nameof(Hover));
            Console.Error.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
        }

        return result;
    }
}