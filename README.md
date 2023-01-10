# Lance
Language Appliance for Numerical Control code of the sinumerik one cnc control system by siemens

For a description of the Extension see [lance-extension/README.md](lance-extension/README.md).

For a changelog see [lance-extension/CHANGELOG.md](lance-extension/CHANGELOG.md).

## How to install
You can simply install the extension via the Visual Studio Code marketplace by searching for the extension lance.

Or you can build it yourself in four steps.:
- First use antlr to build the parser using the grammar under antlr4-grammar. Then copy the resulting c-sharp files into the folder lance-server. This can be done using the script `lance\tools\antlr\build_parser.bat`. 
- Second build the .Net solution using `dotnet build`.
- Third copy the files under `lance-server\bin\Debug\` to `lance-extension\server\`. Then run `npm ci` in the folder `lance-extension\` followed by `npm run compile`.
- Fourth install the package vsce with `npm -g i @vscode/vsce` and package the extension using `vsce package --no-rewrite-relative-links`. Then you can install it with the "Install from VSIX..." option in the extension context menu of VS code.