@echo off
"%ProgramFiles%\Microsoft SQL Server\90\Tools\Publishing\SqlPubWiz.exe" script -S .\sqlexpress -d Kistl -f Kistl.Server\Database\Database.xx.sql
explorer Kistl.Server\Database\