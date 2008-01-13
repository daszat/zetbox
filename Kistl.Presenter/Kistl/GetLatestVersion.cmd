@echo off
copy /y ..\..\Kistl.Client\bin\Debug
copy /y ..\..\Kistl.Server\bin\Debug
copy /y ..\..\Kistl.App.Projekte.Client\bin\Debug
copy /y ..\..\Kistl.App.Projekte.Server\bin\Debug
copy /y C:\temp\KistlCodeGen\bin

xcopy /y /s ..\..\DocumentStore DocumentStore

pause