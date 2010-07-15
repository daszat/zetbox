
namespace Kistl.API.Server
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Linq;
    using System.Text;

    public struct TableRef : IComparable<TableRef>, ICloneable
    {
        private readonly string _database;
        public string Database { get { return _database; } }

        private readonly string _schema;
        public string Schema { get { return _schema; } }

        private readonly string _name;
        public string Name { get { return _name; } }

        public TableRef(string database, string schema, string name)
        {
            _database = database;
            _schema = schema;
            _name = name;
        }

        public override string ToString()
        {
            return String.Format("[{0}].[{1}].[{2}]", _database, _schema, _name);
        }

        public override int GetHashCode()
        {
            return (_database + _schema + _name).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return this == (TableRef)obj;
        }

        int IComparable<TableRef>.CompareTo(TableRef other)
        {
            var db = _database.CompareTo(other._database);
            if (db == 0)
            {
                var sc = _schema.CompareTo(other._schema);
                if (sc == 0)
                {
                    return _name.CompareTo(other._name);
                }
                else
                {
                    return sc;
                }
            }
            else
            {
                return db;
            }
        }

        public static bool operator ==(TableRef x, TableRef y)
        {
            return x._database == y._database
                && x._schema == y._schema
                && x._name == y._name;
        }

        public static bool operator !=(TableRef x, TableRef y)
        {
            return !(x == y);
        }

        public static bool operator >(TableRef x, TableRef y)
        {
            return ((IComparable<TableRef>)x).CompareTo(y) > 0;
        }

        public static bool operator <(TableRef x, TableRef y)
        {
            return ((IComparable<TableRef>)x).CompareTo(y) < 0;
        }

        object ICloneable.Clone()
        {
            return new TableRef(_database, _schema, _name);
        }
    }

    public class TableConstraintNamePair
    {
        public string TableName { get; set; }
        public string ConstraintName { get; set; }
    }

    public class ACL
    {
        public ACL()
        {
            this.Relations = new List<Join>();
        }

        public AccessRights Right { get; set; }
        public List<Join> Relations { get; private set; }
    }

    public enum JoinType
    {
        Inner = 0,
        Left,
    }

    public class Join
    {
        public TableRef JoinTableName { get; set; }
        public string JoinColumnName { get; set; }
        public string FKColumnName { get; set; }
        public JoinType Type { get; set; }

        public override string ToString()
        {
            return string.Format(
                "{0}JOIN {1} ON ({2} = {3})",
                Type == JoinType.Left ? "LEFT " : String.Empty,
                JoinTableName,
                JoinColumnName,
                FKColumnName);
        }
    }

    public class RightsTrigger
    {
        public RightsTrigger()
        {
            this.Relations = new List<Join>();
        }

        public TableRef ViewUnmaterializedName { get; set; }
        public TableRef TblNameRights { get; set; }
        public TableRef TblName { get; set; }

        public List<Join> Relations { get; private set; }
    }

    public class Column
    {
        public string Name { get; set; }
        public System.Data.DbType Type { get; set; }
        public int Size { get; set; }
        public bool IsNullable { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}({2}) {3}", Name, Type, Size, IsNullable ? "NULL" : "NOT NULL");
        }
    }

    public abstract class DefaultConstraint
    {
    }

    public class IntDefaultConstraint : DefaultConstraint
    {
        public int Value { get; set; }
    }

    public class NewGuidDefaultConstraint : DefaultConstraint
    {
    }

    public class DateTimeDefaultConstraint : DefaultConstraint
    {
    }

    public interface ISchemaProvider : IDisposable
    {
        #region Meta data

        string ConfigName { get; }
        string AdoNetProvider { get; }
        string ManifestToken { get; }

        #endregion

        #region ZBox Schema Handling

        string GetSavedSchema();
        void SaveSchema(string schema);

        #endregion

        #region Connection and Transaction Handling

        void Open(string connectionString);
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

        #endregion

        #region Type Mapping

        string DbTypeToNative(DbType type);
        DbType NativeToDbType(string type);

        #endregion

        #region Table Structure

        TableRef GetQualifiedTableName(string tblName);
        bool CheckTableExists(TableRef tblName);
        IEnumerable<TableRef> GetTableNames();

        bool CheckColumnExists(TableRef tblName, string colName);
        IEnumerable<string> GetTableColumnNames(TableRef tblName);
        IEnumerable<Column> GetTableColumns(TableRef tbl);

        bool CheckFKConstraintExists(string fkName);
        bool CheckIndexExists(TableRef tblName, string idxName);

        #endregion

        #region Table Content

        bool CheckTableContainsData(TableRef tblName);
        bool CheckColumnContainsNulls(TableRef tblName, string colName);
        bool CheckColumnContainsUniqueValues(TableRef tblName, string colName);
        bool CheckColumnContainsValues(TableRef tblName, string colName);

        #endregion

        bool CheckViewExists(TableRef viewName);
        bool CheckTriggerExists(TableRef objName, string triggerName);
        bool CheckProcedureExists(string procName);

        bool CheckPositionColumnValidity(TableRef tblName, string positionColumnName);
        bool RepairPositionColumn(TableRef tblName, string positionColumnName);

        bool GetIsColumnNullable(TableRef tblName, string colName);
        bool GetHasColumnDefaultValue(TableRef tblName, string colName);
        int GetColumnMaxLength(TableRef tblName, string colName);

        void CreateTable(TableRef tbl, IEnumerable<Column> cols);
        void CreateTable(TableRef tblName, bool idAsIdentityColumn);
        void CreateTable(TableRef tblName, bool idAsIdentityColumn, bool createPrimaryKey);
        void CreateColumn(TableRef tblName, string colName, DbType type, int size, int scale, bool isNullable, DefaultConstraint defConstraint);
        void AlterColumn(TableRef tblName, string colName, DbType type, int size, int scale, bool isNullable, DefaultConstraint defConstraint);
        IEnumerable<TableConstraintNamePair> GetFKConstraintNames();
        void CreateFKConstraint(TableRef tblName, TableRef refTblName, string colName, string constraintName, bool onDeleteCascade);

        void RenameTable(TableRef oldTblName, TableRef newTblName);
        void RenameColumn(TableRef tblName, string oldColName, string newColName);
        void RenameFKConstraint(string oldConstraintName, string newConstraintName);

        void TruncateTable(TableRef tblName);
        void DropTable(TableRef tblName);
        void DropColumn(TableRef tblName, string colName);
        void DropFKConstraint(TableRef tblName, string fkName);
        void DropTrigger(string triggerName);
        void DropView(TableRef viewName);
        void DropProcedure(string procName);
        void DropIndex(TableRef tblName, string idxName);
        void DropAllObjects();

        void CopyColumnData(TableRef srcTblName, string srcColName, TableRef tblName, string colName);
        void MigrateFKs(TableRef srcTblName, string srcColName, TableRef tblName, string colName);
        void InsertFKs(TableRef srcTblName, string srcColName, TableRef tblName, string colName, string fkColName);
        void CopyFKs(TableRef srcTblName, string srcColName, TableRef destTblName, string destColName, string srcFKColName);

        void CreateIndex(TableRef tblName, string idxName, bool unique, bool clustered, params string[] columns);

        void CreateUpdateRightsTrigger(string triggerName, TableRef tblName, List<RightsTrigger> tblList);
        void CreateRightsViewUnmaterialized(TableRef viewName, TableRef tblName, TableRef tblNameRights, IList<ACL> acls);
        void CreateEmptyRightsViewUnmaterialized(TableRef viewName);
        void CreateRefreshRightsOnProcedure(string procName, TableRef viewUnmaterializedName, TableRef tblName, TableRef tblNameRights);
        void ExecRefreshRightsOnProcedure(string procName);

        /// <summary>
        /// Creates a procedure to check position columns for their validity.
        /// </summary>
        /// <param name="refSpecs">a lookup by table name into lists of (fkColumnName, referencedTableName) pairs</param>
        void CreatePositionColumnValidCheckProcedures(ILookup<string, KeyValuePair<string, string>> refSpecs);

        IDataReader ReadTableData(TableRef tbl, IEnumerable<string> colNames);
        IDataReader ReadJoin(TableRef tbl, IEnumerable<string> colNames, IEnumerable<Join> joins);

        void WriteTableData(TableRef destTbl, IDataReader source, IEnumerable<string> colNames);

        /// <summary>
        /// This can be called after significant changes to the database to cause the DBMS' optimizier to refresh its internal stats.
        /// </summary>
        void RefreshDbStats();
    }
}
