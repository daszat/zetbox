
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

    public abstract class AdoNetSchemaProvider<TConnection, TTransaction, TCommand>
        : ISchemaProvider
        where TConnection : class, IDbConnection
        where TTransaction : class, IDbTransaction
        where TCommand : class, IDbCommand
    {
        protected abstract log4net.ILog Log { get; }
        protected abstract log4net.ILog QueryLog { get; }

        #region ADO.NET Infrastructure

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
                        cmd.Parameters[pair.Key] = pair.Value;
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
            using (var cmd = CreateCommand(query))
            {
                if (args != null)
                {
                    foreach (var pair in args)
                    {
                        cmd.Parameters[pair.Key] = pair.Value;
                    }
                }
                return cmd.ExecuteScalar();
            }
        }

        protected void ExecuteNonQuery(string query)
        {
            ExecuteNonQuery(query, null);
        }

        protected void ExecuteNonQuery(string query, IDictionary<string, object> args)
        {
            QueryLog.Debug(query);
            using (var cmd = CreateCommand(query))
            {
                if (args != null)
                {
                    foreach (var pair in args)
                    {
                        cmd.Parameters[pair.Key] = pair.Value;
                    }
                }
                cmd.ExecuteNonQuery();
            }
        }

        protected void ExecuteNonQueryScriptFromResource(string scriptResourceName)
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

        #endregion

        #region Meta data

        public abstract string ConfigName { get; }
        public abstract string AdoNetProvider { get; }
        public abstract string ManifestToken { get; }

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
            if (db != null) throw new InvalidOperationException("Database already opened");
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException("connectionString");

            db = CreateConnection(connectionString);
            db.Open();
        }

        public void BeginTransaction()
        {
            if (tx != null) throw new InvalidOperationException("Transaction already in progress");
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

        #region ZBox Schema Handling

        public string GetSavedSchema()
        {
            var currentSchemaRef = GetQualifiedTableName("CurrentSchema");
            if (!CheckTableExists(currentSchemaRef))
            {
                return String.Empty;
            }

            var count = (long)ExecuteScalar("SELECT COUNT(*) FROM " + FormatTableName(currentSchemaRef));
            if (count == 0)
            {
                return String.Empty;
            }
            else if (count == 1)
            {
                return (string)ExecuteScalar(BuildSelect(currentSchemaRef, "Schema"));
            }
            else
            {
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
                long count = (long)ExecuteScalar("SELECT COUNT(*) FROM " + FormatTableName(currentSchemaRef));
                if (count == 0)
                {
                    ExecuteNonQuery(GetSchemaInsertStatement(), new Dictionary<string, object>() { { "@schema", schema } });
                }
                else if (count == 1)
                {
                    ExecuteNonQuery(GetSchemaUpdateStatement(), new Dictionary<string, object>() { { "@schema", schema } });
                }
                else
                {
                    throw new InvalidOperationException("There is more then one Schema saved in your Database");
                }
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
                + FormatTableName(tbl);
        }

        #endregion

        #region Table Structure

        protected abstract string FormatTableName(TableRef tbl);

        public TableRef GetQualifiedTableName(string tbl)
        {
            return new TableRef(db.Database, "dbo", tbl);
        }

        // IN: @schema, @table, OUT: exists?
        protected abstract string GetTableExistsStatement();

        public bool CheckTableExists(TableRef tblName)
        {
            return (bool)ExecuteScalar(GetTableExistsStatement());
        }

        // OUT: schema, name
        protected abstract string GetTableNamesStatement();

        public IEnumerable<TableRef> GetTableNames()
        {
            return ExecuteReader(GetTableNamesStatement())
                .Select(rd => new TableRef(db.Database, rd.GetString(0), rd.GetString(1)));
        }

        // IN: schema, table, name, OUT: exists?
        protected abstract string GetColumnExistsStatment();

        public bool CheckColumnExists(TableRef tblName, string colName)
        {
            return (bool)ExecuteScalar(GetColumnExistsStatment(),
                new Dictionary<string, object>(){
                    { "@schema", tblName.Schema },
                    { "@table", tblName.Name },
                    { "@name", colName },
                });
        }

        // IN: @schema, @table, OUT: name, type, max_length, is_nullable
        protected abstract string GetTableColumnsStatement();

        public IEnumerable<string> GetTableColumnNames(TableRef tbl)
        {
            return ExecuteReader(
                    GetTableColumnNamesStatement(),
                    new Dictionary<string, object>() {
                        { "@schema", tbl.Schema },
                        { "@table", tbl.Name }
                    })
                .Select(rd => rd.GetString(0));
        }

        // IN: @schema, @table, OUT: name
        protected abstract string GetTableColumnNamesStatement();

        public IEnumerable<Column> GetTableColumns(TableRef tbl)
        {
            return ExecuteReader(
                    GetTableColumnsStatement(),
                    new Dictionary<string, object>() {
                        { "@schema", tbl.Schema },
                        { "@table", tbl.Name } 
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


        // IN: constraint_name, OUT: exists?
        protected abstract string GetFKConstraintExistsStatement();

        public bool CheckFKConstraintExists(string fkName)
        {
            return (bool)ExecuteScalar(GetFKConstraintExistsStatement(),
                 new Dictionary<string, object>(){
                    { "@constraint_name", fkName },
                });
        }

        // IN: schema, table, index, OUT: exists?
        protected abstract string GetIndexExistsStatement();

        public bool CheckIndexExists(TableRef tblName, string idxName)
        {
            return (bool)ExecuteScalar(GetIndexExistsStatement(),
                      new Dictionary<string, object>(){
                    { "@schema", tblName.Schema },
                    { "@table", tblName.Name},
                    { "@index", idxName },
                });
        }

        #endregion

        #region Table Content

        public abstract bool CheckTableContainsData(TableRef tblName);
        public abstract bool CheckColumnContainsNulls(TableRef tblName, string colName);
        public abstract bool CheckColumnContainsUniqueValues(TableRef tblName, string colName);

        public bool CheckColumnContainsValues(TableRef tbl, string colName)
        {
            return !CheckColumnContainsNulls(tbl, colName);
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


        public abstract bool CheckViewExists(TableRef viewName);
        public abstract bool CheckTriggerExists(TableRef objName, string triggerName);
        public abstract bool CheckProcedureExists(string procName);

        public abstract bool CheckPositionColumnValidity(TableRef tblName, string positionColumnName);
        public abstract bool RepairPositionColumn(TableRef tblName, string positionColumnName);

        public abstract bool GetIsColumnNullable(TableRef tblName, string colName);
        public abstract bool GetHasColumnDefaultValue(TableRef tblName, string colName);
        public abstract int GetColumnMaxLength(TableRef tblName, string colName);

        public abstract void CreateTable(TableRef tbl, IEnumerable<Column> cols);
        public abstract void CreateTable(TableRef tblName, bool idAsIdentityColumn);
        public abstract void CreateTable(TableRef tblName, bool idAsIdentityColumn, bool createPrimaryKey);
        public abstract void CreateColumn(TableRef tblName, string colName, DbType type, int size, int scale, bool isNullable, DefaultConstraint defConstraint);
        public abstract void AlterColumn(TableRef tblName, string colName, DbType type, int size, int scale, bool isNullable, DefaultConstraint defConstraint);
        public abstract IEnumerable<TableConstraintNamePair> GetFKConstraintNames();
        public abstract void CreateFKConstraint(TableRef tblName, TableRef refTblName, string colName, string constraintName, bool onDeleteCascade);

        public abstract void RenameTable(TableRef oldTblName, TableRef newTblName);
        public abstract void RenameColumn(TableRef tblName, string oldColName, string newColName);
        public abstract void RenameFKConstraint(string oldConstraintName, string newConstraintName);

        public abstract void TruncateTable(TableRef tblName);
        public abstract void DropTable(TableRef tblName);
        public abstract void DropColumn(TableRef tblName, string colName);
        public abstract void DropFKConstraint(TableRef tblName, string fkName);
        public abstract void DropTrigger(string triggerName);
        public abstract void DropView(TableRef viewName);
        public abstract void DropProcedure(string procName);
        public abstract void DropIndex(TableRef tblName, string idxName);
        public abstract void DropAllObjects();

        public abstract void CopyColumnData(TableRef srcTblName, string srcColName, TableRef tblName, string colName);
        public abstract void MigrateFKs(TableRef srcTblName, string srcColName, TableRef tblName, string colName);
        public abstract void InsertFKs(TableRef srcTblName, string srcColName, TableRef tblName, string colName, string fkColName);
        public abstract void CopyFKs(TableRef srcTblName, string srcColName, TableRef destTblName, string destColName, string srcFKColName);

        public abstract void CreateIndex(TableRef tblName, string idxName, bool unique, bool clustered, params string[] columns);

        public abstract void CreateUpdateRightsTrigger(string triggerName, TableRef tblName, List<RightsTrigger> tblList);
        public abstract void CreateRightsViewUnmaterialized(TableRef viewName, TableRef tblName, TableRef tblNameRights, IList<ACL> acls);
        public abstract void CreateEmptyRightsViewUnmaterialized(TableRef viewName);
        public abstract void CreateRefreshRightsOnProcedure(string procName, TableRef viewUnmaterializedName, TableRef tblName, TableRef tblNameRights);
        public abstract void ExecRefreshRightsOnProcedure(string procName);

        /// <summary>
        /// Creates a procedure to check position columns for their validity.
        /// </summary>
        /// <param name="refSpecs">a lookup by table name into lists of (fkColumnName, referencedTableName) pairs</param>
        public abstract void CreatePositionColumnValidCheckProcedures(ILookup<string, KeyValuePair<string, string>> refSpecs);

        public abstract IDataReader ReadTableData(TableRef tbl, IEnumerable<string> colNames);
        public abstract IDataReader ReadJoin(TableRef tbl, IEnumerable<Join> joins);

        public abstract void WriteTableData(TableRef destTbl, IDataReader source, IEnumerable<string> colNames);

        /// <summary>
        /// This can be called after significant changes to the database to cause the DBMS' optimizier to refresh its internal stats.
        /// </summary>
        public abstract void RefreshDbStats();
    }
}
