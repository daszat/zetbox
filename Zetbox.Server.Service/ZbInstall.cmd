@echo off
IF NOT EXIST Configs\Local XCOPY /S/E Configs\Examples Configs\Local\

.zetbox\PrepareEnv.exe .zetbox
echo Done