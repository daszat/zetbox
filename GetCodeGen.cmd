@echo off
rem ********************************************************************************
rem copy all generated code back to the ZBox Project for compiling the fallback
rem Used by other command files
rem ********************************************************************************

rem *********** Interface *********** 
xcopy /y .\Kistl.Objects\*.* .\Backup\Kistl.Objects\

del /Q .\Kistl.Objects\*.*

xcopy /y bin\CodeGen\Kistl.Objects\*.* .\Kistl.Objects

rem *********** Server - EF *********** 
xcopy /y .\Kistl.Objects.Ef\*.* .\Backup\Kistl.Objects.Ef\

del /Q .\Kistl.Objects.Ef\*.*

xcopy /y bin\CodeGen\Kistl.Objects.Ef\*.* .\Kistl.Objects.Ef

rem *********** Server - NHibernate *********** 
xcopy /y .\Kistl.Objects.NHibernate\*.* .\Backup\Kistl.ObjectsNHibernate\

del /Q .\Kistl.Objects.NHibernate\*.*

xcopy /y bin\CodeGen\Kistl.Objects.NHibernate\*.* .\Kistl.Objects.NHibernate

rem *********** Memory *********** 
xcopy /y .\Kistl.Objects.Memory\*.* .\Backup\Kistl.Objects.Memory\

del /Q .\Kistl.Objects.Memory\*.*

xcopy /y bin\CodeGen\Kistl.Objects.Memory\*.* .\Kistl.Objects.Memory


rem *********** Assemblies *********** 
rem Do not copy assemblies

rem *********** Build *********** 
rem do not rebuild code as we can't find Arebis
rem C:\Windows\Microsoft.NET\Framework\v3.5\MSBuild.exe /m Kistl.Complete.sln

