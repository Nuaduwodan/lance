using Newtonsoft.Json.Linq;
using StreamJsonRpc;
using System.ComponentModel;
using Antlr4.Runtime;
using LanceServer.Core.Stream;
using LanceServer.Core.Workspace;
using LanceServer.SemanticToken;
using LspTypes;

namespace LanceServer
{
    class Program
    {
        private static void Main(string[] args) => MainAsync(args).Wait();

        private static async Task MainAsync(string[] args)
        {
            System.IO.Stream stdin = Console.OpenStandardInput();
            System.IO.Stream stdout = Console.OpenStandardOutput();
            stdin = new StreamSplitter(stdin, new StreamLog("editor"), StreamSplitter.StreamOwnership.OwnNone);
            stdout = new StreamSplitter(stdout, new StreamLog("server"), StreamSplitter.StreamOwnership.OwnNone);
            var languageServer = new LSPServer(stdout, stdin);
            await Task.Delay(-1);
        }
    }

    class LSPServer : INotifyPropertyChanged, IDisposable
    {
        private readonly JsonRpc rpc;
        private readonly ManualResetEvent disconnectEvent = new ManualResetEvent(false);
        private Dictionary<string, DiagnosticSeverity> diagnostics;
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler Disconnected;
        private bool isDisposed;
        
        public LSPServer(Stream sender, Stream reader)
        {
            rpc = JsonRpc.Attach(sender, reader, this);
            rpc.Disconnected += OnRpcDisconnected;
        }
        private void OnRpcDisconnected(object sender, JsonRpcDisconnectedEventArgs e)
        {
            Exit();
        }
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
                { }
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
                { }
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
                { }
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
                { }
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
                { }
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
                { }
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
                { }
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
        
        // Methods.TextDocumentSemanticTokensFullName is wrong according to the doc
        // https://microsoft.github.io/language-server-protocol/specifications/lsp/3.17/specification/#semanticTokensRegistrationOptions
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
                    DocumentSymbolParams request = arg.ToObject<DocumentSymbolParams>();
                    
                    Document document = Util.CheckDoc(new Uri(Uri.UnescapeDataString(request.TextDocument.Uri)));
                    
                    ICharStream stream = CharStreams.fromString(document.Code);
                    ITokenSource lexer = new SinumerikNCLexer(stream);
                    CommonTokenStream tokens = new CommonTokenStream(lexer);
                    
                    var tokenData = new SemanticTokenData();

                    var tokenHandler = new SemanticTokenHandler();
                    
                    int previousLine = 0;
                    int previousStartChar = 0;
                    while (tokens.LA(1) != IntStreamConstants.EOF)
                    {
                        tokens.Consume();
                        var token = tokens.LT(0);
                        int deltaLine = token.Line - previousLine;
                        int deltaChar = token.StartIndex - previousStartChar;
                        int length = token.StopIndex - token.StartIndex + 1;
                        int type = tokenHandler.TransformType(token.Type);
                        tokenData.AddElement(new SemanticTokenDataElement(deltaLine, deltaChar, length, type, 0));
                        previousLine = token.Line;
                        previousStartChar = token.StartIndex;
                    }
                    
                    result = new SemanticTokens
                    {
                        Data = tokenData.ToDataFormat()
                    };
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
                    HoverParams request = arg.ToObject<HoverParams>();
                    result = new LspTypes.Hover()
                    {
                        Contents = new SumType<string, MarkedString, MarkedString[], MarkupContent>(new MarkupContent()
                        {
                            Kind = MarkupKind.PlainText, Value = "Test\n123"
                        }),
                        Range = new LspTypes.Range()
                        {
                            Start = new Position(0, 0),
                            End = new Position(0, 10)
                        }
                    };
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
