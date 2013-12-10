@echo off
IF NOT EXIST Configs\Local XCOPY /S/E Configs\Examples Configs\Local\

rem nuget install does not remove old versions
rmdir /q /s .\packages

FOR /R %%G IN (packages.config) DO IF EXIST "%%G" .nuget\nuget.exe install "%%G" -o packages
.zetbox\PrepareEnv.exe .zetbox
echo Done