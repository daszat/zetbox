@echo off
rem ********************************************************************************
rem copy all generated code back to the ZBox Project for compiling the fallback
rem Used by other command files
rem ********************************************************************************

rem *********** Interface *********** 
xcopy /y .\Kistl.Objects\*.* .\Backup\Kistl.Objects\

del /Q .\Kistl.Objects\*.*

xcopy /y bin\CodeGen\Kistl.Objects\*.* .\Kistl.Objects

rem *********** Server *********** 
xcopy /y .\Kistl.Objects.Server\*.* .\Backup\Kistl.Objects.Server\

del /Q .\Kistl.Objects.Server\*.*

xcopy /y bin\CodeGen\Kistl.Objects.Server\*.* .\Kistl.Objects.Server

rem *********** Memory *********** 
xcopy /y .\Kistl.Objects.Memory\*.* .\Backup\Kistl.Objects.Memory\

del /Q .\Kistl.Objects.Memory\*.*

xcopy /y bin\CodeGen\Kistl.Objects.Memory\*.* .\Kistl.Objects.Memory


rem *********** Assemblies *********** 
rem Do not copy assemblies

rem *********** Build *********** 
rem do not rebuild code as we can't find Arebis
rem C:\Windows\Microsoft.NET\Framework\v3.5\MSBuild.exe /m Kistl.Complete.sln

