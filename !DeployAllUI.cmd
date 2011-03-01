@echo off
echo ********************************************************************************
echo Deploys and generates an updated ZBox Schema
echo Used to apply changes from SVN
echo ********************************************************************************

bin\debug\Kistl.Server.Service.exe Configs\%zenv%\Kistl.Server.Service.xml -deploy Kistl.Server\Database\Database.xml -updatedeployedschema -repairschema 
IF ERRORLEVEL 1 GOTO FAIL

bin\debug\Kistl.Server.Service.exe Configs\%zenv%\Kistl.Server.Service.xml -generate
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