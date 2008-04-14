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
        private Kistl.API.IKistlContext ctx = null;
        private List<ObjectClass> objClassList = new List<ObjectClass>();
        private List<BaseProperty> propList = new List<BaseProperty>();

        public void Generate(Kistl.API.IKistlContext ctx)
        {
            this.ctx = ctx;

            // Preload all ObjectClasses and Properties to avoid Deadlocks
            objClassList = (from c in ctx.GetTable<ObjectClass>()
                            select c).ToList();
            // Preload Properties
            objClassList.ForEach(o => propList.AddRange(o.Properties.ToList()));
            // Preload BaseClasses
            objClassList.ForEach(o => { var tmp = o.BaseObjectClass; });


            System.Diagnostics.Trace.TraceInformation("Checking {0} Tables with {1} Properties",
                objClassList.Count(), propList.Count);

            using (SqlConnection _db = new SqlConnection(Kistl.API.Configuration.KistlConfig.Current.Server.ConnectionString))
            {
                db = _db;
                db.Open();
                // TODO: Transaktionen können jetzt nicht verwendet werden
                // weil das EF einen Bug beim Laden von Referenzen hat.
                // Wenn eine Referenz null ist, dann wird das Flag IsLoaded niemals auf True gesetzt
                // was zu einem Leseversuch in der Datenbank führt.
                //using (SqlTransaction _tx = db.BeginTransaction())
                {
                    //tx = _tx;

                    foreach (ObjectClass objClass in objClassList)
                    {
                        GenerateTable(objClass);
                    }

                    //tx.Commit();
                    // tx.Rollback();
                }
            }
        }

        private void GenerateTable(ObjectClass objClass)
        {
            SqlCommand cmd = new SqlCommand("select dbo.fn_TableExists(@t)", db, tx);
            cmd.Parameters.AddWithValue("@t", objClass.TableName);

            #region Create/Update Table
            if ((bool)cmd.ExecuteScalar())
            {
                System.Diagnostics.Trace.TraceInformation("Checking Table " + Generator.GetDatabaseTableName(objClass));
                foreach (Property p in objClass.Properties.OfType<Property>())
                {
                    // BackReferenceProperties sind uninteressant, diese ergeben sich

                    if (!p.IsList)
                    {
                        cmd = new SqlCommand("select dbo.fn_ColumnExists(@t, @c)", db, tx);
                        cmd.Parameters.AddWithValue("@t", Generator.GetDatabaseTableName(objClass));
                        if (p is ObjectReferenceProperty)
                        {
                            cmd.Parameters.AddWithValue("@c", "fk_" + p.PropertyName);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@c", p.PropertyName);
                        }

                        bool columnExists = (bool)cmd.ExecuteScalar();
                        System.Diagnostics.Trace.TraceInformation("  " + (columnExists ? "Checking" : "Creating") + " Column " + objClass.TableName + "." + p.PropertyName);

                        cmd = new SqlCommand("", db, tx);
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("alter table [{0}] ", Generator.GetDatabaseTableName(objClass));
                        sb.Append(columnExists ? " alter column " : " add ");

                        sb.Append(GetColumnStmt(p));

                        System.Diagnostics.Trace.TraceInformation("    " + sb.ToString());
                        cmd.CommandText = sb.ToString();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                System.Diagnostics.Trace.TraceInformation("Creating Table " + Generator.GetDatabaseTableName(objClass));

                cmd = new SqlCommand("", db, tx);
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("create table [{0}] ( ", Generator.GetDatabaseTableName(objClass));
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
                    if (!p.IsList)
                    {
                        sb.Append(GetColumnStmt(p));
                        sb.AppendLine(",");
                    }
                }

                sb.AppendFormat("CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED ( [ID] ASC )", Generator.GetDatabaseTableName(objClass));
                sb.AppendLine();
                sb.Append(")");

                System.Diagnostics.Trace.TraceInformation("  " + sb.ToString());
                cmd.CommandText = sb.ToString();
                cmd.ExecuteNonQuery();
            }
            #endregion

            #region Create/Update List Properties
            foreach (Property p in objClass.Properties.OfType<Property>().Where(p => p.IsList))
            {
                cmd = new SqlCommand("select dbo.fn_TableExists(@t)", db, tx);
                cmd.Parameters.AddWithValue("@t", Generator.GetDatabaseTableName(p));

                if ((bool)cmd.ExecuteScalar())
                {
                    System.Diagnostics.Trace.TraceInformation("Checking Table " + Generator.GetDatabaseTableName(p));

                    cmd = new SqlCommand("", db, tx);
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("alter table [{0}] alter column ", Generator.GetDatabaseTableName(p));

                    sb.Append(GetColumnStmt(p));

                    System.Diagnostics.Trace.TraceInformation("    " + sb.ToString());
                    cmd.CommandText = sb.ToString();
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    System.Diagnostics.Trace.TraceInformation("Creating Table " + Generator.GetDatabaseTableName(p));

                    cmd = new SqlCommand("", db, tx);
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("create table [{0}] ( ", Generator.GetDatabaseTableName(p));
                    sb.AppendLine("[ID] [int] IDENTITY(1,1) NOT NULL, ");

                    sb.AppendLine(string.Format("[fk_{0}] [int] NOT NULL, ", p.ObjectClass.ClassName));

                    sb.Append(GetColumnStmt(p));
                    sb.AppendLine(",");

                    sb.AppendFormat("CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED ( [ID] ASC )", Generator.GetDatabaseTableName(p));
                    sb.AppendLine();
                    sb.Append(")");

                    System.Diagnostics.Trace.TraceInformation("  " + sb.ToString());
                    cmd.CommandText = sb.ToString();
                    cmd.ExecuteNonQuery();
                }
            }
            #endregion
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

            if (p is ObjectReferenceProperty)
            {
                sb.AppendFormat(" [fk_{0}] ", p.PropertyName);
            }
            else
            {
                sb.AppendFormat(" [{0}] ", p.PropertyName);
            }

            sb.Append(SQLServerHelper.GetDBType(p));

            if (p is StringProperty)
            {
                sb.AppendFormat(" ({0})", ((StringProperty)p).Length);
            }

            sb.AppendFormat(p.IsNullable ? " NULL " : " NOT NULL ");

            return sb.ToString();
        }
    }
}
