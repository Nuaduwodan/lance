import * as vscode from 'vscode';
import {
    LanguageClient,
    LanguageClientOptions,
    ServerOptions,
    Executable
} from "vscode-languageclient/node";

let client: LanguageClient;
const languageID = "sinumeriknc";

// This method is called when your extension is activated
export function activate(context: vscode.ExtensionContext) {

    var fn = __dirname + '/../server/net6.0/LanceServer.exe';
    const server: Executable =
    {
        command: fn,
        args: ["language-server"],
        options: { shell: false, detached: false }
    };

    const languageConfig = vscode.workspace.getConfiguration("files.associations");
    let fileExtensions = ["*.def","*.mpf","*.spf"];
    let moreExtensions = Object.keys(languageConfig)

    moreExtensions.forEach(element => {
        if (languageConfig[element] === languageID){
            fileExtensions.push(element);
        }
    });

    const serverOptions: ServerOptions = server;

    let clientOptions: LanguageClientOptions = {
        documentSelector: [{language: languageID}],
        progressOnInitialization: true,
        synchronize: {
            configurationSection: "lance",
            fileEvents: vscode.workspace.createFileSystemWatcher(`**/{${fileExtensions.toString()}}`),
        },
    };

    // Create the language client and start the client.
    client = new LanguageClient("lance", "Lance", serverOptions, clientOptions);
    client.registerProposedFeatures();
    client.start();
}

// This method is called when your extension is deactivated
export function deactivate() {
    if (!client) {
        return undefined;
    }
    return client.stop();
}
