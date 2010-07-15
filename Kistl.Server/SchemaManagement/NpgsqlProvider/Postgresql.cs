
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

        #endregion

        #region ZBox Schema Handling

        protected override string GetSchemaInsertStatement()
        {
            return "INSERT INTO \"CurrentSchema\" (\"Version\", \"Schema\") VALUES (1, @schema)";
        }

        protected override string GetSchemaUpdateStatement()
        {
            return "UPDATE \"CurrentSchema\" SET \"Schema\" = @schema, \"Version\" = \"Version\" + 1";
        }

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
                case System.Data.DbType.Byte:
                case System.Data.DbType.Int16:
                    return "int2";
                case System.Data.DbType.UInt16:
                case System.Data.DbType.Int32:
                    return "int4";
                case System.Data.DbType.Single:
                    return "float4";
                case System.Data.DbType.Double:
                    return "float8";
                case System.Data.DbType.String:
                    return "varchar";
                case System.Data.DbType.Date:
                    return "date";
                case System.Data.DbType.DateTime:
                case System.Data.DbType.DateTime2:
                    return "timestamp";
                case System.Data.DbType.Boolean:
                    return "bool";
                case System.Data.DbType.Guid:
                    return "uuid";
                case System.Data.DbType.Binary:
                    return "bytea";
                case System.Data.DbType.Decimal:
                    return "numeric";
                case System.Data.DbType.UInt32: // no int8 supported by Npgsql 2.0.9
                case System.Data.DbType.Int64:
                case System.Data.DbType.UInt64:
                default:
                    throw new ArgumentOutOfRangeException("type", string.Format("Unable to convert type '{0}' to an sql type string", type));
            }
        }

        public override DbType NativeToDbType(string type)
        {
            switch (type)
            {
                case "int2":
                    return System.Data.DbType.Int16;
                case "int4":
                    return System.Data.DbType.Int32;
                case "float4":
                    return System.Data.DbType.Single;
                case "float8":
                    return System.Data.DbType.Double;
                case "varchar":
                case "text":
                    return System.Data.DbType.String;
                case "date":
                    return System.Data.DbType.Date;
                case "timestamp":
                    return System.Data.DbType.DateTime;
                case "bool":
                    return System.Data.DbType.Boolean;
                case "uuid":
                    return System.Data.DbType.Guid;
                case "bytea":
                    return System.Data.DbType.Binary;
                case "numeric":
                    return System.Data.DbType.Decimal;
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
            throw new NotImplementedException();
            // return "SELECT COUNT(*) > 0 FROM pg_tables WHERE schemaname=@schema AND tablename=@table";
        }

        protected override string GetTableNamesStatement()
        {
            return "SELECT schemaname, tablename FROM pg_tables";
        }

        public override bool CheckColumnExists(TableRef tblName, string colName)
        {
            throw new NotImplementedException();
//            return @"
//                SELECT COUNT(*) > 0
//                FROM pg_attribute a
//                    JOIN pg_class c ON c.oid = a.attrelid
//                    LEFT JOIN pg_namespace n ON n.oid = c.relnamespace 
//                WHERE n.nspname = 'dbo' AND c.relname = @table AND a.attname=@name";
        }

        protected override string GetTableColumnsStatement()
        {
            throw new NotImplementedException();
        }

        protected override string GetTableColumnNamesStatement()
        {
            return @"SELECT attname
                        FROM pg_attribute
                            JOIN pg_class ON (attrelid = pg_class.oid)
                            JOIN pg_namespace ON (relnamespace = pg_namespace.oid)
                        WHERE nspname = @schema AND relname = @table and relkind = 'r' AND attnum >= 0";
        }

        public override bool CheckFKConstraintExists(string fkName)
        {
            throw new NotImplementedException();
            // return "SELECT COUNT(*) > 0 FROM pg_constraint WHERE conname = @constraint_name AND contype = 'f'";
        }

        public override bool CheckIndexExists(TableRef tblName, string idxName)
        {
            throw new NotImplementedException();
//            return @"
//                SELECT COUNT(*) > 0
//                FROM pg_index
//                    JOIN pg_class idx ON (indexrelid = idx.oid)
//                    JOIN pg_class tbl ON (indrelid = tbl.oid)
//                    JOIN pg_namespace ON (tbl.relnamespace = pg_namespace.oid)
//                WHERE nspname = @schema AND tbl.relname = @table AND idx.relname = @index";
        }

        #endregion

        #region Table Content

        public override bool CheckTableContainsData(TableRef tbl)
        {
            return (bool)ExecuteScalar(String.Format(
                "SELECT COUNT(*) > 0 FROM (SELECT * FROM {0} LIMIT 1) as data",
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

        #endregion

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
        // ---------------------------------------------------------------------------


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

        public override bool CheckViewExists(TableRef viewName)
        {
            using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM pg_views WHERE schemaname = @schema AND viewname = @view", CurrentConnection, CurrentTransaction))
            {
                cmd.Parameters.AddWithValue("@schema", viewName.Schema);
                cmd.Parameters.AddWithValue("@view", viewName.Name);
                QueryLog.Debug(cmd.CommandText);
                return (long)cmd.ExecuteScalar() > 0;
            }
        }

        public override bool CheckTriggerExists(TableRef objName, string triggerName)
        {
            //// TODO
            //using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(@trigger) AND parent_object_id = OBJECT_ID(@parent) AND type IN (N'TR')", db, tx))
            //{
            //    cmd.Parameters.AddWithValue("@trigger", triggerName);
            //    cmd.Parameters.AddWithValue("@parent", objName);
            //    QueryLog.Debug(cmd.CommandText);
            //    return (long)cmd.ExecuteScalar() > 0;
            //}
            return false;
        }

        public override bool CheckProcedureExists(string procName)
        {
            using (var cmd = new NpgsqlCommand(@"SELECT count(*)
	                                                FROM pg_proc p
	                                                LEFT JOIN pg_namespace n ON p.pronamespace = n.oid
	                                                WHERE n.nspname = 'dbo' AND p.proname = @proc", CurrentConnection, CurrentTransaction))
            {
                cmd.Parameters.AddWithValue("@proc", procName);
                QueryLog.Debug(cmd.CommandText);
                return (long)cmd.ExecuteScalar() > 0;
            }
        }

        public override bool CheckPositionColumnValidity(TableRef tblName, string posName)
        {
            var failed = CheckColumnContainsNulls(tblName, posName);
            if (failed)
            {
                Log.WarnFormat("Order Column \"{0}\".\"{1}\" contains NULLs.", tblName, posName);
                return false;
            }

            return CallRepairPositionColumn(false, tblName, posName);
        }

        public override bool RepairPositionColumn(TableRef tblName, string posName)
        {
            return CallRepairPositionColumn(true, tblName, posName);
        }

        private bool CallRepairPositionColumn(bool repair, TableRef tblName, string indexName)
        {
            using (var cmd = new NpgsqlCommand("SELECT \"RepairPositionColumnValidityByTable\"(@repair, @tblName, @colName)", CurrentConnection, CurrentTransaction))
            {
                cmd.Parameters.AddWithValue("@repair", repair);
                cmd.Parameters.AddWithValue("@tblName", tblName);
                cmd.Parameters.AddWithValue("@colName", indexName);

                QueryLog.Debug(cmd.CommandText);
                return (bool)cmd.ExecuteScalar();
            }
        }

        public override int GetColumnMaxLength(TableRef tblName, string colName)
        {
            using (var cmd = new NpgsqlCommand(@"
                SELECT atttypmod - 4 -- adjust for varchar implementation, storing the length too
                FROM pg_attribute
                    JOIN pg_class ON (attrelid = pg_class.oid)
                WHERE relname = @table AND attname = @column", CurrentConnection, CurrentTransaction))
            {
                cmd.Parameters.AddWithValue("@table", tblName);
                cmd.Parameters.AddWithValue("@column", colName);
                QueryLog.Debug(cmd.CommandText);
                return (int)cmd.ExecuteScalar();
            }
        }

        public override IEnumerable<TableConstraintNamePair> GetFKConstraintNames()
        {
            string sqlQuery = "SELECT conname, relname FROM pg_constraint JOIN pg_class ON (conrelid = pg_class.oid) WHERE contype = 'f' ORDER BY conname";
            QueryLog.Debug(sqlQuery);

            var result = new List<TableConstraintNamePair>();

            using (var cmd = new NpgsqlCommand(sqlQuery, CurrentConnection, CurrentTransaction))
            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read()) result.Add(new TableConstraintNamePair() { ConstraintName = rd.GetString(0), TableName = rd.GetString(1) });
            }
            return result;
        }

        public override void CreateTable(TableRef tblName, IEnumerable<Column> cols)
        {
            Log.DebugFormat("CreateTable \"{0}\"", tblName);
            throw new NotImplementedException();
        }

        public override void CreateTable(TableRef tblName, bool idAsIdentityColumn)
        {
            CreateTable(tblName, idAsIdentityColumn, true);
        }

        public override void CreateTable(TableRef tblName, bool idAsIdentityColumn, bool createPrimaryKey)
        {
            Log.DebugFormat("CreateTable \"{0}\"", tblName);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("CREATE TABLE \"{0}\" ( ", tblName);
            if (idAsIdentityColumn)
            {
                sb.AppendLine("\"ID\" SERIAL NOT NULL");
            }
            else
            {
                sb.AppendLine("\"ID\" integer NOT NULL");
            }

            if (createPrimaryKey)
            {
                sb.AppendFormat(", CONSTRAINT \"PK_{0}\" PRIMARY KEY ( \"ID\" )", tblName);
            }

            sb.AppendLine();
            sb.Append(")");

            ExecuteNonQuery(sb.ToString());
        }

        public override void CreateColumn(TableRef tblName, string colName, System.Data.DbType type, int size, int scale, bool isNullable, DefaultConstraint defConstraint)
        {
            DoColumn(true, tblName, colName, type, size, scale, isNullable, defConstraint);
        }

        public override void AlterColumn(TableRef tblName, string colName, System.Data.DbType type, int size, int scale, bool isNullable, DefaultConstraint defConstraint)
        {
            DoColumn(false, tblName, colName, type, size, scale, isNullable, defConstraint);
        }

        private void DoColumn(bool add, TableRef tblName, string colName, System.Data.DbType type, int size, int scale, bool isNullable, DefaultConstraint defConstraint)
        {
            StringBuilder sb = new StringBuilder();

            string addOrAlter = add ? "ADD" : "ALTER COLUMN";
            string nullable = isNullable ? "NULL" : "NOT NULL";

            if (type == System.Data.DbType.String && size == int.MaxValue)
            {
                // Create text column for unlimited string length
                Log.DebugFormat("\"{0}\" table \"{1}\" column \"{2}\" text \"{3}\"", addOrAlter, tblName, colName, nullable);
                sb.AppendFormat("ALTER TABLE \"{0}\" {1} \"{2}\" {3} {4}", tblName, addOrAlter, colName,
                    "text",
                    nullable);
            }
            else
            {
                string typeString = DbTypeToNative(type);
                if (size > 0 && scale > 0) typeString += string.Format("({0}, {1})", size, scale);
                else if (size > 0 && scale == 0) typeString += string.Format("({0})", size);
                Log.DebugFormat("\"{0}\" table \"{1}\" column \"{2}\" \"{3}\" \"{4}\"", addOrAlter, tblName, colName, typeString, nullable);
                sb.AppendFormat("ALTER TABLE \"{0}\" {1}  \"{2}\" {3} {4}", tblName, addOrAlter, colName,
                    typeString,
                    nullable);
            }

            ExecuteNonQuery(sb.ToString());
        }

        public override void CreateFKConstraint(TableRef tblName, TableRef refTblName, string colName, string constraintName, bool onDeleteCascade)
        {
            Log.DebugFormat("Creating foreign key constraint \"{0}\".\"{1}\" -> \"{2}\".ID", tblName, colName, refTblName);
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

        public override void DropTable(TableRef tblName)
        {
            ExecuteNonQuery(String.Format("DROP TABLE {0}", FormatFullName(tblName)));
        }

        public override void TruncateTable(TableRef tblName)
        {
            ExecuteNonQuery(String.Format("DELETE FROM {0}", FormatFullName(tblName)));
        }

        public override void DropColumn(TableRef tblName, string colName)
        {
            ExecuteNonQuery(String.Format("ALTER TABLE {0} DROP COLUMN {1}",
                FormatFullName(tblName),
                QuoteIdentifier(colName)));
        }

        public override void DropFKConstraint(TableRef tblName, string fkName)
        {
            ExecuteNonQuery(String.Format("ALTER TABLE {0} DROP CONSTRAINT {1}",
                FormatFullName(tblName),
                QuoteIdentifier(fkName)));
        }

        public override void DropTrigger(string triggerName)
        {
            ExecuteNonQuery(String.Format("DROP TRIGGER {0}",
                QuoteIdentifier(triggerName)));
        }

        public override void DropView(TableRef viewName)
        {
            ExecuteNonQuery(String.Format("DROP VIEW {0}",
                FormatFullName(viewName)));
        }

        private IEnumerable<string[]> GetParameterTypes(string procName)
        {
            string sqlQuery = @"SELECT args.proc_oid, t.typname 
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

        /// <summary>
        /// Drops all functions with the specified name.
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

        public override void CopyColumnData(
            TableRef srcTblName, string srcColName,
            TableRef tblName, string colName)
        {
            ExecuteNonQuery(String.Format(
                "UPDATE dest SET dest.{3} = src.{1} FROM {2} dest INNER JOIN {0} src ON dest.{4} = src.{4}",
                FormatFullName(srcTblName), // 0
                QuoteIdentifier(srcColName), // 1
                FormatFullName(tblName),    // 2
                QuoteIdentifier(colName),    // 3
                QuoteIdentifier("ID")));     // 4
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

        public override void DropIndex(TableRef tblName, string idxName)
        {
            ExecuteNonQuery(String.Format(
                "DROP INDEX {0} ON {1}",
                QuoteIdentifier(idxName),
                FormatFullName(tblName)));
        }

        public override void DropAllObjects()
        {
            throw new NotImplementedException();
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
			SELECT {2}, ""Identity"", ""Right"" FROM ""{1}""
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
                                QuoteIdentifier(rel.JoinColumnName),
                                "{0}",
                                QuoteIdentifier(rel.FKColumnName));
                        }
                        else
                        {
                            select.AppendFormat(@"      INNER JOIN {0} t{1} ON (t{1}.{2} = t{3}.{4})",
                                FormatFullName(rel.JoinTableName),
                                idx,
                                QuoteIdentifier(rel.JoinColumnName),
                                idx - 1,
                                QuoteIdentifier(rel.FKColumnName));
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
			SELECT {4}, ""Identity"", ""Right"" FROM ""{3}""
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
            view.AppendFormat(@"CREATE VIEW ""{0}"" AS
SELECT	""ID"", ""Identity"", 
		(case SUM(""Right"" & 1) when 0 then 0 else 1 end) +
		(case SUM(""Right"" & 2) when 0 then 0 else 2 end) +
		(case SUM(""Right"" & 4) when 0 then 0 else 4 end) +
		(case SUM(""Right"" & 8) when 0 then 0 else 8 end) ""Right"" 
FROM (", viewName);
            view.AppendLine();

            foreach (var acl in acls)
            {
                view.AppendFormat(@"  SELECT t1.""ID"" ""ID"", t{0}.""{1}"" ""Identity"", {2} ""Right""",
                    acl.Relations.Count,
                    acl.Relations.Last().FKColumnName,
                    (int)acl.Right);
                view.AppendLine();
                view.AppendFormat(@"  FROM ""{0}"" t1", tblName);
                view.AppendLine();

                int idx = 2;
                foreach (var rel in acl.Relations.Take(acl.Relations.Count - 1))
                {
                    view.AppendFormat(@"  INNER JOIN ""{0}"" t{1} ON t{1}.""{2}"" = t{3}.""{4}""", rel.JoinTableName, idx, rel.JoinColumnName, idx - 1, rel.FKColumnName);
                    view.AppendLine();
                    idx++;
                }
                view.AppendFormat(@"  WHERE t{0}.""{1}"" IS NOT NULL",
                    acl.Relations.Count,
                    acl.Relations.Last().FKColumnName);
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
CREATE FUNCTION {0}(IN refreshID integer DEFAULT NULL) RETURNS void AS
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
            ExecuteNonQuery(String.Format(@"SELECT {0}()", QuoteIdentifier(procName)));
        }

        public override void CreatePositionColumnValidCheckProcedures(ILookup<string, KeyValuePair<string, string>> refSpecs)
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

            ExecuteNonQueryScriptFromResource(String.Format(@"Kistl.Server.SchemaManagement.NpgsqlProvider.Scripts.{0}.sql", procName));

            var createTableProcQuery = new StringBuilder();
            createTableProcQuery.AppendFormat("CREATE FUNCTION \"{0}\" (repair boolean, tblName text, colName text) RETURNS boolean AS $BODY$", tableProcName);
            createTableProcQuery.AppendLine();
            createTableProcQuery.AppendLine("DECLARE result boolean DEFAULT false;");
            createTableProcQuery.AppendLine("BEGIN");
            foreach (var tbl in refSpecs)
            {
                createTableProcQuery.AppendFormat("IF tblName IS NULL OR tblName = '{0}' THEN", tbl.Key);
                createTableProcQuery.AppendLine();
                createTableProcQuery.Append("\t");
                foreach (var refSpec in tbl)
                {
                    createTableProcQuery.AppendFormat("IF colName IS NULL OR colName = '{0}{1}' THEN", refSpec.Value, Kistl.API.Helper.PositionSuffix);
                    createTableProcQuery.AppendLine();
                    createTableProcQuery.AppendFormat(
                        // TODO: use named parameters with 9.0: "\t\tresult := \"RepairPositionColumnValidity\"(repair := repair, tblName := '{0}', refTblName := '{1}', fkColumnName := '{2}', fkPositionName := '{2}{3}');",
                        "\t\tresult := \"RepairPositionColumnValidity\"(repair, '{0}', '{1}', '{2}', '{2}{3}');",
                        tbl.Key,
                        refSpec.Key,
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

        public override void RenameTable(TableRef oldTblName, TableRef newTblName)
        {
            ExecuteNonQuery(String.Format(
                "ALTER TABLE {0} RENAME TO {1}",
                FormatFullName(oldTblName),
                FormatFullName(newTblName)));
        }

        public override void RenameColumn(TableRef tblName, string oldColName, string newColName)
        {
            ExecuteNonQuery(String.Format(
                "ALTER TABLE {0} RENAME COLUMN {1} TO {2}",
                FormatFullName(tblName),
                QuoteIdentifier(oldColName),
                QuoteIdentifier(newColName)));
        }

        public override void RenameFKConstraint(string oldConstraintName, string newConstraintName)
        {
            ExecuteNonQuery(String.Format(
                "ALTER INDEX {0} RENAME TO {1}",
                QuoteIdentifier(oldConstraintName),
                QuoteIdentifier(newConstraintName)));
        }

        public override IDataReader ReadTableData(TableRef tbl, IEnumerable<string> colNames)
        {
            throw new NotImplementedException();
        }

        public override IDataReader ReadJoin(TableRef tbl, IEnumerable<Join> joins)
        {
            throw new NotImplementedException();
        }

        public override void WriteTableData(TableRef destTbl, IDataReader source, IEnumerable<string> colNames)
        {
            throw new NotImplementedException();
        }

        public override void RefreshDbStats()
        {
            Log.Info("Vacuuming database");
            ExecuteNonQuery("VACUUM ANALYZE");
        }

        public override bool GetHasColumnDefaultValue(TableRef tblName, string colName)
        {
            throw new NotImplementedException();
        }
    }
}
