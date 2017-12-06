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

nuget.exe restore Zetbox.Complete.sln

MSBuild.exe /v:m Zetbox.Complete.sln /t:Rebuild /p:Configuration=Minimal
IF ERRORLEVEL 1 GOTO FAIL

call "zbResetDatabase.cmd"
IF ERRORLEVEL 1 GOTO FAIL

call "zbGenerate.cmd"
IF ERRORLEVEL 1 GOTO FAIL

MSBuild.exe /v:m Zetbox.Complete.sln
IF ERRORLEVEL 1 GOTO FAIL

GOTO EOF

:FAIL
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX FAIL XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo                               Aborting zbResetAll
pause
rem return error without closing parent shell
echo A | choice /c:A /n

:EOF
