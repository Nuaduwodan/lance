﻿using LanceServer.Core.Configuration;
using Newtonsoft.Json.Linq;
using StreamJsonRpc;
using LanceServer.Hover;
using LanceServer.Parser;
using LanceServer.SemanticToken;
using LanceServer.Core.Workspace;
using LspTypes;

namespace LanceServer
{
    class LSPServer : IDisposable
    {
        private readonly JsonRpc rpc;
        private readonly ManualResetEvent disconnectEvent = new ManualResetEvent(false);
        public event EventHandler Disconnected;
        private bool isDisposed;

        /// <summary>
        /// The workspace containing all the files 
        /// </summary>
        private Workspace _workspace;

        private SemanticTokenHandler _semanticTokenHandler;
        private HoverHandler _hoverHandler;

        public LSPServer(Stream sender, Stream reader)
        {
            rpc = JsonRpc.Attach(sender, reader, this);
            rpc.Disconnected += OnRpcDisconnected;
        }

        private void OnRpcDisconnected(object sender, JsonRpcDisconnectedEventArgs e)
        {
            Exit();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing)
            {
                // free managed resources
                disconnectEvent.Dispose();
            }

            isDisposed = true;
        }

        public void WaitForExit()
        {
            disconnectEvent.WaitOne();
        }

        ~LSPServer()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        public void Exit()
        {
            disconnectEvent.Set();
            Disconnected?.Invoke(this, new EventArgs());
            System.Environment.Exit(0);
        }

        private static readonly object _object = new object();
        private readonly bool trace = true;

        /// <summary>
        /// Handles the initialize request
        /// </summary>
        [JsonRpcMethod(Methods.InitializeName)]
        public object Initialize(JToken arg)
        {
            lock (_object)
            {
                if (trace)
                {
                    System.Console.Error.WriteLine("<-- Initialize");
                    System.Console.Error.WriteLine(arg.ToString());
                }

                var init_params = arg.ToObject<InitializeParams>();
                
                // Configuration
                var _configurationManager = new ConfigurationManager(init_params.InitializationOptions);
                
                var globalFileEndings = new[] {"def"};
                var globalDirectories = new[] {"CMA.DIR"};
                
                _workspace = new Workspace(new ParserManager(), _configurationManager.GetSymbolTableConfiguration());
                _semanticTokenHandler = new SemanticTokenHandler();
                _hoverHandler = new HoverHandler(_configurationManager.GetDocumentationConfiguration());

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

                InitializeResult result = new InitializeResult
                {
                    Capabilities = capabilities
                };
                if (trace)
                {
                    System.Console.Error.WriteLine("--> " + Newtonsoft.Json.JsonConvert.SerializeObject(result));
                }

                return result;
            }
        }

        [JsonRpcMethod(Methods.InitializedName)]
        public void InitializedName(JToken arg)
        {
            lock (_object)
            {
                try
                {
                    if (trace)
                    {
                        System.Console.Error.WriteLine("<-- Initialized");
                        System.Console.Error.WriteLine(arg.ToString());
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        [JsonRpcMethod(Methods.ShutdownName)]
        public JToken ShutdownName()
        {
            lock (_object)
            {
                try
                {
                    if (trace)
                    {
                        System.Console.Error.WriteLine("<-- Shutdown");
                    }
                }
                catch (Exception)
                {
                }

                return null;
            }
        }

        [JsonRpcMethod(Methods.ExitName)]
        public void ExitName()
        {
            lock (_object)
            {
                try
                {
                    if (trace)
                    {
                        System.Console.Error.WriteLine("<-- Exit");
                    }

                    Exit();
                }
                catch (Exception)
                {
                }
            }
        }

        // ======= WINDOW ========

        [JsonRpcMethod(Methods.WorkspaceDidChangeConfigurationName)]
        public void WorkspaceDidChangeConfigurationName(JToken arg)
        {
            lock (_object)
            {
                try
                {
                    if (trace)
                    {
                        System.Console.Error.WriteLine("<-- WorkspaceDidChangeConfiguration");
                        System.Console.Error.WriteLine(arg.ToString());
                    }

                    // code
                }
                catch (Exception)
                {
                }
            }
        }

        [JsonRpcMethod(Methods.WorkspaceDidChangeWatchedFilesName)]
        public void WorkspaceDidChangeWatchedFilesName(JToken arg)
        {
            lock (_object)
            {
                try
                {
                    if (trace)
                    {
                        System.Console.Error.WriteLine("<-- WorkspaceDidChangeWatchedFiles");
                        System.Console.Error.WriteLine(arg.ToString());
                    }

                    // code
                }
                catch (Exception)
                {
                }
            }
        }

        [JsonRpcMethod(Methods.WorkspaceSymbolName)]
        public JToken WorkspaceSymbolName(JToken arg)
        {
            lock (_object)
            {
                try
                {
                    if (trace)
                    {
                        System.Console.Error.WriteLine("<-- WorkspaceSymbol");
                        System.Console.Error.WriteLine(arg.ToString());
                    }

                    // code
                }
                catch (Exception)
                {
                }

                return null;
            }
        }

        [JsonRpcMethod(Methods.WorkspaceExecuteCommandName)]
        public JToken WorkspaceExecuteCommandName(JToken arg)
        {
            lock (_object)
            {
                try
                {
                    if (trace)
                    {
                        System.Console.Error.WriteLine("<-- WorkspaceExecuteCommand");
                        System.Console.Error.WriteLine(arg.ToString());
                    }

                    // code
                }
                catch (Exception)
                {
                }

                return null;
            }
        }

        [JsonRpcMethod(Methods.WorkspaceApplyEditName)]
        public JToken WorkspaceApplyEditName(JToken arg)
        {
            lock (_object)
            {
                if (trace)
                {
                    System.Console.Error.WriteLine("<-- WorkspaceApplyEdit");
                    System.Console.Error.WriteLine(arg.ToString());
                }

                // code
                return null;
            }
        }

        /// <summary>
        /// Handles a semantic token request
        /// </summary>
        [JsonRpcMethod(Methods.TextDocumentSemanticTokensFull)]
        public SemanticTokens SemanticTokens(JToken arg)
        {
            lock (_object)
            {
                SemanticTokens result = null;
                try
                {
                    if (trace)
                    {
                        System.Console.Error.WriteLine("<-- SemanticTokens");
                        System.Console.Error.WriteLine(arg.ToString());
                    }

                    // Deserialization
                    DocumentSymbolParams request = arg.ToObject<DocumentSymbolParams>();
                    var uri = new Uri(Uri.UnescapeDataString(request.TextDocument.Uri));

                    var document = _workspace.GetDocumentWithParseTree(uri);

                    // Handler could be global as well and the document, handlerParams and the symbolTable could be provided per request
                    result = _semanticTokenHandler.ProcessRequest(document, request);

                    if (trace)
                    {
                        System.Console.Error.Write("returning semantictokens");
                        System.Console.Error.WriteLine(string.Join(" ", result.Data));
                    }
                }
                catch (Exception e)
                {
                    System.Console.Error.WriteLine(e.Message, MessageType.Info);
                }

                return result;
            }
        }

        [JsonRpcMethod(Methods.TextDocumentHoverName)]
        public LspTypes.Hover Hover(JToken arg)
        {
            lock (_object)
            {
                LspTypes.Hover result = null;
                try
                {
                    if (trace)
                    {
                        System.Console.Error.WriteLine("<-- Hover");
                        System.Console.Error.WriteLine(arg.ToString());
                    }

                    // Deserialization
                    HoverParams request = arg.ToObject<HoverParams>();
                    var uri = new Uri(Uri.UnescapeDataString(request.TextDocument.Uri));

                    // File handling could be done by Workspace, reading of content, workspace is set global
                    var document = _workspace.GetDocumentWithUpdatedSymbolTable(uri);

                    // Handler could be global as well and the document, handlerParams and the symbolTable could be provided per request
                    result = _hoverHandler.ProcessRequest(document, request, _workspace);
                }
                catch (Exception e)
                {
                    System.Console.Error.WriteLine(e.Message, MessageType.Info);
                }

                return result;
            }
        }
    }
}