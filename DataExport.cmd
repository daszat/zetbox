@echo off
cd Kistl.Server
bin\Debug\Kistl.Server.exe -generate
bin\debug\Kistl.Server.exe -export Database\Database.xml *
pause
