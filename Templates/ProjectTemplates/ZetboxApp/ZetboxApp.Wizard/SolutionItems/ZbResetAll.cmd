@echo off
echo ********************************************************************************
echo Wipe and reinitialise the database with the basic modules.
echo Changes to the object model are generated.
echo Use this to create a clean environment.
echo ********************************************************************************

set config=Configs\Local\Fallback\Zetbox.Server.Service.xml

if .%1. == .. GOTO GOON

set config=%1

:GOON

call "ZbInstall" %config%

cd bin\Debug

Zetbox.Server.Service.exe %config% -wipe
IF ERRORLEVEL 1 GOTO FAIL1

cd ..\..

call "ZbDeployAll" %config%
IF ERRORLEVEL 1 GOTO FAIL2

echo ********************************************************************************
echo ************************************ Success ***********************************
echo ********************************************************************************
GOTO EOF

:FAIL1
cd ..\..
:FAIL2
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX FAIL XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo                                  Aborting Reset
rem return error without closing parent shell
echo A | choice /c:A /n

:EOF
