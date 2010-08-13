
namespace Kistl.Server.SchemaManagement
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using Kistl.API.Server;
    using Kistl.API.Utils;

    public class LoggingSchemaProviderAdapter
        : ISchemaProvider
    {
        private readonly log4net.ILog Log;
        private readonly ISchemaProvider _provider;

        public LoggingSchemaProviderAdapter(ISchemaProvider provider)
        {
            if (provider == null) throw new ArgumentNullException("provider");
            _provider = provider;
            Log = log4net.LogManager.GetLogger("Kistl.Server.Schema." + provider.ConfigName);
        }

        public string ConfigName
        {
            get { return _provider.ConfigName; }
        }

        public string AdoNetProvider
        {
            get { return _provider.AdoNetProvider; }
        }

        public string ManifestToken
        {
            get { return _provider.ManifestToken; }
        }

        public bool IsStorageProvider
        {
            get { return _provider.IsStorageProvider; }
        }

        public string GetSavedSchema()
        {
            return _provider.GetSavedSchema();
        }

        public void SaveSchema(string schema)
        {
            _provider.SaveSchema(schema);
        }

        public void Open(string connectionString)
        {
            _provider.Open(connectionString);
        }

        private Guid? _currentTxId;
        public void BeginTransaction()
        {
            if (_currentTxId.HasValue)
            {
                Log.WarnFormat("Resetting transaction {0}", _currentTxId);
            }
            _currentTxId = Guid.NewGuid();
            Log.InfoFormat("Begin Transaction {0}", _currentTxId);
            _provider.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _provider.CommitTransaction();
            Log.InfoFormat("Committed Transaction {0}", _currentTxId);
            _currentTxId = null;
        }

        public void RollbackTransaction()
        {
            _provider.RollbackTransaction();
        }

        public string DbTypeToNative(DbType type)
        {
            return _provider.DbTypeToNative(type);
        }

        public DbType NativeToDbType(string type)
        {
            return _provider.NativeToDbType(type);
        }

        public TableRef GetQualifiedTableName(string tblName)
        {
            return _provider.GetQualifiedTableName(tblName);
        }

        public bool CheckTableExists(TableRef tblName)
        {
            return _provider.CheckTableExists(tblName);
        }

        public IEnumerable<TableRef> GetTableNames()
        {
            return _provider.GetTableNames();
        }

        public bool CheckColumnExists(TableRef tblName, string colName)
        {
            return _provider.CheckColumnExists(tblName, colName);
        }

        public IEnumerable<string> GetTableColumnNames(TableRef tblName)
        {
            return _provider.GetTableColumnNames(tblName);
        }

        public IEnumerable<Column> GetTableColumns(TableRef tblName)
        {
            return _provider.GetTableColumns(tblName);
        }

        public bool CheckFKConstraintExists(string fkName)
        {
            return _provider.CheckFKConstraintExists(fkName);
        }

        public bool CheckIndexExists(TableRef tblName, string idxName)
        {
            return _provider.CheckIndexExists(tblName, idxName);
        }

        public bool CheckTableContainsData(TableRef tblName)
        {
            return _provider.CheckTableContainsData(tblName);
        }

        public bool CheckColumnContainsNulls(TableRef tblName, string colName)
        {
            return _provider.CheckColumnContainsNulls(tblName, colName);
        }

        public bool CheckColumnContainsUniqueValues(TableRef tblName, string colName)
        {
            return _provider.CheckColumnContainsUniqueValues(tblName, colName);
        }

        public bool CheckColumnContainsValues(TableRef tblName, string colName)
        {
            return _provider.CheckColumnContainsValues(tblName, colName);
        }

        public long CountRows(TableRef tblName)
        {
            return _provider.CountRows(tblName);
        }

        public bool CheckViewExists(TableRef viewName)
        {
            return _provider.CheckViewExists(viewName);
        }

        public bool CheckTriggerExists(TableRef objName, string triggerName)
        {
            return _provider.CheckTriggerExists(objName, triggerName);
        }

        public bool CheckProcedureExists(string procName)
        {
            return _provider.CheckProcedureExists(procName);
        }

        public bool CheckPositionColumnValidity(TableRef tblName, string positionColumnName)
        {
            return _provider.CheckPositionColumnValidity(tblName, positionColumnName);
        }

        public bool RepairPositionColumn(TableRef tblName, string positionColumnName)
        {
            return _provider.RepairPositionColumn(tblName, positionColumnName);
        }

        public bool GetIsColumnNullable(TableRef tblName, string colName)
        {
            return _provider.GetIsColumnNullable(tblName, colName);
        }

        public bool GetHasColumnDefaultValue(TableRef tblName, string colName)
        {
            return _provider.GetHasColumnDefaultValue(tblName, colName);
        }

        public int GetColumnMaxLength(TableRef tblName, string colName)
        {
            return _provider.GetColumnMaxLength(tblName, colName);
        }

        public void CreateTable(TableRef tblName, IEnumerable<Column> cols)
        {
            if (Log.IsDebugEnabled)
                Log.DebugFormat("CreateTable {0} with {1} columns", tblName, cols.Count());
            _provider.CreateTable(tblName, cols);
        }

        public void CreateTable(TableRef tblName, bool idAsIdentityColumn)
        {
            Log.DebugFormat("CreateTable [{0}] {1} and a primary key",
                tblName,
                idAsIdentityColumn ? "with identity" : "without identity");
            _provider.CreateTable(tblName, idAsIdentityColumn);
        }

        public void CreateTable(TableRef tblName, bool idAsIdentityColumn, bool createPrimaryKey)
        {
            Log.DebugFormat("CreateTable [{0}] {1} {2}",
                tblName,
                idAsIdentityColumn ? "with identity" : "without identity",
                createPrimaryKey ? "and a primary key" : "and no primary key");
            _provider.CreateTable(tblName, idAsIdentityColumn, createPrimaryKey);
        }

        public void CreateColumn(TableRef tblName, string colName, DbType type, int size, int scale, bool isNullable, DefaultConstraint defConstraint)
        {
            _provider.CreateColumn(tblName, colName, type, size, scale, isNullable, defConstraint);
        }

        public void AlterColumn(TableRef tblName, string colName, DbType type, int size, int scale, bool isNullable, DefaultConstraint defConstraint)
        {
            _provider.AlterColumn(tblName, colName, type, size, scale, isNullable, defConstraint);
        }

        public IEnumerable<TableConstraintNamePair> GetFKConstraintNames()
        {
            return _provider.GetFKConstraintNames();
        }

        public void CreateFKConstraint(TableRef tblName, TableRef refTblName, string colName, string constraintName, bool onDeleteCascade)
        {
            Log.DebugFormat("Creating foreign key constraint [{0}].[{1}] -> [{2}].ID", tblName, colName, refTblName);
            _provider.CreateFKConstraint(tblName, refTblName, colName, constraintName, onDeleteCascade);
        }

        public void RenameTable(TableRef oldTblName, TableRef newTblName)
        {
            _provider.RenameTable(oldTblName, newTblName);
        }

        public void RenameColumn(TableRef tblName, string oldColName, string newColName)
        {
            _provider.RenameColumn(tblName, oldColName, newColName);
        }

        public void RenameFKConstraint(string oldConstraintName, string newConstraintName)
        {
            _provider.RenameFKConstraint(oldConstraintName, newConstraintName);
        }

        public void TruncateTable(TableRef tblName)
        {
            Log.DebugFormat("Truncating table [{0}]", tblName);
            _provider.TruncateTable(tblName);
        }

        public void DropTable(TableRef tblName)
        {
            Log.DebugFormat("Dropping table [{0}]", tblName);
            _provider.DropTable(tblName);
        }

        public void DropColumn(TableRef tblName, string colName)
        {
            Log.DebugFormat("Dropping column [{0}].[{1}]", tblName, colName);
            _provider.DropColumn(tblName, colName);
        }

        public void DropFKConstraint(TableRef tblName, string fkName)
        {
            Log.DebugFormat("Dropping foreign key constraint [{0}].[{1}]", tblName, fkName);
            _provider.DropFKConstraint(tblName, fkName);
        }

        public void DropTrigger(TableRef objName, string triggerName)
        {
            Log.DebugFormat("Dropping trigger [{0}]", triggerName);
            _provider.DropTrigger(objName, triggerName);
        }

        public void DropView(TableRef viewName)
        {
            Log.DebugFormat("Dropping view [{0}]", viewName);
            _provider.DropView(viewName);
        }

        public void DropProcedure(string procName)
        {
            Log.DebugFormat("Dropping procedure [{0}]", procName);
            _provider.DropProcedure(procName);
        }

        public void DropIndex(TableRef tblName, string idxName)
        {
            _provider.DropIndex(tblName, idxName);
        }

        public void DropAllObjects()
        {
            _provider.DropAllObjects();
        }

        public void CopyColumnData(TableRef srcTblName, string srcColName, TableRef tblName, string colName)
        {
            _provider.CopyColumnData(srcTblName, srcColName, tblName, colName);
        }

        public void MigrateFKs(TableRef srcTblName, string srcColName, TableRef tblName, string colName)
        {
            _provider.MigrateFKs(srcTblName, srcColName, tblName, colName);
        }

        public void InsertFKs(TableRef srcTblName, string srcColName, TableRef tblName, string colName, string fkColName)
        {
            _provider.InsertFKs(srcTblName, srcColName, tblName, colName, fkColName);
        }

        public void CopyFKs(TableRef srcTblName, string srcColName, TableRef destTblName, string destColName, string srcFKColName)
        {
            _provider.CopyFKs(srcTblName, srcColName, destTblName, destColName, srcFKColName);
        }

        public void CreateIndex(TableRef tblName, string idxName, bool unique, bool clustered, params string[] columns)
        {
            _provider.CreateIndex(tblName, idxName, unique, clustered, columns);
        }

        public void CreateUpdateRightsTrigger(string triggerName, TableRef tblName, List<RightsTrigger> tblList)
        {
            _provider.CreateUpdateRightsTrigger(triggerName, tblName, tblList);
        }

        public void CreateRightsViewUnmaterialized(TableRef viewName, TableRef tblName, TableRef tblNameRights, IList<ACL> acls)
        {
            _provider.CreateRightsViewUnmaterialized(viewName, tblName, tblNameRights, acls);
        }

        public void CreateEmptyRightsViewUnmaterialized(TableRef viewName)
        {
            _provider.CreateEmptyRightsViewUnmaterialized(viewName);
        }

        public void CreateRefreshRightsOnProcedure(string procName, TableRef viewUnmaterializedName, TableRef tblName, TableRef tblNameRights)
        {
            _provider.CreateRefreshRightsOnProcedure(procName, viewUnmaterializedName, tblName, tblNameRights);
        }

        public void ExecRefreshRightsOnProcedure(string procName)
        {
            _provider.ExecRefreshRightsOnProcedure(procName);
        }

        public void CreatePositionColumnValidCheckProcedures(ILookup<TableRef, KeyValuePair<TableRef, string>> refSpecs)
        {
            _provider.CreatePositionColumnValidCheckProcedures(refSpecs);
        }

        public IDataReader ReadTableData(TableRef tblName, IEnumerable<string> colNames)
        {
            return _provider.ReadTableData(tblName, colNames);
        }

        public IDataReader ReadTableData(string sql)
        {
            return _provider.ReadTableData(sql);
        }

        public IDataReader ReadJoin(TableRef tblName, IEnumerable<ProjectionColumn> colNames, IEnumerable<Join> joins)
        {
            return _provider.ReadJoin(tblName, colNames, joins);
        }

        public void WriteTableData(TableRef destTbl, IDataReader source, IEnumerable<string> colNames)
        {
            _provider.WriteTableData(destTbl, source, colNames);
        }

        public void WriteTableData(TableRef destTbl, IEnumerable<string> colNames, IEnumerable values)
        {
            _provider.WriteTableData(destTbl, colNames, values);
        }

        public void RefreshDbStats()
        {
            _provider.RefreshDbStats();
        }

        public void ExecuteSqlResource(Type type, string scriptResourceName)
        {
            _provider.ExecuteSqlResource(type, scriptResourceName);
        }

        public void Dispose()
        {
            _provider.Dispose();
        }

        public void EnsureInfrastructure()
        {
            Log.Debug("Ensuring Infrastructure is available on target");
            _provider.EnsureInfrastructure();
        }

        public void WipeDatabase()
        {
            using (Log.InfoTraceMethodCall("Wiping database"))
            {
                _provider.WipeDatabase();
            }
        }
    }
}
