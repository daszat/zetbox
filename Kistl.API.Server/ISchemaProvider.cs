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

        bool CheckTableExists(string tblName);
        bool CheckColumnExists(string tblName, string colName);
        bool CheckFKConstraintExists(string fkName);

        bool GetIsColumnNullable(string tblName, string colName);
        int GetColumnMaxLength(string tblName, string colName);

        IEnumerable<string> GetTableNames();
        IEnumerable<string> GetTableColumnNames(string tblName);
        IEnumerable<string> GetFKConstraintNames();
    }
}
