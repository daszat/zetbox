using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API.Server;
using System.Data.SqlClient;

namespace Kistl.Server.SchemaManagement.SchemaProvider.SQLServer
{
    public class SchemaProvider : ISchemaProvider
    {
        protected SqlConnection db;
        protected SqlTransaction tx;

        public SchemaProvider()
        {
            db = new SqlConnection(Kistl.API.ApplicationContext.Current.Configuration.Server.ConnectionString);
            db.Open();
        }

        public void BeginTransaction()
        {
            if (tx == null) throw new InvalidOperationException("Transaction is already running");
            tx = db.BeginTransaction();
        }

        public void CommitTransaction()
        {
            tx.Commit();
            tx = null;
        }

        public void RollbackTransaction()
        {
            tx.Rollback();
            tx = null;
        }

        public void Dispose()
        {
            if (tx != null)
            {
                tx.Rollback();
                tx = null;
            }
            if (db != null)
            {
                db.Close();
                db = null;
            }
        }

        public bool CheckTableExists(string tblName)
        {
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(@table) AND type IN (N'U')", db, tx);
            cmd.Parameters.AddWithValue("@table", tblName);
            return (int)cmd.ExecuteScalar() > 0;
        }

        public bool CheckColumnExists(string tblName, string colName)
        {
            SqlCommand cmd = new SqlCommand(@"SELECT COUNT(*) FROM sys.objects o INNER JOIN sys.columns c ON c.object_id=o.object_id
	                                            WHERE o.object_id = OBJECT_ID(@table) 
		                                            AND o.type IN (N'U')
		                                            AND c.Name = @column", db, tx);
            cmd.Parameters.AddWithValue("@table", tblName);
            cmd.Parameters.AddWithValue("@column", colName);
            return (int)cmd.ExecuteScalar() > 0;
        }

        public bool CheckFKConstraintExists(string fkName)
        {
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(@fk_constraint) AND type IN (N'F')", db, tx);
            cmd.Parameters.AddWithValue("@fk_constraint", fkName);
            return (int)cmd.ExecuteScalar() > 0;
        }

        public bool GetIsColumnNullable(string tblName, string colName)
        {
            SqlCommand cmd = new SqlCommand(@"SELECT c.is_nullable FROM sys.objects o INNER JOIN sys.columns c ON c.object_id=o.object_id
	                                            WHERE o.object_id = OBJECT_ID(@table) 
		                                            AND o.type IN (N'U')
		                                            AND c.Name = @column", db, tx);
            cmd.Parameters.AddWithValue("@table", tblName);
            cmd.Parameters.AddWithValue("@column", colName);
            return (bool)cmd.ExecuteScalar();
        }

        public int GetColumnMaxLength(string tblName, string colName)
        {
            // / 2 -> nvarchar!
            SqlCommand cmd = new SqlCommand(@"SELECT c.max_length / 2 FROM sys.objects o INNER JOIN sys.columns c ON c.object_id=o.object_id
	                                            WHERE o.object_id = OBJECT_ID(@table) 
		                                            AND o.type IN (N'U')
		                                            AND c.Name = @column", db, tx);
            cmd.Parameters.AddWithValue("@table", tblName);
            cmd.Parameters.AddWithValue("@column", colName);
            return (int)cmd.ExecuteScalar();
        }

        public IEnumerable<string> GetTableNames()
        {
            SqlCommand cmd = new SqlCommand("SELECT name FROM sys.objects WHERE type IN (N'U') AND name <> 'sysdiagrams'", db, tx);
            using (SqlDataReader rd = cmd.ExecuteReader())
            {
                while(rd.Read()) yield return rd.GetString(0);
            }
        }

        public IEnumerable<string> GetFKConstraintNames()
        {
            SqlCommand cmd = new SqlCommand("SELECT name FROM sys.objects WHERE type IN (N'F')", db, tx);
            using (SqlDataReader rd = cmd.ExecuteReader())
            {
                while (rd.Read()) yield return rd.GetString(0);
            }
        }

        public IEnumerable<string> GetTableColumnNames(string tblName)
        {
            SqlCommand cmd = new SqlCommand(@"SELECT c.name FROM sys.objects o INNER JOIN sys.columns c ON c.object_id=o.object_id
	                                            WHERE o.object_id = OBJECT_ID(@table) 
		                                            AND o.type IN (N'U')", db, tx);
            cmd.Parameters.AddWithValue("@table", tblName);
            using (SqlDataReader rd = cmd.ExecuteReader())
            {
                while (rd.Read()) yield return rd.GetString(0);
            }
        }
    }
}
