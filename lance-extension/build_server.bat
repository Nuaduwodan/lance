pushd ..\lance-server\
dotnet clean
dotnet build
popd
xcopy ..\lance-server\bin\Debug\net6.0 server\net6.0\ /y