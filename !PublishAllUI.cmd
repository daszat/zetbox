@echo off
bin\Debug\Kistl.Server.Service.exe Kistl.Server.Service\DefaultConfig.xml -generate -updateschema -repairschema
bin\debug\Kistl.Server.Service.exe Kistl.Server.Service\DefaultConfig.xml -publish Kistl.Server\Database\Database.xml *
call GetCodeGen.cmd
pause
