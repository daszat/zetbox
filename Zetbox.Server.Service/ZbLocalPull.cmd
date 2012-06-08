@echo off
echo ********************************************************************************
echo Pull the binaries and scripts from a specified location.
echo Use this to update your local environment to a locally compiled copy.
echo ********************************************************************************

set source=P:\Zetbox

if .%1. == .. GOTO NO_SOURCE
set source=%1
:NO_SOURCE

set destination=.\Libs\Zetbox
if .%2. == .. GOTO NO_DESTINATION
set destination=%2
:NO_DESTINATION

robocopy %source%\bin\Debug %destination% /MIR /XD Core.Generated NH.Generated EF.Generated
rem errorlevel 8 or higher indicates errors
IF ERRORLEVEL 8 GOTO FAIL

rem since robocopy cannot exclude directories on the top-level only,
rem we have to delete this after copying
del /q /s /f %destination%\Tests
IF ERRORLEVEL 1 GOTO FAIL
rd /q /s %destination%\Tests
IF ERRORLEVEL 1 GOTO FAIL

rem since robocopy cannot exclude directories on the top-level only,
rem we have to delete this after copying
del /q /s /f %destination%\Configs
IF ERRORLEVEL 1 GOTO FAIL
rd /q /s %destination%\Configs
IF ERRORLEVEL 1 GOTO FAIL

robocopy %source%\Modules %destination%\Modules /MIR
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
echo                                  Aborting Pull
rem return error without closing parent shell
echo A | choice /c:A /n

:EOF
