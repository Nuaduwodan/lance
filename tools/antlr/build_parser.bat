java -jar antlr-4.11.1-complete.jar -visitor -Dlanguage=CSharp ..\..\antlr4-grammar\SinumerikNC.g4
move ..\..\antlr4-grammar\*.cs ..\..\lance-server\Parser\