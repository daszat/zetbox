using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Data.Common;
using System.Text;
using Kistl.App.Base;
using Kistl.API;

namespace Kistl.Server.Generators.SQLServer
{
    /// <summary>
    /// Erzeugt die SQL Server Datenbank
    /// Das Objektmodell hat keine Ahnung von der Datenbank, 
    /// also keine GetTableName, GetColumnName etc. Funktionen.
    /// </summary>
    public class SQLServerDatabaseGenerator : IDatabaseGenerator
    {
        private SqlConnection db = null;
        private SqlTransaction tx = null;
        private Kistl.API.Server.KistlDataContext ctx = null;
        private List<ObjectClass> objClassList = new List<ObjectClass>();
        private List<BaseProperty> propList = new List<BaseProperty>();

        public void Generate(Kistl.API.Server.KistlDataContext ctx)
        {
            this.ctx = ctx;
            Console.WriteLine("Generating Database...");

            // Preload all ObjectClasses and Properties to avoid Deadlocks
            objClassList = (from c in ctx.GetTable<ObjectClass>()
                            select c).ToList();
            // Preload Properties
            objClassList.ForEach(o => propList.AddRange(o.Properties.ToList()));
            // Preload BaseClasses
            objClassList.ForEach(o => { var tmp = o.BaseObjectClass; });


            Console.WriteLine("Checking {0} Tables with {1} Properties",
                objClassList.Count(), propList.Count);

            using (SqlConnection _db = new SqlConnection(Properties.Settings.Default.KistlDatabase))
            {
                db = _db;
                db.Open();
                using (SqlTransaction _tx = db.BeginTransaction())
                {
                    tx = _tx;

                    foreach (ObjectClass objClass in objClassList)
                    {
                        GenerateTable(objClass);
                    }

                    tx.Commit();
                    // tx.Rollback();
                }
            }

            Console.WriteLine("...finished!");
        }

        private void GenerateTable(ObjectClass objClass)
        {
            SqlCommand cmd = new SqlCommand("select dbo.fn_TableExists(@t)", db, tx);
            cmd.Parameters.AddWithValue("@t", objClass.TableName);

            if ((bool)cmd.ExecuteScalar())
            {
                Console.WriteLine("Checking Table " + objClass.TableName);
                foreach (Property p in objClass.Properties.OfType<Property>())
                {
                    // BackReferenceProperties sind uninteressant, diese ergeben sich

                    cmd = new SqlCommand("select dbo.fn_ColumnExists(@t, @c)", db, tx);
                    cmd.Parameters.AddWithValue("@t", objClass.TableName);
                    cmd.Parameters.AddWithValue("@c", p.PropertyName);

                    bool columnExists = (bool)cmd.ExecuteScalar();
                    Console.WriteLine("  " + (columnExists ? "Checking" : "Creating") + " Column " + objClass.TableName + "." + p.PropertyName);

                    cmd = new SqlCommand("", db, tx);
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("alter table [{0}] ", objClass.TableName);
                    sb.Append(columnExists ? " alter column " : " add ");

                    sb.Append(GetColumnStmt(p));

                    Console.WriteLine("    " + sb.ToString());
                    cmd.CommandText = sb.ToString();
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                Console.WriteLine("Creating Table " + objClass.TableName);

                cmd = new SqlCommand("", db, tx);
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("create table [{0}] ( ", objClass.TableName);
                if (objClass.BaseObjectClass != null)
                {
                    sb.AppendLine("[ID] [int] NOT NULL, ");
                }
                else
                {
                    sb.AppendLine("[ID] [int] IDENTITY(1,1) NOT NULL, ");
                }

                foreach (Property p in objClass.Properties.OfType<Property>())
                {
                    // BackReferenceProperties sind uninteressant, diese ergeben sich
                    sb.Append(GetColumnStmt(p));
                    sb.AppendLine(",");
                }

                sb.AppendFormat("CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED ( [ID] ASC )", objClass.ClassName);
                sb.AppendLine();
                sb.Append(")");

                Console.WriteLine("  " + sb.ToString());
                cmd.CommandText = sb.ToString();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Das wird hier analysiert, weil ich nicht will, dass die Objektdefinition 
        /// etwas über die Datenbank wissen muss
        /// was ist, wenn wir andere Datenbanken unterstützen wollen?
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private string GetColumnStmt(Property p)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(" [{0}] ", p.PropertyName);
            if (p is ObjectReferenceProperty)
            {
                sb.Append("int");
            }
            else
            {
                sb.Append(SQLServerHelper.GetDBType(p.GetDataType()));
            }

            if (p is StringProperty)
            {
                sb.AppendFormat(" ({0})", ((StringProperty)p).Length);
            }

            sb.AppendFormat(p.IsNullable.HasValue && p.IsNullable.Value ? " NULL " : " NOT NULL ");

            return sb.ToString();
        }
    }
}
