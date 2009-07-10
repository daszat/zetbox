@echo off
cd Kistl.Server
osql -S .\sqlexpress -E -d Kistl -i Database\Scripts\DropTables.sql
pause