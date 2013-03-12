@echo off
echo ********************************************************************************
echo Deploys changes in the basic modules into the database.
echo Changes to the object model are generated.
echo Use this to apply upstream changes.
echo ********************************************************************************

set config=

if .%1. == .. GOTO GOON
set config=%1

:GOON

call "ZbInstall.cmd" %config%

%windir%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe /v:m /p:Configuration=Fallback $safesolutionname$.sln
IF ERRORLEVEL 1 GOTO FAIL

cd bin\Debug

Zetbox.Cli.exe %config% -fallback -deploy-update -generate -syncidentities
IF ERRORLEVEL 1 GOTO FAIL

echo ********************************************************************************
echo ************************************ Success ***********************************
echo ********************************************************************************
cd ..\..
GOTO EOF

:FAIL
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX FAIL XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo                                  Aborting Deploy
cd ..\..
rem return error without closing parent shell
echo A | choice /c:A /n

:EOF
