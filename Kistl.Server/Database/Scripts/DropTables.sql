declare @sql nvarchar(2000)
declare @fk nvarchar(2000)
declare @tbl nvarchar(2000)

print 'Dropping Contraints'

declare fk_cursor cursor for 
SELECT c.name, t.name FROM sys.objects c inner join sys.sysobjects t  on t.id = c.parent_object_id WHERE c.type IN (N'F') order by c.name
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

declare tbl_cursor cursor for 
SELECT t.name FROM sys.sysobjects t WHERE t.type IN (N'U') order by t.name
open tbl_cursor

fetch next from tbl_cursor
into @tbl

while @@FETCH_STATUS = 0
begin
	select @sql = N'drop table ' + @tbl
	print @sql
	exec sp_executesql @sql
	
	fetch next from tbl_cursor
	into @tbl
end

close tbl_cursor
deallocate tbl_cursor

print 'Dropping Views'

declare view_cursor cursor for 
SELECT t.name FROM sys.sysobjects t WHERE t.type IN (N'V') order by t.name
open view_cursor

fetch next from view_cursor
into @tbl

while @@FETCH_STATUS = 0
begin
	select @sql = N'drop view ' + @tbl
	print @sql
	exec sp_executesql @sql
	
	fetch next from view_cursor
	into @tbl
end

close view_cursor
deallocate view_cursor

print 'Dropping Stored Procedures'

declare s_cursor cursor for 
SELECT t.name FROM sys.sysobjects t WHERE t.type IN (N'P') order by t.name
open s_cursor

fetch next from s_cursor
into @tbl

while @@FETCH_STATUS = 0
begin
	select @sql = N'drop procedure ' + @tbl
	print @sql
	exec sp_executesql @sql
	
	fetch next from s_cursor
	into @tbl
end

close s_cursor
deallocate s_cursor
