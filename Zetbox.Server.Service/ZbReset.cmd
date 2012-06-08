@echo off
echo ********************************************************************************
echo Wipe and reinitialise the database with the basic modules.
echo Use this to create a clean environment.
echo ********************************************************************************

set config=Configs\%zenv%\Zetbox.Server.Service.xml

if .%1. == .. GOTO GOON

set config=%1

:GOON

Libs\Zetbox\Zetbox.Server.Service.exe %config% -wipe -deploy-update -generate -syncidentities
IF ERRORLEVEL 1 GOTO FAIL

echo ********************************************************************************
echo ************************************ Success ***********************************
echo ********************************************************************************
GOTO EOF

:FAIL
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX FAIL XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo                                  Aborting Reset
rem return error without closing parent shell
echo A | choice /c:A /n

:EOF
