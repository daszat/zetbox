@echo off
echo ********************************************************************************
echo Deployes and generates a updated ZBox Schema
echo Used to apply changes from SVN
echo ********************************************************************************

bin\debug\bin\Server\Kistl.Server.Service.exe Kistl.Server.Service\DefaultConfig%zenv%.xml -deploy Kistl.Server\Database\Database.xml -updatedeployedschema -repairschema 
IF ERRORLEVEL 1 GOTO FAIL

bin\debug\bin\Server\Kistl.Server.Service.exe Kistl.Server.Service\DefaultConfig%zenv%.xml -generate
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

:EOF
pause