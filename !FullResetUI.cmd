@echo off

rem remove old assemblies
del /S /Q bin\ bin\CodeGen\*
IF ERRORLEVEL 1 GOTO FAIL
C:\Windows\Microsoft.NET\Framework\v3.5\MSBuild.exe /m Kistl.Complete.sln /target:Clean
IF ERRORLEVEL 1 GOTO FAIL

rem drop database contents
osql -S .\sqlexpress -E -d Kistl -i Kistl.Server\Database\Scripts\DropTables.sql
IF ERRORLEVEL 1 GOTO FAIL

rem build bootstrapper
C:\Windows\Microsoft.NET\Framework\v3.5\MSBuild.exe /m Kistl.Complete.sln
IF ERRORLEVEL 1 GOTO FAIL


rem populate database and generate other assemblies
bin\debug\bin\Server\Kistl.Server.Service.exe Kistl.Server.Service\DefaultConfig.xml -updateschema Kistl.Server\Database\Database.xml
IF ERRORLEVEL 1 GOTO FAIL
bin\debug\bin\Server\Kistl.Server.Service.exe Kistl.Server.Service\DefaultConfig.xml -deploy Kistl.Server\Database\Database.xml -updateschema -checkschema
IF ERRORLEVEL 1 GOTO FAIL
bin\debug\bin\Server\Kistl.Server.Service.exe Kistl.Server.Service\DefaultConfig.xml -generate
IF ERRORLEVEL 1 GOTO FAIL

rem regenerate Database.xml to prove roundtrippability
bin\debug\bin\Server\Kistl.Server.Service.exe Kistl.Server.Service\DefaultConfig.xml -publish Kistl.Server\Database\Database.xml *
IF ERRORLEVEL 1 GOTO FAIL


rem refresh local code
call GetCodeGen.cmd
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

