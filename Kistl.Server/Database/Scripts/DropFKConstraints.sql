select 
name, 
OBJECT_NAME(parent_object_id) parent
into #tmp
from sys.foreign_keys c 

DECLARE @name nvarchar(500)
DECLARE @table nvarchar(500)
DECLARE @sql nvarchar(500)
	
DECLARE c1 CURSOR READ_ONLY
FOR
SELECT *
FROM #tmp


OPEN c1

FETCH NEXT FROM c1
INTO @name, @table

WHILE @@FETCH_STATUS = 0
BEGIN
	select @sql = N'ALTER TABLE ' + @table  + ' DROP CONSTRAINT ' + @name
	print @sql
	execute sp_executesql @sql

	FETCH NEXT FROM c1
	INTO @name, @table

END

CLOSE c1
DEALLOCATE c1


drop table #tmp