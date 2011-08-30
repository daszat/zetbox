@echo off
echo ********************************************************************************
echo Wipe and reinitialise the database with the basic modules.
echo Use this to create a clean environment.
echo ********************************************************************************

set config=Configs\%zenv%\Kistl.Server.Service.xml

if .%1. == .. GOTO GOON

set config=%1

:GOON

bin\debug\Kistl.Server.Service.exe %config% -wipe -updateschema Modules\KistlBasic.xml;Modules\KistlUtils.xml;Modules\TestModules.xml -deploy Modules\KistlBasic.xml -deploy Modules\KistlUtils.xml -deploy Modules\TestModules.xml -updatedeployedschema -repairschema
IF ERRORLEVEL 1 GOTO FAIL

echo ********************************************************************************
echo ************************************ Success ***********************************
echo ********************************************************************************
GOTO EOF

:FAIL
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX FAIL XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo                            Aborting ResetDatabaseDev
rem return error without closing parent shell
echo A | choice /c:A /n

:EOF

