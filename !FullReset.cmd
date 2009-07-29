rem @echo off

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

cd Kistl.Server

rem populate database and generate other assemblies
bin\debug\Kistl.Server.exe -updateschema Database\Database.xml
IF ERRORLEVEL 1 GOTO FAIL
bin\debug\Kistl.Server.exe -deploy Database\Database.xml -updateschema -checkschema
IF ERRORLEVEL 1 GOTO FAIL
bin\debug\Kistl.Server.exe -generate
IF ERRORLEVEL 1 GOTO FAIL

rem regenerate Database.xml to prove roundtrippability
bin\debug\Kistl.Server.exe -publish Database\Database.xml *
IF ERRORLEVEL 1 GOTO FAIL

cd ..

rem refresh local code
GetCodeGen.cmd

rem rebuild with newly generated code
C:\Windows\Microsoft.NET\Framework\v3.5\MSBuild.exe /m Kistl.Complete.sln
IF ERRORLEVEL 1 GOTO FAIL

GOTO EOF

:FAIL
echo Aborting reset.
pause
:EOF
