@echo off
echo ********************************************************************************
echo Cleans all deployed binaries and database, then builds and reinitialises everything.
echo Afterwards the basic modules are published again to see that nothing has changed.
echo Use this to get a really clean environment and verify changes to infrastructure code.
echo ********************************************************************************

C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe CCNet.msbuild /p:SourceLocation=P:\Kistl /p:ArebisMsBuildPath=P:\Kistl\Libs\ArebisCGen\Arebis.CodeGeneration.MsBuild
IF ERRORLEVEL 1 GOTO FAIL

rem regenerate modules to prove roundtrippability
call "!Publish.cmd"
GOTO EOF

:FAIL
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX FAIL XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo                               Aborting FullResetDev
rem return error without closing parent shell
echo A | choice /c:A /n
pause

:EOF

