@echo off
echo ********************************************************************************
echo Only generates new Model binaries from the currently deployed modules.
echo Used if only frozen objects has changed during development.
echo XXXXXXXXXXXXX Do not forget to publish changes before committing! XXXXXXXXXXXXXX
echo ********************************************************************************

set config=Configs\%zenv%\Zetbox.Cli.xml

if .%1. == .. GOTO GOON

set config=%1

:GOON

bin\Debug\Zetbox.Cli.exe %configs% -fallback -generate
IF ERRORLEVEL 1 GOTO FAIL

echo ********************************************************************************
echo ************************************ Success ***********************************
echo ********************************************************************************
GOTO EOF

:FAIL
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX FAIL XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo                                Aborting Generate
pause
rem return error without closing parent shell
echo A | choice /c:A /n

:EOF