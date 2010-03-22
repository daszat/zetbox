
namespace Kistl.API.Server
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

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

    public class Join
    {
        public string JoinTableName { get; set; }
        public string JoinColumnName { get; set; }
        public string FKColumnName { get; set; }

        public override string ToString()
        {
            return string.Format("JOIN {0} ON {1} = {2}", JoinTableName, JoinColumnName, FKColumnName);
        }
    }

    public class RightsTrigger
    {
        public RightsTrigger()
        {
            this.Relations = new List<Join>();
        }

        public string ViewUnmaterializedName { get; set; }
        public string TblNameRights { get; set; }
        public string TblName { get; set; }

        public List<Join> Relations { get; private set; }
    }

    public interface ISchemaProvider : IDisposable
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

        string GetSavedSchema();
        void SaveSchema(string schema);

        bool CheckTableExists(string tblName);
        bool CheckColumnExists(string tblName, string colName);
        bool CheckViewExists(string viewName);
        bool CheckTriggerExists(string objName, string triggerName);
        bool CheckProcedureExists(string procName);
        bool CheckFKConstraintExists(string fkName);
        bool CheckTableContainsData(string tblName);
        bool CheckColumnContainsNulls(string tblName, string colName);
        bool CheckColumnContainsUniqueValues(string tblName, string colName);

        bool CheckPositionColumnValidity(string tblName, string positionColumnName);
        bool RepairPositionColumn(string tblName, string positionColumnName);

        bool GetIsColumnNullable(string tblName, string colName);
        int GetColumnMaxLength(string tblName, string colName);

        IEnumerable<string> GetTableNames();
        IEnumerable<string> GetTableColumnNames(string tblName);
        IEnumerable<TableConstraintNamePair> GetFKConstraintNames();

        void CreateTable(string tblName, bool idAsIdentityColumn);
        void CreateTable(string tblName, bool idAsIdentityColumn, bool createPrimaryKey);
        void CreateColumn(string tblName, string colName, System.Data.DbType type, int size, bool isNullable);
        void AlterColumn(string tblName, string colName, System.Data.DbType type, int size, bool isNullable);
        void CreateFKConstraint(string tblName, string refTblName, string colName, string constraintName, bool onDeleteCascade);

        void RenameTable(string oldTblName, string newTblName);
        void RenameColumn(string tblName, string oldColName, string newColName);
        void RenameFKConstraint(string oldConstraintName, string newConstraintName);

        void DropTable(string tblName);
        void DropColumn(string tblName, string colName);
        void DropFKConstraint(string tblName, string fkName);
        void DropTrigger(string triggerName);
        void DropView(string viewName);
        void DropProcedure(string procName);

        void CopyColumnData(string srcTblName, string srcColName, string tblName, string colName);
        void MigrateFKs(string srcTblName, string srcColName, string tblName, string colName);
        void InsertFKs(string srcTblName, string srcColName, string tblName, string colName, string fkColName);
        void CopyFKs(string srcTblName, string srcColName, string destTblName, string destColName, string srcFKColName);

        void CreateIndex(string tblName, string idxName, bool unique, bool clustered, params string[] columns);

        void CreateUpdateRightsTrigger(string triggerName, string tblName, List<RightsTrigger> tblList);
        void CreateRightsViewUnmaterialized(string viewName, string tblName, string tblNameRights, IList<ACL> acls);
        void CreateEmptyRightsViewUnmaterialized(string viewName);
        void CreateRefreshRightsOnProcedure(string procName, string viewUnmaterializedName, string tblName, string tblNameRights);
        void ExecRefreshRightsOnProcedure(string procName);

        /// <summary>
        /// Creates a procedure to check position columns for their validity.
        /// </summary>
        /// <param name="refSpecs">a lookup by table name into lists of (fkColumnName, referencedTableName) pairs</param>
        void CreatePositionColumnValidCheckProcedures(ILookup<string, KeyValuePair<string, string>> refSpecs);
    }
}
