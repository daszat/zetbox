@echo off
echo ********************************************************************************
echo Drops the current Database
echo ********************************************************************************

osql -S .\sqlexpress -E -d Kistl -i Kistl.Server\Database\Scripts\DropTables.sql
pause