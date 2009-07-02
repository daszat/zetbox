@echo off
cd Kistl.Server
osql -S .\sqlexpress -E -d Kistl -i Database\Scripts\DropTables.sql
bin\debug\Kistl.Server.exe -updateschema Database\Database.xml -import Database\Database.xml -checkschema 
bin\debug\Kistl.Server.exe -generate
pause