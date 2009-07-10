@echo off
cd Kistl.Server
bin\debug\Kistl.Server.exe -deploy Database\Database.xml -updateschema -checkschema 
bin\debug\Kistl.Server.exe -generate
pause