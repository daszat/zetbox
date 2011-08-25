@echo off
echo ********************************************************************************
echo Full reset with roundtrip of ZBox Database
echo Used to initialize a new dev enviromnent
echo ********************************************************************************

C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe CCNet.msbuild /p:SourceLocation=P:\Kistl /p:ArebisMsBuildPath=P:\Kistl\Libs\ArebisCGen\Arebis.CodeGeneration.MsBuild
IF ERRORLEVEL 1 GOTO FAIL

rem regenerate modules to prove roundtrippability
call "!PublishAllUI.cmd"

rem import Ini50 application data
rem re-import SchemaMigration Projects
bin\debug\Kistl.Server.Service.exe Configs\%zenv%\Kistl.Server.Service.xml -import Ini50.Modules\SchemaMigrationProjects.xml
IF ERRORLEVEL 1 GOTO FAIL

rem re-import configuration
bin\debug\Kistl.Server.Service.exe Configs\%zenv%\Kistl.Server.Service.xml -import Ini50.Modules\Ini50.Config.xml
IF ERRORLEVEL 1 GOTO FAIL

rem re-import calendar
bin\debug\Kistl.Server.Service.exe Configs\%zenv%\Kistl.Server.Service.xml -import Ini50.Modules\Calendar.xml
IF ERRORLEVEL 1 GOTO FAIL

rem re-migrate Ini50 data
bin\debug\Ini50.Migrate.exe Configs\%zenv%\Ini50.Migrate.xml
IF ERRORLEVEL 1 GOTO FAIL

echo ********************************************************************************
echo ************************************ Success ***********************************
echo ********************************************************************************
GOTO EOF

:FAIL
echo ********************************************************************************
echo ************************************* FAIL *************************************
echo ********************************************************************************
echo Aborting FullResetUI.

:EOF
pause

