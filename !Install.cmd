@echo off
echo ********************************************************************************
echo Installs the currently compiled binaries and modules from the working directory
echo into the specified directory.
echo Use this to install ZetBox Basic into a Module/App solution.
echo ********************************************************************************

set destination=.\Libs\Kistl

if .%1. == .. GOTO GOON

set destination=%1\Libs\Kistl

:GOON

robocopy bin\Debug %destination% /MIR /PURGE /XD Tests "*Core.Generated" "*NH.Generated" "*EF.Generated"
rem errorlevel 8 or higher indicates errors
IF ERRORLEVEL 8 GOTO FAIL

robocopy Modules %destination%\Modules /MIR 
rem errorlevel 8 or higher indicates errors
IF ERRORLEVEL 8 GOTO FAIL

echo ********************************************************************************
echo ************************************ Success ***********************************
echo ********************************************************************************
GOTO EOF

:FAIL
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX FAIL XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo                               Aborting Install
rem return error without closing parent shell
echo A | choice /c:A /n

:EOF
pause