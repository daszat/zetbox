
namespace Kistl.Server.SchemaManagement.SqlProvider
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.Migration;
    using Kistl.API.Server;
    using Kistl.API.Utils;

    public class SqlServer
        : AdoNetSchemaProvider<SqlConnection, SqlTransaction, SqlCommand>
    {
        private readonly static log4net.ILog _log = log4net.LogManager.GetLogger("Kistl.Server.Schema.MSSQL");
        protected override log4net.ILog Log { get { return _log; } }
        private readonly static log4net.ILog _queryLog = log4net.LogManager.GetLogger("Kistl.Server.Schema.MSSQL.Queries");
        protected override log4net.ILog QueryLog { get { return _queryLog; } }

        #region Meta data

        public override string ConfigName { get { return "MSSQL"; } }
        public override string AdoNetProvider { get { return "System.Data.SqlClient"; } }
        public override string ManifestToken { get { return "2008"; } }

        #endregion

        #region ZBox Schema Handling

        protected override string GetSchemaInsertStatement()
        {
            return "INSERT INTO [CurrentSchema] ([Version], [Schema]) VALUES (1, @schema)";
        }

        protected override string GetSchemaUpdateStatement()
        {
            return "UPDATE [CurrentSchema] SET [Schema] = @schema, [Version] = [Version] + 1";
        }

        #endregion

        #region ADO.NET, Connection and Transaction Handling

        protected override SqlConnection CreateConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

        protected override SqlTransaction CreateTransaction()
        {
            return CurrentConnection.BeginTransaction();
        }

        protected override SqlCommand CreateCommand(string query)
        {
            return new SqlCommand(query, CurrentConnection, CurrentTransaction);
        }

        #endregion

        #region Type Mapping

        public override string DbTypeToNative(DbType type)
        {
            switch (type)
            {
                case System.Data.DbType.UInt16:
                case System.Data.DbType.UInt32:
                case System.Data.DbType.Byte:
                case System.Data.DbType.Int16:
                case System.Data.DbType.Int32:
                    return "int";
                case System.Data.DbType.UInt64:
                case System.Data.DbType.Int64:
                    return "bigint";
                case System.Data.DbType.Single:
                case System.Data.DbType.Double:
                    return "float";
                case System.Data.DbType.String:
                    return "nvarchar";
                case System.Data.DbType.Date:
                case System.Data.DbType.DateTime:
                case System.Data.DbType.DateTime2:
                    // We only support SQLServer 2008
                    return "datetime2";
                case System.Data.DbType.Boolean:
                    return "bit";
                case System.Data.DbType.Guid:
                    return "uniqueidentifier";
                case System.Data.DbType.Binary:
                    return "varbinary";
                case System.Data.DbType.Decimal:
                    return "decimal";
                default:
                    throw new ArgumentOutOfRangeException("type", string.Format("Unable to convert type '{0}' to an sql type string", type));
            }
        }

        public override DbType NativeToDbType(string type)
        {
            switch (type)
            {
                case "int":
                    return System.Data.DbType.Int32;
                case "bigint":
                    return System.Data.DbType.Int64;
                case "float":
                    return System.Data.DbType.Double;
                case "nvarchar":
                case "varchar":
                case "text":
                case "ntext":
                    return System.Data.DbType.String;
                case "datetime":
                    return System.Data.DbType.DateTime;
                case "datetime2":
                    return System.Data.DbType.DateTime2;
                case "bit":
                    return System.Data.DbType.Boolean;
                case "uniqueidentifier":
                    return System.Data.DbType.Guid;
                case "binary":
                case "varbinary":
                    return System.Data.DbType.Binary;
                case "decimal":
                    return System.Data.DbType.Decimal;
                default:
                    throw new ArgumentOutOfRangeException("type", string.Format("Unable to convert type '{0}' to a DbType", type));
            }
        }

        #endregion

        #region SQL Infrastructure

        protected override string QuoteIdentifier(string name)
        {
            return "[" + name + "]";
        }

        #endregion

        #region Table Structure

        protected override string FormatTableName(TableRef tbl)
        {
            return String.Format("[{0}].[{1}].[{2}]", tbl.Database, tbl.Schema, tbl.Name);
        }

        protected override string GetTableExistsStatement()
        {
            return "SELECT COUNT(*) > 0 FROM sys.objects WHERE object_id = OBJECT_ID('[' + @schema + '].[' + @table + ']') AND type IN (N'U')";
        }

        protected override string GetTableNamesStatement()
        {
            return "SELECT s.name, o.name FROM sys.objects o JOIN sys.schemas s ON o.schema_id = s.schema_id WHERE o.type = N'U' AND o.name <> 'sysdiagrams'";
        }

        protected override string GetColumnExistsStatment()
        {
            return @"
                SELECT COUNT(*) > 0
                FROM sys.objects o 
                    INNER JOIN sys.columns c ON c.object_id=o.object_id
                WHERE o.object_id = OBJECT_ID('[' + @schema + '].[' + @table + ']') 
                    AND o.type IN (N'U')
                    AND c.Name = @name";
        }

        protected override string GetTableColumnsStatement()
        {
            return @"SELECT c.name, TYPE_NAME(system_type_id) type, max_length, is_nullable
                        FROM sys.objects o 
                            INNER JOIN sys.columns c ON c.object_id=o.object_id
                        WHERE o.object_id = OBJECT_ID('[' + @schema + '].[' + @table + ']') 
                            AND o.type IN (N'U')";
        }

        protected override string GetTableColumnNamesStatement()
        {
            return @"SELECT c.name
                        FROM sys.objects o 
                            INNER JOIN sys.columns c ON c.object_id=o.object_id
                        WHERE o.object_id = OBJECT_ID('[' + @schema + '].[' + @table + ']') 
                            AND o.type IN (N'U')";
        }

        protected override string GetFKConstraintExistsStatement()
        {
            return "SELECT COUNT(*) > 0 FROM sys.objects WHERE object_id = OBJECT_ID(@constraint_name) AND type IN (N'F')";
        }

        protected override string GetIndexExistsStatement()
        {
            return "SELECT COUNT(*) from sys.sysindexes WHERE id = OBJECT_ID('[' + @schema + '].[' + @table + ']') AND [name] = @index";
        }

        #endregion

        #region Table Content

        public override bool CheckTableContainsData(TableRef tbl)
        {
            return (bool)ExecuteScalar(String.Format(
                "SELECT COUNT(*) > 0 FROM (SELECT TOP 1 * FROM {0}) as data",
                FormatTableName(tbl)));
        }

        public override bool CheckColumnContainsNulls(TableRef tbl, string colName)
        {
            return (bool)ExecuteScalar(String.Format(
                "SELECT COUNT(*) > 0 FROM (SELECT TOP 1 {1} FROM {0} WHERE {1} IS NULL) AS nulls",
                FormatTableName(tbl),
                QuoteIdentifier(colName)));
        }

        public override bool CheckColumnContainsUniqueValues(TableRef tbl, string colName)
        {
            return (bool)ExecuteScalar(String.Format(
                @"SELECT COUNT(*) = 0 FROM (
                    SELECT TOP 1 {1} FROM {0} WHERE {1} IS NOT NULL
                    GROUP BY {1} 
                    HAVING COUNT({1}) > 1) AS tbl",
                FormatTableName(tbl),
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

        private static string ConstructDefaultConstraintName(TableRef tblName, string colName)
        {
            var constrName = string.Format("default_{0}_{1}", tblName.Name, colName);
            return constrName;
        }

        public override bool CheckViewExists(TableRef viewName)
        {
            using (var cmd = new SqlCommand("SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(@view) AND type IN (N'V')", CurrentConnection, CurrentTransaction))
            {
                cmd.Parameters.AddWithValue("@view", viewName);
                QueryLog.Debug(cmd.CommandText);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public override bool CheckTriggerExists(TableRef objName, string triggerName)
        {
            using (var cmd = new SqlCommand("SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(@trigger) AND parent_object_id = OBJECT_ID(@parent) AND type IN (N'TR')", CurrentConnection, CurrentTransaction))
            {
                cmd.Parameters.AddWithValue("@trigger", triggerName);
                cmd.Parameters.AddWithValue("@parent", FormatTableName(objName));
                QueryLog.Debug(cmd.CommandText);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public override bool CheckProcedureExists(string procName)
        {
            using (var cmd = new SqlCommand("SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(@proc) AND type IN (N'P')", CurrentConnection, CurrentTransaction))
            {
                cmd.Parameters.AddWithValue("@proc", procName);
                QueryLog.Debug(cmd.CommandText);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public override bool CheckPositionColumnValidity(TableRef tblName, string posName)
        {
            var failed = CheckColumnContainsNulls(tblName, posName);
            if (failed)
            {
                Log.WarnFormat("Order Column [{0}].[{1}] contains NULLs.", tblName, posName);
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
            using (var cmd = new SqlCommand("RepairPositionColumnValidityByTable", CurrentConnection, CurrentTransaction))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@repair", repair);
                cmd.Parameters.AddWithValue("@tblName", tblName.Name);
                cmd.Parameters.AddWithValue("@colName", indexName);
                cmd.Parameters.Add("@result", System.Data.SqlDbType.Bit).Direction = System.Data.ParameterDirection.Output;

                QueryLog.Debug(cmd.CommandText);
                cmd.ExecuteNonQuery();

                return (bool)cmd.Parameters["@result"].Value;
            }
        }

        public override bool GetIsColumnNullable(TableRef tblName, string colName)
        {
            using (var cmd = new SqlCommand(@"SELECT c.is_nullable FROM sys.objects o INNER JOIN sys.columns c ON c.object_id=o.object_id
	                                            WHERE o.object_id = OBJECT_ID(@table) 
		                                            AND o.type IN (N'U')
		                                            AND c.Name = @column", CurrentConnection, CurrentTransaction))
            {
                cmd.Parameters.AddWithValue("@table", tblName);
                cmd.Parameters.AddWithValue("@column", colName);
                QueryLog.Debug(cmd.CommandText);
                return (bool)cmd.ExecuteScalar();
            }
        }

        public override bool GetHasColumnDefaultValue(TableRef tblName, string colName)
        {
            using (var cmd = new SqlCommand("SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(@def) AND type IN (N'D')", CurrentConnection, CurrentTransaction))
            {
                cmd.Parameters.AddWithValue("@def", ConstructDefaultConstraintName(tblName, colName));
                QueryLog.Debug(cmd.CommandText);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public override int GetColumnMaxLength(TableRef tblName, string colName)
        {
            // / 2 -> nvarchar!
            using (var cmd = new SqlCommand(@"SELECT c.max_length / 2 FROM sys.objects o INNER JOIN sys.columns c ON c.object_id=o.object_id
	                                            WHERE o.object_id = OBJECT_ID(@table) 
		                                            AND o.type IN (N'U')
		                                            AND c.Name = @column", CurrentConnection, CurrentTransaction))
            {
                cmd.Parameters.AddWithValue("@table", tblName);
                cmd.Parameters.AddWithValue("@column", colName);
                QueryLog.Debug(cmd.CommandText);
                return (int)cmd.ExecuteScalar();
            }
        }

        public override IEnumerable<TableConstraintNamePair> GetFKConstraintNames()
        {
            return ExecuteReader("SELECT c.name, t.name FROM sys.objects c inner join sys.sysobjects t  on t.id = c.parent_object_id WHERE c.type IN (N'F') order by c.name")
                .Select(rd => new TableConstraintNamePair() { ConstraintName = rd.GetString(0), TableName = rd.GetString(1) });
        }

        public override void CreateTable(TableRef tblName, IEnumerable<Column> cols)
        {
            if (cols == null) throw new ArgumentNullException("cols");
            Log.DebugFormat("CreateTable {0}", tblName);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("CREATE TABLE {0} (", FormatTableName(tblName));
            sb.AppendLine();

            foreach (var col in cols)
            {
                string nullable = col.IsNullable ? "NULL" : "NOT NULL";

                if (col.Type == System.Data.DbType.String && col.Size == int.MaxValue)
                {
                    // Create ntext for unlimited string length
                    sb.AppendFormat("[{0}] ntext {1},", col.Name, nullable);
                }
                else if (col.Type == System.Data.DbType.Binary && col.Size == int.MaxValue)
                {
                    // Create ntext for unlimited string length
                    sb.AppendFormat("[{0}] image {1},", col.Name, nullable);
                }
                else
                {
                    string size = string.Empty;
                    if (col.Size > 0 && col.Type.In(System.Data.DbType.String, System.Data.DbType.StringFixedLength, System.Data.DbType.AnsiString, System.Data.DbType.AnsiStringFixedLength, System.Data.DbType.Binary))
                    {
                        size = string.Format("({0})", col.Size);
                    }
                    string typeString = DbTypeToNative(col.Type) + size;
                    sb.AppendFormat("[{0}] {1} {2},", col.Name, typeString, nullable);
                }
            }

            sb.Remove(sb.Length - 1, 1);
            sb.Append(")");
            ExecuteNonQuery(sb.ToString());
        }

        public override void CreateTable(TableRef tblName, bool idAsIdentityColumn)
        {
            CreateTable(tblName, idAsIdentityColumn, true);
        }

        public override void CreateTable(TableRef tblName, bool idAsIdentityColumn, bool createPrimaryKey)
        {
            Log.DebugFormat("CreateTable [{0}]", tblName);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("CREATE TABLE {0} (", FormatTableName(tblName));
            if (idAsIdentityColumn)
            {
                sb.AppendLine("[ID] [int] IDENTITY(1,1) NOT NULL");
            }
            else
            {
                sb.AppendLine("[ID] [int] NOT NULL");
            }

            if (createPrimaryKey)
            {
                sb.AppendFormat(", CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED ( [ID] ASC )", tblName);
            }

            sb.AppendLine();
            sb.Append(")");

            ExecuteNonQuery(sb.ToString());
        }

        public override void CreateColumn(TableRef tblName, string colName, DbType type, int size, int scale, bool isNullable, DefaultConstraint defConstraint)
        {
            DoColumn(true, tblName, colName, type, size, scale, isNullable, defConstraint);
        }

        public override void AlterColumn(TableRef tblName, string colName, DbType type, int size, int scale, bool isNullable, DefaultConstraint defConstraint)
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
                // Create ntext for unlimited string length
                Log.DebugFormat("[{0}] table [{1}] column [{2}] ntext [{3}]", addOrAlter, tblName, colName, nullable);
                sb.AppendFormat("ALTER TABLE {0} {1} [{2}] {3} {4}", FormatTableName(tblName), addOrAlter, colName,
                    "ntext",
                    nullable);
            }
            else if (type == System.Data.DbType.Binary && size == int.MaxValue)
            {
                // Create ntext for unlimited string length
                Log.DebugFormat("[{0}] table [{1}] column [{2}] image [{3}]", addOrAlter, tblName, colName, nullable);
                sb.AppendFormat("ALTER TABLE {0} {1} [{2}] {3} {4}", FormatTableName(tblName), addOrAlter, colName,
                    "image",
                    nullable);
            }
            else
            {
                string strSize = string.Empty;
                if (size > 0 && type.In(System.Data.DbType.String, System.Data.DbType.StringFixedLength, System.Data.DbType.AnsiString, System.Data.DbType.AnsiStringFixedLength, System.Data.DbType.Binary))
                {
                    strSize = string.Format("({0})", size);
                }
                else if (size > 0 && type.In(System.Data.DbType.Decimal, System.Data.DbType.VarNumeric))
                {
                    strSize = string.Format("({0}, {1})", size, scale);
                }

                string typeString = DbTypeToNative(type) + strSize;
                Log.DebugFormat("[{0}] table [{1}] column [{2}] [{3}] [{4}]", addOrAlter, tblName, colName, typeString, nullable);
                sb.AppendFormat("ALTER TABLE {0} {1}  [{2}] {3} {4}", FormatTableName(tblName), addOrAlter, colName,
                    typeString,
                    nullable);
            }

            ExecuteNonQuery(sb.ToString());

            var constrName = ConstructDefaultConstraintName(tblName, colName);
            ExecuteNonQuery("IF OBJECT_ID('[{0}]') IS NOT NULL\nALTER TABLE {1} DROP CONSTRAINT [{0}]", constrName, FormatTableName(tblName));
            if (defConstraint != null)
            {
                string defValue;
                if (defConstraint is NewGuidDefaultConstraint)
                {
                    defValue = "NEWID()";
                }
                else if (defConstraint is IntDefaultConstraint)
                {
                    defValue = ((IntDefaultConstraint)defConstraint).Value.ToString();
                }
                else if (defConstraint is DateTimeDefaultConstraint)
                {
                    defValue = "getdate()";
                }
                else
                {
                    throw new ArgumentOutOfRangeException("defConstraint", "Unsupported default constraint " + defConstraint.GetType().Name);
                }
                ExecuteNonQuery("ALTER TABLE {1} ADD CONSTRAINT [{0}] DEFAULT {3} FOR [{2}]", constrName, FormatTableName(tblName), colName, defValue);
            }
        }

        public override void CreateFKConstraint(TableRef tblName, TableRef refTblName, string colName, string constraintName, bool onDeleteCascade)
        {
            Log.DebugFormat("Creating foreign key constraint [{0}].[{1}] -> [{2}].ID", tblName, colName, refTblName);
            ExecuteNonQuery(@"ALTER TABLE {0}  WITH CHECK 
                    ADD CONSTRAINT [{1}] FOREIGN KEY([{2}])
                    REFERENCES {3} ([ID]){4}",
                   FormatTableName(tblName),
                   constraintName,
                   colName,
                   FormatTableName(refTblName),
                   onDeleteCascade ? @" ON DELETE CASCADE" : String.Empty);

            ExecuteNonQuery(@"ALTER TABLE {0} CHECK CONSTRAINT [{1}]",
                   FormatTableName(tblName),
                   constraintName);
        }

        public override void DropTable(TableRef tblName)
        {
            Log.DebugFormat("Dropping table [{0}]", tblName);
            ExecuteNonQuery("DROP TABLE {0}", FormatTableName(tblName));
        }

        public override void TruncateTable(TableRef tblName)
        {
            Log.DebugFormat("Truncating table [{0}]", tblName);
            ExecuteNonQuery("DELETE FROM {0}", FormatTableName(tblName));
        }

        private void ExecuteNonQuery(string nonQueryFormat, params object[] args)
        {
            string query = String.Format(nonQueryFormat, args);

            using (var cmd = new SqlCommand(query, CurrentConnection, CurrentTransaction))
            {
                QueryLog.Debug(query);
                cmd.ExecuteNonQuery();
            }
        }

        private object ExecuteScalar(string nonQueryFormat, params object[] args)
        {
            string query = String.Format(nonQueryFormat, args);

            using (var cmd = new SqlCommand(query, CurrentConnection, CurrentTransaction))
            {
                QueryLog.Debug(query);
                return cmd.ExecuteScalar();
            }
        }

        public override void DropColumn(TableRef tblName, string colName)
        {
            Log.DebugFormat("Dropping column [{0}].[{1}]", tblName, colName);
            ExecuteNonQuery("ALTER TABLE {0} DROP COLUMN [{1}]", FormatTableName(tblName), colName);
        }

        public override void DropFKConstraint(TableRef tblName, string fkName)
        {
            Log.DebugFormat("Dropping foreign key constraint [{0}].[{1}]", tblName, fkName);
            ExecuteNonQuery("ALTER TABLE {0} DROP CONSTRAINT [{1}]", FormatTableName(tblName), fkName);
        }

        public override void DropTrigger(string triggerName)
        {
            Log.DebugFormat("Dropping trigger [{0}]", triggerName);
            ExecuteNonQuery("DROP TRIGGER [{0}]", triggerName);
        }

        public override void DropView(TableRef viewName)
        {
            Log.DebugFormat("Dropping view [{0}]", viewName);
            ExecuteNonQuery("DROP VIEW {0}", FormatTableName(viewName));
        }

        public override void DropProcedure(string procName)
        {
            Log.DebugFormat("Dropping procedure [{0}]", procName);
            ExecuteNonQuery("DROP PROCEDURE [{0}]", procName);
        }

        public override void DropAllObjects()
        {
            ExecuteNonQueryScriptFromResource("Kistl.Server.Database.Scripts.DropTables.sql");
        }

        public override void CopyColumnData(TableRef srcTblName, string srcColName, TableRef tblName, string colName)
        {
            Log.DebugFormat("Copying data from [{0}].[{1}] to [{2}].[{3}]", srcTblName, srcColName, tblName, colName);
            ExecuteNonQuery("UPDATE dest SET dest.[{0}] = src.[{1}] FROM {2} dest INNER JOIN {3} src ON dest.ID = src.ID",
                colName, srcColName, FormatTableName(tblName), FormatTableName(srcTblName));
        }

        public override void MigrateFKs(TableRef srcTblName, string srcColName, TableRef tblName, string colName)
        {
            Log.DebugFormat("Migrating FK data from [{0}].[{1}] to [{2}].[{3}]", srcTblName, srcColName, tblName, colName);
            ExecuteNonQuery("UPDATE dest SET dest.[{0}] = src.[ID] FROM [{2}] dest INNER JOIN [{3}] src ON dest.ID = src.[{1}]",
                colName, srcColName, FormatTableName(tblName), FormatTableName(srcTblName));
        }

        public override void InsertFKs(TableRef srcTblName, string srcColName, TableRef tblName, string colName, string fkColName)
        {
            Log.DebugFormat("Inserting FK data from [{0}]([{1}]) to [{2}]([{3}],[{4}])", srcTblName, srcColName, tblName, colName, fkColName);
            ExecuteNonQuery("INSERT INTO {0} ([{1}], [{2}]) SELECT [ID], [{3}] FROM {4} WHERE [{3}] IS NOT NULL",
                FormatTableName(tblName), colName, fkColName, srcColName, FormatTableName(srcTblName));
        }

        public override void CopyFKs(TableRef srcTblName, string srcColName, TableRef destTblName, string destColName, string srcFKColName)
        {
            Log.DebugFormat("Copy FK data from [{0}]([{1}]) to [{2}]([{3}])", srcTblName, srcColName, destTblName, destColName);
            ExecuteNonQuery("UPDATE dest SET dest.[{0}] = src.[{1}] FROM {2} dest  INNER JOIN {3} src ON src.[{4}] = dest.[ID]",
                destColName, srcColName, FormatTableName(destTblName), FormatTableName(srcTblName), srcFKColName);
        }

        private int GetSQLServerVersion()
        {
            string verStr = (string)ExecuteScalar("SELECT SERVERPROPERTY('productversion')");
            return int.Parse(verStr.Split('.').First());
        }


        public override void DropIndex(TableRef tblName, string idxName)
        {
            ExecuteNonQuery("DROP INDEX [{0}] ON {1}", idxName, FormatTableName(tblName));
        }

        public override void CreateIndex(TableRef tblName, string idxName, bool unique, bool clustered, params string[] columns)
        {
            string colSpec = string.Join(", ", columns.Select(c => "[" + c + "]").ToArray());

            Log.DebugFormat("Creating index {0}.[{1}] ({2})", FormatTableName(tblName), idxName, colSpec);

            string appendIndexFilter = string.Empty;
            if (unique && !clustered && columns.Length == 1)
            {
                bool isNullable = GetIsColumnNullable(tblName, columns.First());
                int dbVer = GetSQLServerVersion();
                // Special checks
                if (isNullable && dbVer < 10)
                {
                    Log.WarnFormat("Warning: Unable to create unique index on nullable column [{0}].[{1}]. Creating non unique index.", tblName, columns);
                    unique = false;
                }
                else if (isNullable && dbVer >= 10)
                {
                    appendIndexFilter = string.Format(" WHERE [{0}] IS NOT NULL", columns.First());
                }
            }

            ExecuteNonQuery("CREATE {0} {1} INDEX {2} ON [{3}] ({4}){5}",
                unique ? "UNIQUE" : string.Empty,
                clustered ? "CLUSTERED" : string.Empty,
                idxName,
                tblName,
                colSpec,
                appendIndexFilter);
        }

        public override void CreateUpdateRightsTrigger(string triggerName, TableRef tblName, List<RightsTrigger> tblList)
        {
            if (tblList == null) throw new ArgumentNullException("tblList");

            Log.DebugFormat("Creating trigger to update rights [{0}]", triggerName);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"CREATE TRIGGER [{0}]
ON {1}
AFTER UPDATE, INSERT, DELETE AS
BEGIN", triggerName, FormatTableName(tblName));
            sb.AppendLine();

            foreach (var tbl in tblList)
            {
                StringBuilder select = new StringBuilder();
                if (tbl.Relations.Count == 0)
                {
                    sb.AppendFormat(@"
    DELETE FROM {0} WHERE [ID] IN (SELECT [ID] FROM inserted)
    DELETE FROM {0} WHERE [ID] IN (SELECT [ID] FROM deleted)
    INSERT INTO {0} ([ID], [Identity], [Right]) 
        SELECT [ID], [Identity], [Right]
        FROM {1}
        WHERE [ID] IN (SELECT [ID] FROM inserted)",
                        FormatTableName(tbl.TblNameRights),
                        FormatTableName(tbl.ViewUnmaterializedName));
                    sb.AppendLine();
                    sb.AppendLine();
                }
                else
                {
                    select.AppendFormat("SELECT t1.[ID] FROM {0} t1", FormatTableName(tbl.TblName));
                    int idx = 2;
                    var lastRel = tbl.Relations.Last();
                    foreach (var rel in tbl.Relations)
                    {
                        var joinTbl = rel == lastRel ? "{0}" : FormatTableName(rel.JoinTableName);
                        select.AppendLine();
                        select.AppendFormat(@"      INNER JOIN {0} t{1} ON t{1}.[{2}] = t{3}.[{4}]", joinTbl, idx, rel.JoinColumnName, idx - 1, rel.FKColumnName);
                        idx++;
                    }
                    string selectFormat = select.ToString();
                    sb.AppendFormat(@"    DELETE FROM {0} WHERE [ID] IN ({1})", FormatTableName(tbl.TblNameRights), string.Format(selectFormat, "inserted"));
                    sb.AppendLine();
                    sb.AppendFormat(@"    DELETE FROM {0} WHERE [ID] IN ({1})", FormatTableName(tbl.TblNameRights), string.Format(selectFormat, "deleted"));
                    sb.AppendLine();
                    sb.AppendFormat(@"    INSERT INTO {0} ([ID], [Identity], [Right]) SELECT [ID], [Identity], [Right] FROM {2} WHERE [ID] IN ({1})",
                        FormatTableName(tbl.TblNameRights), string.Format(selectFormat, "inserted"), FormatTableName(tbl.ViewUnmaterializedName));
                    sb.AppendLine();
                    sb.AppendLine();
                }
            }

            sb.AppendLine("END");
            ExecuteNonQuery(sb.ToString());
        }

        public override void CreateEmptyRightsViewUnmaterialized(TableRef viewName)
        {
            Log.DebugFormat("Creating *empty* unmaterialized rights view [{0}]", viewName);
            ExecuteNonQuery(@"CREATE VIEW {0} AS SELECT 0 [ID], 0 [Identity], 0 [Right] WHERE 0 = 1", FormatTableName(viewName));
        }

        public override void CreateRightsViewUnmaterialized(TableRef viewName, TableRef tblName, TableRef tblNameRights, IList<ACL> acls)
        {
            if (acls == null) throw new ArgumentNullException("acls");
            Log.DebugFormat("Creating unmaterialized rights view for [{0}]", tblName);

            StringBuilder view = new StringBuilder();
            view.AppendFormat(@"CREATE VIEW {0} AS
SELECT	[ID], [Identity], 
		(case SUM([Right] & 1) when 0 then 0 else 1 end) +
		(case SUM([Right] & 2) when 0 then 0 else 2 end) +
		(case SUM([Right] & 4) when 0 then 0 else 4 end) +
		(case SUM([Right] & 8) when 0 then 0 else 8 end) [Right] 
FROM (", FormatTableName(viewName));
            view.AppendLine();

            foreach (var acl in acls)
            {
                view.AppendFormat(@"  SELECT t1.[ID] [ID], t{0}.[{1}] [Identity], {2} [Right]",
                    acl.Relations.Count,
                    acl.Relations.Last().FKColumnName,
                    (int)acl.Right);
                view.AppendLine();
                view.AppendFormat(@"  FROM {0} t1", FormatTableName(tblName));
                view.AppendLine();

                int idx = 2;
                foreach (var rel in acl.Relations.Take(acl.Relations.Count - 1))
                {
                    view.AppendFormat(@"  INNER JOIN {0} t{1} ON t{1}.[{2}] = t{3}.[{4}]", FormatTableName(rel.JoinTableName), idx, rel.JoinColumnName, idx - 1, rel.FKColumnName);
                    view.AppendLine();
                    idx++;
                }
                view.AppendFormat(@"  WHERE t{0}.[{1}] IS NOT NULL",
                    acl.Relations.Count,
                    acl.Relations.Last().FKColumnName);
                view.AppendLine();
                view.AppendLine("  UNION ALL");
            }
            view.Remove(view.Length - 12, 12);

            view.AppendLine(@") unmaterialized GROUP BY [ID], [Identity]");

            ExecuteNonQuery(view.ToString());
        }

        public override void CreateRefreshRightsOnProcedure(string procName, TableRef viewUnmaterializedName, TableRef tblName, TableRef tblNameRights)
        {
            Log.DebugFormat("Creating refresh rights procedure for [{0}]", tblName);
            ExecuteNonQuery(@"CREATE PROCEDURE [{0}] (@ID INT = NULL) AS
                    BEGIN
	                    IF (@ID IS NULL)
		                    BEGIN
			                    TRUNCATE TABLE {1}
			                    INSERT INTO {1} ([ID], [Identity], [Right]) SELECT [ID], [Identity], [Right] FROM {2}
		                    END
	                    ELSE
		                    BEGIN
			                    DELETE FROM {1} WHERE ID = @ID
			                    INSERT INTO {1} ([ID], [Identity], [Right]) SELECT [ID], [Identity], [Right] FROM {2} WHERE [ID] = @ID
		                    END
                    END",
                procName,
                FormatTableName(tblNameRights),
                FormatTableName(viewUnmaterializedName));
        }

        public override void ExecRefreshRightsOnProcedure(string procName)
        {
            Log.DebugFormat("Refreshing rights for [{0}]", procName);
            ExecuteNonQuery(@"EXEC [{0}]", procName);
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

            ExecuteNonQueryScriptFromResource(String.Format(@"Kistl.Server.Database.Scripts.{0}.sql", procName));

            var createTableProcQuery = new StringBuilder();
            createTableProcQuery.AppendFormat("CREATE PROCEDURE [{0}] (@repair BIT, @tblName NVARCHAR(255), @colName NVARCHAR(255), @result BIT OUTPUT) AS", tableProcName);
            createTableProcQuery.AppendLine();
            createTableProcQuery.AppendLine("SET @result = 0");
            foreach (var tbl in refSpecs)
            {
                createTableProcQuery.AppendFormat("IF @tblName IS NULL OR @tblName = '{0}' BEGIN", tbl.Key);
                createTableProcQuery.AppendLine();
                createTableProcQuery.Append("\t");
                foreach (var refSpec in tbl)
                {
                    createTableProcQuery.AppendFormat("IF @colName IS NULL OR @colName = '{0}{1}' BEGIN", refSpec.Value, Kistl.API.Helper.PositionSuffix);
                    createTableProcQuery.AppendLine();
                    createTableProcQuery.AppendFormat(
                        "\t\tEXECUTE RepairPositionColumnValidity @repair=@repair, @tblName='{0}', @refTblName='{1}', @fkColumnName='{2}', @fkPositionName='{2}{3}', @result = @result OUTPUT",
                        tbl.Key,
                        refSpec.Key,
                        refSpec.Value,
                        Kistl.API.Helper.PositionSuffix);
                    createTableProcQuery.AppendLine();
                    createTableProcQuery.AppendLine("\t\tIF @repair = 0 AND @result = 1 RETURN");
                    createTableProcQuery.AppendFormat("\tEND ELSE ", tbl.Key);
                }
                createTableProcQuery.AppendLine("BEGIN");

                // see http://msdn.microsoft.com/en-us/library/ms177497.aspx
                // no sensible advice about the "severity" found, thus using a nice random 17.
                // this also allows client code to use TRY/CATCH on this error
                createTableProcQuery.AppendLine("\t\tRAISERROR (N'Column [%s].[%s] not found', 17, 1, @tblName, @colName)");
                createTableProcQuery.AppendLine("\tEND");

                createTableProcQuery.AppendFormat("END ELSE ", tbl.Key);
            }
            createTableProcQuery.AppendLine("BEGIN");

            // see http://msdn.microsoft.com/en-us/library/ms177497.aspx
            // no sensible advice about the "severity" found, thus using a nice random 17.
            // this also allows client code to use TRY/CATCH on this error
            createTableProcQuery.AppendLine("\tRAISERROR (N'Table [%s] not found', 17, 1, @tblName)");
            createTableProcQuery.AppendLine("END");
            ExecuteNonQuery(createTableProcQuery.ToString());
        }

        public override void RenameTable(TableRef oldTblName, TableRef newTblName)
        {
            // Do not qualify new name as it will be part of the name
            ExecuteNonQuery("EXEC sp_rename '{0}', '{1}'", FormatTableName(oldTblName), FormatTableName(newTblName));
        }

        public override void RenameColumn(TableRef tblName, string oldColName, string newColName)
        {
            // Do not qualify new name as it will be part of the name
            ExecuteNonQuery("EXEC sp_rename '{0}.[{1}]', '{2}', 'COLUMN'", FormatTableName(tblName), oldColName, newColName);
        }

        public override void RenameFKConstraint(string oldConstraintName, string newConstraintName)
        {
            // Do not qualify new name as it will be part of the name
            ExecuteNonQuery("EXEC sp_rename '[{0}]', '{1}', 'OBJECT'", oldConstraintName, newConstraintName);
        }

        public override IDataReader ReadTableData(TableRef tbl, IEnumerable<string> colNames)
        {
            var columns = String.Join(",", colNames.Select(n => QuoteIdentifier(n)).ToArray());
            var query = String.Format("SELECT {0} FROM {1}", columns, FormatTableName(tbl));

            var cmd = new SqlCommand(query, CurrentConnection, CurrentTransaction);
            return cmd.ExecuteReader();
        }

        public override IDataReader ReadJoin(TableRef tbl, IEnumerable<Join> joins)
        {
            //var query = new StringBuilder(String.Format("SELECT * FROM {0}", tbl));
            //foreach (var join in joins)
            //{
            //    query.AppendFormat(@"  {0}JOIN {1} ON t{1}.[{2}] = t{3}.[{4}]", rel.JoinTableName, idx, rel.JoinColumnName, idx - 1, rel.FKColumnName);
            //}
            throw new NotImplementedException();
        }

        public override void WriteTableData(TableRef destTbl, IDataReader source, IEnumerable<string> colNames)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (colNames == null) throw new ArgumentNullException("colNames");

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(CurrentConnection))
            {
                bulkCopy.DestinationTableName = destTbl.Name;

                int i = 0;
                foreach (var colName in colNames)
                {
                    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(i++, colName));
                }

                try
                {
                    bulkCopy.WriteToServer(source);
                }
                catch (Exception ex)
                {
                    Log.Error("Error bulk writing to destination", ex);
                    throw;
                }
            }
        }

        public override void RefreshDbStats()
        {
            // do nothing
        }
    }
}
