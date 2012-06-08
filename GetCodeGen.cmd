@echo off
rem ********************************************************************************
rem copy all generated code back to the Zetbox Project for compiling the fallback
rem Used by other command files
rem ********************************************************************************

rem *********** Interface *********** 
xcopy /y .\Zetbox.Objects\*.* .\Backup\Zetbox.Objects\

del /Q .\Zetbox.Objects\*.*

xcopy /y bin\CodeGen\Zetbox.Objects\*.* .\Zetbox.Objects

rem *********** Server - EF *********** 
xcopy /y .\Zetbox.Objects.Ef\*.* .\Backup\Zetbox.Objects.Ef\

del /Q .\Zetbox.Objects.Ef\*.*

xcopy /y bin\CodeGen\Zetbox.Objects.Ef\*.* .\Zetbox.Objects.Ef

rem *********** Server - NHibernate *********** 
xcopy /y .\Zetbox.Objects.NHibernate\*.* .\Backup\Zetbox.Objects.NHibernate\

del /Q .\Zetbox.Objects.NHibernate\*.*

xcopy /y bin\CodeGen\Zetbox.Objects.NHibernate\*.* .\Zetbox.Objects.NHibernate

rem *********** Memory *********** 
xcopy /y .\Zetbox.Objects.Memory\*.* .\Backup\Zetbox.Objects.Memory\

del /Q .\Zetbox.Objects.Memory\*.*

xcopy /y bin\CodeGen\Zetbox.Objects.Memory\*.* .\Zetbox.Objects.Memory


rem *********** Assemblies *********** 
rem Do not copy assemblies

rem *********** Build *********** 
rem do not rebuild code as we can't find Arebis
rem C:\Windows\Microsoft.NET\Framework\v3.5\MSBuild.exe /m Zetbox.Complete.sln

