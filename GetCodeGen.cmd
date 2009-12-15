@echo off

rem *********** Interface *********** 
xcopy /y .\Kistl.Objects\*.* .\Backup\Kistl.Objects\

del /Q .\Kistl.Objects\*.*

xcopy /y C:\temp\KistlCodeGen\Kistl.Objects\*.* .\Kistl.Objects

rem *********** Server *********** 
xcopy /y .\Kistl.Objects.Server\*.* .\Backup\Kistl.Objects.Server\

del /Q .\Kistl.Objects.Server\*.*

xcopy /y C:\temp\KistlCodeGen\Kistl.Objects.Server\*.* .\Kistl.Objects.Server


rem *********** Assemblies *********** 
rem Do not copy assemblies

rem *********** Build *********** 
rem rebuild with newly generated code
C:\Windows\Microsoft.NET\Framework\v3.5\MSBuild.exe /m Kistl.Complete.sln

