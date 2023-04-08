pushd ..\lance-server\
dotnet clean -c release
dotnet build -c release
popd
xcopy ..\lance-server\bin\Release\net6.0 server\net6.0\ /s /y