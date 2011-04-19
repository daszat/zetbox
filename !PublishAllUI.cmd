@echo off
echo ********************************************************************************
echo Generating, Update Schema and Publish
echo Used if schema has changed
echo ********************************************************************************


bin\Debug\Kistl.Server.Service.exe Configs\%zenv%\Kistl.Server.Service.xml -generate -updatedeployedschema -repairschema
IF ERRORLEVEL 1 GOTO FAIL

rem refresh local code
call GetCodeGen.cmd
IF ERRORLEVEL 1 GOTO FAIL

bin\debug\Kistl.Server.Service.exe Configs\%zenv%\Kistl.Server.Service.xml -publish Kistl.Server\Database\Database.xml *
IF ERRORLEVEL 1 GOTO FAIL

bin\debug\Kistl.Server.Service.exe Configs\%zenv%\Kistl.Server.Service.xml -publish Kistl.Server\Database\KistlBase.xml KistlBase GUI
IF ERRORLEVEL 1 GOTO FAIL

rem managed in Database.xml
rem bin\debug\Kistl.Server.Service.exe Configs\%zenv%\Kistl.Server.Service.xml -publish Kistl.Server\Database\SchemaMigrationSchema.xml SchemaMigration
rem IF ERRORLEVEL 1 GOTO FAIL

bin\debug\Kistl.Server.Service.exe Configs\%zenv%\Kistl.Server.Service.xml -export Kistl.Server\Database\SchemaMigrationProjects.xml SchemaMigration
IF ERRORLEVEL 1 GOTO FAIL

bin\debug\Kistl.Server.Service.exe Configs\%zenv%\Kistl.Server.Service.xml -export Ini50.App.Common\Ini50.Config.xml Ini50.Config
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