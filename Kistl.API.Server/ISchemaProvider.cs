using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Server
{
    public interface ISchemaProvider : IDisposable
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

        string GetSavedSchema();
        void SaveSchema(string schema);

        bool CheckTableExists(string tblName);
        bool CheckColumnExists(string tblName, string colName);
        bool CheckFKConstraintExists(string fkName);
        bool CheckTableContainsData(string tblName);

        bool GetIsColumnNullable(string tblName, string colName);
        int GetColumnMaxLength(string tblName, string colName);

        IEnumerable<string> GetTableNames();
        IEnumerable<string> GetTableColumnNames(string tblName);
        IEnumerable<string> GetFKConstraintNames();

        void CreateTable(string tblName, bool idAsIdentityColumn);
        void CreateColumn(string tblName, string colName, System.Data.DbType type, int size, bool isNullable);
        void CreateFKConstraint(string tblName, string refTblName, string colName, string constraintName);
    }
}
