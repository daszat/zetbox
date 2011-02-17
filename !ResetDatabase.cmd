@echo off
echo ********************************************************************************
echo Full reset of ZBox Database
echo Used to reset dev database
echo ********************************************************************************

bin\debug\bin\Server\Kistl.Server.Service.exe Configs\%zenv%\Kistl.Server.Service.xml -wipe
IF ERRORLEVEL 1 GOTO FAIL

bin\debug\bin\Server\Kistl.Server.Service.exe Configs\%zenv%\Kistl.Server.Service.xml -updateschema Kistl.Server\Database\Database.xml
IF ERRORLEVEL 1 GOTO FAIL

bin\debug\bin\Server\Kistl.Server.Service.exe Configs\%zenv%\Kistl.Server.Service.xml -deploy Kistl.Server\Database\Database.xml -checkdeployedschema
IF ERRORLEVEL 1 GOTO FAIL

bin\debug\bin\Server\Kistl.Server.Service.exe Configs\%zenv%\Kistl.Server.Service.xml -repairschema -syncidentities
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

