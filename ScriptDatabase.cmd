@echo off

del Kistl.Server\Database\Database.sql

"%ProgramFiles%\Microsoft SQL Server\90\Tools\Publishing\1.2\SqlPubWiz.exe" script -S .\sqlexpress -d Kistl -f Kistl.Server\Database\Database.sql

pause
