@echo off
osql -S .\sqlexpress -E -d Kistl -i Kistl.Server\Database\Scripts\DropTables.sql
pause