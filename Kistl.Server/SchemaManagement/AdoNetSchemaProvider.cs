
namespace Kistl.Server.SchemaManagement
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Kistl.API.Server;
    using Kistl.API.Utils;
    using System.Collections;

    public abstract class AdoNetSchemaProvider<TConnection, TTransaction, TCommand>
        : ISchemaProvider
        where TConnection : class, IDbConnection
        where TTransaction : class, IDbTransaction
        where TCommand : class, IDbCommand
    {
        protected abstract log4net.ILog Log { get; }
        protected abstract log4net.ILog QueryLog { get; }

        #region ADO.NET Infrastructure

        protected void LogError(string query, IDictionary<string, object> args)
        {
            Log.ErrorFormat("Error in statement: [{0}]", query);
            if (args != null)
            {
                foreach (var pair in args)
                {
                    // avoid huge values
                    var val = (pair.Value ?? String.Empty).ToString();
                    if (val.Length > 300)
                    {
                        val = val.Substring(0, 296) + " ...";
                    }
                    Log.ErrorFormat("[{0}] => [{1}]", pair.Key, val);
                }
            }
        }

        protected IEnumerable<IDataRecord> ExecuteReader(string query)
        {
            return ExecuteReader(query, null);
        }

        protected IEnumerable<IDataRecord> ExecuteReader(string query, IDictionary<string, object> args)
        {
            QueryLog.Debug(query);
            using (var cmd = CreateCommand(query))
            {
                if (args != null)
                {
                    foreach (var pair in args)
                    {
                        var param = cmd.CreateParameter();
                        param.ParameterName = pair.Key;
                        param.Value = pair.Value;
                        cmd.Parameters.Add(param);
                    }
                }
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        yield return rd;
                    }
                }
            }
        }

        protected object ExecuteScalar(string query)
        {
            return ExecuteScalar(query, null);
        }

        protected object ExecuteScalar(string query, IDictionary<string, object> args)
        {
            QueryLog.Debug(query);
            try
            {
                using (var cmd = CreateCommand(query))
                {
                    if (args != null)
                    {
                        foreach (var pair in args)
                        {
                            var param = cmd.CreateParameter();
                            param.ParameterName = pair.Key;
                            param.Value = pair.Value;
                            cmd.Parameters.Add(param);
                        }
                    }
                    return cmd.ExecuteScalar();
                }
            }
            catch
            {
                LogError(query, args);
                throw;
            }
        }

        protected void ExecuteNonQuery(string query)
        {
            ExecuteNonQuery(query, null);
        }

        protected void ExecuteNonQuery(string query, IDictionary<string, object> args)
        {
            QueryLog.Debug(query);
            try
            {
                using (var cmd = CreateCommand(query))
                {
                    if (args != null)
                    {
                        foreach (var pair in args)
                        {
                            var param = cmd.CreateParameter();
                            param.ParameterName = pair.Key;
                            param.Value = pair.Value;
                            cmd.Parameters.Add(param);
                        }
                    }

                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                LogError(query, args);
                throw;
            }
        }

        public void ExecuteSqlResource(Type type, string scriptResourceNameFormat)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (string.IsNullOrEmpty(scriptResourceNameFormat))
                throw new ArgumentNullException("scriptResourceNameFormat");

            var scriptResourceName = String.Format(scriptResourceNameFormat, ConfigName);
            var stream = type.Assembly.GetManifestResourceStream(scriptResourceName);
            if (stream == null)
                throw new ArgumentOutOfRangeException("scriptResourceNameFormat", String.Format("Script [{0}] not found in assembly [{1}]", scriptResourceName, type.Assembly.FullName));

            using (var scriptStream = new StreamReader(stream, Encoding.UTF8))
            {
                var databaseScript = scriptStream.ReadToEnd();

                foreach (var cmdString in Regex.Split(databaseScript, "\r?\nGO\r?\n").Where(s => !String.IsNullOrEmpty(s)))
                {
                    try
                    {
                        ExecuteNonQuery(cmdString);
                    }
                    catch
                    {
                        LogError(cmdString, null);
                        throw;
                    }
                }
            }
        }

        #endregion

        #region Meta data

        public abstract string ConfigName { get; }
        public abstract string AdoNetProvider { get; }
        public abstract string ManifestToken { get; }
        /// <inheritdoc />
        public abstract bool IsStorageProvider { get; }

        #endregion

        #region ADO.NET, Connection and Transaction Handling

        private TConnection db;
        protected TConnection CurrentConnection { get { return db; } }

        private TTransaction tx;
        protected TTransaction CurrentTransaction { get { return tx; } }

        protected abstract TConnection CreateConnection(string connectionString);

        protected abstract TTransaction CreateTransaction();

        protected abstract TCommand CreateCommand(string query);

        public void Open(string connectionString)
        {
            if (db != null)
                throw new InvalidOperationException("Database already opened");
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connectionString");

            db = CreateConnection(connectionString);
            db.Open();
        }

        public void BeginTransaction()
        {
            if (tx != null)
                throw new InvalidOperationException("Transaction already in progress");
            tx = CreateTransaction();
        }

        public void CommitTransaction()
        {
            if (tx != null)
            {
                tx.Commit();
                tx = null;
            }
        }

        public void RollbackTransaction()
        {
            if (tx != null)
            {
                tx.Rollback();
                tx = null;
            }
        }

        public virtual void Dispose()
        {
            if (tx != null)
            {
                tx.Rollback();
                tx.Dispose();
                tx = null;
            }

            if (db != null)
            {
                db.Close();
                db.Dispose();
                db = null;
            }
        }

        #endregion

        #region Type Mapping

        public abstract string DbTypeToNative(DbType type);
        public abstract DbType NativeToDbType(string type);

        #endregion

        #region SQL Infrastructure

        protected abstract string QuoteIdentifier(string name);

        protected virtual string BuildSelect(TableRef tbl, params string[] columns)
        {
            return "SELECT "
                + String.Join(",", columns.Select(c => QuoteIdentifier(c)).ToArray())
                + " FROM "
                + FormatSchemaName(tbl);
        }

        protected static string ConstructDefaultConstraintName(TableRef tblName, string colName)
        {
            return String.Format("default_{0}_{1}", tblName.Name, colName);
        }

        #endregion

        #region Database Schemas

        public abstract IEnumerable<string> GetSchemaNames();
        public abstract void CreateSchema(string schemaName);
        public abstract void DropSchema(string schemaName, bool force);

        #endregion

        #region Table Structure

        protected abstract string FormatFullName(DboRef tblName);
        protected abstract string FormatSchemaName(DboRef tblName);

        public TableRef GetQualifiedTableName(string tblName)
        {
            if (db == null)
                throw new InvalidOperationException("cannot qualify table name without database connection");
            // keep "dbo" as default schema until we implement schemas in the infrastructure
            return new TableRef(db.Database, "dbo", tblName);
        }

        public abstract bool CheckTableExists(TableRef tblName);

        public abstract IEnumerable<TableRef> GetTableNames();
        public abstract IEnumerable<TableRef> GetViewNames();

        public abstract void CreateTable(TableRef tblName, IEnumerable<Column> cols);

        public void CreateTable(TableRef tblName, bool idAsIdentityColumn)
        {
            // create public key by default
            CreateTable(tblName, idAsIdentityColumn, true);
        }

        public abstract void CreateTable(TableRef tblName, bool idAsIdentityColumn, bool createPrimaryKey);

        public abstract void RenameTable(TableRef oldTblName, TableRef newTblName);

        public virtual void DropTable(TableRef tblName)
        {
            ExecuteNonQuery(String.Format("DROP TABLE {0}", FormatSchemaName(tblName)));
        }

        protected virtual void DropTableCascade(TableRef tblName)
        {
            ExecuteNonQuery(String.Format("DROP TABLE {0} CASCADE", FormatSchemaName(tblName)));
        }

        public abstract bool CheckColumnExists(TableRef tblName, string colName);

        public abstract IEnumerable<string> GetTableColumnNames(TableRef tblName);

        public abstract IEnumerable<Column> GetTableColumns(TableRef tblName);

        public void CreateColumn(TableRef tblName, string colName, DbType type, int size, int scale, bool isNullable, DefaultConstraint defConstraint)
        {
            DoColumn(true, tblName, colName, type, size, scale, isNullable, defConstraint);
        }

        public void AlterColumn(TableRef tblName, string colName, DbType type, int size, int scale, bool isNullable, DefaultConstraint defConstraint)
        {
            DoColumn(false, tblName, colName, type, size, scale, isNullable, defConstraint);
        }

        protected abstract void DoColumn(bool add, TableRef tblName, string colName, DbType type, int size, int scale, bool isNullable, DefaultConstraint defConstraint);

        public abstract void RenameColumn(TableRef tblName, string oldColName, string newColName);

        public abstract bool GetIsColumnNullable(TableRef tblName, string colName);

        public abstract bool GetHasColumnDefaultValue(TableRef tblName, string colName);

        public abstract int GetColumnMaxLength(TableRef tblName, string colName);

        public virtual void DropColumn(TableRef tblName, string colName)
        {
            ExecuteNonQuery(String.Format("ALTER TABLE {0} DROP COLUMN {1}",
                FormatSchemaName(tblName),
                QuoteIdentifier(colName)));
        }

        #endregion

        #region Table Content

        public abstract bool CheckTableContainsData(TableRef tblName);

        public abstract bool CheckColumnContainsNulls(TableRef tblName, string colName);

        public abstract bool CheckColumnContainsUniqueValues(TableRef tblName, string colName);

        public abstract bool CheckColumnContainsValues(TableRef tblName, string colName);

        public abstract long CountRows(TableRef tblName);

        public virtual void TruncateTable(TableRef tblName)
        {
            ExecuteNonQuery(String.Format("DELETE FROM {0}", FormatSchemaName(tblName)));
        }

        #endregion

        #region Constraint and Index Management

        public abstract bool CheckFKConstraintExists(string fkName);
        public abstract IEnumerable<TableConstraintNamePair> GetFKConstraintNames();
        public abstract void CreateFKConstraint(TableRef tblName, TableRef refTblName, string colName, string constraintName, bool onDeleteCascade);
        public abstract void RenameFKConstraint(string oldConstraintName, string newConstraintName);
        public virtual void DropFKConstraint(TableRef tblName, string fkName)
        {
            ExecuteNonQuery(String.Format("ALTER TABLE {0} DROP CONSTRAINT {1}",
                FormatSchemaName(tblName),
                QuoteIdentifier(fkName)));
        }

        public abstract bool CheckIndexExists(TableRef tblName, string idxName);
        public abstract void DropIndex(TableRef tblName, string idxName);

        #endregion

        #region Other DB Objects (Views, Triggers, Procedures)

        public abstract bool CheckViewExists(TableRef viewName);
        public virtual void DropView(TableRef viewName)
        {
            ExecuteNonQuery(String.Format("DROP VIEW {0}",
                FormatSchemaName(viewName)));
        }

        public abstract bool CheckTriggerExists(TableRef objName, string triggerName);
        public abstract void DropTrigger(TableRef objName, string triggerName);

        public ProcRef GetQualifiedProcedureName(string procName)
        {
            if (db == null)
                throw new InvalidOperationException("cannot qualify table name without database connection");
            // keep "dbo" as default schema until we implement schemas in the infrastructure
            return new ProcRef(db.Database, "dbo", procName);
        }

        public abstract IEnumerable<ProcRef> GetProcedureNames();
        public abstract bool CheckProcedureExists(ProcRef procName);
        public abstract void DropProcedure(ProcRef procName);

        public abstract void EnsureInfrastructure();
        public abstract void DropAllObjects();

        #endregion

        #region ZBox Schema Handling

        public string GetSavedSchema()
        {
            var currentSchemaRef = GetQualifiedTableName("CurrentSchema");
            if (!CheckTableExists(currentSchemaRef))
            {
                return String.Empty;
            }

            long count = CountRows(currentSchemaRef);
            switch (count)
            {
                case 0:
                    return String.Empty;
                case 1:
                    return (string)ExecuteScalar(BuildSelect(currentSchemaRef, "Schema"));
                default:
                    throw new InvalidOperationException("There is more then one Schema saved in your Database");
            }
        }

        // IN: schema
        protected abstract string GetSchemaInsertStatement();
        // IN: schema
        protected abstract string GetSchemaUpdateStatement();

        public void SaveSchema(string schema)
        {
            var currentSchemaRef = GetQualifiedTableName("CurrentSchema");
            if (!CheckTableExists(currentSchemaRef))
                throw new InvalidOperationException("Unable to save Schema. Schematable does not exist.");

            using (Log.DebugTraceMethodCall("Saving schema"))
            {
                long count = CountRows(currentSchemaRef);
                switch (count)
                {
                    case 0:
                        ExecuteNonQuery(GetSchemaInsertStatement(), new Dictionary<string, object>() { { "@schema", schema } });
                        break;
                    case 1:
                        ExecuteNonQuery(GetSchemaUpdateStatement(), new Dictionary<string, object>() { { "@schema", schema } });
                        break;
                    default:
                        throw new InvalidOperationException("There is more then one Schema saved in your Database");
                }
            }
        }

        #endregion

        #region zBox Accelerators

        public virtual bool CheckPositionColumnValidity(TableRef tblName, string posName)
        {
            var failed = CheckColumnContainsNulls(tblName, posName);
            if (failed)
            {
                Log.WarnFormat("Order Column [{0}].[{1}] contains NULLs.", tblName, posName);
                return false;
            }

            return CallRepairPositionColumn(false, tblName, posName);
        }

        public virtual bool RepairPositionColumn(TableRef tblName, string posName)
        {
            return CallRepairPositionColumn(true, tblName, posName);
        }

        protected abstract bool CallRepairPositionColumn(bool repair, TableRef tblName, string indexName);

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


        public abstract void CopyColumnData(TableRef srcTblName, string srcColName, TableRef tblName, string colName);
        public abstract void MigrateFKs(TableRef srcTblName, string srcColName, TableRef tblName, string colName);
        public abstract void InsertFKs(TableRef srcTblName, string srcColName, TableRef tblName, string colName, string fkColName);
        public abstract void CopyFKs(TableRef srcTblName, string srcColName, TableRef destTblName, string destColName, string srcFKColName);

        public abstract void CreateIndex(TableRef tblName, string idxName, bool unique, bool clustered, params string[] columns);

        public abstract void CreateUpdateRightsTrigger(string triggerName, TableRef tblName, List<RightsTrigger> tblList);
        public abstract void CreateRightsViewUnmaterialized(TableRef viewName, TableRef tblName, TableRef tblNameRights, IList<ACL> acls);
        public abstract void CreateEmptyRightsViewUnmaterialized(TableRef viewName);
        public abstract void CreateRefreshRightsOnProcedure(ProcRef procName, TableRef viewUnmaterializedName, TableRef tblName, TableRef tblNameRights);
        public abstract void ExecRefreshRightsOnProcedure(ProcRef procName);

        /// <summary>
        /// Creates a procedure to check position columns for their validity.
        /// </summary>
        /// <param name="refSpecs">a lookup by table name into lists of (referencedTableName, fkColumnName) pairs</param>
        public abstract void CreatePositionColumnValidCheckProcedures(ILookup<TableRef, KeyValuePair<TableRef, string>> refSpecs);

        public abstract IDataReader ReadTableData(TableRef tbl, IEnumerable<string> colNames);
        public abstract IDataReader ReadTableData(string sql);
        public abstract IDataReader ReadJoin(TableRef tbl, IEnumerable<ProjectionColumn> colNames, IEnumerable<Join> joins);

        public abstract void WriteTableData(TableRef destTbl, IDataReader source, IEnumerable<string> colNames);
        public abstract void WriteTableData(TableRef destTbl, IEnumerable<string> colNames, IEnumerable values);

        /// <summary>
        /// This can be called after significant changes to the database to cause the DBMS' optimizier to refresh its internal stats.
        /// </summary>
        public abstract void RefreshDbStats();
    }
}
