@echo off
echo ********************************************************************************
echo Cleans all deployed binaries and database, then builds and reinitialises everything.
echo Afterwards the basic modules are published again to see that nothing has changed.
echo Use this to get a really clean environment and verify changes to infrastructure code.
echo ********************************************************************************

IF NOT EXIST Configs\Local XCOPY /S/E Configs\Examples Configs\Local\

rd /s /q ".\bin\"
rd /s /q ".\Zetbox.Server.HttpService\Common\"
rd /s /q ".\Zetbox.Server.HttpService\Bootstrapper\"
rd /s /q ".\Zetbox.Server.HttpService\Client\"
rd /s /q ".\Zetbox.Server.HttpService\Server\"
rd /s /q "%LOCALAPPDATA%\AppData\Temp\zetbox"

dotnet restore Zetbox.Core.sln

dotnet build --disable-parallel --ignore-failed-sources --configuration Minimal Zetbox.Core.sln
IF ERRORLEVEL 1 GOTO FAIL

call "zbResetDatabase.cmd"
IF ERRORLEVEL 1 GOTO FAIL

call "zbGenerate.cmd"
IF ERRORLEVEL 1 GOTO FAIL

dotnet build --disable-parallel --ignore-failed-sources Zetbox.Core.sln
IF ERRORLEVEL 1 GOTO FAIL

GOTO EOF

:FAIL
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX FAIL XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo                               Aborting zbResetAll

rem return error without closing parent shell
echo A | choice /c:A /n

:EOF
