@echo off
echo ********************************************************************************
echo Publish the basic modules for committing and deployment.
echo This updates the Modules and generated code in the source directory.
echo Use this to publish local changes in the basic modules.
echo ********************************************************************************

set config=Configs\%zenv%\Kistl.Server.Service.xml

if .%1. == .. GOTO GOON

set config=%1

:GOON

Libs\Kistl\Kistl.Server.Service.exe %config% -generate -updatedeployedschema -repairschema
IF ERRORLEVEL 1 GOTO FAIL

rem need to export both modules to receive all necessary meta-data
Libs\Kistl\Kistl.Server.Service.exe %config% -publish Libs\Kistl\Modules\KistlBasic.xml -ownermodules KistlBase;GUI
IF ERRORLEVEL 1 GOTO FAIL

Libs\Kistl\Kistl.Server.Service.exe %config% -publish Libs\Kistl\Modules\KistlUtils.xml -ownermodules DocumentManagement;ModuleEditor;SchemaMigration;Calendar
IF ERRORLEVEL 1 GOTO FAIL

Libs\Kistl\Kistl.Server.Service.exe %config% -publish Libs\Kistl\Modules\TestModules.xml -ownermodules Projekte;TestModule
IF ERRORLEVEL 1 GOTO FAIL

echo ********************************************************************************
echo ************************************ Success ***********************************
echo ********************************************************************************
GOTO EOF

:FAIL
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX FAIL XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo                                Aborting Publish
rem return error without closing parent shell
echo A | choice /c:A /n

:EOF
