@echo off
bin\Debug\Kistl.Server.Service.exe -generate -updateschema -repairschema
bin\debug\Kistl.Server.Service.exe -publish Kistl.Server\Database\Database.xml *
cd ..
call GetCodeGen.cmd
pause
