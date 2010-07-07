
namespace Kistl.Server.SchemaManagement.NpgsqlProvider
{
    using System;
    using System.Collections.Generic;
    using Npgsql;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.Server;
    using Kistl.API.Utils;

    public class Postgresql
        : ISchemaProvider
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server.Schema.Npgsql");
        private readonly static log4net.ILog QueryLog = log4net.LogManager.GetLogger("Kistl.Server.Schema.Npgsql.Queries");

        protected NpgsqlConnection db;
        protected NpgsqlTransaction tx;

        public Postgresql()
        {
            NpgsqlEventLog.EchoMessages = true;
            NpgsqlEventLog.Level = LogLevel.Normal;
        }

        public string ConfigName { get { return "POSTGRESQL"; } }
        public string AdoNetProvider { get { return "Npgsql"; } }
        public string ManifestToken { get { return "8.1.3"; } }

        public void Open(string connectionString)
        {
            if (db != null) throw new InvalidOperationException("Database already opened");
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException("connectionString");

            db = new NpgsqlConnection(connectionString);
            db.Open();
        }

        public void BeginTransaction()
        {
            if (tx != null) throw new InvalidOperationException("Transaction is already running");
            QueryLog.Debug("BEGIN WORK");
            tx = db.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (tx != null)
            {
                QueryLog.Debug("COMMIT");
                tx.Commit();
                tx = null;
            }
        }

        public void RollbackTransaction()
        {
            if (tx != null)
            {
                QueryLog.Debug("ROLLBACK");
                tx.Rollback();
                tx = null;
            }
        }

        public void Dispose()
        {
            if (tx != null)
            {
                QueryLog.Debug("ROLLBACK (dispose)");
                tx.Rollback();
                tx.Dispose();
                tx = null;
            }

            if (db != null)
            {
                QueryLog.Debug("CLOSE DB");
                db.Close();
                db.Dispose();
                db = null;
            }
        }

        public string GetSavedSchema()
        {
            if (!CheckTableExists("CurrentSchema")) return string.Empty;

            var count = CheckVersionCount();
            if (count == 0)
            {
                return String.Empty;
            }

            using (var versionCmd = new NpgsqlCommand(@"SELECT ""Schema"" FROM ""CurrentSchema""", db, tx))
            {
                QueryLog.Debug(versionCmd.CommandText);
                return (string)versionCmd.ExecuteScalar();
            }
        }

        private long CheckVersionCount()
        {
            using (var schemaCountCmd = new NpgsqlCommand(@"SELECT count(*) FROM ""CurrentSchema""", db, tx))
            {
                QueryLog.Debug(schemaCountCmd.CommandText);
                var count = (long)schemaCountCmd.ExecuteScalar();
                if (count > 1)
                {
                    throw new InvalidOperationException("There is more then one Schema saved in your Database");
                }
                return count;
            }
        }

        /// <summary>
        /// TODO: Was ist die Version?
        /// </summary>
        /// <param name="schema"></param>
        public void SaveSchema(string schema)
        {
            if (!CheckTableExists("CurrentSchema")) throw new InvalidOperationException("Unable to save Schema. Schematable does not exist.");

            using (Log.DebugTraceMethodCall("Saving schema"))
            {
                long count = CheckVersionCount();
                string commandString = count == 0
                    ? "INSERT INTO \"CurrentSchema\" (\"Version\", \"Schema\") VALUES(1, @schema)"
                    : "UPDATE \"CurrentSchema\" SET \"Schema\" = @schema, \"Version\" = \"Version\" + 1";

                using (var cmd = new NpgsqlCommand(commandString, db, tx))
                {
                    cmd.Parameters.AddWithValue("@schema", schema);
                    QueryLog.Debug(cmd.CommandText);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool CheckTableExists(string tblName)
        {
            using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM pg_tables WHERE schemaname='dbo' AND tablename=@table", db, tx))
            {
                cmd.Parameters.AddWithValue("@table", tblName);
                QueryLog.Debug(cmd.CommandText);
                return (long)cmd.ExecuteScalar() > 0;
            }
        }

        public bool CheckColumnExists(string tblName, string colName)
        {
            using (var cmd = new NpgsqlCommand(@"
SELECT COUNT(*)
FROM pg_attribute a
    JOIN pg_class c ON c.oid = a.attrelid
    LEFT JOIN pg_namespace n ON n.oid = c.relnamespace 
WHERE n.nspname = 'dbo' AND c.relname = @table AND a.attname=@column", db, tx))
            {
                cmd.Parameters.AddWithValue("@table", tblName);
                cmd.Parameters.AddWithValue("@column", colName);
                QueryLog.Debug(cmd.CommandText);
                return (long)cmd.ExecuteScalar() > 0;
            }
        }

        public bool GetIsColumnNullable(string tblName, string colName)
        {
            using (var cmd = new NpgsqlCommand(@"SELECT NOT a.attnotnull
                                                    FROM pg_class c
                                                    LEFT JOIN pg_namespace n ON n.oid = c.relnamespace
                                                    LEFT JOIN pg_attribute a ON c.oid = a.attrelid
                                                    WHERE n.nspname = 'dbo' AND c.relname=@table AND c.relkind = 'r'
                                                        AND a.attnum >= 1 AND a.attname=@column", db, tx))
            {
                cmd.Parameters.AddWithValue("@table", tblName);
                cmd.Parameters.AddWithValue("@column", colName);
                QueryLog.Debug(cmd.CommandText);
                return (bool)cmd.ExecuteScalar();
            }
        }

        public bool CheckFKConstraintExists(string fkName)
        {
            using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(@fk_constraint) AND type IN (N'F')", db, tx))
            {
                cmd.Parameters.AddWithValue("@fk_constraint", fkName);
                QueryLog.Debug(cmd.CommandText);
                return (long)cmd.ExecuteScalar() > 0;
            }
        }

        public bool CheckViewExists(string viewName)
        {
            using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(@view) AND type IN (N'V')", db, tx))
            {
                cmd.Parameters.AddWithValue("@view", viewName);
                QueryLog.Debug(cmd.CommandText);
                return (long)cmd.ExecuteScalar() > 0;
            }
        }

        public bool CheckTriggerExists(string objName, string triggerName)
        {
            using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(@trigger) AND parent_object_id = OBJECT_ID(@parent) AND type IN (N'TR')", db, tx))
            {
                cmd.Parameters.AddWithValue("@trigger", triggerName);
                cmd.Parameters.AddWithValue("@parent", objName);
                QueryLog.Debug(cmd.CommandText);
                return (long)cmd.ExecuteScalar() > 0;
            }
        }

        public bool CheckProcedureExists(string procName)
        {
            using (var cmd = new NpgsqlCommand(@"SELECT count(*)
	                                                FROM pg_proc p
	                                                LEFT JOIN pg_namespace n ON p.pronamespace = n.oid
	                                                WHERE n.nspname = 'dbo' AND p.proname = @proc", db, tx))
            {
                cmd.Parameters.AddWithValue("@proc", procName);
                QueryLog.Debug(cmd.CommandText);
                return (long)cmd.ExecuteScalar() > 0;
            }
        }

        public bool CheckTableContainsData(string tblName)
        {
            using (var cmd = new NpgsqlCommand(string.Format("SELECT COUNT(*) FROM \"{0}\"", tblName), db, tx))
            {
                QueryLog.Debug(cmd.CommandText);
                return (long)cmd.ExecuteScalar() > 0;
            }
        }

        public bool CheckColumnContainsNulls(string tblName, string colName)
        {
            using (var cmd = new NpgsqlCommand(string.Format("SELECT COUNT(*) FROM (SELECT TOP 1 \"{1}\" FROM \"{0}\" WHERE \"{1}\" IS NULL) AS nulls", tblName, colName), db, tx))
            {
                QueryLog.Debug(cmd.CommandText);
                return (long)cmd.ExecuteScalar() > 0;
            }
        }

        public bool CheckColumnContainsUniqueValues(string tblName, string colName)
        {
            using (var cmd = new NpgsqlCommand(string.Format("SELECT COUNT(*) FROM (SELECT TOP 1 \"{1}\" FROM \"{0}\" WHERE \"{1}\" IS NOT NULL GROUP BY \"{1}\" HAVING COUNT(\"{1}\") > 1) AS tbl", tblName, colName), db, tx))
            {
                QueryLog.Debug(cmd.CommandText);
                return (long)cmd.ExecuteScalar() == 0;
            }
        }

        public bool CheckColumnContainsValues(string tblName, string colName)
        {
            using (var cmd = new NpgsqlCommand(string.Format("SELECT COUNT(*) FROM (SELECT TOP 1 \"{1}\" FROM \"{0}\" WHERE \"{1}\" IS NOT NULL) AS nulls", tblName, colName), db, tx))
            {
                QueryLog.Debug(cmd.CommandText);
                return (long)cmd.ExecuteScalar() > 0;
            }
        }

        public bool CheckPositionColumnValidity(string tblName, string posName)
        {
            var failed = CheckColumnContainsNulls(tblName, posName);
            if (failed)
            {
                Log.WarnFormat("Order Column \"{0}\".\"{1}\" contains NULLs.", tblName, posName);
                return false;
            }

            return CallRepairPositionColumn(false, tblName, posName);
        }

        public bool RepairPositionColumn(string tblName, string posName)
        {
            return CallRepairPositionColumn(true, tblName, posName);
        }

        private bool CallRepairPositionColumn(bool repair, string tblName, string indexName)
        {
            using (var cmd = new NpgsqlCommand("RepairPositionColumnValidityByTable", db, tx))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@repair", repair);
                cmd.Parameters.AddWithValue("@tblName", tblName);
                cmd.Parameters.AddWithValue("@colName", indexName);
                //cmd.Parameters.Add("@result", System.Data.SqlDbType.Bit).Direction = System.Data.ParameterDirection.Output;

                QueryLog.Debug(cmd.CommandText);
                cmd.ExecuteNonQuery();

                return (bool)cmd.Parameters["@result"].Value;
            }
        }

        public int GetColumnMaxLength(string tblName, string colName)
        {
            // / 2 -> nvarchar!
            using (var cmd = new NpgsqlCommand(@"SELECT c.max_length / 2 FROM sys.objects o INNER JOIN sys.columns c ON c.object_id=o.object_id
	                                            WHERE o.object_id = OBJECT_ID(@table) 
		                                            AND o.type IN (N'U')
		                                            AND c.Name = @column", db, tx))
            {
                cmd.Parameters.AddWithValue("@table", tblName);
                cmd.Parameters.AddWithValue("@column", colName);
                QueryLog.Debug(cmd.CommandText);
                return (int)cmd.ExecuteScalar();
            }
        }

        public IEnumerable<string> GetTableNames()
        {
            string sqlQuery = "SELECT name FROM sys.objects WHERE type IN (N'U') AND name <> 'sysdiagrams'";
            QueryLog.Debug(sqlQuery);

            using (var cmd = new NpgsqlCommand(sqlQuery, db, tx))
            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read()) yield return rd.GetString(0);
            }
        }

        public IEnumerable<TableConstraintNamePair> GetFKConstraintNames()
        {
            string sqlQuery = "SELECT c.name, t.name FROM sys.objects c inner join sys.sysobjects t  on t.id = c.parent_object_id WHERE c.type IN (N'F') order by c.name";
            QueryLog.Debug(sqlQuery);

            using (var cmd = new NpgsqlCommand(sqlQuery, db, tx))
            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read()) yield return new TableConstraintNamePair() { ConstraintName = rd.GetString(0), TableName = rd.GetString(1) };
            }
        }

        public IEnumerable<Column> GetTableColumns(string tbl)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetTableColumnNames(string tblName)
        {
            using (var cmd = new NpgsqlCommand(@"SELECT c.name
                                FROM sys.objects o 
                                    INNER JOIN sys.columns c ON c.object_id=o.object_id
	                            WHERE o.object_id = OBJECT_ID(@table) 
		                            AND o.type IN (N'U')", db, tx))
            {
                cmd.Parameters.AddWithValue("@table", tblName);
                QueryLog.Debug(cmd.CommandText);
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read()) yield return rd.GetString(0);
                }
            }
        }

        public void CreateTable(string tblName, IEnumerable<Column> cols)
        {
            Log.DebugFormat("CreateTable \"{0}\"", tblName);
            throw new NotImplementedException();
        }

        public void CreateTable(string tblName, bool idAsIdentityColumn)
        {
            CreateTable(tblName, idAsIdentityColumn, true);
        }

        public void CreateTable(string tblName, bool idAsIdentityColumn, bool createPrimaryKey)
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

        public void CreateColumn(string tblName, string colName, System.Data.DbType type, int size, bool isNullable)
        {
            DoColumn(true, tblName, colName, type, size, isNullable);
        }

        public void AlterColumn(string tblName, string colName, System.Data.DbType type, int size, bool isNullable)
        {
            DoColumn(false, tblName, colName, type, size, isNullable);
        }

        private void DoColumn(bool add, string tblName, string colName, System.Data.DbType type, int size, bool isNullable)
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
                string typeString = DbTypeToNative(type) + (size > 0 ? string.Format("({0})", size) : String.Empty);
                Log.DebugFormat("\"{0}\" table \"{1}\" column \"{2}\" \"{3}\" \"{4}\"", addOrAlter, tblName, colName, typeString, nullable);
                sb.AppendFormat("ALTER TABLE \"{0}\" {1}  \"{2}\" {3} {4}", tblName, addOrAlter, colName,
                    typeString,
                    nullable);
            }

            ExecuteNonQuery(sb.ToString());
        }

        public void CreateFKConstraint(string tblName, string refTblName, string colName, string constraintName, bool onDeleteCascade)
        {
            Log.DebugFormat("Creating foreign key constraint \"{0}\".\"{1}\" -> \"{2}\".ID", tblName, colName, refTblName);
            ExecuteNonQuery(@"ALTER TABLE ""{0}""
                    ADD CONSTRAINT ""{1}"" FOREIGN KEY(""{2}"")
                    REFERENCES ""{3}"" (""ID""){4}",
                   tblName,
                   constraintName,
                   colName,
                   refTblName,
                   onDeleteCascade ? @" ON DELETE CASCADE" : String.Empty);
        }

        public void DropTable(string tblName)
        {
            Log.DebugFormat("Dropping table \"{0}\"", tblName);
            ExecuteNonQuery("DROP TABLE \"{0}\"", tblName);
        }

        private void ExecuteNonQuery(string nonQueryFormat, params object[] args)
        {
            string query = String.Format(nonQueryFormat, args);

            using (var cmd = new NpgsqlCommand(query, db, tx))
            {
                QueryLog.Debug(query);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    QueryLog.Error(String.Format("Error executing <<[{0}]>>", query), ex);
                    throw;
                }
            }
        }

        public void DropColumn(string tblName, string colName)
        {
            Log.DebugFormat("Dropping column \"{0}\".\"{1}\"", tblName, colName);
            ExecuteNonQuery("ALTER TABLE \"{0}\" DROP COLUMN \"{1}\"", tblName, colName);
        }

        public void DropFKConstraint(string tblName, string fkName)
        {
            Log.DebugFormat("Dropping foreign key constraint \"{0}\".\"{1}\"", tblName, fkName);
            ExecuteNonQuery("ALTER TABLE \"{0}\" DROP CONSTRAINT \"{1}\"", tblName, fkName);
        }

        public void DropTrigger(string triggerName)
        {
            Log.DebugFormat("Dropping trigger \"{0}\"", triggerName);
            ExecuteNonQuery("DROP TRIGGER \"{0}\"", triggerName);
        }

        public void DropView(string viewName)
        {
            Log.DebugFormat("Dropping view \"{0}\"", viewName);
            ExecuteNonQuery("DROP VIEW \"{0}\"", viewName);
        }

        private IEnumerable<string[]> GetParameterTypes(string procName)
        {
            string sqlQuery = @"SELECT args.proc_oid, t.typname 
                        FROM pg_type t 
                            JOIN (
                                SELECT oid proc_oid, proargtypes::oid[] argtypes, generate_subscripts(proargtypes::oid[], 1) argtype_subscript 
                                FROM pg_proc where proname = '" + procName + @"') args 
                            ON t.oid = args.argtypes[args.argtype_subscript] 
                        ORDER BY args.proc_oid, args.argtype_subscript;";
            QueryLog.Debug(sqlQuery);

            using (var cmd = new NpgsqlCommand(sqlQuery, db, tx))
            using (var rd = cmd.ExecuteReader())
            {
                long? lastProcOid = null;
                List<string> types = null;
                List<string[]> result = new List<string[]>();
                while (rd.Read())
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
        }

        /// <summary>
        /// Drops all functions with the specified name.
        /// </summary>
        public void DropProcedure(string procName)
        {
            Log.DebugFormat("Dropping procedure \"{0}\"", procName);
            foreach (var argTypes in GetParameterTypes(procName))
            {
                ExecuteNonQuery("DROP FUNCTION \"{0}\"({1})", procName, String.Join(",", argTypes));
            }
        }

        public void CopyColumnData(string srcTblName, string srcColName, string tblName, string colName)
        {
            Log.DebugFormat("Copying data from \"{0}\".\"{1}\" to \"{2}\".\"{3}\"", srcTblName, srcColName, tblName, colName);
            ExecuteNonQuery("UPDATE dest SET dest.\"{0}\" = src.\"{1}\" FROM \"{2}\" dest INNER JOIN \"{3}\" src ON dest.ID = src.ID",
                colName, srcColName, tblName, srcTblName);
        }

        public void MigrateFKs(string srcTblName, string srcColName, string tblName, string colName)
        {
            Log.DebugFormat("Migrating FK data from \"{0}\".\"{1}\" to \"{2}\".\"{3}\"", srcTblName, srcColName, tblName, colName);
            ExecuteNonQuery("UPDATE dest SET dest.\"{0}\" = src.\"ID\" FROM \"{2}\" dest INNER JOIN \"{3}\" src ON dest.ID = src.\"{1}\"",
                colName, srcColName, tblName, srcTblName);
        }

        public void InsertFKs(string srcTblName, string srcColName, string tblName, string colName, string fkColName)
        {
            Log.DebugFormat("Inserting FK data from \"{0}\"(\"{1}\") to \"{2}\"(\"{3}\",\"{4}\")", srcTblName, srcColName, tblName, colName, fkColName);
            ExecuteNonQuery("INSERT INTO \"{0}\" (\"{1}\", \"{2}\") SELECT \"ID\", \"{3}\" FROM \"{4}\" WHERE \"{3}\" IS NOT NULL",
                tblName, colName, fkColName, srcColName, srcTblName);
        }

        public void CopyFKs(string srcTblName, string srcColName, string destTblName, string destColName, string srcFKColName)
        {
            Log.DebugFormat("Copy FK data from \"{0}\"(\"{1}\") to \"{2}\"(\"{3}\")", srcTblName, srcColName, destTblName, destColName);
            ExecuteNonQuery("UPDATE dest SET dest.\"{0}\" = src.\"{1}\" FROM \"{2}\" dest  INNER JOIN \"{3}\" src ON src.\"{4}\" = dest.\"ID\"",
                destColName, srcColName, destTblName, srcTblName, srcFKColName);
        }

        public bool CheckIndexExists(string tblName, string idxName)
        {
            using (var cmd = new NpgsqlCommand("SELECT COUNT(*) from sys.sysindexes WHERE id = OBJECT_ID(@tbl) AND \"name\" = @idx", db, tx))
            {
                cmd.Parameters.AddWithValue("@tbl", tblName);
                cmd.Parameters.AddWithValue("@idx", idxName);
                QueryLog.Debug(cmd.CommandText);
                return (long)cmd.ExecuteScalar() > 0;
            }
        }

        public void DropIndex(string tblName, string idxName)
        {
            ExecuteNonQuery("DROP INDEX {0} ON \"{1}\"", idxName, tblName);
        }

        public void DropAllObjects()
        {
            throw new NotImplementedException();
        }

        public void CreateIndex(string tblName, string idxName, bool unique, bool clustered, params string[] columns)
        {
            string colSpec = string.Join(", ", columns.Select(c => "\"" + c + "\"").ToArray());

            Log.DebugFormat("Creating index \"{0}\".\"{1}\" ({2})", tblName, idxName, colSpec);

            ExecuteNonQuery("CREATE {0}INDEX \"{1}\" ON \"{2}\" ({3})",
                unique ? "UNIQUE " : string.Empty,
                idxName,
                tblName,
                colSpec);

            if (clustered)
            {
                ExecuteNonQuery("CLUSTER \"{0}\" USING \"{1}\"",
                    tblName,
                    idxName);
            }
        }

        public void CreateUpdateRightsTrigger(string triggerName, string tblName, List<RightsTrigger> tblList)
        {
            if (tblList == null) throw new ArgumentNullException("tblList");

            Log.DebugFormat("Creating trigger to update rights \"{0}\"", triggerName);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"CREATE TRIGGER ""{0}""
ON ""{1}""
AFTER UPDATE, INSERT, DELETE AS
BEGIN", triggerName, tblName);
            sb.AppendLine();

            foreach (var tbl in tblList)
            {
                StringBuilder select = new StringBuilder();
                if (tbl.Relations.Count == 0)
                {
                    sb.AppendFormat(@"    DELETE FROM ""{0}"" WHERE ""ID"" IN (SELECT ""ID"" FROM inserted)
    DELETE FROM ""{0}"" WHERE ""ID"" IN (SELECT ""ID"" FROM deleted)
    INSERT INTO ""{0}"" (""ID"", ""Identity"", ""Right"") SELECT ""ID"", ""Identity"", ""Right"" FROM ""{1}"" WHERE ""ID"" IN (SELECT ""ID"" FROM inserted)",
                        tbl.TblNameRights, tbl.ViewUnmaterializedName);
                    sb.AppendLine();
                    sb.AppendLine();
                }
                else
                {
                    select.AppendFormat("SELECT t1.\"ID\" FROM \"{0}\" t1", tbl.TblName);
                    int idx = 2;
                    var lastRel = tbl.Relations.Last();
                    foreach (var rel in tbl.Relations)
                    {
                        var joinTbl = rel == lastRel ? "{0}" : rel.JoinTableName;
                        select.AppendLine();
                        select.AppendFormat(@"      INNER JOIN ""{0}"" t{1} ON t{1}.""{2}"" = t{3}.""{4}""", joinTbl, idx, rel.JoinColumnName, idx - 1, rel.FKColumnName);
                        idx++;
                    }
                    string selectFormat = select.ToString();
                    sb.AppendFormat(@"    DELETE FROM ""{0}"" WHERE ""ID"" IN ({1})", tbl.TblNameRights, string.Format(selectFormat, "inserted"));
                    sb.AppendLine();
                    sb.AppendFormat(@"    DELETE FROM ""{0}"" WHERE ""ID"" IN ({1})", tbl.TblNameRights, string.Format(selectFormat, "deleted"));
                    sb.AppendLine();
                    sb.AppendFormat(@"    INSERT INTO ""{0}"" (""ID"", ""Identity"", ""Right"") SELECT ""ID"", ""Identity"", ""Right"" FROM ""{2}"" WHERE ""ID"" IN ({1})",
                        tbl.TblNameRights, string.Format(selectFormat, "inserted"), tbl.ViewUnmaterializedName);
                    sb.AppendLine();
                    sb.AppendLine();
                }
            }

            sb.AppendLine("END");
            ExecuteNonQuery(sb.ToString());

            //            ExecuteNonQuery(@"CREATE TRIGGER \"{0}\"
            //ON \"{1}\"
            //AFTER UPDATE, INSERT, DELETE AS
            //BEGIN
            //    DELETE FROM \"{2}\" WHERE \"ID\" IN (SELECT \"ID\" FROM inserted)
            //    DELETE FROM \"{2}\" WHERE \"ID\" IN (SELECT \"ID\" FROM deleted)
            //    INSERT INTO \"{2}\" (\"ID\", \"Identity\", \"Right\") SELECT \"ID\", \"Identity\", \"Right\" FROM \"{3}\" WHERE \"ID\" IN (SELECT \"ID\" FROM inserted)
            //END",
            //                triggerName,
            //                tblName,
            //                tblNameRights,
            //                viewUnmaterializedName);
        }

        public void CreateEmptyRightsViewUnmaterialized(string viewName)
        {
            Log.DebugFormat("Creating *empty* unmaterialized rights view \"{0}\"", viewName);
            ExecuteNonQuery(@"SELECT 0 ""ID"", 0 ""Identity"", 0 ""Right"" WHERE 0 = 1");
        }

        public void CreateRightsViewUnmaterialized(string viewName, string tblName, string tblNameRights, IList<ACL> acls)
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

        public void CreateRefreshRightsOnProcedure(string procName, string viewUnmaterializedName, string tblName, string tblNameRights)
        {
            Log.DebugFormat("Creating refresh rights procedure for \"{0}\"", tblName);
            ExecuteNonQuery(@"CREATE FUNCTION ""{0}""(IN ""refreshID"" integer DEFAULT NULL) RETURNS void AS
                    $BODY$BEGIN
	                    IF (""refreshID"" IS NULL) THEN
			                    TRUNCATE TABLE ""{1}"";
			                    INSERT INTO ""{1}"" (""ID"", ""Identity"", ""Right"") SELECT ""ID"", ""Identity"", ""Right"" FROM ""{2}"";
	                    ELSE
			                    DELETE FROM ""{1}"" WHERE ""ID"" = ""refreshID"";
			                    INSERT INTO ""{1}"" (""ID"", ""Identity"", ""Right"") SELECT ""ID"", ""Identity"", ""Right"" FROM ""{2}"" WHERE ""ID"" = ""refreshID"";
                        END IF;
                    END$BODY$
                    LANGUAGE 'plpgsql' VOLATILE",
                procName,
                tblNameRights,
                viewUnmaterializedName);
        }

        public void ExecRefreshRightsOnProcedure(string procName)
        {
            Log.DebugFormat("Refreshing rights for \"{0}\"", procName);
            ExecuteNonQuery(@"SELECT ""{0}""()", procName);
        }

        public void CreatePositionColumnValidCheckProcedures(ILookup<string, KeyValuePair<string, string>> refSpecs)
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

            ExecuteScriptFromResource(String.Format(@"Kistl.Server.SchemaManagement.NpgsqlProvider.Scripts.{0}.sql", procName));

            var createTableProcQuery = new StringBuilder();
            createTableProcQuery.AppendFormat("CREATE FUNCTION \"{0}\" (repair boolean, tblName text, colName text) RETURNS boolean AS $BODY$", tableProcName);
            createTableProcQuery.AppendLine();
            createTableProcQuery.AppendLine("DECLARE result boolean DEFAULT true;");
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
                        // TODO: use named parameters with 9.0: "\t\tresult := RepairPositionColumnValidity(repair := repair, tblName := '{0}', refTblName := '{1}', fkColumnName := '{2}', fkPositionName := '{2}{3}');",
                        "\t\tresult := RepairPositionColumnValidity(repair, '{0}', '{1}', '{2}', '{2}{3}');",
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
            createTableProcQuery.AppendLine("END;");
            createTableProcQuery.AppendLine("$BODY$ LANGUAGE 'plpgsql' VOLATILE;");
            ExecuteNonQuery(createTableProcQuery.ToString());
        }

        private void ExecuteScriptFromResource(string scriptResourceName)
        {
            using (var scriptStream = new StreamReader(this.GetType().Assembly.GetManifestResourceStream(scriptResourceName)))
            {
                var databaseScript = scriptStream.ReadToEnd();
                foreach (var cmdString in Regex.Split(databaseScript, "\r?\nGO\r?\n").Where(s => !String.IsNullOrEmpty(s)))
                {
                    ExecuteNonQuery(cmdString);
                }
            }
        }

        public void RenameTable(string oldTblName, string newTblName)
        {
            // Do not qualify new name as it will be part of the name
            ExecuteNonQuery("EXEC sp_rename '\"{0}\"', '{1}'", oldTblName, newTblName);
        }

        public void RenameColumn(string tblName, string oldColName, string newColName)
        {
            // Do not qualify new name as it will be part of the name
            ExecuteNonQuery("EXEC sp_rename '\"{0}\".\"{1}\"', '{2}', 'COLUMN'", tblName, oldColName, newColName);
        }

        public void RenameFKConstraint(string oldConstraintName, string newConstraintName)
        {
            // Do not qualify new name as it will be part of the name
            ExecuteNonQuery("EXEC sp_rename '\"{0}\"', '{1}', 'OBJECT'", oldConstraintName, newConstraintName);
        }

        public System.Data.IDataReader ReadTableData(string tbl, IEnumerable<string> colNames)
        {
            throw new NotImplementedException();
        }

        public void WriteTableData(string tbl, IEnumerable<string> colNames, object[] values)
        {
            throw new NotImplementedException();
        }

        public string DbTypeToNative(System.Data.DbType type)
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

        public System.Data.DbType NativeToDbType(string type)
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
    }
}
