@echo off
bin\Debug\Kistl.Server.Service.exe Kistl.Server.Service\DefaultConfig.xml -generate -updateschema -repairschema
IF ERRORLEVEL 1 GOTO FAIL
bin\debug\Kistl.Server.Service.exe Kistl.Server.Service\DefaultConfig.xml -publish Kistl.Server\Database\Database.xml *
IF ERRORLEVEL 1 GOTO FAIL
call GetCodeGen.cmd

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