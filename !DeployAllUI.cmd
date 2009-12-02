@echo off
bin\debug\Kistl.Server.Service.exe -deploy Kistl.Server\Database\Database.xml -updateschema -checkschema 
bin\debug\Kistl.Server.Service.exe -generate
pause