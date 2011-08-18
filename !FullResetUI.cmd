@echo off
echo ********************************************************************************
echo Full reset with roundtrip of ZBox Database
echo Used to initialize a new dev enviromnent
echo ********************************************************************************

C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe CCNet.msbuild /p:SourceLocation=P:\Kistl /p:ArebisMsBuildPath=P:\Kistl\Libs\ArebisCGen\Arebis.CodeGeneration.MsBuild
IF ERRORLEVEL 1 GOTO FAIL

rem regenerate Database.xml to prove roundtrippability
bin\debug\Kistl.Server.Service.exe Configs\%zenv%\Kistl.Server.Service.xml -publish Kistl.Server\Database\Database.xml
IF ERRORLEVEL 1 GOTO FAIL

rem regenerate KistlBase.xml to prove roundtrippability
bin\debug\Kistl.Server.Service.exe Configs\%zenv%\Kistl.Server.Service.xml -publish Kistl.Server\Database\KistlBase.xml -ownermodules KistlBase;GUI
IF ERRORLEVEL 1 GOTO FAIL

rem re-import SchemaMigration Projects for Ini50
bin\debug\Kistl.Server.Service.exe Configs\%zenv%\Kistl.Server.Service.xml -import Kistl.Server\Database\SchemaMigrationProjects.xml
IF ERRORLEVEL 1 GOTO FAIL

rem re-import Configuration Data for Ini50
bin\debug\Kistl.Server.Service.exe Configs\%zenv%\Kistl.Server.Service.xml -import Ini50.App.Common\Ini50.Config.xml
IF ERRORLEVEL 1 GOTO FAIL

bin\debug\Kistl.Server.Service.exe Configs\%zenv%\Kistl.Server.Service.xml -import Ini50.App.Common\Calendar.xml
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

