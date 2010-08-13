CREATE OR REPLACE FUNCTION "dbo"."RepairPositionColumnValidity"(
	IN repair boolean,
	IN tblName text,
	IN refTblName text,
	IN fkColumnName text,
	IN fkPositionName text)
RETURNS boolean AS
$BODY$
DECLARE
	result boolean DEFAULT true;

	-- this could be optimized further by only querying for
	-- non-empty collections. this requires creating a index 
	-- over $fkColumnName and using SELECT DISTINCT $fkColumnName
	-- Since this creates more overhead for "normal" operations, this 
	-- option is currently not implemented
	idSelectStatement CONSTANT text DEFAULT $$SELECT "ID" FROM $$ || refTblName;
	idToCheckRec RECORD;
	
	duplicateStatement CONSTANT text DEFAULT $$
		SELECT COUNT(*) > 0
		FROM (SELECT $$ || quote_ident(fkPositionName) || $$
				FROM $$ || tblName || $$
				WHERE $$ || quote_ident(fkColumnName) || $$ = $1
				GROUP BY $$ || quote_ident(fkPositionName) || $$
				HAVING COUNT(*) > 1
				LIMIT 1) AS duplicates $$;

	-- rowNum gets multiplied by 100 to spread indices for cheaper 
	-- collection-insertions (no need to move as many higher indices)
	repairStatement CONSTANT text DEFAULT $$
		WITH numbered_rows ("ID", "rowNum") AS 
			(SELECT "ID", ROW_NUMBER() OVER (ORDER BY $$ || quote_ident(fkPositionName) || $$)
			 FROM $$ || tblName || $$
			 WHERE $$ || quote_ident(fkColumnName) || $$ = $1)
		UPDATE $$ || tblName || $$
		SET $$ || quote_ident(fkPositionName) || $$ = (rowNum * 100)
		FROM $$ || tblName || $$ tbl
			INNER JOIN numbered_rows nr ON (tbl.ID = nr.ID)
		WHERE $$ || quote_ident(fkColumnName) || $$ = $1 $$;

	hasDuplicates boolean;
	
	setZeroStatement CONSTANT text DEFAULT $$
		UPDATE $$ || tblName || $$
		SET $$ || quote_ident(fkPositionName) || $$ = 0
		WHERE $$ || quote_ident(fkPositionName) || $$ IS NULL
			AND $$ || quote_ident(fkColumnName) || $$ IS NOT NULL $$;
BEGIN

FOR idToCheckRec IN EXECUTE idSelectStatement LOOP
	EXECUTE duplicateStatement INTO hasDuplicates USING idToCheckRec."ID";
	IF hasDuplicates THEN
		result := false;
		IF repair THEN
			EXECUTE repairStatement USING idToCheckRec."ID";
		ELSE
			-- abort on first error, if we don't repair
			RETURN result;
		END IF;
	END IF;
END LOOP;

-- TODO: is something changes here, we won't notice
EXECUTE setZeroStatement;

RETURN result;

END$BODY$
LANGUAGE 'plpgsql' VOLATILE;
