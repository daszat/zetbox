@echo off
echo ********************************************************************************
echo Deploys changes in the basic modules into the database.
echo Changes to the object model are generated.
echo Use this to apply upstream changes.
echo ********************************************************************************

set config=Configs\%zenv%\Zetbox.Server.Service.xml

if .%1. == .. GOTO GOON

set config=%1

:GOON

bin\debug\Zetbox.Server.Service.exe %config% -deploy-update -generate
IF ERRORLEVEL 1 GOTO FAIL

echo ********************************************************************************
echo ************************************ Success ***********************************
echo ********************************************************************************
GOTO EOF

:FAIL
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX FAIL XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo                               Aborting DeployDev
rem return error without closing parent shell
echo A | choice /c:A /n

:EOF
pause