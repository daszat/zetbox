CREATE FUNCTION "RepairPositionColumnValidity"(
	IN "repair" boolean,
	IN "tblName" text,
	IN "refTblName" text,
	IN "fkColumnName" text,
	IN "fkPositionName" text)
RETURNS boolean AS
$BODY$
DECLARE
	result boolean DEFAULT true;

	idCursor refcursor;
	-- this could be optimized further by only querying for
	-- non-empty collections. this requires creating a index 
	-- over @fkColumnName and using SELECT DISTINCT @fkColumnName
	-- Since this creates more overhead for "normal" operations, this 
	-- option is currently not implemented
	idSelectStatement CONSTANT text DEFAULT $$
		OPEN refcursor NO SCROLL FOR
			SELECT "ID" FROM $$ || quote_ident(refTblName);
BEGIN

EXECUTE idSelectStatement;

END$BODY$
LANGUAGE 'plpgsql' VOLATILE;
