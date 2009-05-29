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
            var relations = schema.GetQuery<Relation>().ToList();
            List<string> relationNames = new List<string>();

            relationNames.AddRange(relations.Where(r => r.GetRelationType() == RelationType.one_n).Select(r => r.GetAssociationName()));
            foreach (var rel in relations.Where(r => r.GetRelationType() == RelationType.n_m))
            {
                relationNames.Add(rel.GetRelationAssociationName(RelationEndRole.A));
                relationNames.Add(rel.GetRelationAssociationName(RelationEndRole.B));
            }

            foreach (var rel in relations.Where(r => r.GetRelationType() == RelationType.one_one))
            {
                if(rel.A.Navigator != null && rel.A.Navigator.HasStorage())
                    relationNames.Add(rel.GetRelationAssociationName(RelationEndRole.A));
                if (rel.B.Navigator != null && rel.B.Navigator.HasStorage())
                    relationNames.Add(rel.GetRelationAssociationName(RelationEndRole.B));
            }

            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().Where(o => o.BaseObjectClass != null))
            {
                relationNames.Add(Construct.InheritanceAssociationName(objClass.BaseObjectClass, objClass));
            }

            foreach (string relName in db.GetFKConstraintNames())
            {
                if (!relationNames.Contains(relName))
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
                string assocName = rel.GetAssociationName();
                report.WriteLine("Relation: \"{0}\" \"{1} - {2}\"", assocName, rel.A.Type.ClassName, rel.B.Type.ClassName);
                switch (rel.GetRelationType())
                {
                    case RelationType.one_one:
                        Check_1_1_RelationColumns(rel, rel.A, RelationEndRole.A);
                        Check_1_1_RelationColumns(rel, rel.B, RelationEndRole.B);
                        break;
                    case RelationType.one_n:
                        Check_1_N_RelationColumns(rel);
                        break;
                    case RelationType.n_m:
                        Check_N_M_RelationColumns(rel);
                        break;
                }
            }
        }

        private void Check_1_1_RelationColumns(Relation rel, RelationEnd relEnd, RelationEndRole role)
        {
            if (relEnd.Navigator != null && relEnd.Navigator.HasStorage())
            {
                string assocNameRole = rel.GetRelationAssociationName(role);
                if (!db.CheckFKConstraintExists(assocNameRole))
                {
                    report.WriteLine("  ** Warning: FK Constraint '{0}' is missing", assocNameRole);
                }
                if (!db.CheckColumnExists(relEnd.Type.TableName, Construct.ForeignKeyColumnName(relEnd.Navigator)))
                {
                    report.WriteLine("  ** Warning: Navigator {0} is missing", role);
                }
                if (rel.NeedsPositionStorage(RelationEndRole.A) && !db.CheckColumnExists(relEnd.Type.TableName, Construct.ListPositionColumnName(relEnd.Navigator)))
                {
                    report.WriteLine("  ** Warning: Position Column {0} is missing", role);
                }
            }
        }

        private void Check_1_N_RelationColumns(Relation rel)
        {
            string assocName = rel.GetAssociationName();

            ObjectReferenceProperty nav = null;
            string tblName = "";
            string refTblName = "";
            bool isIndexed = false;
            if (rel.A.Navigator != null && rel.A.Navigator.HasStorage())
            {
                nav = rel.A.Navigator;
                tblName = rel.A.Type.TableName;
                refTblName = rel.B.Type.TableName;
                isIndexed = rel.NeedsPositionStorage(RelationEndRole.A);
            }
            else if (rel.B.Navigator != null && rel.B.Navigator.HasStorage())
            {
                nav = rel.B.Navigator;
                tblName = rel.B.Type.TableName;
                refTblName = rel.A.Type.TableName;
                isIndexed = rel.NeedsPositionStorage(RelationEndRole.B);
            }

            if (nav == null)
            {
                report.WriteLine("  ** Warning: Relation has no Navigator");
                return;
            }

            if (!db.CheckFKConstraintExists(assocName))
            {
                report.WriteLine("  ** Warning: FK Constraint '{0}' is missing", assocName);
            }

            string colName = Construct.ForeignKeyColumnName(nav);
            if (!db.CheckColumnExists(tblName, colName))
            {
                report.WriteLine("  ** Warning: Navigator is missing");
            }
            if (isIndexed)
            {
                if (!db.CheckColumnExists(tblName, Construct.ListPositionColumnName(nav)))
                {
                    report.WriteLine("  ** Warning: Position Column is missing");
                }
            }
        }

        private void Check_N_M_RelationColumns(Relation rel)
        {
            string assocName = rel.GetAssociationName();

            string tblName = rel.GetRelationTableName();
            string fkAName = rel.GetRelationFkColumnName(RelationEndRole.A);
            string fkBName = rel.GetRelationFkColumnName(RelationEndRole.B);

            if (!db.CheckTableExists(tblName))
            {
                report.WriteLine("  ** Warning: Relation table '{0}' is missing", tblName);
                return;
            }

            if (!db.CheckFKConstraintExists(rel.GetRelationAssociationName(RelationEndRole.A)))
            {
                report.WriteLine("  ** Warning: FK Constraint '{0}' for A is missing", rel.GetRelationAssociationName(RelationEndRole.A));
            }
            if (!db.CheckFKConstraintExists(rel.GetRelationAssociationName(RelationEndRole.B)))
            {
                report.WriteLine("  ** Warning: FK Constraint '{0}' for B is missing", rel.GetRelationAssociationName(RelationEndRole.B));
            }

            if (!db.CheckColumnExists(tblName, fkAName))
            {
                report.WriteLine("  ** Warning: Navigator A '{0}' is missing", fkAName);
            }
            if (rel.NeedsPositionStorage(RelationEndRole.A) && !db.CheckColumnExists(tblName, fkAName + Kistl.API.Helper.PositionSuffix))
            {
                report.WriteLine("  ** Warning: Navigator A '{0}' Position column is missing", fkAName);
            }

            if (!db.CheckColumnExists(tblName, fkBName))
            {
                report.WriteLine("  ** Warning: Navigator B '{0}' is missing", fkBName);
            }
            if (rel.NeedsPositionStorage(RelationEndRole.B) && !db.CheckColumnExists(tblName, fkBName + Kistl.API.Helper.PositionSuffix))
            {
                report.WriteLine("  ** Warning: Navigator B '{0}' Position column is missing", fkBName);
            }

            if (rel.A.Type.ImplementsIExportable(schema) && rel.B.Type.ImplementsIExportable(schema) && !db.CheckColumnExists(tblName, "ExportGuid"))
            {
                report.WriteLine("  ** Warning: ExportGuid is missing", fkBName);
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
                    if (prop is StringProperty)
                    {
                        StringProperty strProp = (StringProperty)prop;

                        // TODO: Introduce TextProperty
                        if (strProp.Length > 1000)
                        {
                            // TODO: Check if ntext
                        }
                        else if (db.GetColumnMaxLength(tblName, colName) != strProp.Length)
                        {
                            if (isOK) report.WriteLine();
                            report.WriteLine("      ** Warning: Column \"{0}\" length mismatch. Columns length is {1} but should be {2}", colName,
                                db.GetColumnMaxLength(tblName, colName),
                                ((StringProperty)prop).Length);
                            isOK = false;
                        }
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
                UpdateRelations();
                UpdateInheritance();

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

            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().OrderBy(o => o.Module.Namespace).ThenBy(o => o.ClassName))
            {
                report.WriteLine("Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.ClassName);
                if (CheckCaseNewObjectClass(objClass))
                {
                    CaseNewObjectClass(objClass);
                }

                UpdateColumns(objClass);
            }
            report.WriteLine();
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

        private void UpdateRelations()
        {
            report.WriteLine("Updating Relations");
            report.WriteLine("------------------");

            foreach (Relation rel in schema.GetQuery<Relation>().OrderBy(r => r.Module.Namespace))
            {
                report.WriteLine("Relation: {0} ({1})", rel.GetAssociationName(), rel.GetRelationType());

                if (rel.GetRelationType() == RelationType.one_n)
                {
                    if (CheckCaseNew_1_N_Relation(rel))
                    {
                        CaseNew_1_N_Relation(rel);
                    }
                }
                else if (rel.GetRelationType() == RelationType.n_m)
                {
                    if (CheckCaseNew_N_M_Relation(rel))
                    {
                        CaseNew_N_M_Relation(rel);
                    }
                }
                else if (rel.GetRelationType() == RelationType.one_one)
                {
                    if (CheckCaseNew_1_1_Relation(rel))
                    {
                        CaseNew_1_1_Relation(rel);
                    }
                }
            }
            report.WriteLine();
        }

        private void UpdateInheritance()
        {
            report.WriteLine("Updating Inheritance");
            report.WriteLine("--------------------");

            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().OrderBy(o => o.Module.Namespace).ThenBy(o => o.ClassName))
            {
                report.WriteLine("Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.ClassName);
                if (CheckCaseNewObjectClassInheritance(objClass))
                {
                    CaseNewObjectClassInheritance(objClass);
                }
            }
            report.WriteLine();
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
                prop is StringProperty ? ((StringProperty)prop).Length : 0, prop.IsNullable);
        }
        #endregion

        #region New_1_N_Relation
        private bool CheckCaseNew_1_N_Relation(Relation rel)
        {
            return savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid) == null;
        }
        private void CaseNew_1_N_Relation(Relation rel)
        {
            string assocName = rel.GetAssociationName();
            report.WriteLine("  New 1:N Relation: {0}", assocName);

            ObjectReferenceProperty nav = null;
            string tblName ="";
            string refTblName="";
            bool isIndexed = false;
            if (rel.A.Navigator != null && rel.A.Navigator.HasStorage())
            {
                nav = rel.A.Navigator;
                tblName = rel.A.Type.TableName;
                refTblName = rel.B.Type.TableName;
                isIndexed = rel.NeedsPositionStorage(RelationEndRole.A);
            }
            else if (rel.B.Navigator != null && rel.B.Navigator.HasStorage())
            {
                nav = rel.B.Navigator;
                tblName = rel.B.Type.TableName;
                refTblName = rel.A.Type.TableName;
                isIndexed = rel.NeedsPositionStorage(RelationEndRole.B);
            }

            if (nav == null)
            {
                report.WriteLine("    ** Warning: Relation '{0}' has no Navigator", assocName);
                return;
            }

            string colName = Construct.ForeignKeyColumnName(nav);
            db.CreateColumn(tblName, colName, System.Data.DbType.Int32, 0, nav.IsNullable);
            db.CreateFKConstraint(tblName, refTblName, colName, assocName);

            if (isIndexed)
            {
                db.CreateColumn(tblName, Construct.ListPositionColumnName(nav), System.Data.DbType.Int32, 0, nav.IsNullable);
            }
        }
        #endregion

        #region New_N_M_Relation
        private bool CheckCaseNew_N_M_Relation(Relation rel)
        {
            return savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid) == null;
        }
        private void CaseNew_N_M_Relation(Relation rel)
        {
            string assocName = rel.GetAssociationName();
            report.WriteLine("  New N:M Relation: {0}", assocName);

            string tblName = rel.GetRelationTableName();
		    string fkAName = rel.GetRelationFkColumnName(RelationEndRole.A);
		    string fkBName = rel.GetRelationFkColumnName(RelationEndRole.B);

            db.CreateTable(tblName, true);

            db.CreateColumn(tblName, fkAName, System.Data.DbType.Int32, 0, false);
            if (rel.NeedsPositionStorage(RelationEndRole.A))
            {
                db.CreateColumn(tblName, fkAName + Kistl.API.Helper.PositionSuffix, System.Data.DbType.Int32, 0, true);
            }

            db.CreateColumn(tblName, fkBName, System.Data.DbType.Int32, 0, false);
            if (rel.NeedsPositionStorage(RelationEndRole.B))
            {
                db.CreateColumn(tblName, fkBName + Kistl.API.Helper.PositionSuffix, System.Data.DbType.Int32, 0, true);
            }

            if (rel.A.Type.ImplementsIExportable(schema) && rel.B.Type.ImplementsIExportable(schema))
            {
                db.CreateColumn(tblName, "ExportGuid", System.Data.DbType.Guid, 0, false);
            }

            db.CreateFKConstraint(tblName, rel.A.Type.TableName, fkAName, rel.GetRelationAssociationName(RelationEndRole.A));
            db.CreateFKConstraint(tblName, rel.B.Type.TableName, fkBName, rel.GetRelationAssociationName(RelationEndRole.B));
        }
        #endregion

        #region New_1_1_Relation
        private bool CheckCaseNew_1_1_Relation(Relation rel)
        {
            return savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid) == null;
        }
        private void CaseNew_1_1_Relation(Relation rel)
        {
            report.WriteLine("  New 1:1 Relation: {0}", rel.GetAssociationName());

            if (rel.A.Navigator.HasStorage())
            {
                CaseNew_1_1_Relation_CreateColumns(rel, rel.A, rel.B, RelationEndRole.A);
            }
            // Difference to 1:N. 1:1 may have storage 'Replicate'
            if (rel.B.Navigator.HasStorage())
            {
                CaseNew_1_1_Relation_CreateColumns(rel, rel.B, rel.A, RelationEndRole.B);
            }
        }

        private void CaseNew_1_1_Relation_CreateColumns(Relation rel, RelationEnd end, RelationEnd otherEnd, RelationEndRole role)
        {
            string tblName = end.Type.TableName;
            string refTblName = otherEnd.Type.TableName;
            string colName = Construct.ForeignKeyColumnName(end.Navigator);
            string assocName = rel.GetRelationAssociationName(role);

            db.CreateColumn(tblName, colName, System.Data.DbType.Int32, 0, end.Navigator.IsNullable);
            db.CreateFKConstraint(tblName, refTblName, colName, assocName);

            if (rel.NeedsPositionStorage(role))
            {
                db.CreateColumn(tblName, Construct.ListPositionColumnName(end.Navigator), System.Data.DbType.Int32, 0, end.Navigator.IsNullable);
            }
        }
        #endregion

        #region NewObjectClassInheritance
        private bool CheckCaseNewObjectClassInheritance(ObjectClass objClass)
        {
            if (objClass.BaseObjectClass == null) return false;

            ObjectClass savedObjClass = savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid);
            return savedObjClass == null || savedObjClass.BaseObjectClass == null;
        }
        private void CaseNewObjectClassInheritance(ObjectClass objClass)
        {
            report.WriteLine("  New ObjectClass Inheritance: {0} -> {1}", objClass.ClassName, objClass.BaseObjectClass.ClassName);

            string tblName = objClass.TableName;
            if (db.CheckTableContainsData(tblName))
            {
                report.WriteLine("    ** Warning: Table '{0}' contains data. unable to add inheritence", tblName);
                return;
            }

            db.CreateFKConstraint(tblName, objClass.BaseObjectClass.TableName, "ID", Construct.InheritanceAssociationName(objClass.BaseObjectClass, objClass));
        }
        #endregion

        #endregion

        #endregion

    }
}
