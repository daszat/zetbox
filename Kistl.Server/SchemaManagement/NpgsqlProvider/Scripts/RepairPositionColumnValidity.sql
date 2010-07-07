CREATE FUNCTION "RepairPositionColumnValidity"(
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
	idSelectStatement CONSTANT text DEFAULT $$SELECT "ID" FROM $$ || quote_ident(refTblName);
	idCursor NO SCROLL CURSOR FOR EXECUTE idSelectStatement;
	
	duplicateStatement CONSTANT text DEFAULT $$
		SELECT COUNT(*)
		FROM (SELECT $$ || quote_ident(fkPositionValue) || $$
				FROM $$ || quote_ident(tblName) || $$
				WHERE $$ || quote_ident(fkColumnName) || $$ = $1
				GROUP BY $$ || quote_ident(fkPositionValue) || $$
				HAVING COUNT(*) > 1
				LIMIT 1) AS duplicates $$
BEGIN

EXECUTE idSelectStatement;

END$BODY$
LANGUAGE 'plpgsql' VOLATILE;
