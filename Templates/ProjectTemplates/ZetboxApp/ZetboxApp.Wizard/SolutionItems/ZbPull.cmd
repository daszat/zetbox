@echo off
echo ********************************************************************************
echo Pull the binaries and scripts from a specified location.
echo Use this to update your local environment to a new version.
echo ********************************************************************************

if .%1. == .. GOTO FAIL
if .%2. == .. GOTO FAIL

set source=%1
set destination=%2

robocopy %source%\bin\Debug %destination% /MIR /XD Core.Generated NH.Generated EF.Generated DocumentStore Configs
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
