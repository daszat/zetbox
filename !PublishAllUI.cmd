@echo off
echo ********************************************************************************
echo Generating, Update Schema and Publish
echo Used if schema has changed
echo ********************************************************************************

call "!PublishDev.cmd"

bin\debug\Kistl.Server.Service.exe Configs\%zenv%\Kistl.Server.Service.xml -publish Modules\Examples.xml -ownermodules CourseOrganiser;Invoicing;Parties;TimeRecords
IF ERRORLEVEL 1 GOTO FAIL

rem publish schema data for Ini50 project
bin\debug\Kistl.Server.Service.exe Configs\%zenv%\Kistl.Server.Service.xml -publish Ini50.Modules\Ini50.xml -schemamodules Ini50;Ini50.Config
IF ERRORLEVEL 1 GOTO FAIL

rem export Ini50's SchemaMigration project
bin\debug\Kistl.Server.Service.exe Configs\%zenv%\Kistl.Server.Service.xml -export Ini50.Modules\SchemaMigrationProject.xml -schemamodules SchemaMigration
IF ERRORLEVEL 1 GOTO FAIL

rem export Ini50's calendar data
bin\debug\Kistl.Server.Service.exe Configs\%zenv%\Kistl.Server.Service.xml -export Ini50.Modules\Calendar.xml -schemamodules Calendar -ownermodules Ini50
IF ERRORLEVEL 1 GOTO FAIL

rem export Ini50.Config data
bin\debug\Kistl.Server.Service.exe Configs\%zenv%\Kistl.Server.Service.xml -export Ini50.Modules\Ini50.Config.xml -schemamodules Ini50.Config
IF ERRORLEVEL 1 GOTO FAIL

echo ********************************************************************************
echo ************************************ Success ***********************************
echo ********************************************************************************
GOTO EOF

:FAIL
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX FAIL XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo                              Aborting PublishAllUI

:EOF
pause