@echo off
bin\Debug\bin\Server\Kistl.Server.Service.exe Kistl.Server.Service\DefaultConfig.xml -generate -updatedeployedschema -repairschema
IF ERRORLEVEL 1 GOTO FAIL

rem refresh local code
call GetCodeGen.cmd
IF ERRORLEVEL 1 GOTO FAIL

bin\debug\bin\Server\Kistl.Server.Service.exe Kistl.Server.Service\DefaultConfig.xml -publish Kistl.Server\Database\Database.xml *
IF ERRORLEVEL 1 GOTO FAIL

bin\debug\bin\Server\Kistl.Server.Service.exe Kistl.Server.Service\DefaultConfig.xml -export Kistl.Server\Database\SchemaMigrationProjects.xml ZBox.App.SchemaMigration
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