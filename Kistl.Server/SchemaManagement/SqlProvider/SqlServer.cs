
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
        public override bool IsStorageProvider { get { return true; } }

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
                case DbType.UInt16:
                case DbType.UInt32:
                case DbType.Byte:
                case DbType.Int16:
                case DbType.Int32:
                    return "int";
                case DbType.UInt64:
                case DbType.Int64:
                    return "bigint";
                case DbType.Single:
                case DbType.Double:
                    return "float";
                case DbType.String:
                    return "nvarchar";
                case DbType.Date:
                case DbType.DateTime:
                case DbType.DateTime2:
                    // We only support SQLServer 2008
                    return "datetime2";
                case DbType.Boolean:
                    return "bit";
                case DbType.Guid:
                    return "uniqueidentifier";
                case DbType.Binary:
                    return "varbinary";
                case DbType.Decimal:
                case DbType.VarNumeric:
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
                    return DbType.Int32;
                case "bigint":
                    return DbType.Int64;
                case "float":
                    return DbType.Double;
                case "nvarchar":
                case "varchar":
                case "text":
                case "ntext":
                    return DbType.String;
                case "datetime":
                    return DbType.DateTime;
                case "datetime2":
                    return DbType.DateTime2;
                case "bit":
                    return DbType.Boolean;
                case "uniqueidentifier":
                    return DbType.Guid;
                case "binary":
                case "varbinary":
                    return DbType.Binary;
                case "decimal":
                    return DbType.Decimal;
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

        protected string GetColumnDefinition(Column col)
        {
            StringBuilder sb = new StringBuilder();

            string nullable = col.IsNullable ? "NULL" : "NOT NULL";

            // hardcoded special cases
            if (col.Type == DbType.String && col.Size == int.MaxValue)
            {
                // Create ntext columns for unlimited string length
                sb.AppendFormat("{0} ntext {1}", QuoteIdentifier(col.Name), nullable);
            }
            else if (col.Type == DbType.Binary && col.Size == int.MaxValue)
            {
                // Create image columns for unlimited blob length
                sb.AppendFormat("{0} image {1}", QuoteIdentifier(col.Name), nullable);
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
                sb.AppendFormat("{0} {1} {2}", QuoteIdentifier(col.Name), typeString, nullable);
            }
            return sb.ToString();
        }

        #endregion

        #region Database Schemas

        public override IEnumerable<string> GetSchemaNames()
        {
            throw new NotImplementedException();
        }

        public override void CreateSchema(string schemaName)
        {
            ExecuteNonQuery(String.Format("CREATE SCHEMA {0}", QuoteIdentifier(schemaName)));
        }

        public override void DropSchema(string schemaName, bool force)
        {
            ExecuteNonQuery(String.Format(
                "DROP SCHEMA {0} {1}",
                QuoteIdentifier(schemaName),
                force ? "CASCADE" : "RESTRICT"));
        }

        #endregion

        #region Table Structure

        protected override string FormatFullName(DboRef dbo)
        {
            return String.Format("[{0}].[{1}].[{2}]", dbo.Database, dbo.Schema, dbo.Name);
        }

        protected override string FormatSchemaName(DboRef dbo)
        {
            return String.Format("[{0}].[{1}]", dbo.Schema, dbo.Name);
        }

        public override bool CheckTableExists(TableRef tblName)
        {
            // TODO: check schema/database
            return (int)ExecuteScalar("SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(@tbl) AND type IN (N'U')",
                new Dictionary<string, object>() { 
                    { "@tbl", FormatFullName(tblName) }
                }) > 0;
        }

        public override IEnumerable<TableRef> GetTableNames()
        {
            return ExecuteReader("SELECT s.name, o.name FROM sys.objects o JOIN sys.schemas s ON o.schema_id = s.schema_id WHERE o.type = N'U' AND o.name <> 'sysdiagrams'")
                .Select(rd => new TableRef(CurrentConnection.Database, rd.GetString(0), rd.GetString(1)));
        }

        public override IEnumerable<TableRef> GetViewNames()
        {
            return ExecuteReader("SELECT s.name, o.name FROM sys.objects o JOIN sys.schemas s ON o.schema_id = s.schema_id WHERE o.type = N'V' AND o.name <> 'sysdiagrams'")
                .Select(rd => new TableRef(CurrentConnection.Database, rd.GetString(0), rd.GetString(1)));
        }

        public override void CreateTable(TableRef tblName, IEnumerable<Column> cols)
        {
            if (cols == null) throw new ArgumentNullException("cols");

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("CREATE TABLE {0} (", FormatSchemaName(tblName));
            sb.AppendLine();

            sb.Append(String.Join(",\n", cols.Select(col => GetColumnDefinition(col)).ToArray()));

            sb.Append(")");
            ExecuteNonQuery(sb.ToString());
        }

        public override void CreateTable(TableRef tblName, bool idAsIdentityColumn, bool createPrimaryKey)
        {
            if (tblName == null) throw new ArgumentNullException("tblName");

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("CREATE TABLE [{0}].[{1}] (", tblName.Schema, tblName.Name);
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
                // TODO: use Construct to create PK_{0}
                sb.AppendFormat(", CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED ( [ID] ASC )", tblName.Name);
            }

            sb.AppendLine();
            sb.Append(")");

            ExecuteNonQuery(sb.ToString());
        }

        public override void RenameTable(TableRef oldTblName, TableRef newTblName)
        {
            if (oldTblName == null) throw new ArgumentNullException("oldTblName");
            if (newTblName == null) throw new ArgumentNullException("newTblName");
            if (!oldTblName.Database.Equals(newTblName.Database)) { throw new ArgumentOutOfRangeException("newTblName", "cannot rename table to different database"); }
            if (!oldTblName.Schema.Equals(newTblName.Schema)) { throw new ArgumentOutOfRangeException("newTblName", "cannot rename table to different schema"); }

            // Do not qualify new name as it will be part of the old schema
            ExecuteNonQuery(string.Format("EXEC sp_rename '{0}', '{1}'", FormatFullName(oldTblName), newTblName.Name));
        }

        public override bool CheckColumnExists(TableRef tblName, string colName)
        {
            return (int)ExecuteScalar(@"SELECT COUNT(*)
                FROM sys.objects o 
                    INNER JOIN sys.columns c ON c.object_id=o.object_id
                WHERE o.object_id = OBJECT_ID(@tbl) 
                    AND o.type IN (N'U', N'V')
                    AND c.Name = @name",
                new Dictionary<string, object>(){
                    { "@tbl", FormatFullName(tblName) },
                    { "@name", colName },
                }) > 0;
        }

        public override IEnumerable<string> GetTableColumnNames(TableRef tblName)
        {
            return ExecuteReader(
               @"SELECT c.name
                        FROM sys.objects o 
                            INNER JOIN sys.columns c ON c.object_id=o.object_id
                        WHERE o.object_id = OBJECT_ID(@tbl)
                            AND o.type IN (N'U', N'V')",
                new Dictionary<string, object>() {
                    { "@tbl", FormatFullName(tblName) },
                })
                .Select(rd => rd.GetString(0));
        }

        public override IEnumerable<Column> GetTableColumns(TableRef tblName)
        {
            return ExecuteReader(
                @"SELECT c.name, TYPE_NAME(system_type_id) type, max_length, is_nullable
                    FROM sys.objects o 
                        INNER JOIN sys.columns c ON c.object_id=o.object_id
                    WHERE o.object_id = OBJECT_ID(@tbl)
                        AND o.type IN (N'U', N'V')",
                new Dictionary<string, object>() {
                    { "@tbl", FormatFullName(tblName) },
                })
                .Select(rd =>
                {
                    var type = NativeToDbType(rd.GetString(1));
                    int maxSize = int.MaxValue;
                    switch (type)
                    {
                        case DbType.AnsiString:
                        case DbType.AnsiStringFixedLength:
                        case DbType.Binary:
                        case DbType.String:
                        case DbType.StringFixedLength:
                        case DbType.Xml:
                            maxSize = rd.GetInt16(2);
                            break;
                        default:
                            break;
                    }
                    return new Column()
                    {
                        Name = rd.GetString(0),
                        Type = type,
                        Size = maxSize,
                        IsNullable = rd.GetBoolean(3)
                    };
                });
        }

        protected override void DoColumn(bool add, TableRef tblName, string colName, DbType type, int size, int scale, bool isNullable, DefaultConstraint defConstraint)
        {
            StringBuilder sb = new StringBuilder();

            // Prepare
            string addOrAlter = add ? "ADD" : "ALTER COLUMN";
            string nullable = isNullable ? "NULL" : "NOT NULL";
            string defValue = string.Empty;
            var constrName = ConstructDefaultConstraintName(tblName, colName);

            if (defConstraint != null)
            {
                if (defConstraint is NewGuidDefaultConstraint)
                {
                    defValue = "NEWID()";
                }
                else if (defConstraint is IntDefaultConstraint)
                {
                    defValue = ((IntDefaultConstraint)defConstraint).Value.ToString();
                }
                else if (defConstraint is BoolDefaultConstraint)
                {
                    defValue = ((BoolDefaultConstraint)defConstraint).Value ? "1" : "0";
                }
                else if (defConstraint is DateTimeDefaultConstraint)
                {
                    defValue = "getdate()";
                }
                else
                {
                    throw new ArgumentOutOfRangeException("defConstraint", "Unsupported default constraint " + defConstraint.GetType().Name);
                }
            }

            // Drop an existing default constraint
            if (!add)
            {
                ExecuteNonQuery(string.Format("IF OBJECT_ID('[{0}]') IS NOT NULL\nALTER TABLE {1} DROP CONSTRAINT [{0}]", constrName, FormatFullName(tblName)));
            }

            // Construct statement
            sb.AppendFormat("ALTER TABLE {0} {1} {2}", FormatFullName(tblName), addOrAlter, GetColumnDefinition(new Column() { Name = colName, Type = type, Size = size, Scale = scale, IsNullable = isNullable }));

            // Add Default Constraint only if column is been added
            if (defConstraint != null && add)
            {
                sb.AppendFormat(" CONSTRAINT {0} DEFAULT {1}", constrName, defValue);
            }

            ExecuteNonQuery(sb.ToString());

            // Recreate Constraint only if not done during add
            if (defConstraint != null && !add)
            {
                ExecuteNonQuery(string.Format("ALTER TABLE {1} ADD CONSTRAINT [{0}] DEFAULT {3} FOR [{2}]", constrName, FormatFullName(tblName), colName, defValue));
            }
        }

        public override void RenameColumn(TableRef tblName, string oldColName, string newColName)
        {
            // Do not qualify new name as it will stay part of the original table
            ExecuteNonQuery(string.Format("EXEC sp_rename '{0}.[{1}]', '{2}', 'COLUMN'", FormatFullName(tblName), oldColName, newColName));
        }

        public override bool GetIsColumnNullable(TableRef tblName, string colName)
        {
            return (bool)ExecuteScalar(@"
                SELECT c.is_nullable
                FROM sys.objects o
                    INNER JOIN sys.columns c ON c.object_id=o.object_id
                WHERE o.object_id = OBJECT_ID(@table) 
		            AND o.type IN (N'U', N'V')
		            AND c.Name = @column",
                new Dictionary<string, object>(){
                    { "@table", FormatFullName(tblName) },
                    { "@column", colName },
                });
        }

        public override bool GetHasColumnDefaultValue(TableRef tblName, string colName)
        {
            return (int)ExecuteScalar("SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(@def) AND type IN (N'D')",
                new Dictionary<string, object>(){
                    { "@def", ConstructDefaultConstraintName(tblName, colName) },
                }) > 0;
        }

        public override int GetColumnMaxLength(TableRef tblName, string colName)
        {
            // divide by 2 to account for _n_varchar!
            return (int)ExecuteScalar(@"
                SELECT c.max_length / 2
                FROM sys.objects o
                    INNER JOIN sys.columns c ON c.object_id=o.object_id
	            WHERE o.object_id = OBJECT_ID(@table) 
		            AND o.type IN (N'U', N'V')
		            AND c.Name = @column",
                new Dictionary<string, object>(){
                    { "@table", FormatFullName(tblName) },
                    { "@column", colName },
                });
        }

        #endregion

        #region Table Content

        public override bool CheckTableContainsData(TableRef tbl)
        {
            return (int)ExecuteScalar(String.Format(
                "SELECT COUNT(*) FROM (SELECT TOP 1 * FROM {0}) AS data",
                FormatFullName(tbl))) > 0;
        }

        public override bool CheckColumnContainsNulls(TableRef tbl, string colName)
        {
            return (int)ExecuteScalar(String.Format(
                "SELECT COUNT(*) FROM (SELECT TOP 1 {1} FROM {0} WHERE {1} IS NULL) AS nulls",
                FormatFullName(tbl),
                QuoteIdentifier(colName))) > 0;
        }

        public override bool CheckColumnContainsUniqueValues(TableRef tbl, string colName)
        {
            return (int)ExecuteScalar(String.Format(
                @"SELECT COUNT(*) FROM (
                    SELECT TOP 1 {1} FROM {0} WHERE {1} IS NOT NULL
                    GROUP BY {1} 
                    HAVING COUNT({1}) > 1) AS tbl",
                FormatFullName(tbl),
                QuoteIdentifier(colName))) == 0;
        }

        public override bool CheckColumnContainsValues(TableRef tbl, string colName)
        {
            return (int)ExecuteScalar(String.Format(
                "SELECT COUNT(*) FROM (SELECT TOP 1 {1} FROM {0} WHERE {1} IS NOT NULL) AS data",
                FormatFullName(tbl),
                QuoteIdentifier(colName))) > 0;
        }

        public override long CountRows(TableRef tblName)
        {
            return (int)ExecuteScalar(String.Format(
                @"SELECT COUNT(*) FROM {0}",
                FormatFullName(tblName)));
        }

        #endregion

        #region Constraint and Index Management

        public override bool CheckFKConstraintExists(string fkName)
        {
            return (int)ExecuteScalar("SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(@constraint_name) AND type IN (N'F')",
                new Dictionary<string, object>() {
                    { "@constraint_name", fkName },
                }) > 0;
        }

        public override IEnumerable<TableConstraintNamePair> GetFKConstraintNames()
        {
            return ExecuteReader("SELECT c.name, t.name FROM sys.objects c INNER JOIN sys.sysobjects t ON t.id = c.parent_object_id WHERE c.type IN (N'F') ORDER BY c.name")
                .Select(rd => new TableConstraintNamePair()
                {
                    ConstraintName = rd.GetString(0),
                    // TODO: remove 'dbo' reference and return all tablerefs with proper namespace
                    TableName = new TableRef(CurrentConnection.Database, "dbo", rd.GetString(1))
                });
        }

        public override void CreateFKConstraint(TableRef tblName, TableRef refTblName, string colName, string constraintName, bool onDeleteCascade)
        {
            ExecuteNonQuery(string.Format(@"
                ALTER TABLE {0} WITH CHECK 
                ADD CONSTRAINT [{1}] FOREIGN KEY([{2}])
                REFERENCES {3} ([ID]){4}",
                FormatFullName(tblName),
                constraintName,
                colName,
                FormatFullName(refTblName),
                onDeleteCascade ? @" ON DELETE CASCADE" : String.Empty));

            ExecuteNonQuery(string.Format(@"ALTER TABLE {0} CHECK CONSTRAINT [{1}]",
                   FormatFullName(tblName),
                   constraintName));
        }

        public override void RenameFKConstraint(string oldConstraintName, string newConstraintName)
        {
            // Do not qualify new name as it will be part of the name
            ExecuteNonQuery(string.Format("EXEC sp_rename '[{0}]', '{1}', 'OBJECT'", oldConstraintName, newConstraintName));
        }

        public override bool CheckIndexExists(TableRef tblName, string idxName)
        {
            return (int)ExecuteScalar("SELECT COUNT(*) from sys.sysindexes WHERE id = OBJECT_ID(@tbl) AND [name] = @index",
                new Dictionary<string, object>(){
                    { "@tbl", FormatFullName(tblName) },
                    { "@index", idxName },
                }) > 0;
        }

        public override void DropIndex(TableRef tblName, string idxName)
        {
            ExecuteNonQuery(string.Format("DROP INDEX [{0}] ON {1}", idxName, FormatFullName(tblName)));
        }

        #endregion

        #region Other DB Objects (Views, Triggers, Procedures)

        public override bool CheckViewExists(TableRef viewName)
        {
            return (int)ExecuteScalar(
                "SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(@view) AND type IN (N'V')",
                new Dictionary<string, object>() {
                    { "@view", FormatFullName(viewName) },
                }) > 0;
        }

        public override bool CheckTriggerExists(TableRef objName, string triggerName)
        {
            return (int)ExecuteScalar(
                "SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(@trigger) AND parent_object_id = OBJECT_ID(@parent) AND type IN (N'TR')",
                new Dictionary<string, object>(){
                    { "@trigger", triggerName },
                    { "@parent", FormatFullName(objName) },
                }) > 0;
        }

        public override void DropTrigger(TableRef objName, string triggerName)
        {
            ExecuteNonQuery(string.Format("DROP TRIGGER [{0}]", triggerName));
        }

        public override IEnumerable<ProcRef> GetProcedureNames()
        {
            throw new NotImplementedException();
        }

        public override bool CheckProcedureExists(ProcRef procName)
        {
            return (int)ExecuteScalar(
                  "SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(@proc) AND type IN (N'P')",
                  new Dictionary<string, object>(){
                    { "@proc", FormatSchemaName(procName) },
                }) > 0;
        }

        public override void DropProcedure(ProcRef procName)
        {
            ExecuteNonQuery(string.Format("DROP PROCEDURE {0}", FormatSchemaName(procName)));
        }

        public override void EnsureInfrastructure()
        {
        }

        public override void DropAllObjects()
        {
            ExecuteSqlResource(this.GetType(), "Kistl.Server.SchemaManagement.SqlProvider.Scripts.DropTables.sql");
        }

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

        #region zBox Accelerators

        protected override bool CallRepairPositionColumn(bool repair, TableRef tblName, string indexName)
        {
            using (var cmd = new SqlCommand("RepairPositionColumnValidityByTable", CurrentConnection, CurrentTransaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@repair", repair);
                cmd.Parameters.AddWithValue("@tblName", tblName.Name);
                cmd.Parameters.AddWithValue("@colName", indexName);
                cmd.Parameters.Add("@result", SqlDbType.Bit).Direction = ParameterDirection.Output;

                QueryLog.Debug(cmd.CommandText);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    LogError("RepairPositionColumnValidityByTable(@repair, @tblName, @colName)",
                        new Dictionary<string, object>() {
                            { "@repair", repair },
                            { "@tblName", tblName.Name },
                            { "@colName", indexName },
                        });
                }
                return (bool)cmd.Parameters["@result"].Value;
            }
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


        public override void CopyColumnData(TableRef srcTblName, string srcColName, TableRef tblName, string colName)
        {
            Log.DebugFormat("Copying data from [{0}].[{1}] to [{2}].[{3}]", srcTblName, srcColName, tblName, colName);
            ExecuteNonQuery(string.Format("UPDATE dest SET dest.[{0}] = src.[{1}] FROM {2} dest INNER JOIN {3} src ON dest.ID = src.ID",
                colName, srcColName, FormatFullName(tblName), FormatFullName(srcTblName)));
        }

        public override void MigrateFKs(TableRef srcTblName, string srcColName, TableRef tblName, string colName)
        {
            Log.DebugFormat("Migrating FK data from [{0}].[{1}] to [{2}].[{3}]", srcTblName, srcColName, tblName, colName);
            ExecuteNonQuery(string.Format("UPDATE dest SET dest.[{0}] = src.[ID] FROM [{2}] dest INNER JOIN [{3}] src ON dest.ID = src.[{1}]",
                colName, srcColName, FormatFullName(tblName), FormatFullName(srcTblName)));
        }

        public override void InsertFKs(TableRef srcTblName, string srcColName, TableRef tblName, string colName, string fkColName)
        {
            Log.DebugFormat("Inserting FK data from [{0}]([{1}]) to [{2}]([{3}],[{4}])", srcTblName, srcColName, tblName, colName, fkColName);
            ExecuteNonQuery(string.Format("INSERT INTO {0} ([{1}], [{2}]) SELECT [ID], [{3}] FROM {4} WHERE [{3}] IS NOT NULL",
                FormatFullName(tblName), colName, fkColName, srcColName, FormatFullName(srcTblName)));
        }

        public override void CopyFKs(TableRef srcTblName, string srcColName, TableRef destTblName, string destColName, string srcFKColName)
        {
            Log.DebugFormat("Copy FK data from [{0}]([{1}]) to [{2}]([{3}])", srcTblName, srcColName, destTblName, destColName);
            ExecuteNonQuery(string.Format("UPDATE dest SET dest.[{0}] = src.[{1}] FROM {2} dest  INNER JOIN {3} src ON src.[{4}] = dest.[ID]",
                destColName, srcColName, FormatFullName(destTblName), FormatFullName(srcTblName), srcFKColName));
        }

        private int GetSQLServerVersion()
        {
            string verStr = (string)ExecuteScalar("SELECT SERVERPROPERTY('productversion')");
            return int.Parse(verStr.Split('.').First());
        }

        public override void CreateIndex(TableRef tblName, string idxName, bool unique, bool clustered, params string[] columns)
        {
            string colSpec = string.Join(", ", columns.Select(c => "[" + c + "]").ToArray());

            Log.DebugFormat("Creating index {0}.[{1}] ({2})", FormatFullName(tblName), idxName, colSpec);

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

            ExecuteNonQuery(string.Format("CREATE {0} {1} INDEX {2} ON [{3}].[{4}] ({5}){6}",
                unique ? "UNIQUE" : string.Empty,
                clustered ? "CLUSTERED" : string.Empty,
                idxName,
                tblName.Schema,
                tblName.Name,
                colSpec,
                appendIndexFilter));
        }

        public override void CreateUpdateRightsTrigger(string triggerName, TableRef tblName, List<RightsTrigger> tblList)
        {
            if (tblList == null) throw new ArgumentNullException("tblList");

            Log.DebugFormat("Creating trigger to update rights [{0}]", triggerName);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"CREATE TRIGGER [{0}]
ON {1}
AFTER UPDATE, INSERT, DELETE AS
BEGIN", triggerName, FormatFullName(tblName));
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
                        FormatSchemaName(tbl.TblNameRights),
                        FormatSchemaName(tbl.ViewUnmaterializedName));
                    sb.AppendLine();
                    sb.AppendLine();
                }
                else
                {
                    select.AppendFormat("SELECT t1.[ID] FROM {0} t1", FormatSchemaName(tbl.TblName));
                    int idx = 2;
                    var lastRel = tbl.Relations.Last();
                    foreach (var rel in tbl.Relations)
                    {
                        var joinTbl = rel == lastRel ? "{0}" : FormatSchemaName(rel.JoinTableName);
                        select.AppendLine();
                        select.AppendFormat(@"      INNER JOIN {0} t{1} ON t{1}.[{2}] = t{3}.[{4}]", joinTbl, idx, rel.JoinColumnName.Single().ColumnName, idx - 1, rel.FKColumnName.Single().ColumnName);
                        idx++;
                    }
                    string selectFormat = select.ToString();
                    sb.AppendFormat(@"    DELETE FROM {0} WHERE [ID] IN ({1})", FormatSchemaName(tbl.TblNameRights), string.Format(selectFormat, "inserted"));
                    sb.AppendLine();
                    sb.AppendFormat(@"    DELETE FROM {0} WHERE [ID] IN ({1})", FormatSchemaName(tbl.TblNameRights), string.Format(selectFormat, "deleted"));
                    sb.AppendLine();
                    sb.AppendFormat(@"    INSERT INTO {0} ([ID], [Identity], [Right]) SELECT [ID], [Identity], [Right] FROM {2} WHERE [ID] IN ({1})",
                        FormatSchemaName(tbl.TblNameRights), string.Format(selectFormat, "inserted"), FormatSchemaName(tbl.ViewUnmaterializedName));
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
            ExecuteNonQuery(string.Format(@"CREATE VIEW {0} AS SELECT 0 [ID], 0 [Identity], 0 [Right] WHERE 0 = 1", FormatSchemaName(viewName)));
        }

        public override void CreateRightsViewUnmaterialized(TableRef viewName, TableRef tblName, TableRef tblNameRights, IList<ACL> acls)
        {
            if (viewName == null) throw new ArgumentNullException("viewName");
            if (tblName == null) throw new ArgumentNullException("tblName");
            if (acls == null) throw new ArgumentNullException("acls");
            Log.DebugFormat("Creating unmaterialized rights view for [{0}]", tblName);

            StringBuilder view = new StringBuilder();
            view.AppendFormat(@"CREATE VIEW [{0}].[{1}] AS
SELECT	[ID], [Identity], 
		(case SUM([Right] & 1) when 0 then 0 else 1 end) +
		(case SUM([Right] & 2) when 0 then 0 else 2 end) +
		(case SUM([Right] & 4) when 0 then 0 else 4 end) +
		(case SUM([Right] & 8) when 0 then 0 else 8 end) [Right] 
FROM (", viewName.Schema, viewName.Name);
            view.AppendLine();

            foreach (var acl in acls)
            {
                view.AppendFormat(@"  SELECT t1.[ID] [ID], t{0}.[{1}] [Identity], {2} [Right]",
                    acl.Relations.Count,
                    acl.Relations.Last().FKColumnName.Single().ColumnName,
                    (int)acl.Right);
                view.AppendLine();
                view.AppendFormat(@"  FROM {0} t1", FormatSchemaName(tblName));
                view.AppendLine();

                int idx = 2;
                foreach (var rel in acl.Relations.Take(acl.Relations.Count - 1))
                {
                    view.AppendFormat(@"  INNER JOIN {0} t{1} ON t{1}.[{2}] = t{3}.[{4}]", FormatSchemaName(rel.JoinTableName), idx, rel.JoinColumnName.Single().ColumnName, idx - 1, rel.FKColumnName.Single().ColumnName);
                    view.AppendLine();
                    idx++;
                }
                view.AppendFormat(@"  WHERE t{0}.[{1}] IS NOT NULL",
                    acl.Relations.Count,
                    acl.Relations.Last().FKColumnName.Single().ColumnName);
                view.AppendLine();
                view.AppendLine("  UNION ALL");
            }
            view.Remove(view.Length - 12, 12);

            view.AppendLine(@") unmaterialized GROUP BY [ID], [Identity]");

            ExecuteNonQuery(view.ToString());
        }

        public override void CreateRefreshRightsOnProcedure(ProcRef procName, TableRef viewUnmaterializedName, TableRef tblName, TableRef tblNameRights)
        {
            Log.DebugFormat("Creating refresh rights procedure for [{0}]", tblName);
            ExecuteNonQuery(string.Format(@"CREATE PROCEDURE [{0}] (@ID INT = NULL) AS
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
                FormatSchemaName(procName),
                FormatSchemaName(tblNameRights),
                FormatSchemaName(viewUnmaterializedName)));
        }

        public override void ExecRefreshRightsOnProcedure(ProcRef procName)
        {
            Log.DebugFormat("Refreshing rights for [{0}]", procName);
            ExecuteNonQuery(string.Format(@"EXEC {0}", FormatSchemaName(procName)));
        }

        public override void CreatePositionColumnValidCheckProcedures(ILookup<TableRef, KeyValuePair<TableRef, string>> refSpecs)
        {
            if (refSpecs == null) { throw new ArgumentNullException("refSpecs"); }

            var procName = "RepairPositionColumnValidity";
            var tableProcName = procName + "ByTable";

            foreach (var name in new[] { procName, tableProcName })
            {
                var procRef = new ProcRef(null, "dbo", name);
                if (CheckProcedureExists(procRef))
                {
                    DropProcedure(procRef);
                }
            }

            ExecuteSqlResource(this.GetType(), String.Format(@"Kistl.Server.Database.Scripts.{0}.sql", procName));

            var createTableProcQuery = new StringBuilder();
            createTableProcQuery.AppendFormat("CREATE PROCEDURE [{0}] (@repair BIT, @tblName NVARCHAR(255), @colName NVARCHAR(255), @result BIT OUTPUT) AS", tableProcName);
            createTableProcQuery.AppendLine();
            createTableProcQuery.AppendLine("SET @result = 0");
            foreach (var tbl in refSpecs)
            {
                createTableProcQuery.AppendFormat("IF @tblName IS NULL OR @tblName = '{0}' BEGIN", FormatSchemaName(tbl.Key));
                createTableProcQuery.AppendLine();
                createTableProcQuery.Append("\t");
                foreach (var refSpec in tbl)
                {
                    createTableProcQuery.AppendFormat("IF @colName IS NULL OR @colName = '{0}{1}' BEGIN", refSpec.Value, Kistl.API.Helper.PositionSuffix);
                    createTableProcQuery.AppendLine();
                    createTableProcQuery.AppendFormat(
                        "\t\tEXECUTE RepairPositionColumnValidity @repair=@repair, @tblName='{0}', @refTblName='{1}', @fkColumnName='{2}', @fkPositionName='{2}{3}', @result = @result OUTPUT",
                        FormatSchemaName(tbl.Key),
                        FormatSchemaName(refSpec.Key),
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

        public override IDataReader ReadTableData(TableRef tbl, IEnumerable<string> colNames)
        {
            var columns = String.Join(",", colNames.Select(n => QuoteIdentifier(n)).ToArray());
            var query = String.Format("SELECT {0} FROM {1}", columns, FormatFullName(tbl));

            var cmd = new SqlCommand(query, CurrentConnection, CurrentTransaction);
            return cmd.ExecuteReader();
        }

        public override IDataReader ReadTableData(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, CurrentConnection, CurrentTransaction);
            return cmd.ExecuteReader();
        }


        public override IDataReader ReadJoin(TableRef tbl, IEnumerable<ProjectionColumn> colNames, IEnumerable<Join> joins)
        {
            if (tbl == null) throw new ArgumentNullException("tbl");
            if (colNames == null) throw new ArgumentNullException("colNames");
            if (joins == null) throw new ArgumentNullException("joins");

            var join_alias = new Dictionary<Join, string>();
            var joinQueryPart = new StringBuilder();
            int idx = 1;
            foreach (var join in joins)
            {
                AddReadJoin(joinQueryPart, ref idx, join, join_alias);
                idx++;
            }

            var columns = String.Join(",\n", colNames.Select(pc =>
            {
                string result = "\t";
                if (pc.Source == null) result += string.Format("t0.{0}", QuoteIdentifier(pc.ColumnName));
                else result += string.Format("{0}.{1}", join_alias[pc.Source], QuoteIdentifier(pc.ColumnName));

                if (!string.IsNullOrEmpty(pc.NullValue))
                {
                    result = string.Format("ISNULL({0}, {1})", result, pc.NullValue);
                }
                if (!string.IsNullOrEmpty(pc.Alias))
                {
                    result += " " + pc.Alias;
                }
                return result;
            }).ToArray());

            var query = new StringBuilder();
            query.AppendFormat("SELECT \n{0} \nFROM {1} t0{2}", columns, FormatFullName(tbl), joinQueryPart.ToString());
            var cmd = new SqlCommand(query.ToString(), CurrentConnection, CurrentTransaction);
            return cmd.ExecuteReader();
        }

        private static void AddReadJoin(StringBuilder query, ref int idx, Join join, Dictionary<Join, string> join_alias)
        {
            if (join.JoinColumnName.Length != join.FKColumnName.Length) throw new ArgumentException(string.Format("Column count on Join '{0}' does not match", join), "join");

            join_alias[join] = string.Format("t{0}", idx);
            query.AppendFormat("\n  {2} JOIN {0} t{1} ON ", join.JoinTableName, idx, join.Type.ToString().ToUpper());
            for (int i = 0; i < join.JoinColumnName.Length; i++)
            {
                query.AppendFormat("{0}.[{1}] = {2}.[{3}]",
                    join.JoinColumnName[i].Source == ColumnRef.PrimaryTable ? "t0" : (join.JoinColumnName[i].Source == ColumnRef.Local ? "t" + idx : join_alias[join.JoinColumnName[i].Source]),
                    join.JoinColumnName[i].ColumnName,
                    join.FKColumnName[i].Source == ColumnRef.PrimaryTable ? "t0" : (join.FKColumnName[i].Source == ColumnRef.Local ? "t" + idx : join_alias[join.FKColumnName[i].Source]),
                    join.FKColumnName[i].ColumnName);
                if (i < join.JoinColumnName.Length - 1) query.Append(" AND ");
            }
            foreach (var j in join.Joins)
            {
                idx++;
                AddReadJoin(query, ref idx, j, join_alias);
            }
        }

        public override void WriteTableData(TableRef destTbl, IDataReader source, IEnumerable<string> colNames)
        {
            if (destTbl == null) throw new ArgumentNullException("destTbl");
            if (source == null) throw new ArgumentNullException("source");
            if (colNames == null) throw new ArgumentNullException("colNames");

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(CurrentConnection, SqlBulkCopyOptions.CheckConstraints, null))
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

        public override void WriteTableData(TableRef destTbl, IEnumerable<string> colNames, System.Collections.IEnumerable values)
        {
            if (colNames == null) throw new ArgumentNullException("colNames");
            if (values == null) throw new ArgumentNullException("values");

            var sb = new StringBuilder();
            sb.AppendLine(string.Format("INSERT INTO {0} (", FormatFullName(destTbl)));

            colNames.ForEach(i => sb.Append("[" + i + "],"));
            sb.Remove(sb.Length - 1, 1);

            sb.AppendLine(") VALUES (");

            int counter = 0;
            colNames.ForEach(i => sb.Append(string.Format("@param{0},", ++counter)));
            sb.Remove(sb.Length - 1, 1);

            sb.AppendLine(")");

            var cmd = new SqlCommand(sb.ToString(), CurrentConnection, CurrentTransaction);
            counter = 0;
            foreach (var v in values)
            {
                cmd.Parameters.AddWithValue(string.Format("@param{0}", ++counter), v ?? DBNull.Value);
            }

            cmd.ExecuteNonQuery();
        }

        public override void RefreshDbStats()
        {
            // do nothing
        }
    }
}
