# Lance
Language Appliance for Numerical Control code of the sinumerik one cnc control system by siemens

## Description
Lance is an Extension which provides common language features for the g-code based NC language from Siemens. The grammar for these features is an own interpretation of the available manuals from Siemens for the control system SINUMERIK ONE of the CNC software version 6.20 released in july 2022. Primarily the manuals "Basic functions" and "NC programming" and secondarily the manuals "Axes and spindles
", "Monitoring and compensating", "Synchronized actions", "Technologies", "Tool management", "Tools" and "Transformations" are used for the grammar.

## Features

Syntax highlighting with theme support.

![Syntax highlighting](images/syntax_highlight_feature.gif)

Hover over symbols to see the definition of them.

![Hover feature](images/hover_feature.gif)

## Extension Settings

To change the colors of the syntax highlighting you can [change the theme](https://code.visualstudio.com/docs/getstarted/themes) or [configure some color customizations](https://code.visualstudio.com/docs/getstarted/themes#_customizing-a-color-theme).

There is a setting to set the file extensions of the files which contain global symbols. These files are loaded on startup to provide the respective insights. 

There are also some settings for a customizable preprocessor. It can be used to replace project specific placeholders before the file is parsed.

## Known Issues

If you have any problems, see if there is a related issue or write me one on [github](https://github.com/Nuaduwodan/lance/issues/new).
