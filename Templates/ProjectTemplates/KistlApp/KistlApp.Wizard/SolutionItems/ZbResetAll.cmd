@echo off
echo ********************************************************************************
echo Wipe and reinitialise the database with the basic modules.
echo Changes to the object model are generated.
echo Use this to create a clean environment.
echo ********************************************************************************

set config=Configs\Local\Fallback\Kistl.Server.Service.xml

if .%1. == .. GOTO GOON

set config=%1

:GOON

bin\debug\Kistl.Server.Service.exe %config% -wipe
IF ERRORLEVEL 1 GOTO FAIL

call "ZbDeployAll" %config%
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
