@echo off
bin\debug\bin\Server\Kistl.Server.Service.exe Kistl.Server.Service\DefaultConfig.xml -deploy Kistl.Server\Database\Database.xml -updateschema -checkschema 
bin\debug\bin\Server\Kistl.Server.Service.exe Kistl.Server.Service\DefaultConfig.xml -generate
pause