@echo off
echo ********************************************************************************
echo Full reset with roundtrip of ZBox Database
echo Used to initialize a new dev enviromnent
echo ********************************************************************************

C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe CCNet.msbuild /p:SourceLocation=P:\Kistl /p:ArebisMsBuildPath=P:\Kistl\Libs\ArebisCGen\Arebis.CodeGeneration.MsBuild
IF ERRORLEVEL 1 GOTO FAIL

rem regenerate Database.xml to prove roundtrippability
bin\debug\bin\Server\Kistl.Server.Service.exe Kistl.Server.Service\DefaultConfig.xml -publish Kistl.Server\Database\Database.xml *
IF ERRORLEVEL 1 GOTO FAIL

rem regenerate KistlBase.xml to prove roundtrippability
bin\debug\bin\Server\Kistl.Server.Service.exe Kistl.Server.Service\DefaultConfig.xml -publish Kistl.Server\Database\KistlBase.xml KistlBase GUI
IF ERRORLEVEL 1 GOTO FAIL

rem re-import SchemaMigration Projects for Ini50
bin\debug\bin\Server\Kistl.Server.Service.exe Kistl.Server.Service\DefaultConfig.xml -import Kistl.Server\Database\SchemaMigrationProjects.xml SchemaMigration
IF ERRORLEVEL 1 GOTO FAIL

rem re-migrate Ini50 data
bin\debug\bin\Server\Ini50.Migrate.exe Ini50.Migrate\DefaultConfig.xml
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

