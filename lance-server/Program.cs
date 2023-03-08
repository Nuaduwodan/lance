using LanceServer.Core.Stream;

namespace LanceServer
{
    class Program
    {
        private static void Main(string[] args) => MainAsync(args).Wait();

        private static async Task MainAsync(string[] args)
        {
            Stream stdin = Console.OpenStandardInput();
            Stream stdout = Console.OpenStandardOutput();
            stdin = new StreamSplitter(stdin, new StreamLog("editor"), StreamSplitter.StreamOwnership.OwnNone);
            stdout = new StreamSplitter(stdout, new StreamLog("server"), StreamSplitter.StreamOwnership.OwnNone);
            var languageServer = new LSPServer(stdout, stdin);
            await Task.Delay(-1);
        }
    }
}