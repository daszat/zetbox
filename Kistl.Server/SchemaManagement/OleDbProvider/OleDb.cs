
namespace Kistl.Server.SchemaManagement.OleDbProvider
{
    using System;
    using System.Collections.Generic;
    using System.Data.OleDb;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.Server;
    using Kistl.API.Utils;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Data;

    public class OleDb
        : ISchemaProvider
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server.Schema.OLEDB");
        private readonly static log4net.ILog QueryLog = log4net.LogManager.GetLogger("Kistl.Server.Schema.OLEDB.Queries");

        protected OleDbConnection db;
        protected OleDbTransaction tx;
        protected string quotePrefix;
        protected string quoteSuffix;


        public OleDb(string connectionString)
        {
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

        public bool CheckTableExists(string tblName)
        {
            using (var cmd = new OleDbCommand("SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(@table) AND type IN (N'U')", db, tx))
            {
                cmd.Parameters.AddWithValue("@table", tblName);
                QueryLog.Debug(cmd.CommandText);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public bool CheckColumnExists(string tblName, string colName)
        {
            using (var cmd = new OleDbCommand(@"SELECT COUNT(*) FROM sys.objects o INNER JOIN sys.columns c ON c.object_id=o.object_id
	                                            WHERE o.object_id = OBJECT_ID(@table) 
		                                            AND o.type IN (N'U')
		                                            AND c.Name = @column", db, tx))
            {
                cmd.Parameters.AddWithValue("@table", tblName);
                cmd.Parameters.AddWithValue("@column", colName);
                QueryLog.Debug(cmd.CommandText);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public bool GetIsColumnNullable(string tblName, string colName)
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

        public bool CheckViewExists(string viewName)
        {
            using (var cmd = new OleDbCommand("SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(@view) AND type IN (N'V')", db, tx))
            {
                cmd.Parameters.AddWithValue("@view", viewName);
                QueryLog.Debug(cmd.CommandText);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public bool CheckTriggerExists(string objName, string triggerName)
        {
            throw new NotSupportedException();
        }

        public bool CheckProcedureExists(string procName)
        {
            throw new NotSupportedException();
        }

        public bool CheckTableContainsData(string tblName)
        {
            using (var cmd = new OleDbCommand(string.Format("SELECT COUNT(*) FROM {0}", Quote(tblName)), db, tx))
            {
                QueryLog.Debug(cmd.CommandText);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public bool CheckColumnContainsNulls(string tblName, string colName)
        {
            using (var cmd = new OleDbCommand(string.Format("SELECT COUNT(*) FROM (SELECT TOP 1 [{1}] FROM [{0}] WHERE [{1}] IS NULL) AS nulls", tblName, colName), db, tx))
            {
                QueryLog.Debug(cmd.CommandText);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public bool CheckColumnContainsUniqueValues(string tblName, string colName)
        {
            using (var cmd = new OleDbCommand(string.Format("SELECT COUNT(*) FROM (SELECT TOP 1 [{1}] FROM [{0}] WHERE [{1}] IS NOT NULL GROUP BY [{1}] HAVING COUNT([{1}]) > 1) AS tbl", tblName, colName), db, tx))
            {
                QueryLog.Debug(cmd.CommandText);
                return (int)cmd.ExecuteScalar() == 0;
            }
        }

        public bool CheckColumnContainsValues(string tblName, string colName)
        {
            using (var cmd = new OleDbCommand(string.Format("SELECT COUNT(*) FROM (SELECT TOP 1 [{1}] FROM [{0}] WHERE [{1}] IS NOT NULL) AS nulls", tblName, colName), db, tx))
            {
                QueryLog.Debug(cmd.CommandText);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public bool CheckPositionColumnValidity(string tblName, string posName)
        {
            throw new NotSupportedException();
        }

        public bool RepairPositionColumn(string tblName, string posName)
        {
            throw new NotSupportedException();
        }

        public int GetColumnMaxLength(string tblName, string colName)
        {
            // / 2 -> nvarchar!
            using (var cmd = new OleDbCommand(@"SELECT c.max_length / 2 FROM sys.objects o INNER JOIN sys.columns c ON c.object_id=o.object_id
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

            using (var cmd = new OleDbCommand(sqlQuery, db, tx))
            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read()) yield return rd.GetString(0);
            }
        }

        public IEnumerable<TableConstraintNamePair> GetFKConstraintNames()
        {
            string sqlQuery = "SELECT c.name, t.name FROM sys.objects c inner join sys.sysobjects t  on t.id = c.parent_object_id WHERE c.type IN (N'F') order by c.name";
            QueryLog.Debug(sqlQuery);

            using (var cmd = new OleDbCommand(sqlQuery, db, tx))
            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read()) yield return new TableConstraintNamePair() { ConstraintName = rd.GetString(0), TableName = rd.GetString(1) };
            }
        }

        public IEnumerable<string> GetTableColumnNames(string tblName)
        {
            using (var cmd = new OleDbCommand(@"SELECT c.name
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

        public void CreateTable(string tblName, bool idAsIdentityColumn)
        {
            CreateTable(tblName, idAsIdentityColumn, true);
        }

        public void CreateTable(string tblName, bool idAsIdentityColumn, bool createPrimaryKey)
        {
            throw new NotSupportedException();
        }

        public void CreateColumn(string tblName, string colName, System.Data.DbType type, int size, bool isNullable)
        {
            throw new NotSupportedException();
        }

        public void AlterColumn(string tblName, string colName, System.Data.DbType type, int size, bool isNullable)
        {
            throw new NotSupportedException();
        }

        public void CreateFKConstraint(string tblName, string refTblName, string colName, string constraintName, bool onDeleteCascade)
        {
            throw new NotSupportedException();
        }

        public void DropTable(string tblName)
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

        public void DropColumn(string tblName, string colName)
        {
            throw new NotSupportedException();
        }

        public void DropFKConstraint(string tblName, string fkName)
        {
            throw new NotSupportedException();
        }

        public void DropTrigger(string triggerName)
        {
            throw new NotSupportedException();
        }

        public void DropView(string viewName)
        {
            throw new NotSupportedException();
        }

        public void DropProcedure(string procName)
        {
            throw new NotSupportedException();
        }

        public void CopyColumnData(string srcTblName, string srcColName, string tblName, string colName)
        {
            Log.DebugFormat("Copying data from [{0}].[{1}] to [{2}].[{3}]", srcTblName, srcColName, tblName, colName);
            ExecuteNonQuery("UPDATE dest SET dest.[{0}] = src.[{1}] FROM [{2}] dest INNER JOIN [{3}] src ON dest.ID = src.ID",
                colName, srcColName, tblName, srcTblName);
        }

        public void MigrateFKs(string srcTblName, string srcColName, string tblName, string colName)
        {
            Log.DebugFormat("Migrating FK data from [{0}].[{1}] to [{2}].[{3}]", srcTblName, srcColName, tblName, colName);
            ExecuteNonQuery("UPDATE dest SET dest.[{0}] = src.[ID] FROM [{2}] dest INNER JOIN [{3}] src ON dest.ID = src.[{1}]",
                colName, srcColName, tblName, srcTblName);
        }

        public void InsertFKs(string srcTblName, string srcColName, string tblName, string colName, string fkColName)
        {
            Log.DebugFormat("Inserting FK data from [{0}]([{1}]) to [{2}]([{3}],[{4}])", srcTblName, srcColName, tblName, colName, fkColName);
            ExecuteNonQuery("INSERT INTO [{0}] ([{1}], [{2}]) SELECT [ID], [{3}] FROM [{4}] WHERE [{3}] IS NOT NULL",
                tblName, colName, fkColName, srcColName, srcTblName);
        }

        public void CopyFKs(string srcTblName, string srcColName, string destTblName, string destColName, string srcFKColName)
        {
            Log.DebugFormat("Copy FK data from [{0}]([{1}]) to [{2}]([{3}])", srcTblName, srcColName, destTblName, destColName);
            ExecuteNonQuery("UPDATE dest SET dest.[{0}] = src.[{1}] FROM [{2}] dest  INNER JOIN [{3}] src ON src.[{4}] = dest.[ID]",
                destColName, srcColName, destTblName, srcTblName, srcFKColName);
        }

        public bool CheckIndexExists(string tblName, string idxName)
        {
            throw new NotImplementedException();
        }

        public void DropIndex(string tblName, string idxName)
        {
            throw new NotImplementedException();
        }

        public void CreateIndex(string tblName, string idxName, bool unique, bool clustered, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public void CreateUpdateRightsTrigger(string triggerName, string tblName, List<RightsTrigger> tblList)
        {
            throw new NotSupportedException();
        }

        public void CreateEmptyRightsViewUnmaterialized(string viewName)
        {
            throw new NotSupportedException();
        }

        public void CreateRightsViewUnmaterialized(string viewName, string tblName, string tblNameRights, IList<ACL> acls)
        {
            throw new NotSupportedException();
        }

        public void CreateRefreshRightsOnProcedure(string procName, string viewUnmaterializedName, string tblName, string tblNameRights)
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

        public void RenameTable(string oldTblName, string newTblName)
        {
            throw new NotSupportedException();
        }

        public void RenameColumn(string tblName, string oldColName, string newColName)
        {
            throw new NotSupportedException();
        }

        public void RenameFKConstraint(string oldConstraintName, string newConstraintName)
        {
            throw new NotSupportedException();
        }
    }
}
