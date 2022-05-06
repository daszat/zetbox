@echo off
echo ********************************************************************************
echo Publish the basic modules for committing and deployment.
echo This updates the Modules and generated code in the source directory.
echo Use this to publish local changes in the basic modules.
echo ********************************************************************************

set config=Configs\%zenv%\Zetbox.Cli.xml

if .%1. == .. GOTO GOON

set config=%1

:GOON

bin\Debug\Zetbox.Cli.exe %config% -fallback -generate -updatedeployedschema -repairschema
IF ERRORLEVEL 1 GOTO FAIL

bin\Debug\Zetbox.Cli.exe %configs% -generate-resources
IF ERRORLEVEL 1 GOTO FAIL

rem refresh local code
call GetCodeGen.cmd
IF ERRORLEVEL 1 GOTO FAIL

dotnet build .\Zetbox.Objects\Zetbox.Objects.csproj
IF ERRORLEVEL 1 GOTO FAIL

rem need to export both modules to receive all necessary meta-data
bin\debug\Zetbox.Cli.exe %config% -publish Modules\ZetboxBasic.xml -ownermodules ZetboxBase;GUI;SchemaMigration
IF ERRORLEVEL 1 GOTO FAIL

bin\debug\Zetbox.Cli.exe %config% -publish Modules\ZetboxUtils.xml -ownermodules DocumentManagement;ModuleEditor;Calendar;LicenseManagement
IF ERRORLEVEL 1 GOTO FAIL

bin\debug\Zetbox.Cli.exe %config% -publish Modules\TestModules.xml -ownermodules Projekte;TestModule
IF ERRORLEVEL 1 GOTO FAIL

echo ********************************************************************************
echo ************************************ Success ***********************************
echo ********************************************************************************
GOTO EOF

:FAIL
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX FAIL XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo                              Aborting zbPublishAll
pause
rem return error without closing parent shell
echo A | choice /c:A /n

:EOF