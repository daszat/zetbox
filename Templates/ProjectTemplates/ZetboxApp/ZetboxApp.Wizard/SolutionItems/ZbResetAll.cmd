@echo off
echo ********************************************************************************
echo Wipe and reinitialise the database with the basic modules.
echo Changes to the object model are generated.
echo Use this to create a clean environment.
echo ********************************************************************************

set config=

if .%1. == .. GOTO GOON

set config=%1

:GOON
pushd

call "ZbInstall" %config%

cd bin\Debug

Zetbox.Cli.exe %config% -fallback -wipe
IF ERRORLEVEL 1 GOTO FAIL

cd ..\..

call "ZbDeployAll" %config%
IF ERRORLEVEL 1 GOTO FAIL

popd
echo ********************************************************************************
echo ************************************ Success ***********************************
echo ********************************************************************************
GOTO EOF

:FAIL
popd
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX FAIL XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo                                  Aborting Reset
rem return error without closing parent shell
echo A | choice /c:A /n

:EOF
