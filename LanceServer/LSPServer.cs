using System.Diagnostics.CodeAnalysis;
using LanceServer.Core.Configuration;
using LanceServer.Core.Configuration.DataModel;
using Newtonsoft.Json.Linq;
using StreamJsonRpc;
using LanceServer.Core.Workspace;
using LanceServer.Protocol;
using LanceServer.RequestHandler.Diagnostic;
using LanceServer.RequestHandler.GoToDefinition;
using LanceServer.RequestHandler.Hover;
using LanceServer.RequestHandler.SemanticToken;
using LspTypes;
using StreamJsonRpc.Protocol;
using InitializeResult = LanceServer.Protocol.InitializeResult;
using SemanticTokenModifiers = LanceServer.RequestHandler.SemanticToken.SemanticTokenModifiers;
using ServerCapabilities = LanceServer.Protocol.ServerCapabilities;

namespace LanceServer;

/// <summary>
/// The remote procedure call interface to work as a language server as defined by the language server protocol.
/// </summary>
class LSPServer : IDisposable
{
    private const string TraceIn = "<-- ";
    private const string TraceOut = "--> ";
        
    private readonly JsonRpc _rpc;
        
    private readonly ManualResetEvent _disconnectEvent = new(false);

    private bool _isDisposed;

    public event EventHandler? Disconnected;
    
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
    
    //// Base protocol interface
    
    /// <summary>
    /// Handles the initialize request
    /// </summary>
    [JsonRpcMethod(Methods.InitializeName)]
    public InitializeResult Initialize(int? processId, string? rootUri, ClientCapabilities capabilities, _InitializeParams_ClientInfo? clientInfo = null, string? locale = "utf-8", string? rootPath = null, object? initializationOptions = null, TraceValue trace = TraceValue.Off, WorkspaceFolder[]? workspaceFolders = null)
    {
        Console.Error.WriteLine(TraceIn + nameof(Initialize));

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
                        tokenModifiers = SemanticTokenModifiers.GetModifiers()
                    }
                },
                DiagnosticProvider = new DiagnosticOptions
                {
                    InterFileDependencies = true,
                    WorkspaceDiagnostics = false
                }
            };

            result.Capabilities = serverCapabilities;
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception.ToString());
            throw new LocalRpcException(exception.Message, exception) { ErrorCode = (int)JsonRpcErrorCode.InternalError };
        }
            
        Console.Error.WriteLine(TraceOut + nameof(InitializedAsync));

        return result;
    }

    /// <summary>
    /// Handles the initialized notification.
    /// Triggers the processing of the whole workspace to provide more functionality afterwards.
    /// </summary>
    [JsonRpcMethod(Methods.InitializedName)]
    public async Task InitializedAsync()
    {
        await Console.Error.WriteLineAsync(TraceIn + nameof(InitializedAsync));

        try
        {
            var token = Guid.NewGuid().ToString();
            var progressReport = new Progress<WorkDoneProgressReport>();
            var shouldReport = _configurationManager.ClientCapabilities.Window.WorkDoneProgress == true && await RequestWorkDoneProgressTokenAsync(token);
            
            if (shouldReport)
            {
                NotifyProgress(new WorkDoneProgressBegin
                {
                    Kind = "begin",
                    Title = "Parsing files",
                    Message = "Parsing files",
                    Percentage = 0
                }, token);
                progressReport.ProgressChanged += (_, workDoneReport) =>
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

            await RequestDiagnosticRefreshAsync();
            await RequestSemanticTokensRefreshAsync();
        }
        catch (Exception exception)
        {
            await Console.Error.WriteLineAsync(exception.ToString());
            throw new LocalRpcException(exception.Message, exception) { ErrorCode = (int)JsonRpcErrorCode.InternalError };
        }
            
        await Console.Error.WriteAsync(TraceOut + nameof(InitializedAsync));
    }

    /// <summary>
    /// Handles the shutdown request.
    /// </summary>
    [JsonRpcMethod(Methods.ShutdownName)]
    public JToken Shutdown()
    {
        Console.Error.WriteLine(TraceIn + nameof(Shutdown));

        try
        {
            return new JObject();
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception.ToString());
            throw new LocalRpcException(exception.Message, exception) { ErrorCode = (int)JsonRpcErrorCode.InternalError };
        }
    }

    /// <summary>
    /// Handles the exit request.
    /// </summary>
    [JsonRpcMethod(Methods.ExitName)]
    public void Exit()
    {
        Console.Error.WriteLine(TraceIn + nameof(Exit));

        try
        {
            InternalExit();
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception.ToString());
            throw new LocalRpcException(exception.Message, exception) { ErrorCode = (int)JsonRpcErrorCode.InternalError };
        }
    }
    
    //// Server to client requests and notifications
    
    /// <summary>
    /// Requests the configuration from the client.
    /// </summary>
    [SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits")]
    public ServerConfiguration RequestConfiguration()
    {
        Console.Error.WriteLine(TraceOut + nameof(RequestConfiguration));

        ServerConfiguration result;
        
        try
        {
            result = Task.Run(async delegate
            {
                var response = await _rpc.InvokeWithParameterObjectAsync<ServerConfiguration[]>(Method.WorkspaceConfiguration, new ConfigurationParams
                {
                    Items = new[]
                    {
                        new ConfigurationItem
                        {
                            Section = "lance"
                        }
                    }
                });
                return response[0];
            }).Result;
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception.ToString());
            throw new LocalRpcException(exception.Message, exception) { ErrorCode = (int)JsonRpcErrorCode.InternalError };
        }

        Console.Error.WriteLine(TraceIn + nameof(RequestConfiguration));

        return result;
    }
    
    /// <summary>
    /// Requests a diagnostic refresh on all documents the client is interested.
    /// </summary>
    public async Task<bool> RequestDiagnosticRefreshAsync()
    {
        await Console.Error.WriteLineAsync(TraceOut + nameof(RequestDiagnosticRefreshAsync));

        var result = false;
        try
        {
            var task = Task.Run(async delegate
            {
                await _rpc.InvokeAsync(Method.WorkspaceDiagnosticRefresh);
            });
            await task;
            result = task.IsCompletedSuccessfully;
        }
        catch (Exception exception)
        {
            await Console.Error.WriteLineAsync(exception.ToString());
        }

        await Console.Error.WriteLineAsync(TraceIn + nameof(RequestDiagnosticRefreshAsync));

        return result;
    }
    
    /// <summary>
    /// Requests a semantic token refresh on all documents the client is interested.
    /// </summary>
    public async Task<bool> RequestSemanticTokensRefreshAsync()
    {
        await Console.Error.WriteLineAsync(TraceOut + nameof(RequestSemanticTokensRefreshAsync));

        var result = false;
        try
        {
            var task = Task.Run(async delegate
            {
                await _rpc.InvokeAsync(Method.WorkspaceSemanticTokensRefresh);
            });
            await task;
            result = task.IsCompletedSuccessfully;
        }
        catch (Exception exception)
        {
            await Console.Error.WriteLineAsync(exception.ToString());
        }

        await Console.Error.WriteLineAsync(TraceIn + nameof(RequestSemanticTokensRefreshAsync));

        return result;
    }
    
    /// <summary>
    /// Requests a progress token from the client to be able to report progress to it.
    /// </summary>
    /// <param name="token">The token which will be used.</param>
    public async Task<bool> RequestWorkDoneProgressTokenAsync(string token)
    {
        await Console.Error.WriteLineAsync(TraceOut + nameof(RequestWorkDoneProgressTokenAsync));

        var result = false;
        
        try
        {
            var task = Task.Run(async delegate
            {
                await _rpc.InvokeWithParameterObjectAsync(Method.WindowWorkDoneProgressCreate, new WorkDoneProgressCreateParams
                {
                    Token = token
                });
            });
            await task;
            result = task.IsCompletedSuccessfully;
        }
        catch (Exception exception)
        {
            await Console.Error.WriteLineAsync(exception.ToString());
        }

        await Console.Error.WriteLineAsync(TraceIn + nameof(RequestWorkDoneProgressTokenAsync));

        return result;
    }

    /// <summary>
    /// Notifies the client of the progress using a work done token.
    /// </summary>
    /// <typeparam name="T">The type of progress reported.</typeparam>
    [SuppressMessage("Usage", "VSTHRD110:Observe result of async calls")]
    public void NotifyProgress<T>(T workDoneProgress, SumType<string, int> workDoneToken)
    {
        Console.Error.WriteLine(TraceOut + nameof(NotifyProgress));

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

        Console.Error.WriteLine(TraceIn + nameof(NotifyProgress));
    }
    
    //// Client to server auxiliary requests and notifications

    /// <summary>
    /// Handles the did change configuration request.
    /// Updates the configuration.
    /// </summary>
    [JsonRpcMethod(Methods.WorkspaceDidChangeConfigurationName)]
    public void WorkspaceDidChangeConfiguration(Settings settings)
    {
        Console.Error.WriteLine(TraceIn + nameof(WorkspaceDidChangeConfiguration));

        try
        {
            _configurationManager.ExtractConfiguration(settings.Lance);
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception.ToString());
            throw new LocalRpcException(exception.Message, exception) { ErrorCode = (int)JsonRpcErrorCode.InternalError };
        }

        Console.Error.WriteLine(TraceOut + nameof(WorkspaceDidChangeConfiguration));
    }
    
    /// <summary>
    /// Handles the text document did change request.
    /// Updates the content of the respective document.
    /// </summary>
    [JsonRpcMethod(Methods.TextDocumentDidChangeName)]
    public void TextDocumentDidChange(VersionedTextDocumentIdentifier textDocument, TextDocumentContentChangeEvent[] contentChanges)
    {
        Console.Error.WriteLine(TraceIn + nameof(TextDocumentDidChange));

        var uri = FileUtil.UriStringToUri(textDocument.Uri);
            
        try
        {
            _workspace.UpdateDocumentContent(uri, contentChanges.Last().Text);
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception.ToString());
            throw new LocalRpcException(exception.Message, exception) { ErrorCode = (int)JsonRpcErrorCode.InternalError };
        }

        Console.Error.WriteLine(TraceOut + nameof(TextDocumentDidChange));
    }
    
    /// <summary>
    /// Handles the text document did open request.
    /// Requests the respective document from the workspace to reduce wait times on future requests for that document.
    /// </summary>
    [JsonRpcMethod(Methods.TextDocumentDidOpenName)]
    public void TextDocumentDidOpen(TextDocumentItem textDocument)
    {
        Console.Error.WriteLine(TraceIn + nameof(TextDocumentDidOpen));

        var uri = FileUtil.UriStringToUri(textDocument.Uri);
            
        try
        {
            if (!_workspace.IsWorkspaceInitialised) return;
            
            _workspace.GetSymbolUseExtractedDocument(uri);
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception.ToString());
            throw new LocalRpcException(exception.Message, exception) { ErrorCode = (int)JsonRpcErrorCode.InternalError };
        }

        Console.Error.WriteLine(TraceOut + nameof(TextDocumentDidOpen));
    }
        
    //// Client to server language feature requests
    
    /// <summary>
    /// Handles the text document diagnostic request.
    /// Generates the diagnostic report for the respective document and returns it.
    /// </summary>
    [JsonRpcMethod(Method.TextDocumentDiagnostic)]
    public DocumentDiagnosticReport TextDocumentDiagnostic(TextDocumentIdentifier textDocument, string identifier = "", string previousResultId = "", SumType<string, int>? workDoneToken = null, SumType<string, int>? partialResultToken = null)
    {
        Console.Error.WriteLine(TraceIn + nameof(TextDocumentDiagnostic));

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

        Console.Error.WriteLine(TraceOut + nameof(TextDocumentDiagnostic));

        return result;
    }
    
    /// <summary>
    /// Handles the text document definition request.
    /// Identifies the referenced symbol(s) and returns their locations.
    /// </summary>
    [JsonRpcMethod(Methods.TextDocumentDefinitionName)]
    public LocationLink[] TextDocumentDefinition(TextDocumentIdentifier textDocument, Position position, SumType<string, int>? workDoneToken = null, SumType<string, int>? partialResultToken = null)
    {
        Console.Error.WriteLine(TraceIn + nameof(TextDocumentDefinition));

        var uri = FileUtil.UriStringToUri(textDocument.Uri);
        LocationLink[] result;
            
        try
        {
            var document = _workspace.GetSymbolUseExtractedDocument(uri);
            result = _gotoDefinitionHandler.HandleRequest(document, position, _workspace);
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception.ToString());
            throw new LocalRpcException(exception.Message, exception) { ErrorCode = (int)JsonRpcErrorCode.InternalError };
        }

        Console.Error.WriteLine(TraceOut + nameof(TextDocumentDefinition));

        return result;
    }

    /// <summary>
    /// Handles the text document semantic token request.
    /// Generates the relative list of tokens with their types and returns it.
    /// </summary>
    [JsonRpcMethod(Methods.TextDocumentSemanticTokensFull)]
    public SemanticTokens SemanticTokens(TextDocumentIdentifier textDocument, SumType<string, int>? workDoneToken = null, SumType<string, int>? partialResultToken = null)
    {
        Console.Error.WriteLine(TraceIn + nameof(SemanticTokens));

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
            throw new LocalRpcException(exception.Message, exception) { ErrorCode = (int)JsonRpcErrorCode.InternalError };
        }

        Console.Error.WriteLine(TraceOut + nameof(SemanticTokens));
        Console.Error.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));

        return result;
    }

    /// <summary>
    /// Handles the text document hover request.
    /// Identifies the referenced symbol and returns some (hopefully) relevant information about it.
    /// </summary>
    [JsonRpcMethod(Methods.TextDocumentHoverName)]
    public Hover Hover(TextDocumentIdentifier textDocument, Position position, SumType<string, int>? workDoneToken = null)
    {
        Console.Error.WriteLine(TraceIn + nameof(Hover));

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
            throw new LocalRpcException(exception.Message, exception) { ErrorCode = (int)JsonRpcErrorCode.InternalError };
        }

        Console.Error.WriteLine(TraceOut + nameof(Hover));
        Console.Error.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));

        return result;
    }
}