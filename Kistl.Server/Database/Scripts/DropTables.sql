declare @sql nvarchar(2000)
declare @fk nvarchar(2000)
declare @tbl nvarchar(2000)

print 'Dropping Contraints'

declare fk_cursor cursor for 
SELECT c.name, t.name FROM sys.objects c inner join sys.sysobjects t  on t.id = c.parent_object_id WHERE c.type IN (N'F')
open fk_cursor

fetch next from fk_cursor
into @fk, @tbl

while @@FETCH_STATUS = 0
begin
	select @sql = N'alter table ' + @tbl + ' drop constraint ' + @fk
	print @sql
	exec sp_executesql @sql
	
	fetch next from fk_cursor
	into @fk, @tbl
end

close fk_cursor
deallocate fk_cursor


print 'Dropping Tables'

declare fk_tbl cursor for 
SELECT t.name FROM sys.sysobjects t WHERE t.type IN (N'U')
open fk_tbl

fetch next from fk_tbl
into @tbl

while @@FETCH_STATUS = 0
begin
	select @sql = N'drop table ' + @tbl
	print @sql
	exec sp_executesql @sql
	
	fetch next from fk_tbl
	into @tbl
end

close fk_tbl
deallocate fk_tbl
