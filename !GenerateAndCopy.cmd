@echo off
bin\Debug\bin\Server\Kistl.Server.Service.exe Kistl.Server.Service\DefaultConfig.xml -generate
IF ERRORLEVEL 1 GOTO FAIL

rem refresh local code
call GetCodeGen.cmd
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