@echo off
echo ********************************************************************************
echo Deploys changes in the basic modules into the database.
echo Changes to the object model are generated.
echo Use this to apply upstream changes.
echo ********************************************************************************

set config=Configs\%zenv%\Kistl.Server.Service.xml

if .%1. == .. GOTO GOON

set config=%1

:GOON

Libs\Kistl\Kistl.Server.Service.exe %config% -deploy Libs\Kistl\Modules\KistlBasic.xml -deploy Libs\Kistl\Modules\KistlUtils.xml -deploy Libs\Kistl\Modules\TestModules.xml -updatedeployedschema -repairschema
IF ERRORLEVEL 1 GOTO FAIL

Libs\Kistl\Kistl.Server.Service.exe %config% -generate
IF ERRORLEVEL 1 GOTO FAIL

echo ********************************************************************************
echo ************************************ Success ***********************************
echo ********************************************************************************
GOTO EOF

:FAIL
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX FAIL XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo                                 Aborting Deploy
rem return error without closing parent shell
echo A | choice /c:A /n

:EOF
