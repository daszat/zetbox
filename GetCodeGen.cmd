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
rem Server & Interfaces comes from MSBuild
xcopy /y C:\temp\KistlCodeGen\bin\Debug\Kistl.Objects.Client.* .\bin\debug
xcopy /y C:\temp\KistlCodeGen\bin\Debug\Kistl.Objects.Frozen.* .\bin\debug
