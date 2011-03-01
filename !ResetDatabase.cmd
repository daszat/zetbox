@echo off
echo ********************************************************************************
echo Full reset of ZBox Database
echo Used to reset dev database
echo ********************************************************************************

set config=Configs\%zenv%\Kistl.Server.Service.xml

if .%1. == .. GOTO GOON

set config=%1

:GOON

bin\debug\Kistl.Server.Service.exe %config% -wipe
IF ERRORLEVEL 1 GOTO FAIL

bin\debug\Kistl.Server.Service.exe %config% -updateschema Kistl.Server\Database\Database.xml
IF ERRORLEVEL 1 GOTO FAIL

bin\debug\Kistl.Server.Service.exe %config% -deploy Kistl.Server\Database\Database.xml -checkdeployedschema
IF ERRORLEVEL 1 GOTO FAIL

bin\debug\Kistl.Server.Service.exe %config% -repairschema -syncidentities
IF ERRORLEVEL 1 GOTO FAIL

echo ********************************************************************************
echo ************************************ Success ***********************************
echo ********************************************************************************
GOTO EOF

:FAIL
echo ********************************************************************************
echo ************************************* FAIL *************************************
echo ********************************************************************************
echo Aborting reset.
rem return error without closing parent shell
echo A | choice /c:A /n

:EOF

