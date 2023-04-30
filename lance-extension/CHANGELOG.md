# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.0.4] - 2023-05-01

### Added 

- a go to definition feature with which you can jump to the definition of a symbol.

### Changed 

- improved the syntax highlighting to have a semantic highlighting as well.
- improved hover feature to show more information.
- improved hover feature to make it work with labels and block numbers as well.

### Fixed 

- a race condition which lead to a crash on startup.

## [0.0.3] - 2023-04-10

### Added 

- a hover feature with which you can hover over symbols to get a popup with the definition.
- a preprocessor which can be used to replace project specific placeholders.
- supports R parameters written with a leading dollar sign like `$R42`.

## [0.0.2] - 2023-01-10

### Changed 

- moved changelog to the file CHANGELOG.md.

### Fixed

- Fixed image links in the README of the published extension.

## [0.0.1] - 2023-01-07

### Added 

- syntax highlighting.