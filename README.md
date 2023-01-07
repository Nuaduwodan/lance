# lance
Language Appliance for Numerical Control codE of the sinumerik one cnc control system by siemens

## Description
Lance is an Extension which provides common language features for the g-code based NC language from Siemens. The grammar for these features is an own interpretation of the available manuals from Siemens for the control system SINUMERIK ONE of the CNC software version 6.20 released in july 2022. Primarily the manuals "Basic functions" and "NC programming" and secondarily the manuals "Axes and spindles
", "Monitoring and compensating", "Synchronized actions", "Technologies", "Tool management", "Tools" and "Transformations" are used for the grammar.

## How to install
You can simply install the extension via the Visual Studio Code marketplace by searching for the extension lance.

Or you can build it yourself in four steps.:
- First use antlr to build the parser using the grammar under antlr4-grammar. Then copy the resulting c-sharp files into the folder lance-server. This can be done using the script lance\tools\antlr\build_parser.bat. 
- Second build the .Net solution using dotnet build.
- Third copy the files under lance-server\bin\Debug to lance-extension\server. Then run `npm ci` in the folder lance-extension followed by `npm run compile`.
- Fourth install the package vsce with `npm -g i @vscode/vsce` and package the extension using `vsce package`. Then you can install it with the "Install from VSIX..." option in the extension context menu of VS code.