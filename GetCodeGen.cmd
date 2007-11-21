@echo off
rem *********** Objects *********** 
xcopy /y .\Kistl.Objects\*.Designer.cs .\Kistl.Objects\Backup
xcopy /y .\Kistl.Objects\Model.csdl .\Kistl.Objects\Backup
xcopy /y .\Kistl.Objects\Model.msl .\Kistl.Objects\Backup
xcopy /y .\Kistl.Objects\Model.ssdl .\Kistl.Objects\Backup

del .\Kistl.Objects\*.Designer.cs

xcopy /y C:\temp\KistlCodeGen\Kistl.Objects\*.* .\Kistl.Objects


rem *********** Client *********** 
xcopy /y .\Kistl.Objects.Client\*.Designer.cs .\Kistl.Objects.Client\Backup

del .\Kistl.Objects.Client\*.Designer.cs

xcopy /y C:\temp\KistlCodeGen\Kistl.Objects.Client\*.* .\Kistl.Objects.Client


rem *********** Server *********** 
xcopy /y .\Kistl.Objects.Server\*.Designer.cs .\Kistl.Objects.Server\Backup

del .\Kistl.Objects.Server\*.Designer.cs

xcopy /y C:\temp\KistlCodeGen\Kistl.Objects.Server\*.* .\Kistl.Objects.Server

Pause