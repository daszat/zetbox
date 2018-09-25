// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.Server.SchemaManagement
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using Zetbox.API.Server;
    using Zetbox.API.Utils;

    /// <summary>
    /// Logs all calls to the underlying ISchemaProvider which are expected to go to the database.
    /// Results from read only calls are logged on Debug level, calls that actually modify the database are logged on Info level.
    /// </summary>
    public class LoggingSchemaProviderAdapter
        : ISchemaProvider
    {
        private readonly log4net.ILog Log;
        private readonly ISchemaProvider _provider;

        public LoggingSchemaProviderAdapter(ISchemaProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");
            _provider = provider;
            Log = log4net.LogManager.GetLogger("Zetbox.Server.Schema.Report." + provider.ConfigName);
        }

        #region Logging

        private void LogExistance(string type, DboRef dboName, bool result, string spec = "")
        {
            LogExistance(type, dboName.ToString(), result, spec);
        }

        private void LogExistance(string type, string name, bool result, string spec = "")
        {
            if (Log.IsDebugEnabled)
                Log.DebugFormat(string.Join(" ", type, name, result ? "exists" : "does not exist", spec));
        }

        private void LogNameFetcher(string type, IEnumerable<DboRef> result)
        {
            LogNameFetcher(type, result.Select(r => r.ToString()));
        }

        private void LogNameFetcher(string type, IEnumerable<Column> result)
        {
            LogNameFetcher(type, result.Select(r => r.ToString()));
        }

        private void LogNameFetcher(string type, IEnumerable<string> result)
        {
            if (Log.IsDebugEnabled)
            {
                var count = result.Count();
                Log.DebugFormat("Fetched {0} {1} names: <{2}{3}>", count, type, string.Join(", ", result.Take(3)), count > 3 ? ", ..." : string.Empty);
            }
        }

        #endregion

        public string ConfigName { get { return _provider.ConfigName; } }

        public string AdoNetProvider { get { return _provider.AdoNetProvider; } }

        public string ManifestToken { get { return _provider.ManifestToken; } }

        public bool IsStorageProvider { get { return _provider.IsStorageProvider; } }

        public string GetSavedSchema() { return _provider.GetSavedSchema(); }

        public void SaveSchema(string schema) { _provider.SaveSchema(schema); }

        public void Open(string connectionString)
        {
            Log.InfoFormat("Opening connection to [{0}]", _provider.GetSafeConnectionString(connectionString));
            _provider.Open(connectionString);
        }

        public void Close()
        {
            Log.Info("Closing connection");
            _provider.Close();
        }

        public string GetSafeConnectionString() { return _provider.GetSafeConnectionString(); }

        public string GetSafeConnectionString(string connectionString) { return _provider.GetSafeConnectionString(connectionString); }

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
            Log.InfoFormat("Rolling back Transaction {0}", _currentTxId);
            _provider.RollbackTransaction();
        }

        public void DblinkConnect(TableRef tblName)
        {
            Log.InfoFormat("Executing DBLINK to {0}", tblName);
            _provider.DblinkConnect(tblName);
        }

        public string DbTypeToNative(DbType type) { return _provider.DbTypeToNative(type); }

        public DbType NativeToDbType(string type) { return _provider.NativeToDbType(type); }

        public TableRef GetTableName(string schemaName, string tblName) { return _provider.GetTableName(schemaName, tblName); }

        public bool CheckTableExists(TableRef tblName)
        {
            var result = _provider.CheckTableExists(tblName);
            LogExistance("Table", tblName, result);
            return result;
        }

        public IEnumerable<TableRef> GetTableNames()
        {
            var result = _provider.GetTableNames();
            LogNameFetcher("table", result);
            return result;
        }

        public IEnumerable<TriggerRef> GetTriggerNames()
        {
            var result = _provider.GetTriggerNames();
            LogNameFetcher("trigger", result);
            return result;
        }

        public IEnumerable<TableRef> GetViewNames()
        {
            var result = _provider.GetViewNames();
            LogNameFetcher("view", result);
            return result;
        }

        public string GetViewDefinition(TableRef view)
        {
            var result = _provider.GetViewDefinition(view);
            Log.DebugFormat("Fetched view {0} definition:\n{1}", view, result);
            return result;
        }

        public ProcRef GetProcedureName(string schemaName, string procName)
        {
            return _provider.GetProcedureName(schemaName, procName);
        }

        public IEnumerable<ProcRef> GetProcedureNames()
        {
            var result = _provider.GetProcedureNames();
            LogNameFetcher("procedure", result);
            return result;
        }

        public string GetProcedureDefinition(ProcRef proc)
        {
            var result = _provider.GetProcedureDefinition(proc);
            Log.DebugFormat("Fetched procedure {0} definition:\n{1}", proc, result);
            return result;
        }

        public bool CheckColumnExists(TableRef tblName, string colName)
        {
            var result = _provider.CheckColumnExists(tblName, colName);
            LogExistance("Column", colName, result, "in " + tblName);
            return result;
        }

        public IEnumerable<string> GetTableColumnNames(TableRef tblName)
        {
            var result = _provider.GetTableColumnNames(tblName);
            LogNameFetcher(string.Format("column names of table {0}", tblName), result);
            return result;
        }

        public IEnumerable<Column> GetTableColumns(TableRef tblName)
        {
            var result = _provider.GetTableColumns(tblName);
            LogNameFetcher(string.Format("column defs of table {0}", tblName), result);
            return result;
        }

        public bool CheckFKConstraintExists(TableRef tblName, string fkName)
        {
            var result = _provider.CheckFKConstraintExists(tblName, fkName);
            LogExistance("FK", fkName, result, "in " + tblName);
            return result;
        }

        public bool CheckIndexExists(TableRef tblName, string idxName)
        {
            var result = _provider.CheckIndexExists(tblName, idxName);
            LogExistance("Index", idxName, result, "in " + tblName);
            return result;
        }

        public bool CheckTableContainsData(TableRef tblName)
        {
            var result = _provider.CheckTableContainsData(tblName);
            Log.DebugFormat(result ? "Table {0} contains data" : "Table {0} is empty", tblName);
            return result;
        }

        public bool CheckTableContainsData(TableRef tblName, IEnumerable<string> discriminatorFilter)
        {
            var result = _provider.CheckTableContainsData(tblName, discriminatorFilter);
            Log.DebugFormat(result ? "Table {0} contains data of type {1}" : "Table {0} contains no data of type {1}", tblName, discriminatorFilter);
            return result;
        }

        public bool CheckColumnContainsNulls(TableRef tblName, string colName)
        {
            var result = _provider.CheckColumnContainsNulls(tblName, colName);
            Log.DebugFormat(result ? "Column {0} of {1} contains nulls" : "Column {0} of {1} has no nulls", colName, tblName);
            return result;
        }

        public bool CheckFKColumnContainsUniqueValues(TableRef tblName, string colName)
        {
            var result = _provider.CheckFKColumnContainsUniqueValues(tblName, colName);
            Log.DebugFormat(result ? "Column {0} of {1} contains unique values" : "Column {0} of {1} has duplicate values", colName, tblName);
            return result;
        }

        public bool CheckColumnContainsValues(TableRef tblName, string colName)
        {
            var result = _provider.CheckColumnContainsValues(tblName, colName);
            Log.DebugFormat(result ? "Column {0} of {1} contains values" : "Column {0} of {1} has no values", colName, tblName);
            return result;
        }

        public long CountRows(TableRef tblName)
        {
            var result = _provider.CountRows(tblName);
            Log.DebugFormat("Table {0} has {1} rows (counted)", tblName, result);
            return result;
        }

        public bool CheckViewExists(TableRef viewName)
        {
            var result = _provider.CheckViewExists(viewName);
            LogExistance("View", viewName, result);
            return result;
        }

        public bool CheckTriggerExists(TriggerRef triggerName)
        {
            var result = _provider.CheckTriggerExists(triggerName);
            LogExistance("Trigger", triggerName, result);
            return result;
        }

        public bool CheckProcedureExists(ProcRef procName)
        {
            var result = _provider.CheckProcedureExists(procName);
            LogExistance("Procedure", procName, result);
            return result;
        }

        public bool CheckPositionColumnValidity(TableRef tblName, string fkName, string positionColumnName)
        {
            var result = _provider.CheckPositionColumnValidity(tblName, fkName, positionColumnName);
            Log.DebugFormat(result ? "Position column {0} of {1} is valid" : "Position column {0} of {1} is NOT valid", positionColumnName, tblName);
            return result;
        }

        public bool RepairPositionColumn(TableRef tblName, string positionColumnName)
        {
            using (Log.InfoTraceMethodCallFormat("RepairPositionColumn", "column {0} of {1}", positionColumnName, tblName))
            {
                var result = _provider.RepairPositionColumn(tblName, positionColumnName);
                Log.Info(result ? "Repairing succeeded" : "Repairing failed");
                return result;
            }
        }

        public bool GetIsColumnNullable(TableRef tblName, string colName)
        {
            var result = _provider.GetIsColumnNullable(tblName, colName);
            Log.DebugFormat(result ? "Column {0} on {1} is nullable" : "Column {0} on {1} is not nullable", colName, tblName);
            return result;
        }

        public bool GetHasColumnDefaultValue(TableRef tblName, string colName)
        {
            var result = _provider.GetHasColumnDefaultValue(tblName, colName);
            Log.DebugFormat(result ? "Column {0} on {1} has a default value" : "Column {0} on {1} has no default value", colName, tblName);
            return result;
        }

        public int GetColumnMaxLength(TableRef tblName, string colName)
        {
            var result = _provider.GetColumnMaxLength(tblName, colName);
            Log.DebugFormat("Column {0} on {1} has max length {2}", colName, tblName, result);
            return result;
        }

        public void CreateTable(TableRef tblName, IEnumerable<Column> cols)
        {
            if (Log.IsInfoEnabled)
                Log.InfoFormat("CreateTable {0} with {1} columns", tblName, cols.Count());
            _provider.CreateTable(tblName, cols);
        }

        public void CreateTable(TableRef tblName, bool idAsIdentityColumn)
        {
            Log.InfoFormat("CreateTable [{0}] {1} and a primary key",
                tblName,
                idAsIdentityColumn ? "with identity" : "without identity");
            _provider.CreateTable(tblName, idAsIdentityColumn);
        }

        public void CreateTable(TableRef tblName, bool idAsIdentityColumn, bool createPrimaryKey)
        {
            if (tblName == null)
                throw new ArgumentNullException("tblName");

            Log.InfoFormat("CreateTable [{0}] {1} {2}",
                tblName,
                idAsIdentityColumn ? "with identity" : "without identity",
                createPrimaryKey ? "and a primary key" : "and no primary key");
            _provider.CreateTable(tblName, idAsIdentityColumn, createPrimaryKey);
        }

        public void CreateColumn(TableRef tblName, string colName, DbType type, int size, int scale, bool isNullable, params DatabaseConstraint[] constraints)
        {
            Log.InfoFormat("CreateColumn(tblName={0}, colName={1}, type={2}, size={3}, scale={4}, isNullable={5}, constraints=[{6}])", tblName, colName, type, size, scale, isNullable, constraints != null ? string.Join(", ", constraints.Where(c => c != null).Select(c => c.ToString())) : string.Empty);
            _provider.CreateColumn(tblName, colName, type, size, scale, isNullable, constraints);
        }

        public void AlterColumn(TableRef tblName, string colName, DbType type, int size, int scale, bool isNullable, params DatabaseConstraint[] constraints)
        {
            Log.InfoFormat("AlterColumn(tblName={0}, colName={1}, type={2}, size={3}, scale={4}, isNullable={5}, constraints=[{6}])", tblName, colName, type, size, scale, isNullable, constraints != null ? string.Join(", ", constraints.Where(c => c != null).Select(c => c.ToString())) : string.Empty);
            _provider.AlterColumn(tblName, colName, type, size, scale, isNullable, constraints);
        }

        public IEnumerable<TableConstraintNamePair> GetFKConstraintNames()
        {
            var result = _provider.GetFKConstraintNames();
            LogNameFetcher("fk constraints", result.Select(p => p.ToString()));
            return result;
        }

        public void CreateFKConstraint(TableRef tblName, TableRef refTblName, string colName, string constraintName, bool onDeleteCascade)
        {
            Log.InfoFormat("Creating foreign key constraint [{0}].[{1}] -> [{2}].ID", tblName, colName, refTblName);
            _provider.CreateFKConstraint(tblName, refTblName, colName, constraintName, onDeleteCascade);
        }

        public void RenameTable(TableRef oldTblName, TableRef newTblName)
        {
            Log.InfoFormat("Renaming table [{0}] -> [{1}]", oldTblName, newTblName);
            _provider.RenameTable(oldTblName, newTblName);
        }

        public void RenameColumn(TableRef tblName, string oldColName, string newColName)
        {
            Log.InfoFormat("Renaming column on [{0}]: [{1}] -> [{2}]", tblName, oldColName, newColName);
            _provider.RenameColumn(tblName, oldColName, newColName);
        }

        public void RenameDiscriminatorValue(TableRef tblName, string oldValue, string newValue)
        {
            Log.InfoFormat("Renaming discriminator value on [{0}]: [{1}] -> [{2}]", tblName, oldValue, newValue);
            _provider.RenameDiscriminatorValue(tblName, oldValue, newValue);
        }

        public void RenameFKConstraint(TableRef tblName, string oldConstraintName, TableRef refTblName, string colName, string newConstraintName, bool onDeleteCascade)
        {
            Log.InfoFormat("Renaming fk constraint on [{0}]: [{1}] -> [{2}].[{3}] to [{4}] ({5})", tblName, oldConstraintName, refTblName, colName, newConstraintName, onDeleteCascade ? "CASCADING" : "not CASCADING");
            _provider.RenameFKConstraint(tblName, oldConstraintName, refTblName, colName, newConstraintName, onDeleteCascade);
        }

        public void TruncateTable(TableRef tblName)
        {
            Log.InfoFormat("Truncating table [{0}]", tblName);
            _provider.TruncateTable(tblName);
        }

        public void DropTable(TableRef tblName)
        {
            Log.DebugFormat("Dropping table [{0}]", tblName);
            _provider.DropTable(tblName);
        }

        public void DropColumn(TableRef tblName, string colName)
        {
            Log.InfoFormat("Dropping column [{0}].[{1}]", tblName, colName);
            _provider.DropColumn(tblName, colName);
        }

        public void DropFKConstraint(TableRef tblName, string constraintName)
        {
            Log.InfoFormat("Dropping foreign key constraint [{0}].[{1}]", tblName, constraintName);
            _provider.DropFKConstraint(tblName, constraintName);
        }

        public void DropTrigger(TriggerRef triggerName)
        {
            Log.InfoFormat("Dropping trigger [{0}]", triggerName);
            _provider.DropTrigger(triggerName);
        }

        public void DropView(TableRef viewName)
        {
            Log.InfoFormat("Dropping view [{0}]", viewName);
            _provider.DropView(viewName);
        }

        public void DropProcedure(ProcRef procName)
        {
            Log.InfoFormat("Dropping procedure [{0}]", procName);
            _provider.DropProcedure(procName);
        }

        public void DropIndex(TableRef tblName, string idxName)
        {
            Log.InfoFormat("Dropping index [{0}].[{1}]", tblName, idxName);
            _provider.DropIndex(tblName, idxName);
        }

        public void RenameIndex(TableRef tblName, string oldIdxName, string newIdxName)
        {
            Log.InfoFormat("Renaming index [{0}].[{1}] -> [{2}]", tblName, oldIdxName, newIdxName);
            _provider.RenameIndex(tblName, oldIdxName, newIdxName);
        }

        public void DropAllObjects()
        {
            using (Log.InfoTraceMethodCall("Dropping all database objects"))
            {
                _provider.DropAllObjects();
            }
        }

        public void CopyColumnData(TableRef srcTblName, string srcColName, TableRef tblName, string colName)
        {
            using (Log.InfoTraceMethodCallFormat("CopyColumnData", "copying column data from [{0}].[{1}] -> [{2}].[{3}]", srcTblName, srcColName, tblName, colName))
            {
                _provider.CopyColumnData(srcTblName, srcColName, tblName, colName);
            }
        }

        public void CopyColumnData(TableRef srcTblName, string[] srcColName, TableRef tblName, string[] colName, string discriminatorValue)
        {
            using (Log.InfoTraceMethodCallFormat("CopyColumnData", "copying column data from [{0}].[{1}] -> [{2}].[{3}] using discriminator [{4}]", srcTblName, srcColName, tblName, colName, discriminatorValue))
            {
                _provider.CopyColumnData(srcTblName, srcColName, tblName, colName, discriminatorValue);
            }
        }

        public void MapColumnData(TableRef srcTblName, string[] srcColNames, TableRef tblName, string[] colNames, Dictionary<object, object>[] mappings)
        {
            using (Log.InfoTraceMethodCallFormat("MapColumnData", "mapping {0} columns from [{1}] to [{2}]", srcColNames == null ? 0 : srcColNames.Length, srcTblName, tblName))
            {
                if (Log.IsDebugEnabled && srcColNames != null && colNames != null && mappings != null)
                {
                    for (int i = 0; i < srcColNames.Length; i++)
                    {
                        Log.DebugFormat("mapping column data from [{0}].[{1}] -> [{2}].[{3}] using mapping [{4}]",
                            srcTblName, srcColNames[i],
                            tblName, colNames[i],
                            string.Join(", ", mappings[i].Select(kvp => string.Format("{0} => {1}", kvp.Key, kvp.Value))));
                    }
                }
                _provider.MapColumnData(srcTblName, srcColNames, tblName, colNames, mappings);
            }
        }

        public object MappingDefaultSourceValue { get { return _provider.MappingDefaultSourceValue; } }

        public void MigrateFKs(TableRef srcTblName, string srcColName, TableRef tblName, string colName)
        {
            using (Log.InfoTraceMethodCallFormat("MigrateFKs", "Migrating FK data from [{0}].[{1}] to [{2}].[{3}]", srcTblName, srcColName, tblName, colName))
            {
                _provider.MigrateFKs(srcTblName, srcColName, tblName, colName);
            }
        }

        public void InsertFKs(TableRef srcTblName, string srcColName, TableRef tblName, string colName, string fkColName)
        {
            using (Log.InfoTraceMethodCallFormat("InsertFKs", "Inserting FK data from [{0}]([{1}], [ID]) into [{2}]([{3}], [{4}])", srcTblName, srcColName, tblName, colName, fkColName))
            {
                _provider.InsertFKs(srcTblName, srcColName, tblName, colName, fkColName);
            }
        }

        public void CopyFKs(TableRef srcTblName, string srcColName, TableRef destTblName, string destColName, string srcFKColName)
        {
            using (Log.InfoTraceMethodCallFormat("CopyFKs", "Copying FK data from [{0}]([{1}]) to [{2}]([{3}]) using [{4}] as FK", srcTblName, srcColName, destTblName, destColName, srcFKColName))
            {
                _provider.CopyFKs(srcTblName, srcColName, destTblName, destColName, srcFKColName);
            }
        }
        public void CreateIndex(TableRef tblName, string idxName, bool unique, bool clustered, params string[] columns)
        {
            using (Log.InfoTraceMethodCallFormat("CreateIndex", "Creating index [{0}].[{1}]: unique={2} clustered={3} columns=[{4}]", tblName, idxName, unique, clustered, string.Join(", ", columns)))
            {
                _provider.CreateIndex(tblName, idxName, unique, clustered, columns);
            }
        }

        public void CreateUpdateRightsTrigger(TriggerRef triggerName, TableRef tblName, List<RightsTrigger> tblList, List<string> dependingCols)
        {
            if (Log.IsInfoEnabled)
                Log.InfoFormat("Creating update rights trigger [{0}] for [{1}] checking [{2}]", triggerName, tblName, string.Join(", ", dependingCols));
            if (Log.IsDebugEnabled && tblList != null)
            {
                foreach (var tbl in tblList)
                {
                    Log.DebugFormat("Updating [{0}] via [{1}]", tbl.TblName, string.Join(" => ", tbl.ObjectRelations.Select(r => r.ToString())));
                    Log.DebugFormat("                   [{0}]", string.Join(" => ", tbl.IdentityRelations.Select(r => r.ToString())));
                }
            }
            _provider.CreateUpdateRightsTrigger(triggerName, tblName, tblList, dependingCols);
        }

        public void CreateRightsViewUnmaterialized(TableRef viewName, TableRef tblName, TableRef tblNameRights, IList<ACL> acls)
        {
            Log.InfoFormat("Creating unmaterialized rights view for [{0}]", tblName);
            _provider.CreateRightsViewUnmaterialized(viewName, tblName, tblNameRights, acls);
        }

        public void CreateEmptyRightsViewUnmaterialized(TableRef viewName)
        {
            Log.InfoFormat("Creating *empty* unmaterialized rights view [{0}]", viewName);
            _provider.CreateEmptyRightsViewUnmaterialized(viewName);
        }

        public void CreateRefreshRightsOnProcedure(ProcRef procName, TableRef viewUnmaterializedName, TableRef tblName, TableRef tblNameRights)
        {
            Log.InfoFormat("Creating refresh rights procedure for [{0}]", tblName);
            _provider.CreateRefreshRightsOnProcedure(procName, viewUnmaterializedName, tblName, tblNameRights);
        }

        public void CreateRefreshAllRightsProcedure(List<ProcRef> refreshProcNames)
        {
            Log.InfoFormat("Creating refresh all rights procedure for [{0}]", string.Join(", ", refreshProcNames.Select(p => p.ToString())));
            _provider.CreateRefreshAllRightsProcedure(refreshProcNames);
        }

        public void ExecRefreshAllRightsProcedure()
        {
            using (Log.InfoTraceMethodCall("RefreshRights", "Refreshing all rights"))
            {
                _provider.ExecRefreshAllRightsProcedure();
            }
        }

        public void ExecRefreshRightsOnProcedure(ProcRef procName)
        {
            using (Log.InfoTraceMethodCallFormat("RefreshRights", "Refreshing [{0}]", procName))
            {
                _provider.ExecRefreshRightsOnProcedure(procName);
            }
        }

        public void CreatePositionColumnValidCheckProcedures(ILookup<TableRef, KeyValuePair<TableRef, string>> refSpecs)
        {
            Log.Info("Creating position-valid-check procedures");
            _provider.CreatePositionColumnValidCheckProcedures(refSpecs);
        }

        public void CreateSequenceNumberProcedure()
        {
            Log.Info("Creating sequence number procedure");
            _provider.CreateSequenceNumberProcedure();
        }

        public void CreateContinuousSequenceNumberProcedure()
        {
            Log.Info("Creating continuous sequence number procedure");
            _provider.CreateContinuousSequenceNumberProcedure();
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

        public void WriteDefaultValue(TableRef tblName, string colName, object value)
        {
            _provider.WriteDefaultValue(tblName, colName, value);
        }

        public void WriteDefaultValue(TableRef tblName, string colName, object value, IEnumerable<string> discriminatorFilter)
        {
            _provider.WriteDefaultValue(tblName, colName, value, discriminatorFilter);
        }

        public void WriteGuidDefaultValue(TableRef tblName, string colName)
        {
            _provider.WriteGuidDefaultValue(tblName, colName);
        }

        public void WriteGuidDefaultValue(TableRef tblName, string colName, IEnumerable<string> discriminatorFilter)
        {
            _provider.WriteGuidDefaultValue(tblName, colName, discriminatorFilter);
        }

        public void RefreshDbStats()
        {
            using (Log.InfoTraceMethodCall("RefreshDbStats"))
            {
                _provider.RefreshDbStats();
            }
        }

        public void ExecuteSqlResource(Type type, string scriptResourceNameFormat)
        {
            using (Log.InfoTraceMethodCallFormat("ExecuteSqlResource", "Executing [{0}] for [{1}]", scriptResourceNameFormat, type == null ? "(null)" : type.FullName))
            {
                _provider.ExecuteSqlResource(type, scriptResourceNameFormat);
            }
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

        public bool CheckDatabaseExists(string dbName)
        {
            Log.Debug("Checking whether database [{0}] exists");
            return _provider.CheckDatabaseExists(dbName);
        }

        public void CreateDatabase(string dbName)
        {
            using (Log.InfoTraceMethodCallFormat("CreateDatabase", "Creating Database [{0}]", dbName))
            {
                _provider.CreateDatabase(dbName);
            }
        }

        public void DropDatabase(string dbName)
        {
            using (Log.InfoTraceMethodCallFormat("DropDatabase", "Dropping Database [{0}]", dbName))
            {
                _provider.DropDatabase(dbName);
            }
        }

        public bool CheckSchemaExists(string schemaName)
        {
            var result = _provider.CheckSchemaExists(schemaName);
            LogExistance("schema", schemaName, result);
            return result;
        }

        public IEnumerable<string> GetSchemaNames()
        {
            var result = _provider.GetSchemaNames();
            LogNameFetcher("schema", result);
            return result;
        }

        public void CreateSchema(string schemaName)
        {
            Log.InfoFormat("Creating schema [{0}]", schemaName);
            _provider.CreateSchema(schemaName);
        }

        public void DropSchema(string schemaName, bool force)
        {
            using (Log.InfoTraceMethodCallFormat("DropSchema", "Dropping schema [{0}], force={1}", schemaName, force))
            {
                _provider.DropSchema(schemaName, force);
            }
        }

        public ProcRef GetFunctionName(string schemaName, string funcName)
        {
            return _provider.GetFunctionName(schemaName, funcName);
        }

        public IEnumerable<ProcRef> GetFunctionNames()
        {
            var result = _provider.GetFunctionNames();
            LogNameFetcher("function", result);
            return result;
        }

        public bool CheckFunctionExists(ProcRef funcName)
        {
            var result = _provider.CheckFunctionExists(funcName);
            LogExistance("Function", funcName, result);
            return result;
        }

        public void DropFunction(ProcRef funcName)
        {
            Log.InfoFormat("Dropping function [{0}]", funcName);
            _provider.DropFunction(funcName);
        }

        public bool CheckIndexPossible(TableRef tblName, string idxName, bool unique, bool clustered, params string[] columns)
        {
            using (Log.DebugTraceMethodCallFormat("CheckIndexPossible", "tblName=[{0}], idxName=[{1}], unique={2}, clustered={3}, columns=[{4}]", tblName, idxName, unique, clustered, string.Join(", ", columns)))
            {
                var result = _provider.CheckIndexPossible(tblName, idxName, unique, clustered, columns);
                Log.Debug(result ? "possible" : "not possible");
                return result;
            }
        }

        public bool CheckCheckConstraintPossible(TableRef tblName, string colName, string newConstraintName, Dictionary<List<string>, Expression<Func<string, bool>>> checkExpressions)
        {
            using (Log.DebugTraceMethodCallFormat("CheckCheckConstraintPossible", "tblName=[{0}], colName=[{1}], newConstraintName={2}, checkExpressions=[{3}]", tblName, colName, newConstraintName, checkExpressions))
            {
                var result = _provider.CheckCheckConstraintPossible(tblName, colName, newConstraintName, checkExpressions);
                Log.Debug(result ? "possible" : "not possible");
                return result;
            }
        }

        public bool CheckCheckConstraintExists(TableRef tblName, string constraintName)
        {
            var result = _provider.CheckCheckConstraintExists(tblName, constraintName);
            LogExistance("check constraint", constraintName, result, "on " + tblName);
            return result;
        }

        public void CreateCheckConstraint(TableRef tblName, string colName, string newConstraintName, Dictionary<List<string>, Expression<Func<string, bool>>> checkExpressions)
        {
            using (Log.InfoTraceMethodCallFormat("CreateCheckConstraint", "tblName=[{0}], colName=[{1}], newConstraintName={2}, checkExpressions=[{3}]", tblName, colName, newConstraintName, checkExpressions))
            {
                _provider.CreateCheckConstraint(tblName, colName, newConstraintName, checkExpressions);
            }
        }

        public void DropCheckConstraint(TableRef tblName, string constraintName)
        {
            Log.InfoFormat("Dropping check constraint [{0}] from [{1}]", constraintName, tblName);
            _provider.DropCheckConstraint(tblName, constraintName);
        }

        public string QuoteIdentifier(string name)
        {
            return _provider.QuoteIdentifier(name);
        }
    }
}
