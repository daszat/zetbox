@echo off

rem *********** Interface *********** 
xcopy /y .\Kistl.Objects\*.cs .\Kistl.Objects\Backup

del .\Kistl.Objects\*.cs

xcopy /y C:\temp\KistlCodeGen\Kistl.Objects\*.* .\Kistl.Objects


rem *********** Client *********** 
xcopy /y .\Kistl.Objects.Client\*.Designer.cs .\Kistl.Objects.Client\Backup

del .\Kistl.Objects.Client\*.Designer.cs

xcopy /y C:\temp\KistlCodeGen\Kistl.Objects.Client\*.* .\Kistl.Objects.Client


rem *********** Server *********** 
xcopy /y .\Kistl.Objects.Server\*.Designer.cs .\Kistl.Objects.Server\Backup
xcopy /y .\Kistl.Objects.Server\Model.csdl .\Kistl.Objects.Server\Backup
xcopy /y .\Kistl.Objects.Server\Model.msl .\Kistl.Objects.Server\Backup
xcopy /y .\Kistl.Objects.Server\Model.ssdl .\Kistl.Objects.Server\Backup

del .\Kistl.Objects.Server\*.Designer.cs

xcopy /y C:\temp\KistlCodeGen\Kistl.Objects.Server\*.* .\Kistl.Objects.Server

Pause