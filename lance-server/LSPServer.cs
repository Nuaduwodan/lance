using LanceServer.Core.Configuration;
using Newtonsoft.Json.Linq;
using StreamJsonRpc;
using LanceServer.Hover;
using LanceServer.Parser;
using LanceServer.SemanticToken;
using LanceServer.Core.Workspace;
using LspTypes;
using StreamJsonRpc.Protocol;

namespace LanceServer
{
    class LSPServer : IDisposable
    {
        private const string InvalidParamsMessage = "params could not be parsed into type ";
        private const string TraceIn = "<-- ";
        private const string TraceOut = "--> ";
        
        private readonly JsonRpc _rpc;
        private readonly IConfigurationServer _configurationServer;
        
        private readonly ManualResetEvent _disconnectEvent = new ManualResetEvent(false);
        private readonly bool _trace = true;
        
        private static readonly object Lock = new object();
        
        private bool _isDisposed;

        public event EventHandler Disconnected;
        
        /// <summary>
        /// The workspace containing all the files 
        /// </summary>
        private Workspace _workspace;

        private SemanticTokenHandler _semanticTokenHandler;
        private HoverHandler _hoverHandler;
        private ConfigurationManager _configurationManager;

        public LSPServer(Stream sender, Stream reader)
        {
            _rpc = JsonRpc.Attach(sender, reader, this);
            _rpc.Disconnected += OnRpcDisconnected;
            _configurationServer = JsonRpc.Attach<IConfigurationServer>(sender, reader);
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
        public InitializeResult Initialize(JToken parameter)
        {
            if (_trace)
            {
                Console.Error.WriteLine(TraceIn + nameof(Initialize));
                Console.Error.WriteLine(parameter.ToString());
            }

            var request = DeserializeParams<InitializeParams>(parameter);
            
            InitializeResult result = new InitializeResult();
            
            try
            {
                lock (Lock)
                {
                    // Configuration
                    _configurationManager = new ConfigurationManager(_configurationServer);
                    _workspace = new Workspace(new ParserManager(), _configurationManager, request.WorkspaceFolders.Select(folder => new Uri(folder.Uri)));
                    _semanticTokenHandler = new SemanticTokenHandler();
                    _hoverHandler = new HoverHandler(_configurationManager);

                    ServerCapabilities capabilities = new ServerCapabilities
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

                        DefinitionProvider = false,

                        TypeDefinitionProvider = false,

                        ImplementationProvider = false,

                        ReferencesProvider = false,

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
                    };

                    result.Capabilities = capabilities;
                }
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception.Message, MessageType.Info);
                throw new LocalRpcException(exception.Message, exception){ErrorCode = (int)JsonRpcErrorCode.InternalError};
            }
            
            if (_trace)
            {
                Console.Error.Write(TraceOut + nameof(Initialized));
                Console.Error.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            }
            
            return result;
        }

        [JsonRpcMethod(Methods.InitializedName)]
        public void Initialized(JToken parameter)
        {
            if (_trace)
            {
                Console.Error.WriteLine(TraceIn + nameof(Initialized));
                Console.Error.WriteLine(parameter.ToString());
            }
            
            try
            {
                lock (Lock)
                {
                    //_workspace.InitWorkspaceAsync();
                }
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception.Message, MessageType.Info);
                throw new LocalRpcException(exception.Message, exception){ErrorCode = (int)JsonRpcErrorCode.InternalError};
            }
            
            if (_trace)
            {
                Console.Error.Write(TraceOut + nameof(Initialized));
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
                lock (Lock)
                {
                    return new JObject();
                }
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception.Message, MessageType.Info);
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
                lock (Lock)
                {
                    InternalExit();
                }
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception.Message, MessageType.Info);
                throw new LocalRpcException(exception.Message, exception){ErrorCode = (int)JsonRpcErrorCode.InternalError};
            }
        }

        [JsonRpcMethod(Methods.WorkspaceDidChangeConfigurationName)]
        public void WorkspaceDidChangeConfiguration(JToken parameter)
        {
            if (_trace)
            {
                Console.Error.WriteLine(TraceIn + nameof(WorkspaceDidChangeConfiguration));
                Console.Error.WriteLine(parameter.ToString());
            }

            var request = DeserializeParams<ConfigurationResult>(parameter);

            try
            {
                lock (Lock)
                {
                    _configurationManager.ExtractConfiguration(request);
                }
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception.Message, MessageType.Info);
                throw new LocalRpcException(exception.Message, exception){ErrorCode = (int)JsonRpcErrorCode.InternalError};
            }

            if (_trace)
            {
                Console.Error.Write(TraceOut + nameof(WorkspaceDidChangeConfiguration));
            }
        }
        
        [JsonRpcMethod(Methods.TextDocumentDidChangeName)]
        public void TextDocumentDidChange(JToken parameter)
        {
            if (_trace)
            {
                Console.Error.WriteLine(TraceIn + nameof(TextDocumentDidChange));
                Console.Error.WriteLine(parameter.ToString());
            }
            
            var request = DeserializeParams<DidChangeTextDocumentParams>(parameter);
            var uri = new Uri(Uri.UnescapeDataString(request.TextDocument.Uri));
            
            try
            {
                lock (Lock)
                {
                    var document = _workspace.GetDocument(uri);
                    document.Content = request.ContentChanges.Last().Text;
                }
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception.Message, MessageType.Info);
                throw new LocalRpcException(exception.Message, exception){ErrorCode = (int)JsonRpcErrorCode.InternalError};
            }

            if (_trace)
            {
                Console.Error.Write(TraceOut + nameof(TextDocumentDidChange));
            }
        }
        
        [JsonRpcMethod(Methods.TextDocumentSemanticTokensFull)]
        public SemanticTokens SemanticTokens(JToken parameter)
        {
            if (_trace)
            {
                Console.Error.WriteLine(TraceIn + nameof(SemanticTokens));
                Console.Error.WriteLine(parameter.ToString());
            }

            var request = DeserializeParams<DocumentSymbolParams>(parameter);
            var uri = new Uri(Uri.UnescapeDataString(request.TextDocument.Uri));
            SemanticTokens result;
            
            try
            {
                lock (Lock)
                {
                    var document = _workspace.GetDocumentWithParseTree(uri);

                    result = _semanticTokenHandler.ProcessRequest(document, request);
                }
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception.Message, MessageType.Info);
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
        public LspTypes.Hover Hover(JToken parameter)
        {
            if (_trace)
            {
                Console.Error.WriteLine(TraceIn + nameof(Hover));
                Console.Error.WriteLine(parameter.ToString());
            }
            
            var request = DeserializeParams<HoverParams>(parameter);
            var uri = new Uri(Uri.UnescapeDataString(request.TextDocument.Uri));
            LspTypes.Hover result;
            
            try
            {
                lock (Lock)
                {
                    var document = _workspace.GetDocumentWithUpdatedSymbolTable(uri);

                    result = _hoverHandler.ProcessRequest(document, request, _workspace);
                }
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception.Message, MessageType.Info);
                throw new LocalRpcException(exception.Message, exception){ErrorCode = (int)JsonRpcErrorCode.InternalError};
            }

            if (_trace)
            {
                Console.Error.Write(TraceOut + nameof(Hover));
                Console.Error.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            }

            return result;
        }

        private T DeserializeParams<T>(JToken parameter)
        {
            return parameter.ToObject<T>() ?? 
                   throw new LocalRpcException(InvalidParamsMessage + nameof(T)){ErrorCode = (int)JsonRpcErrorCode.InvalidParams};
        }
    }
}