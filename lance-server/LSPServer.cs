using LanceServer.Core.Configuration;
using LanceServer.Core.Configuration.DataModel;
using Newtonsoft.Json.Linq;
using StreamJsonRpc;
using LanceServer.Hover;
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
        
        private readonly ManualResetEvent _disconnectEvent = new ManualResetEvent(false);
        private bool _trace = true;
        
        private static readonly object Lock = new object();
        
        private bool _isDisposed;

        public event EventHandler Disconnected;
        
        /// <summary>
        /// The workspace containing all the files 
        /// </summary>
        private readonly IWorkspace _workspace;
        private readonly IConfigurationManager _configurationManager;
        private readonly ISemanticTokenHandler _semanticTokenHandler;
        private readonly IHoverHandler _hoverHandler;

        public LSPServer(Stream sender, Stream reader, IWorkspace workspace, IConfigurationManager configurationManager, ISemanticTokenHandler semanticTokenHandler, IHoverHandler hoverHandler)
        {
            _rpc = JsonRpc.Attach(sender, reader, this);
            _rpc.Disconnected += OnRpcDisconnected;
            _workspace = workspace;
            _configurationManager = configurationManager;
            _semanticTokenHandler = semanticTokenHandler;
            _hoverHandler = hoverHandler;
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
                    _configurationManager.Initialize(request);
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

            var request = DeserializeParams<ConfigurationParameters>(parameter);

            try
            {
                lock (Lock)
                {
                    _configurationManager.ExtractConfiguration(request);
                    _trace = request.Settings.Lance.Trace;
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
            var uri = ExtractUri(request.TextDocument.Uri);
            
            try
            {
                lock (Lock)
                {
                    _workspace.UpdateDocumentContent(uri, request.ContentChanges.Last().Text);
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
        
        [JsonRpcMethod(Methods.TextDocumentDidOpenName)]
        public void TextDocumentDidOpen(JToken parameter)
        {
            if (_trace)
            {
                Console.Error.WriteLine(TraceIn + nameof(TextDocumentDidOpen));
                Console.Error.WriteLine(parameter.ToString());
            }
            
            var request = DeserializeParams<DidOpenTextDocumentParams>(parameter);
            var uri = ExtractUri(request.TextDocument.Uri);
            
            try
            {
                lock (Lock)
                {
                    _workspace.GetSymbolisedDocument(uri);
                }
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception.Message, MessageType.Info);
                throw new LocalRpcException(exception.Message, exception){ErrorCode = (int)JsonRpcErrorCode.InternalError};
            }

            if (_trace)
            {
                Console.Error.Write(TraceOut + nameof(TextDocumentDidOpen));
            }
        }
        
        [JsonRpcMethod(Methods.TextDocumentDefinitionName)]
        public LocationLink[] TextDocumentDefinition(JToken parameter)
        {
            if (_trace)
            {
                Console.Error.WriteLine(TraceIn + nameof(TextDocumentDefinition));
                Console.Error.WriteLine(parameter.ToString());
            }
            
            var request = DeserializeParams<TypeDefinitionParams>(parameter);
            var uri = ExtractUri(request.TextDocument.Uri);
            LocationLink[] result = Array.Empty<LocationLink>();
            
            try
            {
                lock (Lock)
                {
                    var document = _workspace.GetSymbolisedDocument(uri);
                    // Todo process request
                }
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception.Message, MessageType.Info);
                throw new LocalRpcException(exception.Message, exception){ErrorCode = (int)JsonRpcErrorCode.InternalError};
            }

            if (_trace)
            {
                Console.Error.Write(TraceOut + nameof(TextDocumentDefinition));
            }

            return result;
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
            var uri = ExtractUri(request.TextDocument.Uri);
            SemanticTokens result;
            
            try
            {
                lock (Lock)
                {
                    _workspace.InitWorkspace();
                    
                    var document = _workspace.GetParsedDocument(uri);

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
            var uri = ExtractUri(request.TextDocument.Uri);
            LspTypes.Hover result;
            
            try
            {
                lock (Lock)
                {
                    var document = _workspace.GetSymbolisedDocument(uri);

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

        private Uri ExtractUri(string escapedUriString)
        {
            var uriString = Uri.UnescapeDataString(escapedUriString);
            var escapedPath = uriString.Replace("#", "%23");
            Uri uri = new Uri(escapedPath);
            return uri;
        }
    }
}