@echo off
echo ********************************************************************************
echo Publish the basic modules for committing and deployment.
echo This updates the Modules and generated code in the source directory.
echo Use this to publish local changes in the basic modules.
echo ********************************************************************************

set config=Configs\%zenv%\Zetbox.Server.Service.xml

if .%1. == .. GOTO GOON

set config=%1

:GOON

bin\Debug\Zetbox.Server.Service.exe %config% -generate -updatedeployedschema -repairschema
IF ERRORLEVEL 1 GOTO FAIL

rem refresh local code
call GetCodeGen.cmd
IF ERRORLEVEL 1 GOTO FAIL

rem need to export both modules to receive all necessary meta-data
bin\debug\Zetbox.Server.Service.exe %config% -publish Modules\ZetboxBasic.xml -ownermodules ZetboxBase;GUI
IF ERRORLEVEL 1 GOTO FAIL

bin\debug\Zetbox.Server.Service.exe %config% -publish Modules\ZetboxUtils.xml -ownermodules DocumentManagement;ModuleEditor;SchemaMigration;Calendar
IF ERRORLEVEL 1 GOTO FAIL

bin\debug\Zetbox.Server.Service.exe %config% -publish Modules\TestModules.xml -ownermodules Projekte;TestModule
IF ERRORLEVEL 1 GOTO FAIL

echo ********************************************************************************
echo ************************************ Success ***********************************
echo ********************************************************************************
GOTO EOF

:FAIL
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX FAIL XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo                              Aborting PublishDev
rem return error without closing parent shell
echo A | choice /c:A /n

:EOF
pause