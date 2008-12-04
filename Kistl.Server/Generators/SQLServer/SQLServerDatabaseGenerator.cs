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
using Kistl.Server;
using Kistl.Server.Generators.Helper;

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

        public void Generate(Kistl.API.IKistlContext ctx)
        {
            this.ctx = ctx;

            int propCount = 0;
            // Preload all ObjectClasses and Properties to avoid Deadlocks
            objClassList = ctx.GetQuery<ObjectClass>().ToList();
            // Preload Properties
            objClassList.ForEach(o => propCount += o.Properties.Count);
            // Touch BaseClasses
            objClassList.ForEach(o => { var tmp = o.BaseObjectClass; });
            // Touch Modules
            objClassList.ForEach(o => { var tmp = o.Module; });


            System.Diagnostics.Trace.TraceInformation("Checking {0} Tables with {1} Properties",
                objClassList.Count, propCount);

            using (SqlConnection _db = new SqlConnection(ApplicationContext.Current.Configuration.Server.ConnectionString))
            {
                db = _db;
                db.Open();
                // TODO: Geht leider doch nicht.
                // Jetzt meckert er in der Applikation, dass bei einem Update nix geändert wurde oder so...
                // d.H. r.Load() muss ausgelöst werden...
                //using (SqlTransaction _tx = db.BeginTransaction())
                {
                    //tx = _tx;

                    foreach (ObjectClass objClass in objClassList)
                    {
                        GenerateTable(objClass);
                    }

                    foreach (ObjectClass objClass in objClassList)
                    {
                        GenerateFKConstraints(objClass);
                    }

                    //tx.Commit();
                    // tx.Rollback();
                }
            }
        }

        #region GenerateFKConstraints
        private void GenerateFKConstraints(ObjectClass objClass)
        {
            // Create FK to BaseClass
            if (objClass.BaseObjectClass != null)
            {
                SQLServerHelper.CreateFKConstraint(objClass.BaseObjectClass, objClass, "ID", db, tx);
            }

            foreach (Property p in objClass.Properties.OfType<Property>().ToList().Where(p => p.HasStorage()))
            {
                if (p.IsList)
                {
                    // FK to Parent
                    SQLServerHelper.CreateFKConstraint(objClass, p, p.ObjectClass.ClassName.CalcForeignKeyColumnName(""), db, tx);
                }

                if (p is ObjectReferenceProperty)
                {
                    ObjectReferenceProperty orp = (ObjectReferenceProperty)p;
                    if (orp.IsList)
                    {
                        SQLServerHelper.CreateFKConstraint(orp.ReferenceObjectClass, orp, orp.PropertyName.CalcForeignKeyColumnName(""), db, tx);
                    }
                    else
                    {
                        SQLServerHelper.CreateFKConstraint(orp.ReferenceObjectClass, objClass, orp.PropertyName.CalcForeignKeyColumnName(""), db, tx);
                    }
                }
            }
        }


        #endregion

        private void CheckTableProperties(ObjectClass classToCheck, DataType obj, string parentPropertyName)
        {
            foreach (Property p in obj.Properties.OfType<Property>().ToList().Where(p => p.IsList == false && p.HasStorage()))
            {
                if (p is StructProperty)
                {
                    CheckTableProperties(classToCheck, ((StructProperty)p).StructDefinition, 
                        p.PropertyName.CalcColumnName(parentPropertyName));
                }
                else
                {
                    bool columnExists = SQLServerHelper.CheckColumnExists(classToCheck, p, parentPropertyName, db, tx);
                    System.Diagnostics.Trace.TraceInformation("  " + (columnExists ? "Checking" : "Creating") + " Column " + classToCheck.TableName + "." + p.PropertyName.CalcColumnName(parentPropertyName));
                    if (columnExists)
                    {
                        SQLServerHelper.AlterColumn(classToCheck, p, parentPropertyName, db, tx);
                        if (p.NeedsPositionColumn())
                        {
                            if (SQLServerHelper.CheckListPositionColumnExists(classToCheck, parentPropertyName, p, db, tx))
                                SQLServerHelper.AlterListPositionColumn(classToCheck, p, parentPropertyName, db, tx);
                            else
                                SQLServerHelper.CreateListPositionColumn(classToCheck, p, parentPropertyName, db, tx);
                        }
                    }
                    else
                    {
                        SQLServerHelper.CreateColumn(classToCheck, p, parentPropertyName, db, tx);
                        if (p.NeedsPositionColumn())
                        {
                            SQLServerHelper.CreateListPositionColumn(classToCheck, p, parentPropertyName, db, tx);
                        }
                    }
                }
            }
        }

        private void AppendTableProperties(StringBuilder sb, DataType obj, string parentPropertyName)
        {
            foreach (Property p in obj.Properties.OfType<Property>().ToList().Where(p => p.IsList == false && p.HasStorage()))
            {
                if (p is StructProperty)
                {
                    AppendTableProperties(sb, ((StructProperty)p).StructDefinition, 
                        p.PropertyName.CalcColumnName(parentPropertyName));
                }
                else
                {
                    sb.Append(SQLServerHelper.GetColumnStmt(p, parentPropertyName));
                    if (p.NeedsPositionColumn())
                    {
                        sb.AppendFormat(",[{0}] int NULL ", p.PropertyName.CalcListPositionColumnName(parentPropertyName));
                    }
                    sb.AppendLine(",");
                }
            }
        }

        private void GenerateTable(ObjectClass objClass)
        {
            #region Create/Update Table
            if (SQLServerHelper.CheckTableExists(objClass, db, tx))
            {
                System.Diagnostics.Trace.TraceInformation("Checking Table " + Generator.GetDatabaseTableName(objClass));
                CheckTableProperties(objClass, objClass, "");
            }
            else
            {
                System.Diagnostics.Trace.TraceInformation("Creating Table " + Generator.GetDatabaseTableName(objClass));

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

                AppendTableProperties(sb, objClass, "");

                sb.AppendFormat("CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED ( [ID] ASC )", Generator.GetDatabaseTableName(objClass));
                sb.AppendLine();
                sb.Append(")");

                System.Diagnostics.Trace.TraceInformation("  " + sb.ToString());
                SqlCommand cmd = new SqlCommand(sb.ToString(), db, tx);
                cmd.ExecuteNonQuery();
            }
            #endregion

            #region Create/Update List Properties
            foreach (Property p in objClass.Properties.OfType<Property>().ToList().Where(p => p.IsList && p.HasStorage()))
            {
                string tableName = Generator.GetDatabaseTableName(p);
                string parentFKColumn = p.ObjectClass.ClassName.CalcForeignKeyColumnName("");
                string parentPositionColumn = p.ObjectClass.ClassName.CalcListPositionColumnName("");

                if (SQLServerHelper.CheckTableExists(p, db, tx))
                {
                    System.Diagnostics.Trace.TraceInformation("Checking Table " + Generator.GetDatabaseTableName(p));
                    if (p.IsIndexed && !SQLServerHelper.CheckColumnExists(tableName, parentPositionColumn, db, tx))
                    {
                        SQLServerHelper.CreateColumn(tableName, parentPositionColumn, "int", true, db, tx);
                    }

                    SQLServerHelper.AlterColumn(p, "", db, tx);
                    if (p.NeedsPositionColumn())
                    {
                        if (SQLServerHelper.CheckListPositionColumnExists(p, "", db, tx))
                            SQLServerHelper.AlterListPositionColumn(p, "", db, tx);
                        else
                            SQLServerHelper.CreateListPositionColumn(p, "", db, tx);
                    }
                }
                else
                {
                    System.Diagnostics.Trace.TraceInformation("Creating Table " + Generator.GetDatabaseTableName(p));
                    
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("create table [{0}] ( ", Generator.GetDatabaseTableName(p));
                    sb.AppendLine("[ID] [int] IDENTITY(1,1) NOT NULL, ");

                    sb.AppendLine(string.Format("[{0}] [int] NOT NULL, ", parentFKColumn));
                    if (p.IsIndexed)
                    {
                        sb.AppendFormat("[{0}] int NULL, ", p.ObjectClass.ClassName.CalcListPositionColumnName(""));
                    }

                    sb.Append(SQLServerHelper.GetColumnStmt(p, ""));
                    if (p.NeedsPositionColumn())
                    {
                        sb.AppendFormat(",[{0}] int NULL ", p.PropertyName.CalcListPositionColumnName(""));
                    }
                    sb.AppendLine(",");

                    sb.AppendFormat("CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED ( [ID] ASC )", Generator.GetDatabaseTableName(p));
                    sb.AppendLine();
                    sb.Append(")");

                    System.Diagnostics.Trace.TraceInformation("  " + sb.ToString());
                    SqlCommand cmd = new SqlCommand(sb.ToString(), db, tx);
                    cmd.ExecuteNonQuery();
                }
            }
            #endregion
        }
    }
}
