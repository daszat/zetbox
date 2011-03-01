@echo off
echo ********************************************************************************
echo Only Generating, **NO** Publish
echo Used if only frozen objects has changed during development
echo Call GenerateAndCopy before commit
echo ********************************************************************************

bin\Debug\Kistl.Server.Service.exe Configs\%zenv%\Kistl.Server.Service.xml -generate
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