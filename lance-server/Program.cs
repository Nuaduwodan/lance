using OmniSharp.Extensions.LanguageServer.Server;

LanguageServerOptions options = new LanguageServerOptions();
options.WithInput();

LanguageServer.Create(options => {});