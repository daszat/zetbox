@echo off
rem ********************************************************************************
rem copy all generated code back to the Zetbox Project for compiling the fallback
rem Used by other command files
rem ********************************************************************************

rem *********** Interface *********** 
echo Interfaces
del /S /Q .\Zetbox.Objects\*.* >nul
xcopy /s /y bin\CodeGen\Zetbox.Objects\*.* .\Zetbox.Objects >nul

rem *********** Server - NHibernate *********** 
echo Server
del /S /Q .\Zetbox.Objects.NHibernate\*.* >nul
xcopy /s /y bin\CodeGen\Zetbox.Objects.NHibernate\*.* .\Zetbox.Objects.NHibernate >nul

rem *********** Memory *********** 
echo Memory
del /S /Q .\Zetbox.Objects.Memory\*.* >nul
xcopy /s /y bin\CodeGen\Zetbox.Objects.Memory\*.* .\Zetbox.Objects.Memory >nul

rem *********** Assets *********** 
echo Assets
xcopy /s /y bin\CodeGen\Assets\*.* .\Zetbox.Assets >nul

exit /b 0