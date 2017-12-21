@echo off
rem ********************************************************************************
rem copy all generated code back to the Zetbox Project for compiling the fallback
rem Used by other command files
rem ********************************************************************************

rem *********** Interface *********** 
del /S /Q .\Zetbox.Objects.Fallback\*.* >nul
xcopy /s /y bin\CodeGen\Zetbox.Objects\*.* .\Zetbox.Objects.Fallback >nul

rem *********** Server - EF *********** 
del /S /Q .\Zetbox.Objects.Ef.Fallback\*.* >nul
xcopy /s /y bin\CodeGen\Zetbox.Objects.Ef\*.* .\Zetbox.Objects.Ef.Fallback >nul

rem *********** Server - NHibernate *********** 
del /S /Q .\Zetbox.Objects.NHibernate.Fallback\*.* >nul
xcopy /s /y bin\CodeGen\Zetbox.Objects.NHibernate\*.* .\Zetbox.Objects.NHibernate.Fallback >nul

rem *********** Memory *********** 
del /S /Q .\Zetbox.Objects.Memory.Fallback\*.* >nul
xcopy /s /y bin\CodeGen\Zetbox.Objects.Memory\*.* .\Zetbox.Objects.Memory.Fallback >nul

exit /b 0