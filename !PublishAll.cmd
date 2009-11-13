@echo off
cd Kistl.Server.Service
bin\Debug\Kistl.Server.Service.exe -generate -updateschema -repairschema
bin\debug\Kistl.Server.Service.exe -publish P:\Kistl\Kistl.Server\Database\Database.xml *
pause
