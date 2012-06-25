@echo off
.nuget\nuget.exe install .nuget\packages.config -o packages
.zetbox\PrepareEnv.exe .zetbox
echo Done