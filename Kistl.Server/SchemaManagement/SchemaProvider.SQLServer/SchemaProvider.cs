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
            if (tx != null) throw new InvalidOperationException("Transaction is already running");
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

        private string GetSqlTypeString(System.Data.DbType type)
        {
            switch (type)
            {
                case System.Data.DbType.Int32:
                    return "int";
                case System.Data.DbType.Double:
                    return "float";
                case System.Data.DbType.String:
                    return "nvarchar";
                case System.Data.DbType.Date:
                    return "datetime";
                case System.Data.DbType.DateTime:
                    return "datetime";
                case System.Data.DbType.Boolean:
                    return "bit";
                case System.Data.DbType.Guid:
                    return "uniqueidentifier";
                default:
                    throw new ArgumentOutOfRangeException("type", string.Format("Unable to convert type '{0}' to an sql type string", type));
            }
        }

        public string GetSavedSchema()
        {
            if (!CheckTableExists("CurrentSchema")) return string.Empty;

            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM CurrentSchema", db, tx);
            int count = (int)cmd.ExecuteScalar();
            if (count > 1)
            {
                throw new InvalidOperationException("There is more then one Schema saved in your Database");
            }
            if (count == 0) return string.Empty;
            cmd = new SqlCommand("SELECT [Schema] FROM CurrentSchema", db, tx);
            return (string)cmd.ExecuteScalar();
        }

        /// <summary>
        /// TODO: Was ist die Version?
        /// </summary>
        /// <param name="schema"></param>
        public void SaveSchema(string schema)
        {
            if (!CheckTableExists("CurrentSchema")) throw new InvalidOperationException("Unable to save Schema. Schematable does not exist.");

            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [CurrentSchema]", db, tx);
            int count = (int)cmd.ExecuteScalar();
            if (count > 1)
            {
                throw new InvalidOperationException("There is more then one Schema saved in your Database");
            }
            if (count == 0)
            {
                cmd = new SqlCommand("INSERT INTO [CurrentSchema] ([Version], [Schema]) VALUES(1, @schema)", db, tx);
            }
            else
            {
                cmd = new SqlCommand("UPDATE [CurrentSchema] SET [Schema] = @schema, [Version] = [Version] + 1", db, tx);
            }
            cmd.Parameters.AddWithValue("@schema", schema);
            cmd.ExecuteNonQuery();
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

        public void CreateTable(string tblName, bool idAsIdentityColumn)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("CREATE TABLE [{0}] ( ", tblName);
            if (idAsIdentityColumn)
            {
                sb.AppendLine("[ID] [int] IDENTITY(1,1) NOT NULL, ");
            }
            else
            {
                sb.AppendLine("[ID] [int] NOT NULL, ");
            }

            sb.AppendFormat("CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED ( [ID] ASC )", tblName);
            sb.AppendLine();
            sb.Append(")");

            SqlCommand cmd = new SqlCommand(sb.ToString(), db, tx);
            cmd.ExecuteNonQuery();
        }

        public void CreateColumn(string tblName, string colName, System.Data.DbType type, int size, bool isNullable)
        {
            StringBuilder sb = new StringBuilder();

            // TODO: Trick 17, temporäre Lösung
            if (type == System.Data.DbType.String && size > 1000)
            {
                sb.AppendFormat("ALTER TABLE [{0}] ADD [{1}] {2} {3}", tblName, colName, 
                    "ntext", 
                    isNullable ? "NULL" : "NOT NULL");
            }
            else
            {
                sb.AppendFormat("ALTER TABLE [{0}] ADD [{1}] {2}{3} {4}", tblName, colName, 
                    GetSqlTypeString(type),
                    size > 0 ? string.Format("({0})", size) : "", 
                    isNullable ? "NULL" : "NOT NULL");
            }

            SqlCommand cmd = new SqlCommand(sb.ToString(), db, tx);
            cmd.ExecuteNonQuery();
        }
    }
}
