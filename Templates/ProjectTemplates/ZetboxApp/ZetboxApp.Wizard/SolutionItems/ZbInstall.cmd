@echo off
IF NOT EXIST Configs\Local XCOPY /S/E Configs\Examples Configs\Local\
.nuget\nuget.exe install .nuget\packages.config -o packages
.zetbox\PrepareEnv.exe .zetbox
echo Done