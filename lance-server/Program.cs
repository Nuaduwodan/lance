using Microsoft.Extensions.Logging;
using OmniSharp.Extensions.LanguageServer.Server;

namespace LanceServer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }
        
        private static async Task MainAsync(string[] args)
        {
            var server = await LanguageServer.From(
                options =>
                    options
                        .WithInput(Console.OpenStandardInput())
                        .WithOutput(Console.OpenStandardOutput())
                        .ConfigureLogging(
                            x => x
                                .AddLanguageProtocolLogging()
                                .SetMinimumLevel(LogLevel.Debug)
                        )
                        .WithHandler<TextDocumentHandler>()     
            ).ConfigureAwait(false);

            await server.WaitForExit.ConfigureAwait(false);
        }
    }
}