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
    public class SchemaManager : IDisposable
    {
        #region Fields
        private IKistlContext schema;
        private IKistlContext savedSchema;
        private ISchemaProvider db;
        private TextWriter report;
        #endregion

        #region Constructor
        public SchemaManager(IKistlContext schema, Stream reportStream)
        {
            this.schema = schema;
            report = new StreamWriter(reportStream);
            db = GetProvider();
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            // Do not dispose "schema" -> passed to this class
            if (savedSchema != null) savedSchema.Dispose();
            if (db != null) db.Dispose();
            if (report != null) report.Dispose();
        }

        #endregion

        #region Private Functions
        private static ISchemaProvider GetProvider()
        {
            return new SchemaProvider.SQLServer.SchemaProvider();
        }

        private void WriteReportHeader(string reportName)
        {
            report.WriteLine("== {0} ==", reportName);
            report.WriteLine("Date: {0}", DateTime.Now);
            report.WriteLine("Database: {0}", ApplicationContext.Current.Configuration.Server.ConnectionString);
            report.WriteLine();
        }

        private System.Data.DbType GetDbType(Property p)
        {
            if (p is ObjectReferenceProperty)
            {
                return System.Data.DbType.Int32;
            }
            else if (p is EnumerationProperty)
            {
                return System.Data.DbType.Int32;
            }
            else if (p is IntProperty)
            {
                return System.Data.DbType.Int32;
            }
            else if (p is StringProperty)
            {
                return System.Data.DbType.String;
            }
            else if (p is DoubleProperty)
            {
                return System.Data.DbType.Double;
            }
            else if (p is BoolProperty)
            {
                return System.Data.DbType.Boolean;
            }
            else if (p is DateTimeProperty)
            {
                return System.Data.DbType.DateTime;
            }
            else if (p is GuidProperty)
            {
                return System.Data.DbType.Guid;
            }
            else
            {
                throw new DBTypeNotFoundException(p);
            }
        }
        #endregion

        #region SavedSchema
        public static IKistlContext GetSavedSchema()
        {
            IKistlContext ctx = new MemoryContext();
            using (ISchemaProvider db = GetProvider())
            {
                string schema = db.GetSavedSchema();
                if (!string.IsNullOrEmpty(schema))
                {
                    using (var ms = new MemoryStream(ASCIIEncoding.Default.GetBytes(schema)))
                    {
                        Packaging.Importer.Import(ctx, ms);
                    }
                }
            }
            return ctx;
        }

        private void SaveSchema(IKistlContext schema)
        {
            using (var ms = new MemoryStream())
            {
                Packaging.Exporter.Export(schema, ms, new string[] { "Kistl.App.Base" });
                string schemaStr = ASCIIEncoding.Default.GetString(ms.GetBuffer());
                db.SaveSchema(schemaStr);
            }
        }
        #endregion

        #region CheckSchema
        public void CheckSchema()
        {
            WriteReportHeader("Check Schema Report");

            if (schema.GetQuery<Kistl.App.Base.ObjectClass>().Count() == 0)
            {
                report.WriteLine("** Error: Current Schema is empty");
            }
            else
            {
                CheckTables();
                CheckExtraTables();

                report.WriteLine();

                CheckRelations();
                CheckExtraRelations();
            }
        }

        private void CheckExtraTables()
        {
            // All ObjectClasses
            List<string> tableNames = schema.GetQuery<ObjectClass>().Select(o => o.TableName).ToList();

            // Add ValueTypeProperties
            tableNames.AddRange(schema.GetQuery<ValueTypeProperty>().Where(p => p.IsList).ToList()
                .Select(p => p.GetCollectionEntryTable()));

            // Add Relations with sep. Storage
            tableNames.AddRange(schema.GetQuery<Relation>()
                            .Where(r => (int)r.Storage == (int)StorageType.Separate)
                            .ToList()
                            .Select(r => r.GetRelationTableName()));

            foreach (string tblName in db.GetTableNames())
            {
                if (!tableNames.Contains(tblName))
                {
                    report.WriteLine("** Warning: Table \"{0}\" found in database but no ObjectClass was defined", tblName);
                }
            }
        }

        private void CheckExtraColumns(ObjectClass objClass)
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

        private void CheckExtraRelations()
        {
            List<string> relations = schema.GetQuery<Relation>().ToList().Select(r => r.GetAssociationName()).ToList();

            foreach (string relName in db.GetFKConstraintNames())
            {
                if (!relations.Contains(relName))
                {
                    report.WriteLine("** Warning: Relation \"{0}\" found in database but no relation object was defined", relName);
                }
            }
        }

        private void CheckRelations()
        {
            report.WriteLine("Checking Relations");
            report.WriteLine("------------------");
            foreach (Relation rel in schema.GetQuery<Relation>())
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

        private void CheckTables()
        {
            report.WriteLine("Checking Tables & Columns");
            report.WriteLine("-------------------------");
            // Checking Tables
            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().OrderBy(o => o.Module.Namespace).ThenBy(o => o.ClassName))
            {
                report.WriteLine("Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.ClassName);

                if (db.CheckTableExists(objClass.TableName))
                {
                    report.WriteLine("  Table: {0}", objClass.TableName);
                    CheckColumns(objClass);
                    CheckExtraColumns(objClass);
                }
                else
                {
                    report.WriteLine("  ** Warning: Table \"{0}\" is missing", objClass.TableName);
                }
            }
        }

        private void CheckColumns(ObjectClass objClass)
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
        public void UpdateSchema()
        {
            savedSchema = GetSavedSchema();
            WriteReportHeader("Update Schema Report");

            db.BeginTransaction();
            try
            {
                UpdateTables();

                SaveSchema(schema);

                db.CommitTransaction();
            }
            catch (Exception ex)
            {
                db.RollbackTransaction();

                report.WriteLine();
                report.WriteLine("** ERROR:");
                report.WriteLine(ex.ToString());
                report.Flush();

                throw;
            }
        }

        private void UpdateTables()
        {
            report.WriteLine("Updating Tables & Columns");
            report.WriteLine("-------------------------");
            // Checking Tables
            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().OrderBy(o => o.Module.Namespace).ThenBy(o => o.ClassName))
            {
                report.WriteLine("Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.ClassName);
                if (CheckCaseNewObjectClass(objClass))
                {
                    CaseNewObjectClass(objClass);
                }

                UpdateColumns(objClass);
            }
        }

        private void UpdateColumns(ObjectClass objClass)
        {
            foreach (ValueTypeProperty prop in objClass.Properties.OfType<ValueTypeProperty>().Where(p => !p.IsList && p.HasStorage()))
            {
                if (CheckCaseNewValueTypeProperty(prop))
                {
                    CaseNewValueTypeProperty(prop);
                }
            }
        }

        #region Cases

        #region NewObjectClass
        private bool CheckCaseNewObjectClass(ObjectClass objClass)
        {
            return savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid) == null;
        }
        private void CaseNewObjectClass(ObjectClass objClass)
        {
            report.WriteLine("  New Table: {0}", objClass.TableName);
            db.CreateTable(objClass.TableName, objClass.BaseObjectClass == null);
        }
        #endregion

        #region NewValueTypeProperty
        private bool CheckCaseNewValueTypeProperty(ValueTypeProperty prop)
        {
            return savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid) == null;
        }
        private void CaseNewValueTypeProperty(ValueTypeProperty prop)
        {
            report.WriteLine("    New ValueType Property: {0}", prop.PropertyName);
            db.CreateColumn(((ObjectClass)prop.ObjectClass).TableName, prop.PropertyName, GetDbType(prop), 
                prop is StringProperty ? ((StringProperty)prop).Length : 0 , prop.IsNullable);
        }
        #endregion

        #endregion

        #endregion

    }
}
