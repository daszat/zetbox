using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
// TODO: Das gehört angeschaut.
using Kistl.Server.Generators.EntityFramework.Implementation;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.SchemaManagement
{
    public class SchemaManager
    {
        #region Private Functions
        private static ISchemaProvider GetProvider()
        {
            return new SchemaProvider.SQLServer.SchemaProvider();
        }

        private static void WriteReportHeader(TextWriter report, string reportName)
        {
            report.WriteLine("== {0} ==", reportName);
            report.WriteLine("Date: {0}", DateTime.Now);
            report.WriteLine("Database: {0}", ApplicationContext.Current.Configuration.Server.ConnectionString);
            report.WriteLine();
        }
        #endregion

        #region GetSavedSchema()
        public static IKistlContext GetSavedSchema()
        {
            IKistlContext ctx = new MemoryContext();
            using (ISchemaProvider db = GetProvider())
            {
                string schema = db.GetSavedSchema();
                if (!string.IsNullOrEmpty(schema))
                {
                    using (var sr = new MemoryStream(ASCIIEncoding.Default.GetBytes(schema)))
                    {
                        Packaging.Importer.Import(ctx, sr);
                    }
                }
            }
            return ctx;
        }
        #endregion

        #region CheckSchema
        public static void CheckSchema(IKistlContext ctx, Stream reportStream)
        {
            using (ISchemaProvider db = GetProvider())
            {
                using (TextWriter report = new StreamWriter(reportStream))
                {
                    WriteReportHeader(report, "Check Schema Report");

                    if (ctx.GetQuery<Kistl.App.Base.ObjectClass>().Count() == 0)
                    {
                        report.WriteLine("** Error: Current Schema is empty");
                    }
                    else
                    {
                        CheckTables(ctx, db, report);
                        CheckExtraTables(ctx, db, report);

                        report.WriteLine();

                        CheckRelations(ctx, db, report);
                        CheckExtraRelations(ctx, db, report);
                    }
                }
            }
        }

        private static void CheckExtraTables(IKistlContext ctx, ISchemaProvider db, TextWriter report)
        {
            // All ObjectClasses
            List<string> tableNames = ctx.GetQuery<ObjectClass>().Select(o => o.TableName).ToList();

            // Add ValueTypeProperties
            tableNames.AddRange(ctx.GetQuery<ValueTypeProperty>().Where(p => p.IsList).ToList()
                .Select(p => ((ObjectClass)p.ObjectClass).TableName + "_" + p.PropertyName + "Collection"));

            // Add Relations with sep. Storage
            tableNames.AddRange(ctx.GetQuery<Relation>()
                            .Where(r => (int)r.Storage == (int)StorageType.Separate)
                            .ToList()
                            .Select(r => r.GetCollectionEntryTableName(ctx)));

            foreach (string tblName in db.GetTableNames())
            {
                if (!tableNames.Contains(tblName))
                {
                    report.WriteLine("** Warning: Table \"{0}\" found in database but no ObjectClass was defined", tblName);
                }
            }
        }

        private static void CheckExtraColumns(ISchemaProvider db, TextWriter report, ObjectClass objClass)
        {
            List<string> columns = objClass.Properties.OfType<ValueTypeProperty>().Where(p => !p.IsList).Select(p => p.PropertyName).ToList();

            foreach (string propName in db.GetTableColumnNames(objClass.TableName))
            {
                if (propName == "ID") continue;
                if (propName.StartsWith("fk_")) continue;
                if (!columns.Contains(propName))
                {
                    report.WriteLine("    ** Warning: Column \"{0}\" found in database but no Property was defined", propName);
                }
            }
        }

        private static void CheckExtraRelations(IKistlContext ctx, ISchemaProvider db, TextWriter report)
        {
            List<string> relations = ctx.GetQuery<Relation>().ToList().Select(r => r.GetAssociationName()).ToList();

            foreach (string relName in db.GetFKConstraintNames())
            {
                if (!relations.Contains(relName))
                {
                    report.WriteLine("** Warning: Relation \"{0}\" found in database but no relation object was defined", relName);
                }
            }
        }

        private static void CheckRelations(IKistlContext ctx, ISchemaProvider db, TextWriter report)
        {
            report.WriteLine("Checking Relations");
            report.WriteLine("------------------");
            foreach (Relation rel in ctx.GetQuery<Relation>())
            {
                string fkName = rel.GetAssociationName();
                if (db.CheckFKConstraintExists(fkName))
                {
                    report.WriteLine("Relation: \"{0}\" \"{1} - {2}\"", fkName, rel.A.Type.ClassName, rel.B.Type.ClassName);
                }
                else
                {
                    report.WriteLine("** Warning: Relation \"{0}\" \"{1} - {2}\" is missing", fkName, rel.A.Type.ClassName, rel.B.Type.ClassName);
                }
            }
        }

        private static void CheckTables(IKistlContext ctx, ISchemaProvider db, TextWriter report)
        {
            report.WriteLine("Checking Tables & Columns");
            report.WriteLine("-------------------------");
            // Checking Tables
            foreach (ObjectClass objClass in ctx.GetQuery<ObjectClass>().OrderBy(o => o.Module.Namespace).ThenBy(o => o.ClassName))
            {
                report.WriteLine("Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.ClassName);

                if (db.CheckTableExists(objClass.TableName))
                {
                    report.WriteLine("  Table: {0}", objClass.TableName);
                    CheckColumns(db, report, objClass);
                    CheckExtraColumns(db, report, objClass);
                }
                else
                {
                    report.WriteLine("  ** Warning: Table \"{0}\" is missing", objClass.TableName);
                }
            }
        }

        private static void CheckColumns(ISchemaProvider db, TextWriter report, ObjectClass objClass)
        {
            report.WriteLine("  Columns: ");
            foreach (ValueTypeProperty prop in objClass.Properties.OfType<ValueTypeProperty>()
                .Where(p => !p.IsList)
                .OrderBy(p => p.Module.Namespace).ThenBy(p => p.PropertyName))
            {
                string tblName = objClass.TableName;
                string colName = prop.PropertyName;
                if (db.CheckColumnExists(tblName, colName))
                {
                    report.Write("    {0}", colName);
                    // TODO: Add DataType Check
                    bool isOK = true;
                    if (db.GetIsColumnNullable(tblName, colName) != prop.IsNullable)
                    {
                        if (isOK) report.WriteLine();
                        report.WriteLine("      ** Warning: Column \"{0}\" nullable mismatch. Column is {1} but should be {2}", colName,
                            db.GetIsColumnNullable(tblName, colName) ? "NULLABLE" : "NOT NULLABLE",
                            prop.IsNullable ? "NULLABLE" : "NOT NULLABLE");
                        isOK = false;
                    }
                    if (prop is StringProperty && db.GetColumnMaxLength(tblName, colName) != ((StringProperty)prop).Length)
                    {
                        if (isOK) report.WriteLine();
                        report.WriteLine("      ** Warning: Column \"{0}\" length mismatch. Columns length is {1} but should be {2}", colName,
                            db.GetColumnMaxLength(tblName, colName),
                            ((StringProperty)prop).Length);
                        isOK = false;
                    }

                    if (isOK)
                    {
                        report.WriteLine(" OK");
                    }
                }
                else
                {
                    report.WriteLine("    ** Warning: Column \"{0}\" is missing", colName);
                }
            }
        }
        #endregion

        #region UpdateSchema
        public static void UpdateSchema(IKistlContext schema, Stream reportStream)
        {
            using (ISchemaProvider db = GetProvider())
            {
                using (IKistlContext savedSchema = SchemaManagement.SchemaManager.GetSavedSchema())
                {
                    using (TextWriter report = new StreamWriter(reportStream))
                    {
                        WriteReportHeader(report, "Update Schema Report");
                    }
                }
            }
        }
        #endregion
    }
}
