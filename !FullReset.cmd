@echo off
echo ********************************************************************************
echo Cleans all deployed binaries and database, then builds and reinitialises everything.
echo Afterwards the basic modules are published again to see that nothing has changed.
echo Use this to get a really clean environment and verify changes to infrastructure code.
echo ********************************************************************************

IF NOT EXIST Configs\Local XCOPY /S/E Configs\Examples Configs\Local\

rem restrict /m to two cores, since msbuild is not able to build the solution with more parallelism
%windir%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe /m:2 /v:m CCNet.msbuild
IF ERRORLEVEL 1 GOTO FAIL

bin\Debug\Zetbox.Server.Service.exe -syncidentities
IF ERRORLEVEL 1 GOTO FAIL

rem regenerate modules to prove roundtrippability
call "!Publish.cmd"
IF ERRORLEVEL 1 GOTO FAIL

%windir%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe /v:m Zetbox.Complete.sln
IF ERRORLEVEL 1 GOTO FAIL

GOTO EOF

:FAIL
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX FAIL XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo                               Aborting FullResetDev
pause
rem return error without closing parent shell
echo A | choice /c:A /n

:EOF