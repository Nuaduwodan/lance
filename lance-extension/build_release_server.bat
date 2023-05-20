pushd ..\LanceServer\
dotnet clean -c release
dotnet build -c release
popd
xcopy ..\LanceServer\bin\Release\net6.0 server\net6.0\ /s /y
cd server\net6.0
del config.json
del preprocessor_config.json