# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.1] - 2023-06-03

### Added 

- added the itor and rtoi functions which convert between integer and real values.
- added repeatb and repeat using labels.

### Changed 

- improved sposa, fgref, fgroup, scpara, setint and coupling definitions.
- changed the spi function to allow expressions as argument.

### Fixed 

- fixed an issue where a syntactically wrong m code with parameter could lead to a crash.
- fixed an issue where a syntactically wrong macro could lead to a crash.
- fixed inline position for lexer problems.

## [1.0.0] - 2023-05-21

### Added 

- added diagnostic reports for syntax errors, symbols, procedures and extern declarations.
- added a command line interface.

### Changed 

- improved the goto feature to show alternatives if applicable.
- improved hover feature to show some information about language specific tokens such as g codes, functions or other keywords.
- improved start up behavior to automatically parse all documents of the workspace and report progress in the bottom left.

### Fixed 

- fixed an issue if the extension was started without a workspace.

## [0.0.4] - 2023-05-01

### Added 

- adds a go to definition feature with which you can jump to the definition of a symbol.

### Changed 

- improved the syntax highlighting to have a semantic highlighting as well.
- improved hover feature to show more information.
- improved hover feature to make it work with labels and block numbers as well.

### Fixed 

- fixed a race condition which lead to a crash on startup.

## [0.0.3] - 2023-04-10

### Added 

- added a hover feature with which you can hover over symbols to get a popup with the definition.
- added a preprocessor which can be used to replace project specific placeholders.
- supports R parameters written with a leading dollar sign like `$R42`.

## [0.0.2] - 2023-01-10

### Changed 

- moved changelog to the file CHANGELOG.md.

### Fixed

- fixed image links in the README of the published extension.

## [0.0.1] - 2023-01-07

### Added 

- syntax highlighting.