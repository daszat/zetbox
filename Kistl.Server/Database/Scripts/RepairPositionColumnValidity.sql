CREATE PROCEDURE RepairPositionColumnValidity(
	@repair BIT,
	@tblName nvarchar(255),
	@refTblName nvarchar(255),
	@fkColumnName nvarchar(255),
	@fkPositionName nvarchar(255),
	@result BIT OUTPUT) AS 

SET @result = 1

-- this could be optimized further by only querying for
-- non-empty collections. this requires creating a index 
-- over @fkColumnName and using SELECT DISTINCT @fkColumnName
-- Since this creates more overhead for "normal" operations, this 
-- option is currently not implemented
DECLARE @idSelectStatement nvarchar(4000) = N'
DECLARE ids CURSOR LOCAL FAST_FORWARD FOR
SELECT [ID] FROM [' + @refTblName +']
OPEN ids

SET @idCursor = ids
'
DECLARE @idSelectStatementParamDef nvarchar(255) = N'@idCursor CURSOR OUTPUT'

DECLARE @duplicateStatement nvarchar(4000) = N'
SELECT @cnt = COUNT(*) FROM (
	SELECT TOP 1 [' + @fkPositionName + ']
		FROM [' + @tblName + ']
		WHERE [' + @fkColumnName + '] = @fk
		GROUP BY [' + @fkPositionName + ']
		HAVING COUNT(*) > 1
	) AS duplicates
'
DECLARE @duplicateStatementParamDef nvarchar(255) = N'@fk INT, @cnt INT OUTPUT'

-- rowNum gets multiplied by 100 to spread indices for cheaper 
-- collection-insertions (no need to move as many higher indices)
DECLARE @repairStatement nvarchar(4000) = N'
WITH numbered_rows ([ID], [rowNum]) AS 
	(SELECT [ID], ROW_NUMBER() OVER (ORDER BY [' + @fkPositionName + ']) FROM [' + @tblName + '] WHERE [' + @fkColumnName + '] = @fk)
UPDATE [' + @tblName + '] SET [' + @fkPositionName + '] = (rowNum * 100)
FROM [' + @tblName + '] tbl INNER JOIN numbered_rows nr ON (tbl.ID = nr.ID)
WHERE [' + @fkColumnName + '] = @fk'
DECLARE @repairStatementParamDef nvarchar(255) = N'@fk INT'

DECLARE @idsToCheck CURSOR
EXECUTE sp_executesql @idSelectStatement, @idSelectStatementParamDef, @idCursor = @idsToCheck OUTPUT

DECLARE @fk INT
FETCH NEXT FROM @idsToCheck
INTO @fk

WHILE @@FETCH_STATUS = 0
BEGIN
	DECLARE @cnt INT
	EXECUTE sp_executesql @duplicateStatement, @duplicateStatementParamDef, @fk = @fk, @cnt = @cnt OUTPUT

	IF @cnt > 0 
	BEGIN
		SET @result = 0
		IF @repair = 1
			EXECUTE sp_executesql @repairStatement, @repairStatementParamDef, @fk = @fk
		ELSE
			RETURN
	END 

	FETCH NEXT FROM @idsToCheck
	INTO @fk
END

IF @repair = 1
BEGIN
	-- due to the @duplicateStatement check, collections with only a single item are not checked (they have always unique position)
	-- but the positions can still be null. Set them all to Zero now
	DECLARE @setZeroStatement nvarchar(4000) = N'
UPDATE [' + @tblName + '] SET [' + @fkPositionName + '] = 0
WHERE [' + @fkPositionName + '] IS NULL'
	EXECUTE sp_executesql @setZeroStatement
END
	
