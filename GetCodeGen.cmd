@echo off

rem *********** Interface *********** 
xcopy /y .\Kistl.Objects\*.* .\Backup\Kistl.Objects\

del /Q .\Kistl.Objects\*.*

xcopy /y C:\temp\KistlCodeGen\Kistl.Objects\*.* .\Kistl.Objects


rem *********** Client *********** 
xcopy /y .\Kistl.Objects.Client\*.* .\Backup\Kistl.Objects.Client\

del /Q .\Kistl.Objects.Client\*.*

xcopy /y C:\temp\KistlCodeGen\Kistl.Objects.Client\*.* .\Kistl.Objects.Client


rem *********** Server *********** 
xcopy /y .\Kistl.Objects.Server\*.* .\Backup\Kistl.Objects.Server\

del /Q .\Kistl.Objects.Server\*.*

xcopy /y C:\temp\KistlCodeGen\Kistl.Objects.Server\*.* .\Kistl.Objects.Server

rem *********** FrozenObjects *********** 
xcopy /y .\Kistl.Objects.Frozen\*.* .\Backup\Kistl.Objects.Frozen\

del /Q .\Kistl.Objects.Frozen\*.*

xcopy /y C:\temp\KistlCodeGen\Kistl.Objects.Frozen\*.* .\Kistl.Objects.Frozen

Pause
