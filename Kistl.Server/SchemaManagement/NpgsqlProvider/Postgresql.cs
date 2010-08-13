
namespace Kistl.Server.SchemaManagement.NpgsqlProvider
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.Server;
    using Kistl.API.Utils;
    using Npgsql;

    public class Postgresql
        : AdoNetSchemaProvider<NpgsqlConnection, NpgsqlTransaction, NpgsqlCommand>
    {
        private readonly static log4net.ILog _log = log4net.LogManager.GetLogger("Kistl.Server.Schema.Npgsql");
        protected override log4net.ILog Log { get { return _log; } }
        private readonly static log4net.ILog _queryLog = log4net.LogManager.GetLogger("Kistl.Server.Schema.Npgsql.Queries");
        protected override log4net.ILog QueryLog { get { return _queryLog; } }

        #region Meta data

        public override string ConfigName { get { return "POSTGRESQL"; } }
        public override string AdoNetProvider { get { return "Npgsql"; } }
        public override string ManifestToken { get { return "8.1.3"; } }
        public override bool IsStorageProvider { get { return true; } }

        #endregion

        #region ADO.NET, Connection and Transaction Handling

        protected override NpgsqlConnection CreateConnection(string connectionString)
        {
            return new NpgsqlConnection(connectionString);
        }

        protected override NpgsqlTransaction CreateTransaction()
        {
            return CurrentConnection.BeginTransaction();
        }

        protected override NpgsqlCommand CreateCommand(string query)
        {
            return new NpgsqlCommand(query, CurrentConnection, CurrentTransaction);
        }

        #endregion

        #region Type Mapping

        public override string DbTypeToNative(DbType type)
        {
            switch (type)
            {
                case DbType.Byte:
                case DbType.Int16:
                    return "int2";
                case DbType.UInt16:
                case DbType.Int32:
                    return "int4";
                case DbType.Single:
                    return "float4";
                case DbType.Double:
                    return "float8";
                case DbType.String:
                    return "varchar";
                case DbType.Date:
                    return "date";
                case DbType.DateTime:
                case DbType.DateTime2:
                    return "timestamp";
                case DbType.Boolean:
                    return "bool";
                case DbType.Guid:
                    return "uuid";
                case DbType.Binary:
                    return "bytea";
                case DbType.Decimal:
                case DbType.VarNumeric:
                    return "numeric";
                case DbType.UInt32: // no int8 supported by Npgsql 2.0.9
                case DbType.Int64:
                case DbType.UInt64:
                default:
                    throw new ArgumentOutOfRangeException("type", string.Format("Unable to convert type '{0}' to an sql type string", type));
            }
        }

        public override DbType NativeToDbType(string type)
        {
            switch (type)
            {
                case "int2":
                    return DbType.Int16;
                case "int4":
                    return DbType.Int32;
                case "float4":
                    return DbType.Single;
                case "float8":
                    return DbType.Double;
                case "varchar":
                case "text":
                    return DbType.String;
                case "date":
                    return DbType.Date;
                case "timestamp":
                    return DbType.DateTime;
                case "bool":
                    return DbType.Boolean;
                case "uuid":
                    return DbType.Guid;
                case "bytea":
                    return DbType.Binary;
                case "numeric":
                    return DbType.Decimal;
                default:
                    throw new ArgumentOutOfRangeException("type", string.Format("Unable to convert type '{0}' to a DbType", type));
            }
        }

        #endregion

        #region SQL Infrastructure

        protected override string QuoteIdentifier(string name)
        {
            return "\"" + name + "\"";
        }

        private string GetColumnDefinition(Column col)
        {
            string nullable = col.IsNullable ? "NULL" : "NOT NULL";

            // hardcode special cases
            if (col.Type == DbType.String && col.Size == int.MaxValue)
            {
                // Create text columns for unlimited string length
                return String.Format("{0} text {1}", QuoteIdentifier(col.Name), nullable);
            }
            else if (col.Type == DbType.Binary && col.Size == int.MaxValue)
            {
                // Create bytea columns for unlimited blob length
                return String.Format("{0} bytea {1}", QuoteIdentifier(col.Name), nullable);
            }
            else
            {
                string size = string.Empty;
                if (col.Size > 0 && col.Type.In(DbType.String, DbType.StringFixedLength, DbType.AnsiString, DbType.AnsiStringFixedLength, DbType.Binary))
                {
                    size = String.Format("({0})", col.Size);
                }
                else if (col.Size > 0 && col.Type.In(DbType.Decimal, DbType.VarNumeric))
                {
                    size = String.Format("({0}, {1})", col.Size, col.Scale.Value);
                }

                string typeString = DbTypeToNative(col.Type) + size;
                return String.Format("{0} {1} {2}", QuoteIdentifier(col.Name), typeString, nullable);
            }
        }

        #endregion

        #region Table Structure

        protected override string FormatFullName(TableRef tbl)
        {
            return String.Format("\"{0}\".\"{1}\".\"{2}\"", tbl.Database, tbl.Schema, tbl.Name);
        }

        protected override string FormatSchemaName(TableRef tbl)
        {
            return String.Format("\"{0}\".\"{1}\"", tbl.Schema, tbl.Name);
        }

        public override bool CheckTableExists(TableRef tblName)
        {
            return (bool)ExecuteScalar("SELECT COUNT(*) > 0 FROM pg_tables WHERE schemaname=@schema AND tablename=@table",
                new Dictionary<string, object>()
                {
                    { "@schema", tblName.Schema},
                    { "@table", tblName.Name},
                });
        }

        public override IEnumerable<TableRef> GetTableNames()
        {
            return ExecuteReader("SELECT schemaname, tablename FROM pg_tables WHERE schemaname not in ('information_schema', 'pg_catalog')")
                .Select(rd => new TableRef(CurrentConnection.Database, rd.GetString(0), rd.GetString(1)));
        }

        public override void CreateTable(TableRef tblName, IEnumerable<Column> cols)
        {
            if (cols == null) throw new ArgumentNullException("cols");

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("CREATE TABLE {0} (", FormatSchemaName(tblName));
            sb.AppendLine();

            sb.Append(String.Join(",\n", cols.Select(col => GetColumnDefinition(col)).ToArray()));

            sb.Remove(sb.Length - 1, 1);
            sb.Append(")");
            ExecuteNonQuery(sb.ToString());
        }

        public override void CreateTable(TableRef tblName, bool idAsIdentityColumn, bool createPrimaryKey)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("CREATE TABLE \"{0}\".\"{1}\" ( ", tblName.Schema, tblName.Name);
            if (idAsIdentityColumn)
            {
                sb.AppendLine("\"ID\" serial NOT NULL");
            }
            else
            {
                sb.AppendLine("\"ID\" integer NOT NULL");
            }

            if (createPrimaryKey)
            {
                // TODO: use Construct to create PK_{0}
                sb.AppendFormat(", CONSTRAINT \"PK_{0}\" PRIMARY KEY ( \"ID\" )", tblName);
            }

            sb.AppendLine();
            sb.Append(")");

            ExecuteNonQuery(sb.ToString());
        }

        public override void RenameTable(TableRef oldTblName, TableRef newTblName)
        {
            if (!oldTblName.Database.Equals(newTblName.Database)) { throw new ArgumentOutOfRangeException("newTblName", "cannot rename table to different database"); }
            if (!oldTblName.Schema.Equals(newTblName.Schema)) { throw new ArgumentOutOfRangeException("newTblName", "cannot rename table to different schema"); }

            ExecuteNonQuery(String.Format(
                "ALTER TABLE {0} RENAME TO {1}",
                FormatFullName(oldTblName),
                QuoteIdentifier(newTblName.Name)));
        }

        public override bool CheckColumnExists(TableRef tblName, string colName)
        {
            return (bool)ExecuteScalar(@"
                SELECT COUNT(*) > 0
                FROM pg_attribute a
                    JOIN pg_class c ON c.oid = a.attrelid
                    LEFT JOIN pg_namespace n ON n.oid = c.relnamespace 
                WHERE n.nspname = @schema AND c.relname = @table AND a.attname=@name",
                new Dictionary<string, object>()
                {
                    { "@schema", tblName.Schema},
                    { "@table", tblName.Name},
                    { "@name", colName},
                });
        }

        public override IEnumerable<string> GetTableColumnNames(TableRef tbl)
        {
            return ExecuteReader(
                    @"SELECT attname
                        FROM pg_attribute
                            JOIN pg_class ON (attrelid = pg_class.oid)
                            JOIN pg_namespace ON (relnamespace = pg_namespace.oid)
                        WHERE nspname = @schema AND relname = @table and relkind = 'r' AND attnum >= 0",
                    new Dictionary<string, object>() {
                        { "@schema", tbl.Schema },
                        { "@table", tbl.Name }
                    })
                .Select(rd => rd.GetString(0));
        }

        public override IEnumerable<Column> GetTableColumns(TableRef tblName)
        {
            return ExecuteReader(
                @"SELECT a.attname, t.typname, a.atttypmod - 4 as len, not a.attnotnull as nullable, t.typlen < 0 as variable_length
                    FROM pg_attribute a
                        JOIN pg_class c ON (a.attrelid = c.oid)
                        JOIN pg_namespace n ON (c.relnamespace = n.oid)
                        JOIN pg_type t ON (a.atttypid = t.oid)
                    WHERE n.nspname = @schema AND c.relname = @table and c.relkind = 'r' AND a.attnum >= 0",
                new Dictionary<string, object>() {
                    { "@schema", tblName.Schema },
                    { "@table", tblName.Name }
                })
                .Select(rd => new Column()
                {
                    Name = rd.GetString(0),
                    Type = NativeToDbType(rd.GetString(1)),
                    Size = rd.GetBoolean(4) ? rd.GetInt16(2) : int.MaxValue,
                    IsNullable = rd.GetBoolean(3)
                });
        }

        protected override void DoColumn(bool add, TableRef tblName, string colName, DbType type, int size, int scale, bool isNullable, DefaultConstraint defConstraint)
        {
            StringBuilder sb = new StringBuilder();

            string addOrAlter = add ? "ADD" : "ALTER COLUMN";
            string nullable = isNullable ? "NULL" : "NOT NULL";

            sb.AppendFormat("ALTER TABLE {0} {1} {2}", FormatFullName(tblName), addOrAlter, GetColumnDefinition(new Column() { Name = colName, Type = type, Size = size, Scale = scale, IsNullable = isNullable }));

            ExecuteNonQuery(sb.ToString());

            var constrName = ConstructDefaultConstraintName(tblName, colName);
            if (GetHasColumnDefaultValue(tblName, colName))
            {
                ExecuteNonQuery(String.Format("ALTER TABLE {0} ALTER COLUMN {0} DROP DEFAULT", FormatFullName(tblName), QuoteIdentifier(colName)));
            }

            if (defConstraint != null)
            {
                string defValue;
                if (defConstraint is NewGuidDefaultConstraint)
                {
                    defValue = "uuid_generate_v4()";
                }
                else if (defConstraint is IntDefaultConstraint)
                {
                    defValue = ((IntDefaultConstraint)defConstraint).Value.ToString();
                }
                else if (defConstraint is BoolDefaultConstraint)
                {
                    defValue = ((BoolDefaultConstraint)defConstraint).Value ? "TRUE" : "FALSE";
                }
                else if (defConstraint is DateTimeDefaultConstraint)
                {
                    defValue = "now()";
                }
                else
                {
                    throw new ArgumentOutOfRangeException("defConstraint", "Unsupported default constraint " + defConstraint.GetType().Name);
                }
                ExecuteNonQuery(string.Format("ALTER TABLE {0} ALTER COLUMN {1} SET DEFAULT {2}", FormatFullName(tblName), QuoteIdentifier(colName), defValue));
            }
        }

        public override void RenameColumn(TableRef tblName, string oldColName, string newColName)
        {
            ExecuteNonQuery(String.Format(
                "ALTER TABLE {0} RENAME COLUMN {1} TO {2}",
                FormatFullName(tblName),
                QuoteIdentifier(oldColName),
                QuoteIdentifier(newColName)));
        }

        public override bool GetIsColumnNullable(TableRef tblName, string colName)
        {
            return (bool)ExecuteScalar(@"
                SELECT NOT a.attnotnull
                FROM pg_class c
                    LEFT JOIN pg_namespace n ON n.oid = c.relnamespace
                    LEFT JOIN pg_attribute a ON c.oid = a.attrelid
                WHERE n.nspname = @schema AND c.relname = @table AND c.relkind = 'r'
                    AND a.attnum >= 1 AND a.attname=@column",
                new Dictionary<string, object>() { 
                    { "@schema", tblName.Schema },
                    { "@table", tblName.Name}, 
                    { "@column", colName}
                });
        }

        public override bool GetHasColumnDefaultValue(TableRef tblName, string colName)
        {
            return (bool)ExecuteScalar(@"
                SELECT (d.adbin IS NOT NULL AND d.adbin <> '') as has_default
                FROM pg_class c
	                LEFT JOIN pg_namespace n ON n.oid = c.relnamespace
	                LEFT JOIN pg_attribute a ON c.oid = a.attrelid
	                LEFT JOIN pg_attrdef d ON c.oid = d.adrelid AND a.attnum = d.adnum
                WHERE n.nspname = @schema AND c.relname = @table AND a.attname = @column",
                new Dictionary<string, object>() { 
                    { "@schema", tblName.Schema },
                    { "@table", tblName.Name}, 
                    { "@column", colName}
                });
        }

        public override int GetColumnMaxLength(TableRef tblName, string colName)
        {
            return (int)ExecuteScalar(@"
                SELECT a.atttypmod - 4 -- adjust for varchar implementation, which is storing the length too
                FROM pg_class c
	                LEFT JOIN pg_namespace n ON n.oid = c.relnamespace
	                LEFT JOIN pg_attribute a ON c.oid = a.attrelid
                WHERE n.nspname = @schema AND c.relname = @table AND a.attname = @column",
                new Dictionary<string, object>() { 
                    { "@schema", tblName.Schema },
                    { "@table", tblName.Name}, 
                    { "@column", colName}
                });
        }

        #endregion

        #region Table Content

        public override bool CheckTableContainsData(TableRef tbl)
        {
            return (bool)ExecuteScalar(String.Format(
                "SELECT COUNT(*) > 0 FROM (SELECT * FROM {0} LIMIT 1) AS data",
                FormatFullName(tbl)));
        }

        public override bool CheckColumnContainsNulls(TableRef tbl, string colName)
        {
            return (bool)ExecuteScalar(String.Format(
                "SELECT COUNT(*) > 0 FROM (SELECT {1} FROM {0} WHERE {1} IS NULL LIMIT 1) AS nulls",
                FormatFullName(tbl),
                QuoteIdentifier(colName)));
        }

        public override bool CheckColumnContainsUniqueValues(TableRef tbl, string colName)
        {
            return (bool)ExecuteScalar(String.Format(
                @"SELECT COUNT(*) = 0 FROM (
                    SELECT {1} FROM {0} WHERE {1} IS NOT NULL
                    GROUP BY {1}
                    HAVING COUNT({1}) > 1 LIMIT 1) AS tbl",
                FormatFullName(tbl),
                QuoteIdentifier(colName)));
        }

        public override bool CheckColumnContainsValues(TableRef tbl, string colName)
        {
            return (bool)ExecuteScalar(String.Format(
                "SELECT COUNT(*) > 0 FROM (SELECT {1} FROM {0} WHERE {1} IS NOT NULL LIMIT 1) AS data",
                FormatFullName(tbl),
                QuoteIdentifier(colName)));
        }

        public override long CountRows(TableRef tblName)
        {
            return (long)ExecuteScalar(String.Format(
                @"SELECT COUNT(*) FROM {0}",
                FormatFullName(tblName)));
        }

        #endregion

        #region Constraint and Index Management

        public override bool CheckFKConstraintExists(string fkName)
        {
            return (bool)ExecuteScalar("SELECT COUNT(*) > 0 FROM pg_constraint WHERE conname = @constraint_name AND contype = 'f'",
                new Dictionary<string, object>(){
                    { "@constraint_name", fkName }
                });
        }

        public override IEnumerable<TableConstraintNamePair> GetFKConstraintNames()
        {
            return ExecuteReader(@"
                SELECT n.nspname, c.relname, conname
                FROM pg_class c
                    LEFT JOIN pg_namespace n ON n.oid = c.relnamespace
                    LEFT JOIN pg_constraint ON conrelid = c.oid
                WHERE contype = 'f'
                ORDER BY conname")
                .Select(rd => new TableConstraintNamePair()
                {
                    ConstraintName = rd.GetString(2),
                    TableName = new TableRef(CurrentConnection.Database, rd.GetString(0), rd.GetString(1))
                })
                .ToList();
        }

        public override void CreateFKConstraint(TableRef tblName, TableRef refTblName, string colName, string constraintName, bool onDeleteCascade)
        {
            ExecuteNonQuery(String.Format(
                @"ALTER TABLE {0}
                    ADD CONSTRAINT {1} FOREIGN KEY({2})
                    REFERENCES {3} ({4}){5}",
                FormatFullName(tblName),
                QuoteIdentifier(constraintName),
                QuoteIdentifier(colName),
                FormatFullName(refTblName),
                QuoteIdentifier("ID"),
                onDeleteCascade ? @" ON DELETE CASCADE" : String.Empty));
        }

        public override void RenameFKConstraint(string oldConstraintName, string newConstraintName)
        {
            ExecuteNonQuery(String.Format(
                "ALTER INDEX {0} RENAME TO {1}",
                QuoteIdentifier(oldConstraintName),
                QuoteIdentifier(newConstraintName)));
        }

        public override bool CheckIndexExists(TableRef tblName, string idxName)
        {
            return (bool)ExecuteScalar(@"
                SELECT COUNT(*) > 0
                    FROM pg_index
                        JOIN pg_class idx ON (indexrelid = idx.oid)
                        JOIN pg_class tbl ON (indrelid = tbl.oid)
                        JOIN pg_namespace ON (tbl.relnamespace = pg_namespace.oid)
                    WHERE nspname = @schema AND tbl.relname = @table AND idx.relname = @index",
                new Dictionary<string, object>(){
                    { "@schema", tblName.Schema },
                    { "@table", tblName.Name },
                    { "@index", idxName },
                });
        }

        public override void DropIndex(TableRef tblName, string idxName)
        {
            ExecuteNonQuery(String.Format(
                "DROP INDEX {0}.{1}",
                QuoteIdentifier(tblName.Schema),
                QuoteIdentifier(idxName)));
        }

        #endregion

        #region Other DB Objects (Views, Triggers, Procedures)

        public override bool CheckViewExists(TableRef viewName)
        {
            return (bool)ExecuteScalar(@"
                SELECT COUNT(*) > 0
                FROM pg_views
                WHERE schemaname = @schema AND viewname = @view",
                new Dictionary<string, object>() {
                    { "@schema", viewName.Schema },
                    { "@view", viewName.Name },
                });
        }

        public override bool CheckTriggerExists(TableRef objName, string triggerName)
        {
            return (bool)ExecuteScalar(@"
                SELECT count(*) > 0
                FROM pg_proc p
                    JOIN pg_namespace n ON p.pronamespace = n.oid
                    JOIN pg_type t ON p.prorettype = t.oid
                WHERE n.nspname = @schema AND p.proname = @trigger AND t.typname = 'trigger'",
                new Dictionary<string, object>(){
                    { "@schema", objName.Schema },
                    { "@trigger", triggerName },
                });
        }

        public override void DropTrigger(TableRef objName, string triggerName)
        {
            ExecuteNonQuery(String.Format("DROP FUNCTION \"dbo\".{0}() CASCADE",
                QuoteIdentifier(triggerName)));
        }

        public override bool CheckProcedureExists(string procName)
        {
            // TODO: remove 'dbo' reference
            return (bool)ExecuteScalar(@"
                SELECT count(*) > 0
                FROM pg_proc p
                    LEFT JOIN pg_namespace n ON p.pronamespace = n.oid
                WHERE n.nspname = 'dbo' AND p.proname = @proc",
                new Dictionary<string, object>() {
                    { "@proc", procName },
                });
        }

        /// <summary>
        /// Drops all versions of the function with the specified name.
        /// </summary>
        public override void DropProcedure(string procName)
        {
            foreach (var argTypes in GetParameterTypes(procName))
            {
                ExecuteNonQuery(String.Format("DROP FUNCTION {0}({1})",
                    QuoteIdentifier(procName),
                    String.Join(",", argTypes)));
            }
        }

        private IEnumerable<string[]> GetParameterTypes(string procName)
        {
            string sqlQuery = @"
                SELECT args.proc_oid, t.typname 
                FROM pg_type t 
                    JOIN (
                        SELECT oid proc_oid, proargtypes::oid[] argtypes, generate_subscripts(proargtypes::oid[], 1) argtype_subscript 
                        FROM pg_proc where proname = @procName) args 
                    ON t.oid = args.argtypes[args.argtype_subscript] 
                ORDER BY args.proc_oid, args.argtype_subscript;";
            QueryLog.Debug(sqlQuery);

            long? lastProcOid = null;
            List<string> types = null;
            List<string[]> result = new List<string[]>();
            foreach (var rd in ExecuteReader(sqlQuery, new Dictionary<string, object>() { { "@procname", procName } }))
            {
                var procOid = rd.GetInt64(0);
                var argType = rd.GetString(1);
                if (lastProcOid != procOid)
                {
                    if (types != null)
                    {
                        result.Add(types.ToArray());
                    }
                    lastProcOid = procOid;
                    types = new List<string>();
                }
                types.Add(argType);
            }
            result.Add(types.ToArray());
            return result;
        }

        public override void EnsureInfrastructure()
        {
        }

        public override void DropAllObjects()
        {
            ExecuteSqlResource(this.GetType(), "Kistl.Server.SchemaManagement.NpgsqlProvider.Scripts.ResetSchema.sql");
        }

        #endregion

        #region ZBox Schema Handling

        protected override string GetSchemaInsertStatement()
        {
            return "INSERT INTO \"dbo\".\"CurrentSchema\" (\"Version\", \"Schema\") VALUES (1, @schema)";
        }

        protected override string GetSchemaUpdateStatement()
        {
            return "UPDATE \"dbo\".\"CurrentSchema\" SET \"Schema\" = @schema, \"Version\" = \"Version\" + 1";
        }

        #endregion

        #region zBox Accelerators

        protected override bool CallRepairPositionColumn(bool repair, TableRef tblName, string indexName)
        {
            return (bool)ExecuteScalar("SELECT \"dbo\".\"RepairPositionColumnValidityByTable\"(@repair, @tblName, @colName)",
                 new Dictionary<string, object>() {
                    {"@repair", repair},
                    {"@tblName", FormatSchemaName(tblName)},
                    {"@colName", indexName},
                 });
        }

        #endregion

        // --  TODO  --  Cleanup stuff below  ----------------------------------------
        // ---------------------------------------------------------------------------
        // ---------------------------------------------------------------------------
        // ---------------------------------------------------------------------------
        // ---------------------------------------------------------------------------
        // ---------------------------------------------------------------------------
        // ---------------------------------------------------------------------------
        // ---------------------------------------------------------------------------
        // ---------------------------------------------------------------------------
        // ---------------------------------------------------------------------------
        // ---------------------------------------------------------------------------
        // ---------------------------------------------------------------------------
        // ---------------------------------------------------------------------------

        public override void CopyColumnData(
            TableRef srcTblName, string srcColName,
            TableRef tblName, string colName)
        {
            ExecuteNonQuery(String.Format(
                "UPDATE dest SET dest.{3} = src.{1} FROM {2} dest INNER JOIN {0} src ON dest.{4} = src.{4}",
                FormatFullName(srcTblName),     // 0
                QuoteIdentifier(srcColName),    // 1
                FormatFullName(tblName),        // 2
                QuoteIdentifier(colName),       // 3
                QuoteIdentifier("ID")));        // 4
        }

        public override void MigrateFKs(
            TableRef srcTblName, string srcColName,
            TableRef tblName, string colName)
        {
            ExecuteNonQuery(String.Format(
                "UPDATE dest SET dest.{3} = src.{4} FROM {2} dest INNER JOIN {0} src ON dest.{4} = src.{1}",
                FormatFullName(srcTblName), // 0
                QuoteIdentifier(srcColName), // 1
                FormatFullName(tblName),    // 2
                QuoteIdentifier(colName),    // 3
                QuoteIdentifier("ID")));     // 4
        }

        public override void InsertFKs(
            TableRef srcTblName, string srcColName,
            TableRef tblName, string colName,
            string fkColName)
        {
            ExecuteNonQuery(String.Format(
                "INSERT INTO {2} ({3}, {4}) SELECT {5}, {1} FROM {0} WHERE {1} IS NOT NULL",
                FormatFullName(srcTblName), // 0
                QuoteIdentifier(srcColName), // 1
                FormatFullName(tblName),    // 2
                QuoteIdentifier(colName),    // 3
                QuoteIdentifier(fkColName),  // 4
                QuoteIdentifier("ID")));     // 5
        }

        public override void CopyFKs(
            TableRef srcTblName, string srcColName,
            TableRef tblName, string colName,
            string srcFkColName)
        {
            ExecuteNonQuery(String.Format(
                "UPDATE dest SET dest.{3} = src.{1} FROM {2} dest INNER JOIN {0} src ON src.{4} = dest.{5}",
                FormatFullName(srcTblName), // 0
                QuoteIdentifier(srcColName), // 1
                FormatFullName(tblName),    // 2
                QuoteIdentifier(colName),    // 3
                QuoteIdentifier(srcFkColName),  // 4
                QuoteIdentifier("ID")));     // 5
        }

        public override void CreateIndex(TableRef tblName, string idxName, bool unique, bool clustered, params string[] columns)
        {
            ExecuteNonQuery(String.Format(
                "CREATE {0}INDEX {1} ON {2} ({3})",
                unique ? "UNIQUE " : String.Empty,
                QuoteIdentifier(idxName),
                FormatFullName(tblName),
                String.Join(", ", columns.Select(c => QuoteIdentifier(c)).ToArray())));

            if (clustered)
            {
                ExecuteNonQuery(String.Format("CLUSTER {0} USING {1}",
                    FormatFullName(tblName),
                    QuoteIdentifier(idxName)));
            }
        }

        public override void CreateUpdateRightsTrigger(string triggerName, TableRef tblName, List<RightsTrigger> tblList)
        {
            if (String.IsNullOrEmpty(triggerName)) throw new ArgumentNullException("triggerName");
            if (tblList == null) throw new ArgumentNullException("tblList");

            StringBuilder sb = new StringBuilder();
            sb.Append("CREATE OR REPLACE FUNCTION ");
            sb.Append(QuoteIdentifier("dbo"));
            sb.Append(".");
            sb.Append(QuoteIdentifier(triggerName));
            sb.Append("()");
            sb.AppendLine();
            sb.Append(@"  RETURNS trigger AS
$BODY$BEGIN
");
            foreach (var tbl in tblList)
            {
                if (tbl.Relations.Count == 0)
                {
                    sb.AppendFormat(@"
	IF OLD <> NULL THEN
		DELETE FROM {0} WHERE {2} = OLD.{2};
	END IF;
	IF NEW <> NULL THEN
		DELETE FROM {0} WHERE {2} = NEW.{2};
		INSERT INTO {0} ({2}, ""Identity"", ""Right"")
			SELECT {2}, ""Identity"", ""Right"" FROM {1}
			WHERE {2} = NEW.{2};
	END IF;
", FormatFullName(tbl.TblNameRights), FormatFullName(tbl.ViewUnmaterializedName), QuoteIdentifier("ID"));
                }
                else
                {
                    StringBuilder select = new StringBuilder();
                    select.AppendFormat("SELECT t1.\"ID\" FROM {0} t1", FormatFullName(tbl.TblName));
                    int idx = 2;
                    var lastRel = tbl.Relations.Last();
                    foreach (var rel in tbl.Relations)
                    {
                        select.AppendLine();
                        if (rel == lastRel)
                        {
                            select.AppendFormat(@"   WHERE t{0}.{1} = {2}.{3}",
                                idx,
                                QuoteIdentifier(rel.JoinColumnName.Single().ColumnName),
                                "{0}",
                                QuoteIdentifier(rel.FKColumnName.Single().ColumnName));
                        }
                        else
                        {
                            select.AppendFormat(@"      INNER JOIN {0} t{1} ON (t{1}.{2} = t{3}.{4})",
                                FormatFullName(rel.JoinTableName),
                                idx,
                                QuoteIdentifier(rel.JoinColumnName.Single().ColumnName),
                                idx - 1,
                                QuoteIdentifier(rel.FKColumnName.Single().ColumnName));
                        }
                        idx++;
                    }
                    string selectFormat = select.ToString();

                    sb.AppendFormat(@"
	IF OLD <> NULL THEN
		DELETE FROM {0} WHERE {4} IN ({1});
	END IF;
	IF NEW <> NULL THEN
		DELETE FROM {0} WHERE {4} IN ({2});
		INSERT INTO {0} ({4}, ""Identity"", ""Right"")
			SELECT {4}, ""Identity"", ""Right"" FROM {3}
			WHERE {4} IN ({2});
	END IF;
",
                        FormatFullName(tbl.TblNameRights),
                        String.Format(selectFormat, "OLD"),
                        String.Format(selectFormat, "NEW"),
                        FormatFullName(tbl.ViewUnmaterializedName),
                        QuoteIdentifier("ID"));

                }
                sb.AppendLine();
                sb.AppendLine();
            }

            sb.Append(@"	RETURN NULL;
END$BODY$
  LANGUAGE 'plpgsql' VOLATILE
");
            ExecuteNonQuery(sb.ToString());
            ExecuteNonQuery(String.Format(@"
                CREATE TRIGGER {0} AFTER INSERT OR UPDATE OR DELETE
                    ON {1} FOR EACH ROW
                    EXECUTE PROCEDURE ""dbo"".{0}()",
                QuoteIdentifier(triggerName),
                FormatSchemaName(tblName)
                ));
        }

        public override void CreateEmptyRightsViewUnmaterialized(TableRef viewName)
        {
            Log.DebugFormat("Creating *empty* unmaterialized rights view \"{0}\"", viewName);
            ExecuteNonQuery(String.Format(@"CREATE VIEW {0} AS SELECT 0 AS ""ID"", 0 AS ""Identity"", 0 AS ""Right"" WHERE 0 = 1", FormatFullName(viewName)));
        }

        public override void CreateRightsViewUnmaterialized(TableRef viewName, TableRef tblName, TableRef tblNameRights, IList<ACL> acls)
        {
            if (acls == null) throw new ArgumentNullException("acls");
            Log.DebugFormat("Creating unmaterialized rights view for \"{0}\"", tblName);

            StringBuilder view = new StringBuilder();
            view.AppendFormat(@"CREATE VIEW {0} AS
SELECT  ""ID"", ""Identity"", 
		(case SUM(""Right"" & 1) when 0 then 0 else 1 end) +
		(case SUM(""Right"" & 2) when 0 then 0 else 2 end) +
		(case SUM(""Right"" & 4) when 0 then 0 else 4 end) +
		(case SUM(""Right"" & 8) when 0 then 0 else 8 end) ""Right"" 
FROM (", FormatFullName(viewName));
            view.AppendLine();

            foreach (var acl in acls)
            {
                view.AppendFormat(@"  SELECT t1.""ID"" ""ID"", t{0}.{1} ""Identity"", {2} ""Right""",
                    acl.Relations.Count,
                    QuoteIdentifier(acl.Relations.Last().FKColumnName.Single().ColumnName),
                    (int)acl.Right);
                view.AppendLine();
                view.AppendFormat(@"  FROM {0} t1", FormatFullName(tblName));
                view.AppendLine();

                int idx = 2;
                foreach (var rel in acl.Relations.Take(acl.Relations.Count - 1))
                {
                    view.AppendFormat(@"  INNER JOIN {0} t{1} ON t{1}.{2} = t{3}.{4}",
                        FormatFullName(rel.JoinTableName),
                        idx,
                        QuoteIdentifier(rel.JoinColumnName.Single().ColumnName),
                        idx - 1,
                        QuoteIdentifier(rel.FKColumnName.Single().ColumnName));
                    view.AppendLine();
                    idx++;
                }
                view.AppendFormat(@"  WHERE t{0}.{1} IS NOT NULL",
                    acl.Relations.Count,
                    QuoteIdentifier(acl.Relations.Last().FKColumnName.Single().ColumnName));
                view.AppendLine();
                view.AppendLine("  UNION ALL");
            }
            view.Remove(view.Length - 12, 12);

            view.AppendLine(@") unmaterialized GROUP BY ""ID"", ""Identity""");

            ExecuteNonQuery(view.ToString());
        }

        public override void CreateRefreshRightsOnProcedure(
            string procName,
            TableRef viewUnmaterializedName,
            TableRef tblName,
            TableRef tblNameRights)
        {
            Log.DebugFormat("Creating refresh rights procedure for \"{0}\"", tblName);
            ExecuteNonQuery(String.Format(
                @"
CREATE FUNCTION ""dbo"".{0}(IN refreshID integer DEFAULT NULL) RETURNS void AS
$BODY$BEGIN
    IF (refreshID IS NULL) THEN
            TRUNCATE TABLE {1};
            INSERT INTO {1} (""ID"", ""Identity"", ""Right"") SELECT ""ID"", ""Identity"", ""Right"" FROM {2};
    ELSE
            DELETE FROM {1} WHERE ""ID"" = refreshID;
            INSERT INTO {1} (""ID"", ""Identity"", ""Right"") SELECT ""ID"", ""Identity"", ""Right"" FROM {2} WHERE ""ID"" = refreshID;
    END IF;
END$BODY$
LANGUAGE 'plpgsql' VOLATILE",
                QuoteIdentifier(procName),
                FormatFullName(tblNameRights),
                FormatFullName(viewUnmaterializedName)));
        }

        public override void ExecRefreshRightsOnProcedure(string procName)
        {
            Log.DebugFormat("Refreshing rights for \"{0}\"", procName);
            ExecuteNonQuery(String.Format(@"SELECT ""dbo"".{0}()", QuoteIdentifier(procName)));
        }

        public override void CreatePositionColumnValidCheckProcedures(ILookup<TableRef, KeyValuePair<TableRef, string>> refSpecs)
        {
            if (refSpecs == null) { throw new ArgumentNullException("refSpecs"); }

            var procName = "RepairPositionColumnValidity";
            var tableProcName = procName + "ByTable";

            foreach (var name in new[] { procName, tableProcName })
            {
                if (CheckProcedureExists(name))
                {
                    DropProcedure(name);
                }
            }

            ExecuteSqlResource(this.GetType(), String.Format(@"Kistl.Server.SchemaManagement.NpgsqlProvider.Scripts.{0}.sql", procName));

            var createTableProcQuery = new StringBuilder();
            createTableProcQuery.AppendFormat("CREATE OR REPLACE FUNCTION \"dbo\".\"{0}\" (repair boolean, tblName text, colName text) RETURNS boolean AS $BODY$", tableProcName);
            createTableProcQuery.AppendLine();
            createTableProcQuery.AppendLine("DECLARE result boolean DEFAULT false;");
            createTableProcQuery.AppendLine("BEGIN");
            foreach (var tbl in refSpecs)
            {
                createTableProcQuery.AppendFormat("IF tblName IS NULL OR tblName = '{0}' THEN", FormatSchemaName(tbl.Key));
                createTableProcQuery.AppendLine();
                createTableProcQuery.Append("\t");
                foreach (var refSpec in tbl)
                {
                    createTableProcQuery.AppendFormat("IF colName IS NULL OR colName = '{0}{1}' THEN", refSpec.Value, Kistl.API.Helper.PositionSuffix);
                    createTableProcQuery.AppendLine();
                    createTableProcQuery.AppendFormat(
                        // TODO: use named parameters with 9.0: "\t\tresult := \"RepairPositionColumnValidity\"(repair := repair, tblName := '{0}', refTblName := '{1}', fkColumnName := '{2}', fkPositionName := '{2}{3}');",
                        "\t\tresult := \"dbo\".\"RepairPositionColumnValidity\"(repair, '{0}', '{1}', '{2}', '{2}{3}');",
                        FormatSchemaName(tbl.Key),
                        FormatSchemaName(refSpec.Key),
                        refSpec.Value,
                        Kistl.API.Helper.PositionSuffix);
                    createTableProcQuery.AppendLine();
                    createTableProcQuery.AppendLine("\t\tIF NOT repair AND result THEN RETURN true; END IF;");
                    createTableProcQuery.AppendFormat("\tELS");
                }

                // Complete ELS-E
                createTableProcQuery.AppendLine("E");
                createTableProcQuery.AppendLine("\t\tRAISE EXCEPTION 'Column [%].[%] not found', tblName, colName;");
                createTableProcQuery.AppendLine("\tEND IF;");

                createTableProcQuery.Append("ELS");
            }

            // Complete ELS-E
            createTableProcQuery.AppendLine("E");
            createTableProcQuery.AppendLine("\tRAISE EXCEPTION 'Table [%] not found', tblName;");
            createTableProcQuery.AppendLine("END IF;");
            createTableProcQuery.AppendLine("RETURN result;");
            createTableProcQuery.AppendLine("END;");
            createTableProcQuery.AppendLine("$BODY$ LANGUAGE 'plpgsql' VOLATILE;");
            ExecuteNonQuery(createTableProcQuery.ToString());
        }

        public override IDataReader ReadTableData(TableRef tbl, IEnumerable<string> colNames)
        {
            throw new NotImplementedException();
        }

        public override IDataReader ReadTableData(string sql)
        {
            throw new NotImplementedException();
        }

        public override IDataReader ReadJoin(TableRef tbl, IEnumerable<ProjectionColumn> colNames, IEnumerable<Join> joins)
        {
            throw new NotImplementedException();
        }

        public override void WriteTableData(TableRef destTbl, IDataReader source, IEnumerable<string> colNames)
        {
            throw new NotImplementedException();
        }

        public override void WriteTableData(TableRef destTbl, IEnumerable<string> colNames, System.Collections.IEnumerable values)
        {
            throw new NotImplementedException();
        }

        public override void RefreshDbStats()
        {
            Log.Info("Vacuuming database");
            ExecuteNonQuery("VACUUM ANALYZE");
        }

        public override void WipeDatabase()
        {
            ExecuteSqlResource(this.GetType(), "Kistl.Server.SchemaManagement.NpgsqlProvider.Scripts.ResetSchema.sql");
        }
    }
}
