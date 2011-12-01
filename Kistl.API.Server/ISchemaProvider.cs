
namespace Kistl.API.Server
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    public abstract class DboRef : IComparable<DboRef>
    {
        private readonly string _database;

        /// <summary>
        /// The database containing this database object.
        /// </summary>
        public string Database { get { return _database; } }

        private readonly string _schema;

        /// <summary>
        /// The database schema containing this database object.
        /// </summary>
        public string Schema { get { return _schema; } }

        private readonly string _name;

        /// <summary>
        /// The name of the database object.
        /// </summary>
        public string Name { get { return _name; } }

        protected DboRef(string database, string schema, string name)
        {
            _database = database;
            _schema = schema;
            _name = name;
        }

        public override string ToString()
        {
            return String.Format("Ref: '{0}'.'{1}'.'{2}'", _database, _schema, _name);
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

            return this == (DboRef)obj;
        }

        int IComparable<DboRef>.CompareTo(DboRef other)
        {
            if (other == null)
                return -1;

            var type = this.GetType().AssemblyQualifiedName.CompareTo(other.GetType().AssemblyQualifiedName);
            if (type != 0)
                return type;

            var db = _database.CompareTo(other._database);
            if (db != 0)
                return db;

            var sc = _schema.CompareTo(other._schema);
            if (sc != 0)
                return sc;

            return _name.CompareTo(other._name);
        }

        public static bool operator ==(DboRef x, DboRef y)
        {
            if (!object.ReferenceEquals(x, null) && !object.ReferenceEquals(y, null))
            {
                return x.GetType().Equals(y.GetType())
                    && x._database == y._database
                    && x._schema == y._schema
                    && x._name == y._name;
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(DboRef x, DboRef y)
        {
            return !(x == y);
        }

        public static bool operator >(DboRef x, DboRef y)
        {
            if (x == null)
                throw new ArgumentNullException("x");
            return ((IComparable<DboRef>)x).CompareTo(y) > 0;
        }

        public static bool operator <(DboRef x, DboRef y)
        {
            if (x == null)
                throw new ArgumentNullException("x");
            return ((IComparable<DboRef>)x).CompareTo(y) < 0;
        }
    }

    public sealed class TableRef : DboRef, IComparable<TableRef>, ICloneable
    {
        public TableRef(string database, string schema, string name)
            : base(database, schema, name)
        {
        }

        int IComparable<TableRef>.CompareTo(TableRef other)
        {
            return ((IComparable<DboRef>)this).CompareTo(other);
        }

        public static bool operator ==(TableRef x, TableRef y)
        {
            return ((DboRef)x) == ((DboRef)y);
        }
        public static bool operator !=(TableRef x, TableRef y)
        {
            return ((DboRef)x) != ((DboRef)y);
        }
        public static bool operator >(TableRef x, TableRef y)
        {
            return ((DboRef)x) > ((DboRef)y);
        }
        public static bool operator <(TableRef x, TableRef y)
        {
            return ((DboRef)x) < ((DboRef)y);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        object ICloneable.Clone()
        {
            return new TableRef(Database, Schema, Name);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public sealed class ProcRef : DboRef, IComparable<ProcRef>, ICloneable
    {
        public ProcRef(string database, string schema, string name)
            : base(database, schema, name)
        {
        }

        int IComparable<ProcRef>.CompareTo(ProcRef other)
        {
            return ((IComparable<DboRef>)this).CompareTo(other);
        }

        public static bool operator ==(ProcRef x, ProcRef y)
        {
            return ((DboRef)x) == ((DboRef)y);
        }
        public static bool operator !=(ProcRef x, ProcRef y)
        {
            return ((DboRef)x) != ((DboRef)y);
        }
        public static bool operator >(ProcRef x, ProcRef y)
        {
            return ((DboRef)x) > ((DboRef)y);
        }
        public static bool operator <(ProcRef x, ProcRef y)
        {
            return ((DboRef)x) < ((DboRef)y);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }


        object ICloneable.Clone()
        {
            return new ProcRef(Database, Schema, Name);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public sealed class ConstraintRef : DboRef, IComparable<ConstraintRef>, ICloneable
    {
        public ConstraintRef(string database, string schema, string name)
            : base(database, schema, name)
        {
        }

        int IComparable<ConstraintRef>.CompareTo(ConstraintRef other)
        {
            return ((IComparable<DboRef>)this).CompareTo(other);
        }

        public static bool operator ==(ConstraintRef x, ConstraintRef y)
        {
            return ((DboRef)x) == ((DboRef)y);
        }
        public static bool operator !=(ConstraintRef x, ConstraintRef y)
        {
            return ((DboRef)x) != ((DboRef)y);
        }
        public static bool operator >(ConstraintRef x, ConstraintRef y)
        {
            return ((DboRef)x) > ((DboRef)y);
        }
        public static bool operator <(ConstraintRef x, ConstraintRef y)
        {
            return ((DboRef)x) < ((DboRef)y);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }


        object ICloneable.Clone()
        {
            return new ConstraintRef(Database, Schema, Name);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public sealed class TriggerRef : DboRef, IComparable<TriggerRef>, ICloneable
    {
        public TriggerRef(string database, string schema, string name)
            : base(database, schema, name)
        {
        }

        int IComparable<TriggerRef>.CompareTo(TriggerRef other)
        {
            return ((IComparable<DboRef>)this).CompareTo(other);
        }

        public static bool operator ==(TriggerRef x, TriggerRef y)
        {
            return ((DboRef)x) == ((DboRef)y);
        }
        public static bool operator !=(TriggerRef x, TriggerRef y)
        {
            return ((DboRef)x) != ((DboRef)y);
        }
        public static bool operator >(TriggerRef x, TriggerRef y)
        {
            return ((DboRef)x) > ((DboRef)y);
        }
        public static bool operator <(TriggerRef x, TriggerRef y)
        {
            return ((DboRef)x) < ((DboRef)y);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }


        object ICloneable.Clone()
        {
            return new TriggerRef(Database, Schema, Name);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class TableConstraintNamePair
    {
        public TableRef TableName { get; set; }
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

    /// <summary>
    /// Represents a Join Operation between two Tables
    /// </summary>
    public class Join
    {
        /// <summary>
        /// The Table to join
        /// </summary>
        public TableRef JoinTableName { get; set; }
        /// <summary>
        /// The Columns to join in the referenced table
        /// </summary>
        public ColumnRef[] JoinColumnName { get; set; }
        /// <summary>
        /// The own FK-Columns
        /// </summary>
        public ColumnRef[] FKColumnName { get; set; }
        /// <summary>
        /// Type of Join
        /// </summary>
        public JoinType Type { get; set; }
        /// <summary>
        /// Whether or not this join treats nulls as equal.
        ///
        /// By default, SQL treats comparisons to null as null, but in some special cases, we want to treat null as equal to null. Set this property to true to do so.
        /// </summary>
        public bool[] CompareNullsAsEqual { get; set; }

        private List<Join> _joins = null;
        public List<Join> Joins
        {
            get
            {
                if (_joins == null)
                {
                    _joins = new List<Join>();
                }
                return _joins;
            }
        }

        public override string ToString()
        {
            return string.Format(
                "{0}JOIN {1} ON (({2}) = ({3}))",
                Type == JoinType.Left ? "LEFT " : String.Empty,
                JoinTableName,
                String.Join(", ", JoinColumnName.Select(cr => cr.ToString()).ToArray()),
                String.Join(", ", FKColumnName.Select(cr => cr.ToString()).ToArray()));
        }
    }

    public class ColumnRef
    {
        private class LocalJoin : Join
        {
            public override string ToString()
            {
                return "Local Join";
            }
        }
        private class PrimaryJoin : Join
        {
            public override string ToString()
            {
                return "Primary Table Join";
            }
        }
        public static readonly Join Local = new LocalJoin();
        public static readonly Join PrimaryTable = new PrimaryJoin();

        public ColumnRef()
        {
        }

        public ColumnRef(string name, Join source)
            : this(name, source, null)
        {
        }

        public ColumnRef(string name, Join source, DbType? colType)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            if (source == null)
                throw new ArgumentNullException("source");

            this.ColumnName = name;
            this.Source = source;
            this.Type = colType;
        }

        public string ColumnName { get; private set; }
        public Join Source { get; private set; }
        public DbType? Type { get; private set; }

        public override string ToString()
        {
            return string.Format("CR: {0}{1}", Source != PrimaryTable ? "." + Source : string.Empty, ColumnName);
        }
    }

    public class ProjectionColumn : ColumnRef
    {
        public ProjectionColumn(string name, Join source, string alias)
            : this(name, source, null, alias, null)
        {
        }

        public ProjectionColumn(string name, Join source, string alias, string nullValue)
            : this(name, source, null, alias, nullValue)
        {
        }

        public ProjectionColumn(string name, Join source, DbType? colType, string alias)
            : this(name, source, colType, alias, null)
        {
        }

        public ProjectionColumn(string name, Join source, DbType? colType, string alias, string nullValue)
            : base(name, source, colType)
        {
            if (alias == String.Empty)
                throw new ArgumentNullException("alias");

            this.Alias = alias;
            this.NullValue = nullValue;
        }

        public string Alias { get; private set; }
        public string NullValue { get; private set; }

        public override string ToString()
        {
            return string.Format("PC: {0}{1}", Source == PrimaryTable ? PrimaryTable.ToString() : "." + Source, ColumnName);
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
        public DbType Type { get; set; }
        public int Size { get; set; }
        public int? Scale { get; set; }
        public bool IsNullable { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}({2}{3}) {3}",
                Name,
                Type,
                Size,
                Scale.HasValue ? ", " + Scale.Value.ToString() : String.Empty,
                IsNullable ? "NULL" : "NOT NULL");
        }
    }

    public abstract class DatabaseConstraint
    {
        public static readonly DatabaseConstraint[] EmptyArray = new DatabaseConstraint[] { };
    }

    public abstract class DefaultConstraint : DatabaseConstraint
    {
    }

    public class IntDefaultConstraint : DefaultConstraint
    {
        public int Value { get; set; }
    }

    public class BoolDefaultConstraint : DefaultConstraint
    {
        public bool Value { get; set; }
    }

    public class NewGuidDefaultConstraint : DefaultConstraint
    {
    }

    public class DateTimeDefaultConstraint : DefaultConstraint
    {
    }

    public abstract class CheckConstraint : DatabaseConstraint
    {
    }

    public class BoolCheckConstraint : CheckConstraint
    {
        public bool Value { get; set; }
    }

    public interface ISchemaProvider : IDisposable
    {
        #region Meta data

        string ConfigName { get; }
        string AdoNetProvider { get; }
        string ManifestToken { get; }
        /// <summary>
        /// Returns true if this ISchemaProvider implementation can be used as ZBox Storage provider.
        /// Currently only SqlServer and Postgres are supported. OleDb is only capable of providing functionality
        /// for Data/Schema migration.
        /// </summary>
        bool IsStorageProvider { get; }

        #endregion

        #region Connection and Transaction Handling

        void Open(string connectionString);

        /// <summary>
        /// Gets a copy of the connection string for the currently open connection, safe for logging. That is, without any sensitive credential information.
        /// </summary>
        /// <returns></returns>
        string GetSafeConnectionString();

        /// <summary>
        /// Gets a copy of the connection string, safe for logging. That is, without any sensitive credential information.
        /// </summary>
        /// <returns></returns>
        string GetSafeConnectionString(string connectionString);

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        void DblinkConnect(TableRef tblName);

        #endregion

        #region Type Mapping

        string DbTypeToNative(DbType type);
        DbType NativeToDbType(string type);

        #endregion

        #region Database Management

        /// <summary>
        /// Checks whether the specified database exists.
        /// </summary>
        /// <param name="dbName">the name of the database</param>
        /// <returns>true if the database exists, false otherwise</returns>
        bool CheckDatabaseExists(string dbName);

        /// <summary>
        /// Creates the specified database with default parameters.
        /// </summary>
        /// <param name="dbName">the name of the database</param>
        void CreateDatabase(string dbName);

        /// <summary>
        /// Deletes the specified database
        /// </summary>
        /// <param name="dbName">the name of the database</param>
        void DropDatabase(string dbName);

        #endregion

        #region Database Schemas

        bool CheckSchemaExists(string schemaName);
        IEnumerable<string> GetSchemaNames();
        void CreateSchema(string schemaName);
        void DropSchema(string schemaName, bool force);

        #endregion

        #region Table Structure

        TableRef GetTableName(string schemaName, string tblName);

        bool CheckTableExists(TableRef tblName);
        IEnumerable<TableRef> GetTableNames();
        IEnumerable<TableRef> GetViewNames();
        string GetViewDefinition(TableRef view);

        void CreateTable(TableRef tblName, IEnumerable<Column> cols);
        void CreateTable(TableRef tblName, bool idAsIdentityColumn);
        void CreateTable(TableRef tblName, bool idAsIdentityColumn, bool createPrimaryKey);

        void RenameTable(TableRef oldTblName, TableRef newTblName);

        void DropTable(TableRef tblName);

        bool CheckColumnExists(TableRef tblName, string colName);
        IEnumerable<string> GetTableColumnNames(TableRef tblName);
        IEnumerable<Column> GetTableColumns(TableRef tblName);

        void CreateColumn(TableRef tblName, string colName, DbType type, int size, int scale, bool isNullable, params DatabaseConstraint[] constraints);
        void AlterColumn(TableRef tblName, string colName, DbType type, int size, int scale, bool isNullable, params DatabaseConstraint[] constraints);

        void RenameColumn(TableRef tblName, string oldColName, string newColName);

        bool GetIsColumnNullable(TableRef tblName, string colName);
        bool GetHasColumnDefaultValue(TableRef tblName, string colName);
        int GetColumnMaxLength(TableRef tblName, string colName);

        void DropColumn(TableRef tblName, string colName);

        #endregion

        #region Table Content

        bool CheckTableContainsData(TableRef tblName);
        bool CheckColumnContainsNulls(TableRef tblName, string colName);
        bool CheckColumnContainsUniqueValues(TableRef tblName, string colName);
        bool CheckColumnContainsValues(TableRef tblName, string colName);
        long CountRows(TableRef tableRef);

        void TruncateTable(TableRef tblName);

        void CopyColumnData(TableRef srcTblName, string srcColName, TableRef tblName, string colName);

        #endregion

        #region Constraint and Index Management

        bool CheckFKConstraintExists(TableRef tblName, string fkName);
        IEnumerable<TableConstraintNamePair> GetFKConstraintNames();
        void CreateFKConstraint(TableRef tblName, TableRef refTblName, string colName, string newConstraintName, bool onDeleteCascade);
        void RenameFKConstraint(TableRef tblName, string oldConstraintName, TableRef refTblName, string colName, string newConstraintName, bool onDeleteCascade);
        void DropFKConstraint(TableRef tblName, string constraintName);

        bool CheckIndexExists(TableRef tblName, string idxName);
        void DropIndex(TableRef tblName, string idxName);

        #endregion

        #region Other DB Objects (Views, Triggers, Procedures)

        bool CheckViewExists(TableRef viewName);
        void DropView(TableRef viewName);

        bool CheckTriggerExists(TableRef objName, string triggerName);
        void DropTrigger(TableRef objName, string triggerName);

        ProcRef GetProcedureName(string schemaName, string procName);
        IEnumerable<ProcRef> GetProcedureNames();
        string GetProcedureDefinition(ProcRef proc);
        bool CheckProcedureExists(ProcRef procName);
        void DropProcedure(ProcRef procName);

        ProcRef GetFunctionName(string schemaName, string funcName);
        IEnumerable<ProcRef> GetFunctionNames();
        bool CheckFunctionExists(ProcRef funcName);
        void DropFunction(ProcRef funcName);

        /// <summary>
        /// Setup schema provider-local structures
        /// </summary>
        void EnsureInfrastructure();
        void DropAllObjects();

        #endregion

        #region ZBox Schema Handling

        string GetSavedSchema();
        void SaveSchema(string schema);

        #endregion

        #region zBox Accelerators

        bool CheckPositionColumnValidity(TableRef tblName, string positionColumnName);
        bool RepairPositionColumn(TableRef tblName, string positionColumnName);

        #endregion

        void MigrateFKs(TableRef srcTblName, string srcColName, TableRef tblName, string colName);
        void InsertFKs(TableRef srcTblName, string srcColName, TableRef tblName, string colName, string fkColName);
        void CopyFKs(TableRef srcTblName, string srcColName, TableRef destTblName, string destColName, string srcFKColName);

        void CreateIndex(TableRef tblName, string idxName, bool unique, bool clustered, params string[] columns);

        void CreateUpdateRightsTrigger(string triggerName, TableRef tblName, List<RightsTrigger> tblList);
        void CreateRightsViewUnmaterialized(TableRef viewName, TableRef tblName, TableRef tblNameRights, IList<ACL> acls);
        void CreateEmptyRightsViewUnmaterialized(TableRef viewName);
        void CreateRefreshRightsOnProcedure(ProcRef procName, TableRef viewUnmaterializedName, TableRef tblName, TableRef tblNameRights);
        void ExecRefreshRightsOnProcedure(ProcRef procName);
        void CreateRefreshAllRightsProcedure(List<ProcRef> refreshProcNames);
        void ExecRefreshAllRightsProcedure();

        /// <summary>
        /// Creates a procedure to check position columns for their validity.
        /// </summary>
        /// <param name="refSpecs">a lookup by table name into lists of (referencedTableName, fkColumnName) pairs</param>
        void CreatePositionColumnValidCheckProcedures(ILookup<TableRef, KeyValuePair<TableRef, string>> refSpecs);

        void CreateSequenceNumberProcedure();
        void CreateContinuousSequenceNumberProcedure();

        IDataReader ReadTableData(TableRef tblName, IEnumerable<string> colNames);
        IDataReader ReadTableData(string sql);
        IDataReader ReadJoin(TableRef tblName, IEnumerable<ProjectionColumn> colNames, IEnumerable<Join> joins);

        void WriteTableData(TableRef destTblName, IDataReader source, IEnumerable<string> colNames);
        void WriteTableData(TableRef destTblName, IEnumerable<string> colNames, IEnumerable values);

        /// <summary>
        /// This can be called after significant changes to the database to cause the DBMS' optimizier to refresh its internal stats.
        /// </summary>
        void RefreshDbStats();

        /// <param name="type">A Type to locate the Resource's Assembly</param>
        /// <param name="scriptResourceNameFormat">the Resource path to locate the SQL script. "{0}" is replaced by the ConfigName.</param>
        void ExecuteSqlResource(Type type, string scriptResourceNameFormat);
    }
}
