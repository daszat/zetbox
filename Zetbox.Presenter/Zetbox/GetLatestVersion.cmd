@echo off
copy /y ..\..\Zetbox.Client\bin\Debug
copy /y ..\..\Zetbox.Server\bin\Debug
copy /y ..\..\Zetbox.App.Projekte.Client\bin\Debug
copy /y ..\..\Zetbox.App.Projekte.Server\bin\Debug
copy /y C:\temp\ZetboxCodeGen\bin

xcopy /y /s ..\..\DocumentStore DocumentStore

pause