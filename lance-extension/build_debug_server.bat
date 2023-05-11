pushd ..\LanceServer\
dotnet clean
dotnet build
popd
xcopy ..\LanceServer\bin\Debug\net6.0 server\net6.0\ /s /y