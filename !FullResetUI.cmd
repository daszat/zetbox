@echo off

rem remove old assemblies
del /S /Q bin\ C:\temp\KistlCodeGen\*
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
bin\debug\Kistl.Server.Service.exe -updateschema Kistl.Server\Database\Database.xml
IF ERRORLEVEL 1 GOTO FAIL
bin\debug\Kistl.Server.Service.exe -deploy Kistl.Server\Database\Database.xml -updateschema -checkschema
IF ERRORLEVEL 1 GOTO FAIL
bin\debug\Kistl.Server.Service.exe -generate
IF ERRORLEVEL 1 GOTO FAIL

rem regenerate Database.xml to prove roundtrippability
bin\debug\Kistl.Server.Service.exe -publish Kistl.Server\Database\Database.xml *
IF ERRORLEVEL 1 GOTO FAIL


rem refresh local code
rem *********** Interface *********** 
xcopy /y .\Kistl.Objects\*.* .\Backup\Kistl.Objects\

del /Q .\Kistl.Objects\*.*

xcopy /y C:\temp\KistlCodeGen\Kistl.Objects\*.* .\Kistl.Objects

rem *********** Server *********** 
xcopy /y .\Kistl.Objects.Server\*.* .\Backup\Kistl.Objects.Server\

del /Q .\Kistl.Objects.Server\*.*

xcopy /y C:\temp\KistlCodeGen\Kistl.Objects.Server\*.* .\Kistl.Objects.Server

rem rebuild with newly generated code
C:\Windows\Microsoft.NET\Framework\v3.5\MSBuild.exe /m Kistl.Complete.sln
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

