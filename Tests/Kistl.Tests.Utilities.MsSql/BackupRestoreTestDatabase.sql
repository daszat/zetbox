-- Backup Database
BACKUP DATABASE zbox TO DISK = 'c:\temp\zbox.bak' WITH FORMAT
GO
-- Kill existing users
DECLARE @dbname varchar(40)
select @dbname = 'zbox_test'
DECLARE @strSQL varchar(255)

CREATE table #tmpUsers(
spid int,
eid int,
status varchar(30),
loginname varchar(50),
hostname varchar(50),
blk int,
dbname varchar(50),
cmd varchar(30),
request_id int )

INSERT INTO #tmpUsers EXEC SP_WHO


DECLARE LoginCursor CURSOR
READ_ONLY
FOR SELECT spid FROM #tmpUsers WHERE cmd != 'CHECKPOINT' AND dbname = @dbname

DECLARE @spid varchar(30)
OPEN LoginCursor

FETCH NEXT FROM LoginCursor INTO @spid
WHILE (@@fetch_status <> -1)
BEGIN
IF (@@fetch_status <> -2)
BEGIN
PRINT 'Killing ' + @spid
SET @strSQL ='KILL ' + @spid
EXEC (@strSQL)
END
FETCH NEXT FROM LoginCursor INTO @spid
END

CLOSE LoginCursor
DEALLOCATE LoginCursor

DROP table #tmpUsers

GO
-- Restore database
declare @dbfile nvarchar(500)
declare @logfile nvarchar(500)
SELECT @dbfile = filename FROM zbox_test.sys.sysfiles where filename like '%.mdf'
SELECT @logfile = filename FROM zbox_test.sys.sysfiles where filename like '%.ldf'

RESTORE DATABASE zbox_test FROM DISK = 'c:\temp\zbox.bak' 
	WITH RECOVERY, REPLACE, 
	MOVE 'zbox' to @dbfile,
	MOVE 'zbox_log' to @logfile
	
-- for example:
-- SELECT filename FROM zbox.sys.sysfiles where filename like '%.mdf'
-- SELECT  filename FROM zbox.sys.sysfiles where filename like '%.ldf'

-- RESTORE DATABASE zbox_test FROM DISK = 'c:\temp\kistl.bak' 
-- 	WITH RECOVERY, REPLACE, 
-- 	MOVE 'zbox' to 'C:\ProgramData\MSSQL10.SQLEXPRESS\MSSQL\DATA\zbox_test.mdf',
-- 	MOVE 'zbox_log' to 'C:\ProgramData\MSSQL10.SQLEXPRESS\MSSQL\DATA\zbox_test_log.ldf'
