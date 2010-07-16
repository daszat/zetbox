
namespace Kistl.Server.SchemaManagement.OleDbProvider
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.Server;
    using Kistl.API.Utils;
    using System.Collections;

    public class OleDb
        : ISchemaProvider
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server.Schema.OLEDB");
        private readonly static log4net.ILog QueryLog = log4net.LogManager.GetLogger("Kistl.Server.Schema.OLEDB.Queries");

        protected OleDbConnection db;
        protected OleDbTransaction tx;
        protected string quotePrefix;
        protected string quoteSuffix;

        public string ConfigName { get { return "OLEDB"; } }
        public string AdoNetProvider { get { return null; } }
        public string ManifestToken { get { return null; } }

        public void Open(string connectionString)
        {
            if (db != null) throw new InvalidOperationException("Database already opened");
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException("connectionString");

            db = new OleDbConnection(connectionString);
            db.Open();

            DataTable literals = db.GetOleDbSchemaTable(OleDbSchemaGuid.DbInfoLiterals, new object[] { });
            literals.PrimaryKey = new DataColumn[] { literals.Columns["Literal"] };
            DataRow row = null;
            row = literals.Rows.Find((int)OleDbLiteral.Quote_Prefix);
            if (row != null) quotePrefix = row["LiteralValue"] as string;
            row = literals.Rows.Find((int)OleDbLiteral.Quote_Suffix);
            if (row != null) quoteSuffix = row["LiteralValue"] as string;
        }

        public void BeginTransaction()
        {
            if (tx != null) throw new InvalidOperationException("Transaction is already running");
            tx = db.BeginTransaction();
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

        public void Dispose()
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

        private string Quote(string val)
        {
            return string.Format("{0}{1}{2}", quotePrefix ?? string.Empty, val, quoteSuffix ?? string.Empty);
        }

        private string FormatTableName(TableRef tbl)
        {
            return String.Format("[{0}].[{1}]", tbl.Schema, tbl.Name);
        }

        public string GetSavedSchema()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// TODO: Was ist die Version?
        /// </summary>
        /// <param name="schema"></param>
        public void SaveSchema(string schema)
        {
            throw new NotSupportedException();
        }

        public bool CheckTableExists(TableRef tblName)
        {
            using (var cmd = new OleDbCommand("SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(@table) AND type IN (N'U')", db, tx))
            {
                cmd.Parameters.AddWithValue("@table", tblName.Name);
                QueryLog.Debug(cmd.CommandText);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public bool CheckColumnExists(TableRef tblName, string colName)
        {
            using (var cmd = new OleDbCommand(@"SELECT COUNT(*) FROM sys.objects o INNER JOIN sys.columns c ON c.object_id=o.object_id
	                                            WHERE o.object_id = OBJECT_ID(@table) 
		                                            AND o.type IN (N'U')
		                                            AND c.Name = @column", db, tx))
            {
                cmd.Parameters.AddWithValue("@table", tblName.Name);
                cmd.Parameters.AddWithValue("@column", colName);
                QueryLog.Debug(cmd.CommandText);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public bool GetIsColumnNullable(TableRef tblName, string colName)
        {
            using (var cmd = new OleDbCommand(@"SELECT c.is_nullable FROM sys.objects o INNER JOIN sys.columns c ON c.object_id=o.object_id
	                                            WHERE o.object_id = OBJECT_ID(@table) 
		                                            AND o.type IN (N'U')
		                                            AND c.Name = @column", db, tx))
            {
                cmd.Parameters.AddWithValue("@table", tblName);
                cmd.Parameters.AddWithValue("@column", colName);
                QueryLog.Debug(cmd.CommandText);
                return (bool)cmd.ExecuteScalar();
            }
        }

        public bool CheckFKConstraintExists(string fkName)
        {
            throw new NotImplementedException();
        }

        public bool CheckViewExists(TableRef viewName)
        {
            using (var cmd = new OleDbCommand("SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(@view) AND type IN (N'V')", db, tx))
            {
                cmd.Parameters.AddWithValue("@view", viewName);
                QueryLog.Debug(cmd.CommandText);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public bool CheckTriggerExists(TableRef objName, string triggerName)
        {
            throw new NotSupportedException();
        }

        public bool CheckProcedureExists(string procName)
        {
            throw new NotSupportedException();
        }

        public bool CheckTableContainsData(TableRef tblName)
        {
            using (var cmd = new OleDbCommand(string.Format("SELECT COUNT(*) FROM {0}", FormatTableName(tblName), db, tx)))
            {
                QueryLog.Debug(cmd.CommandText);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public bool CheckColumnContainsNulls(TableRef tblName, string colName)
        {
            using (var cmd = new OleDbCommand(string.Format("SELECT COUNT(*) FROM (SELECT TOP 1 [{1}] FROM [{0}] WHERE [{1}] IS NULL) AS nulls", tblName, colName), db, tx))
            {
                QueryLog.Debug(cmd.CommandText);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public bool CheckColumnContainsUniqueValues(TableRef tblName, string colName)
        {
            using (var cmd = new OleDbCommand(string.Format("SELECT COUNT(*) FROM (SELECT TOP 1 [{1}] FROM [{0}] WHERE [{1}] IS NOT NULL GROUP BY [{1}] HAVING COUNT([{1}]) > 1) AS tbl", tblName, colName), db, tx))
            {
                QueryLog.Debug(cmd.CommandText);
                return (int)cmd.ExecuteScalar() == 0;
            }
        }

        public bool CheckColumnContainsValues(TableRef tblName, string colName)
        {
            using (var cmd = new OleDbCommand(string.Format("SELECT COUNT(*) FROM (SELECT TOP 1 [{1}] FROM [{0}] WHERE [{1}] IS NOT NULL) AS nulls", tblName, colName), db, tx))
            {
                QueryLog.Debug(cmd.CommandText);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public bool CheckPositionColumnValidity(TableRef tblName, string posName)
        {
            throw new NotSupportedException();
        }

        public bool RepairPositionColumn(TableRef tblName, string posName)
        {
            throw new NotSupportedException();
        }

        public int GetColumnMaxLength(TableRef tblName, string colName)
        {
            throw new NotImplementedException();
        }

        public TableRef GetQualifiedTableName(string tblName)
        {
            return new TableRef(db.Database, "dbo", tblName);
        }

        public IEnumerable<TableRef> GetTableNames()
        {
            QueryLog.Debug("GetSchema(TABLES)");
            var tables = db.GetSchema(OleDbMetaDataCollectionNames.Tables, new string[] { null, null, null, "TABLE" });
            foreach (DataRow tbl in tables.Rows)
            {
                yield return GetQualifiedTableName((string)tbl["TABLE_NAME"]);
            }
        }

        public IEnumerable<TableConstraintNamePair> GetFKConstraintNames()
        {
            throw new NotImplementedException();
        }

        private class DataType
        {
            public string TypeName { get; set; }
            public int ProviderDbType { get; set; }
            public Type Type { get; set; }

            public override string ToString()
            {
                return string.Format("{0}: {1}", ProviderDbType, TypeName);
            }
        }

        private Dictionary<int, DataType> _DataTypes = null;
        private Dictionary<int, DataType> DataTypes
        {
            get
            {
                if (_DataTypes == null)
                {
                    _DataTypes = new Dictionary<int, DataType>();
                    foreach (DataRow dt in db.GetSchema("DataTypes").Rows)
                    {
                        int id = (int)dt["ProviderDbType"];
                        _DataTypes[id] = new DataType() { TypeName = (string)dt["TypeName"], Type = Type.GetType((string)dt["DataType"]), ProviderDbType = id };
                    }

                    // Add some more
                    _DataTypes[(int)OleDbType.Boolean] = new DataType() { Type = typeof(bool) };
                    _DataTypes[(int)OleDbType.TinyInt] = new DataType() { Type = typeof(short) };
                    _DataTypes[(int)OleDbType.Single] = new DataType() { Type = typeof(short) };
                    _DataTypes[(int)OleDbType.SmallInt] = new DataType() { Type = typeof(short) };
                    _DataTypes[(int)OleDbType.Integer] = new DataType() { Type = typeof(int) };
                    _DataTypes[(int)OleDbType.BigInt] = new DataType() { Type = typeof(long) };
                    _DataTypes[(int)OleDbType.UnsignedBigInt] = new DataType() { Type = typeof(ulong) };
                    _DataTypes[(int)OleDbType.UnsignedInt] = new DataType() { Type = typeof(uint) };
                    _DataTypes[(int)OleDbType.UnsignedSmallInt] = new DataType() { Type = typeof(ushort) };
                    _DataTypes[(int)OleDbType.UnsignedTinyInt] = new DataType() { Type = typeof(ushort) };

                    _DataTypes[(int)OleDbType.Char] = new DataType() { Type = typeof(char) };

                    _DataTypes[(int)OleDbType.WChar] = new DataType() { Type = typeof(string) };
                    _DataTypes[(int)OleDbType.VarWChar] = new DataType() { Type = typeof(string) };
                    _DataTypes[(int)OleDbType.VarChar] = new DataType() { Type = typeof(string) };
                    _DataTypes[(int)OleDbType.BSTR] = new DataType() { Type = typeof(string) };
                    _DataTypes[(int)OleDbType.LongVarChar] = new DataType() { Type = typeof(string) };
                    _DataTypes[(int)OleDbType.LongVarWChar] = new DataType() { Type = typeof(string) };

                    _DataTypes[(int)OleDbType.Binary] = new DataType() { Type = typeof(byte[]) };
                    _DataTypes[(int)OleDbType.LongVarBinary] = new DataType() { Type = typeof(byte[]) };
                    _DataTypes[(int)OleDbType.VarBinary] = new DataType() { Type = typeof(byte[]) };

                    _DataTypes[(int)OleDbType.Currency] = new DataType() { Type = typeof(decimal) };
                    _DataTypes[(int)OleDbType.Decimal] = new DataType() { Type = typeof(decimal) };
                    _DataTypes[(int)OleDbType.Double] = new DataType() { Type = typeof(double) };
                    _DataTypes[(int)OleDbType.Numeric] = new DataType() { Type = typeof(double) };

                    _DataTypes[(int)OleDbType.Date] = new DataType() { Type = typeof(DateTime) };
                    _DataTypes[(int)OleDbType.DBDate] = new DataType() { Type = typeof(DateTime) };
                    _DataTypes[(int)OleDbType.DBTime] = new DataType() { Type = typeof(DateTime) };
                    _DataTypes[(int)OleDbType.DBTimeStamp] = new DataType() { Type = typeof(DateTime) };
                    _DataTypes[(int)OleDbType.Filetime] = new DataType() { Type = typeof(DateTime) };

                    _DataTypes[(int)OleDbType.Guid] = new DataType() { Type = typeof(Guid) };

                }
                return _DataTypes;
            }
        }

        public IEnumerable<Column> GetTableColumns(TableRef tbl)
        {
            QueryLog.Debug("GetSchema(Columns)");
            var columns = db.GetSchema(OleDbMetaDataCollectionNames.Columns, new string[] { null, null, tbl.Name, null });
            foreach (DataRow col in columns.Rows)
            {
                int dt = (int)col["DATA_TYPE"];
                Type type = DataTypes.ContainsKey(dt) ? DataTypes[dt].Type : typeof(string);
                int size = (int)(col["CHARACTER_MAXIMUM_LENGTH"] as long? ?? 0);
                if (size == 0 && (type == typeof(string) || type == typeof(byte[])))
                {
                    size = int.MaxValue;
                }
                yield return new Column()
                {
                    Name = (string)col["COLUMN_NAME"],
                    Size = size,
                    IsNullable = (bool)col["IS_NULLABLE"],
                    Type = DbTypeMapper.GetDbType(type)
                };
            }
        }

        public IEnumerable<string> GetTableColumnNames(TableRef tblName)
        {
            QueryLog.Debug("GetSchema(Columns)");
            var columns = db.GetSchema(OleDbMetaDataCollectionNames.Columns, new string[] { null, null, tblName.Name, null });
            foreach (DataRow col in columns.Rows)
            {
                yield return (string)col["COLUMN_NAME"];
            }
        }

        public void CreateTable(TableRef tbl, IEnumerable<Column> cols)
        {
            throw new NotSupportedException();
        }

        public void CreateTable(TableRef tblName, bool idAsIdentityColumn)
        {
            CreateTable(tblName, idAsIdentityColumn, true);
        }

        public void CreateTable(TableRef tblName, bool idAsIdentityColumn, bool createPrimaryKey)
        {
            throw new NotSupportedException();
        }

        public void CreateColumn(TableRef tblName, string colName, DbType type, int size, int scale, bool isNullable, DefaultConstraint defConstraint)
        {
            throw new NotSupportedException();
        }

        public void AlterColumn(TableRef tblName, string colName, DbType type, int size, int scale, bool isNullable, DefaultConstraint defConstraint)
        {
            throw new NotSupportedException();
        }

        public void CreateFKConstraint(TableRef tblName, TableRef refTblName, string colName, string constraintName, bool onDeleteCascade)
        {
            throw new NotSupportedException();
        }

        public void DropTable(TableRef tblName)
        {
            throw new NotSupportedException();
        }

        public void TruncateTable(TableRef tblName)
        {
            throw new NotSupportedException();
        }

        private void ExecuteNonQuery(string nonQueryFormat, params object[] args)
        {
            string query = String.Format(nonQueryFormat, args);

            using (var cmd = new OleDbCommand(query, db, tx))
            {
                QueryLog.Debug(query);
                cmd.ExecuteNonQuery();
            }
        }

        //private object ExecuteScalar(string nonQueryFormat, params object[] args)
        //{
        //    string query = String.Format(nonQueryFormat, args);

        //    using (var cmd = new OleDbCommand(query, db, tx))
        //    {
        //        QueryLog.Debug(query);
        //        return cmd.ExecuteScalar();
        //    }
        //}

        public void DropColumn(TableRef tblName, string colName)
        {
            throw new NotSupportedException();
        }

        public void DropFKConstraint(TableRef tblName, string fkName)
        {
            throw new NotSupportedException();
        }

        public void DropTrigger(string triggerName)
        {
            throw new NotSupportedException();
        }

        public void DropView(TableRef viewName)
        {
            throw new NotSupportedException();
        }

        public void DropProcedure(string procName)
        {
            throw new NotSupportedException();
        }

        public void DropAllObjects()
        {
            throw new NotSupportedException();
        }

        public void CopyColumnData(TableRef srcTblName, string srcColName, TableRef tblName, string colName)
        {
            Log.DebugFormat("Copying data from [{0}].[{1}] to [{2}].[{3}]", srcTblName, srcColName, tblName, colName);
            ExecuteNonQuery("UPDATE dest SET dest.[{0}] = src.[{1}] FROM [{2}] dest INNER JOIN [{3}] src ON dest.ID = src.ID",
                colName, srcColName, tblName, srcTblName);
        }

        public void MigrateFKs(TableRef srcTblName, string srcColName, TableRef tblName, string colName)
        {
            Log.DebugFormat("Migrating FK data from [{0}].[{1}] to [{2}].[{3}]", srcTblName, srcColName, tblName, colName);
            ExecuteNonQuery("UPDATE dest SET dest.[{0}] = src.[ID] FROM [{2}] dest INNER JOIN [{3}] src ON dest.ID = src.[{1}]",
                colName, srcColName, tblName, srcTblName);
        }

        public void InsertFKs(TableRef srcTblName, string srcColName, TableRef tblName, string colName, string fkColName)
        {
            Log.DebugFormat("Inserting FK data from [{0}]([{1}]) to [{2}]([{3}],[{4}])", srcTblName, srcColName, tblName, colName, fkColName);
            ExecuteNonQuery("INSERT INTO [{0}] ([{1}], [{2}]) SELECT [ID], [{3}] FROM [{4}] WHERE [{3}] IS NOT NULL",
                tblName, colName, fkColName, srcColName, srcTblName);
        }

        public void CopyFKs(TableRef srcTblName, string srcColName, TableRef destTblName, string destColName, string srcFKColName)
        {
            Log.DebugFormat("Copy FK data from [{0}]([{1}]) to [{2}]([{3}])", srcTblName, srcColName, destTblName, destColName);
            ExecuteNonQuery("UPDATE dest SET dest.[{0}] = src.[{1}] FROM [{2}] dest  INNER JOIN [{3}] src ON src.[{4}] = dest.[ID]",
                destColName, srcColName, destTblName, srcTblName, srcFKColName);
        }

        public bool CheckIndexExists(TableRef tblName, string idxName)
        {
            throw new NotImplementedException();
        }

        public void DropIndex(TableRef tblName, string idxName)
        {
            throw new NotImplementedException();
        }

        public void CreateIndex(TableRef tblName, string idxName, bool unique, bool clustered, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public void CreateUpdateRightsTrigger(string triggerName, TableRef tblName, List<RightsTrigger> tblList)
        {
            throw new NotSupportedException();
        }

        public void CreateEmptyRightsViewUnmaterialized(TableRef viewName)
        {
            throw new NotSupportedException();
        }

        public void CreateRightsViewUnmaterialized(TableRef viewName, TableRef tblName, TableRef tblNameRights, IList<ACL> acls)
        {
            throw new NotSupportedException();
        }

        public void CreateRefreshRightsOnProcedure(string procName, TableRef viewUnmaterializedName, TableRef tblName, TableRef tblNameRights)
        {
            throw new NotSupportedException();
        }

        public void ExecRefreshRightsOnProcedure(string procName)
        {
            throw new NotSupportedException();
        }

        public void CreatePositionColumnValidCheckProcedures(ILookup<string, KeyValuePair<string, string>> refSpecs)
        {
            throw new NotSupportedException();
        }

        //private void ExecuteScriptFromResource(string scriptResourceName)
        //{
        //    using (var scriptStream = new StreamReader(this.GetType().Assembly.GetManifestResourceStream(scriptResourceName)))
        //    {
        //        var databaseScript = scriptStream.ReadToEnd();
        //        foreach (var cmdString in Regex.Split(databaseScript, "\r?\nGO\r?\n").Where(s => !String.IsNullOrEmpty(s)))
        //        {
        //            ExecuteNonQuery(cmdString);
        //        }
        //    }
        //}

        public void RenameTable(TableRef oldTblName, TableRef newTblName)
        {
            throw new NotSupportedException();
        }

        public void RenameColumn(TableRef tblName, string oldColName, string newColName)
        {
            throw new NotSupportedException();
        }

        public void RenameFKConstraint(string oldConstraintName, string newConstraintName)
        {
            throw new NotSupportedException();
        }

        public IDataReader ReadTableData(TableRef tbl, IEnumerable<string> colNames)
        {
            var sb = new StringBuilder();
            sb.AppendLine("SELECT ");
            colNames.ForEach(i => sb.Append(Quote(i) + ","));
            sb.Remove(sb.Length - 1, 1);
            sb.AppendLine(" FROM " + tbl.Name);

            var cmd = new OleDbCommand(sb.ToString(), db, tx);
            return cmd.ExecuteReader();
        }

        public IDataReader ReadTableData(string sql)
        {
            var cmd = new OleDbCommand(sql, db, tx);
            return cmd.ExecuteReader();
        }

        public IDataReader ReadJoin(TableRef tbl, IEnumerable<ProjectionColumn> colNames, IEnumerable<Join> joins)
        {
            throw new NotImplementedException();
        }

        public void WriteTableData(TableRef destTbl, IDataReader source, IEnumerable<string> colNames)
        {
            if (source == null) throw new ArgumentNullException("source");

            var values = new object[source.FieldCount];
            while (source.Read())
            {
                source.GetValues(values);
                WriteTableData(destTbl, this.GetTableColumnNames(destTbl), values);
            }
        }

        public void WriteTableData(TableRef tbl, IEnumerable<string> colNames, IEnumerable values)
        {
            if (colNames == null) throw new ArgumentNullException("colNames");
            if (values == null) throw new ArgumentNullException("values");

            var sb = new StringBuilder();
            sb.AppendLine(string.Format("INSERT INTO {0} (", Quote(tbl.Name)));

            colNames.ForEach(i => sb.Append(Quote(i) + ","));
            sb.Remove(sb.Length - 1, 1);

            sb.AppendLine(") VALUES (");

            colNames.ForEach(i => sb.Append("?,"));
            sb.Remove(sb.Length - 1, 1);

            sb.AppendLine(")");

            var cmd = new OleDbCommand(sb.ToString(), db, tx);
            int counter = 0;
            foreach (var v in values)
            {
                cmd.Parameters.AddWithValue(string.Format("@param{0}", ++counter), v ?? DBNull.Value);
            }

            cmd.ExecuteNonQuery();
        }


        /// <summary>Not implemented.</summary>
        string ISchemaProvider.DbTypeToNative(DbType type)
        {
            throw new NotImplementedException();
        }

        /// <summary>Not implemented.</summary>
        DbType ISchemaProvider.NativeToDbType(string type)
        {
            throw new NotImplementedException();
        }

        public void RefreshDbStats()
        {
            // do nothing
        }

        public bool GetHasColumnDefaultValue(TableRef tblName, string colName)
        {
            throw new NotImplementedException();
        }

        public void ExecuteSqlResource(Type type, string scriptResourceName)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (string.IsNullOrEmpty(scriptResourceName)) throw new ArgumentNullException("scriptResourceName");

            using (var scriptStream = new StreamReader(type.Assembly.GetManifestResourceStream(scriptResourceName)))
            {
                var databaseScript = scriptStream.ReadToEnd();
                foreach (var cmdString in Regex.Split(databaseScript, "\r?\nGO\r?\n").Where(s => !String.IsNullOrEmpty(s)))
                {
                    ExecuteNonQuery(cmdString);
                }
            }
        }
    }
}