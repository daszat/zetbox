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

pushd

MSBuild.exe /v:m /p:Configuration=Fallback /p:Platform="Any CPU" $safesolutionname$.sln
IF ERRORLEVEL 1 GOTO FAIL

cd bin\Debug

Zetbox.Cli.exe %config% -fallback -deploy-update -generate -syncidentities
IF ERRORLEVEL 1 GOTO FAIL

cd ..\..

MSBuild.exe /v:m $safesolutionname$.sln
IF ERRORLEVEL 1 GOTO FAIL

cd bin\Debug

rem Optional, if the migration project is used
rem Zetbox.Cli.exe %config% -import ..\..\Data\SchemaMigrationProject.xml
rem IF ERRORLEVEL 1 GOTO FAIL

cd ..\..

echo ********************************************************************************
echo ************************************ Success ***********************************
echo ********************************************************************************
GOTO EOF

:FAIL
popd
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX FAIL XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo                                  Aborting Deploy
rem return error without closing parent shell
echo A | choice /c:A /n

:EOF
