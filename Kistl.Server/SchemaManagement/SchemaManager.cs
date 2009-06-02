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
        private bool repair = false;
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
        public void CheckSchema(bool withRepair)
        {
            this.repair = withRepair;
            WriteReportHeader(withRepair ? "Check Schema Report with repair" : "Check Schema Report");

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
                report.WriteLine();

                CheckInheritance();
                report.WriteLine();

                CheckExtraRelations();
            }
        }

        private void CheckInheritance()
        {
            report.WriteLine("Checking Inheritance");
            report.WriteLine("--------------------");

            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().Where(o => o.BaseObjectClass != null).OrderBy(o => o.Module.Namespace).ThenBy(o => o.ClassName))
            {
                report.WriteLine("Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.ClassName);
                string assocName = Construct.InheritanceAssociationName(objClass.BaseObjectClass, objClass);
                if (!db.CheckFKConstraintExists(assocName))
                {
                    report.WriteLine("** Warning: FK Contraint to BaseClass is missing");
                    if (repair)
                    {
                        CaseNewObjectClassInheritance(objClass);
                    }
                }
            }
            report.WriteLine();
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
            report.WriteLine("Checking extra Relations");
            report.WriteLine("------------------------");

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

            foreach (ValueTypeProperty prop in schema.GetQuery<ValueTypeProperty>().Where(p => p.IsList))
            {
                relationNames.Add(prop.GetAssociationName());
            }

            foreach (var rel in db.GetFKConstraintNames())
            {
                if (!relationNames.Contains(rel.ConstraintName))
                {
                    report.WriteLine("** Warning: Relation '{0}' on table '{1}' found in database but no relation object was defined", rel.ConstraintName, rel.TableName);
                    if (repair)
                    {
                        db.DropFKConstraint(rel.TableName, rel.ConstraintName);
                    }
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
                string tblName = relEnd.Type.TableName;
                string refTblName = rel.GetOtherEnd(relEnd).Type.TableName;
                string colName = Construct.ForeignKeyColumnName(relEnd.Navigator);
                string assocName = rel.GetRelationAssociationName(role);

                if (!db.CheckFKConstraintExists(assocName))
                {
                    report.WriteLine("  ** Warning: FK Constraint '{0}' is missing", assocName);
                    if (repair)
                    {
                        db.CreateFKConstraint(tblName, refTblName, colName, assocName);
                    }
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

            string colName = Construct.ForeignKeyColumnName(nav);
            string indexName = Construct.ListPositionColumnName(nav);
            if (!db.CheckFKConstraintExists(assocName))
            {
                report.WriteLine("  ** Warning: FK Constraint '{0}' is missing", assocName);
                if (repair)
                {
                    db.CreateFKConstraint(tblName, refTblName, colName, assocName);
                }
            }

            if (!db.CheckColumnExists(tblName, colName))
            {
                report.WriteLine("  ** Warning: Navigator is missing");
            }
            if (isIndexed && !db.CheckColumnExists(tblName, indexName))
            {
                report.WriteLine("  ** Warning: Index Column is missing");
            }
            if (!isIndexed && db.CheckColumnExists(tblName, indexName))
            {
                report.WriteLine("  ** Warning: Index Column exists but property is not indexed");
            }
        }

        private void Check_N_M_RelationColumns(Relation rel)
        {
            string assocName = rel.GetAssociationName();

            string tblName = rel.GetRelationTableName();
            string fkAName = rel.GetRelationFkColumnName(RelationEndRole.A);
            string fkBName = rel.GetRelationFkColumnName(RelationEndRole.B);
            string assocAName = rel.GetRelationAssociationName(RelationEndRole.A);
            string assocBName = rel.GetRelationAssociationName(RelationEndRole.B);
            string fkAIndex = fkAName + Kistl.API.Helper.PositionSuffix;
            string fkBIndex = fkBName + Kistl.API.Helper.PositionSuffix;

            if (!db.CheckTableExists(tblName))
            {
                report.WriteLine("  ** Warning: Relation table '{0}' is missing", tblName);
                return;
            }

            if (!db.CheckFKConstraintExists(assocAName))
            {
                report.WriteLine("  ** Warning: FK Constraint '{0}' for A is missing", assocAName);
                if (repair)
                {
                    db.CreateFKConstraint(tblName, rel.A.Type.TableName, fkAName, assocAName);
                }
            }
            if (!db.CheckFKConstraintExists(assocBName))
            {
                report.WriteLine("  ** Warning: FK Constraint '{0}' for B is missing", assocBName);
                if (repair)
                {
                    db.CreateFKConstraint(tblName, rel.B.Type.TableName, fkBName, assocBName);
                }
            }

            if (!db.CheckColumnExists(tblName, fkAName))
            {
                report.WriteLine("  ** Warning: Navigator A '{0}' is missing", fkAName);
            }
            if (rel.NeedsPositionStorage(RelationEndRole.A) && !db.CheckColumnExists(tblName, fkAIndex))
            {
                report.WriteLine("  ** Warning: Navigator A '{0}' Index Column is missing", fkAName);
                if (repair)
                {
                    // TODO: Call case
                }
            }
            if (!rel.NeedsPositionStorage(RelationEndRole.A) && db.CheckColumnExists(tblName, fkAIndex))
            {
                report.WriteLine("  ** Warning: Navigator A '{0}' Index Column exists but property is not indexed", fkAName);
                if (repair)
                {
                    // TODO: Call case
                }
            }

            if (!db.CheckColumnExists(tblName, fkBName))
            {
                report.WriteLine("  ** Warning: Navigator B '{0}' is missing", fkBName);
            }
            if (rel.NeedsPositionStorage(RelationEndRole.B) && !db.CheckColumnExists(tblName, fkBIndex))
            {
                report.WriteLine("  ** Warning: Navigator B '{0}' Index Column is missing", fkBName);
                if (repair)
                {
                    // TODO: Call case
                }
            }
            if (!rel.NeedsPositionStorage(RelationEndRole.B) && db.CheckColumnExists(tblName, fkBIndex))
            {
                report.WriteLine("  ** Warning: Navigator B '{0}' Index Column exists but property is not indexed", fkBName);
                if (repair)
                {
                    // TODO: Call case
                }
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
                    CheckValueTypeCollections(objClass);
                    CheckExtraColumns(objClass);
                }
                else
                {
                    report.WriteLine("  ** Warning: Table '{0}' is missing", objClass.TableName);
                }
            }
        }
        private void CheckValueTypeCollections(ObjectClass objClass)
        {
            report.WriteLine("  ValueType Collections: ");
            foreach (ValueTypeProperty prop in objClass.Properties.OfType<ValueTypeProperty>()
                .Where(p => p.IsList)
                .OrderBy(p => p.Module.Namespace).ThenBy(p => p.PropertyName))
            {
                string tblName = prop.GetCollectionEntryTable();
                string fkName = "fk_" + prop.ObjectClass.ClassName;
                string valPropName = prop.PropertyName;
                string valPropIndexName = prop.PropertyName + "Index";
                string assocName = prop.GetAssociationName();
                string refTblName = objClass.TableName;
                if (db.CheckTableExists(tblName))
                {
                    report.WriteLine("    {0}", prop.PropertyName);
                    if (!db.CheckColumnExists(tblName, fkName))
                    {
                        report.WriteLine("      ** Warning: FK Column '{0}' is missing", fkName);
                    }
                    if (!db.CheckColumnExists(tblName, valPropName))
                    {
                        report.WriteLine("      ** Warning: Value Column '{0}' is missing", valPropName);
                    }
                    if (prop.IsIndexed && !db.CheckColumnExists(tblName, valPropIndexName))
                    {
                        report.WriteLine("      ** Warning: Index Column '{0}' is missing", valPropIndexName);
                    }
                    if (!prop.IsIndexed && db.CheckColumnExists(tblName, valPropIndexName))
                    {
                        report.WriteLine("      ** Warning: Index Column '{0}' exists but property is not indexed", valPropIndexName);
                    }
                    if (!db.CheckFKConstraintExists(assocName))
                    {
                        report.WriteLine("      ** Warning: FK Constraint is missing", prop.PropertyName);
                        if (repair)
                        {
                            db.CreateFKConstraint(tblName, refTblName, fkName, assocName);
                        }
                    }
                }
                else
                {
                    report.WriteLine("    ** Warning: Table '{0}' for Property '{1}' is missing", tblName, prop.PropertyName);
                    if (repair)
                    {
                        CaseNewValueTypePropertyList(prop);
                    }
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
                    report.WriteLine("    {0}", colName);
                    // TODO: Add DataType Check
                    if (db.GetIsColumnNullable(tblName, colName) != prop.IsNullable)
                    {
                        report.WriteLine("      ** Warning: Column \"{0}\" nullable mismatch. Column is {1} but should be {2}", colName,
                            db.GetIsColumnNullable(tblName, colName) ? "NULLABLE" : "NOT NULLABLE",
                            prop.IsNullable ? "NULLABLE" : "NOT NULLABLE");
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
                            report.WriteLine("      ** Warning: Column \"{0}\" length mismatch. Columns length is {1} but should be {2}", colName,
                                db.GetColumnMaxLength(tblName, colName),
                                ((StringProperty)prop).Length);
                        }
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
                if (IsCaseNewObjectClass(objClass))
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
                if (IsCaseNewValueTypePropertyNullable(prop))
                {
                    CaseNewValueTypePropertyNullable(prop);
                }
                if (IsCaseNewValueTypePropertyNotNullable(prop))
                {
                    CaseNewValueTypePropertyNotNullable(prop);
                }
            }

            foreach (ValueTypeProperty prop in objClass.Properties.OfType<ValueTypeProperty>().Where(p => p.IsList))
            {
                if (IsCaseNewValueTypePropertyList(prop))
                {
                    CaseNewValueTypePropertyList(prop);
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
                    if (IsCaseNew_1_N_Relation(rel))
                    {
                        CaseNew_1_N_Relation(rel);
                    }
                }
                else if (rel.GetRelationType() == RelationType.n_m)
                {
                    if (IsCaseNew_N_M_Relation(rel))
                    {
                        CaseNew_N_M_Relation(rel);
                    }
                }
                else if (rel.GetRelationType() == RelationType.one_one)
                {
                    if (IsCaseNew_1_1_Relation(rel))
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
                if (IsCaseNewObjectClassInheritance(objClass))
                {
                    CaseNewObjectClassInheritance(objClass);
                }
            }
            report.WriteLine();
        }

        #region Cases

        #region NewObjectClass
        private bool IsCaseNewObjectClass(ObjectClass objClass)
        {
            return savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid) == null;
        }
        private void CaseNewObjectClass(ObjectClass objClass)
        {
            report.WriteLine("  New Table: {0}", objClass.TableName);
            db.CreateTable(objClass.TableName, objClass.BaseObjectClass == null);
        }
        #endregion

        #region NewValueTypeProperty nullable
        private bool IsCaseNewValueTypePropertyNullable(ValueTypeProperty prop)
        {
            return prop.IsNullable && savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid) == null;
        }
        private void CaseNewValueTypePropertyNullable(ValueTypeProperty prop)
        {
            report.WriteLine("    New nullable ValueType Property: {0}", prop.PropertyName);
            db.CreateColumn(((ObjectClass)prop.ObjectClass).TableName, prop.PropertyName, GetDbType(prop),
                prop is StringProperty ? ((StringProperty)prop).Length : 0, true);
        }
        #endregion

        #region NewValueTypeProperty not nullable
        private bool IsCaseNewValueTypePropertyNotNullable(ValueTypeProperty prop)
        {
            return !prop.IsNullable && savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid) == null;
        }
        private void CaseNewValueTypePropertyNotNullable(ValueTypeProperty prop)
        {
            report.WriteLine("    New nullable ValueType Property: {0}", prop.PropertyName);
            string tblName = ((ObjectClass)prop.ObjectClass).TableName;
            string colName = prop.PropertyName;
            if (!db.CheckTableContainsData(tblName))
            {
                db.CreateColumn(tblName, colName, GetDbType(prop),
                    prop is StringProperty ? ((StringProperty)prop).Length : 0, false);
            }
            else
            {
                report.WriteLine("    ** Warning: unable to create new nullable ValueType Property '{0}' when table '{1}' contains data", colName, tblName);
            }
        }
        #endregion

        #region NewValueTypePropertyList
        private bool IsCaseNewValueTypePropertyList(ValueTypeProperty prop)
        {
            return savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid) == null;
        }
        private void CaseNewValueTypePropertyList(ValueTypeProperty prop)
        {
            report.WriteLine("    New ValueType Property List: {0}", prop.PropertyName);
            string tblName = prop.GetCollectionEntryTable();
            string fkName = "fk_" + prop.ObjectClass.ClassName;
            string valPropName = prop.PropertyName;
            string valPropIndexName = prop.PropertyName + "Index";
            string assocName = prop.GetAssociationName();

            db.CreateTable(tblName, true);

            db.CreateColumn(tblName, fkName, System.Data.DbType.Int32, 0, false);
            db.CreateColumn(tblName, valPropName, GetDbType(prop), prop is StringProperty ? ((StringProperty)prop).Length : 0, false);
            if (prop.IsIndexed)
            {
                db.CreateColumn(tblName, valPropIndexName, System.Data.DbType.Int32, 0, false);
            }
            db.CreateFKConstraint(tblName, ((ObjectClass)prop.ObjectClass).TableName, fkName, prop.GetAssociationName());
        }
        #endregion

        #region New_1_N_Relation
        private bool IsCaseNew_1_N_Relation(Relation rel)
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
        private bool IsCaseNew_N_M_Relation(Relation rel)
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
        private bool IsCaseNew_1_1_Relation(Relation rel)
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
        private bool IsCaseNewObjectClassInheritance(ObjectClass objClass)
        {
            if (objClass.BaseObjectClass == null) return false;

            ObjectClass savedObjClass = savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid);
            return savedObjClass == null || savedObjClass.BaseObjectClass == null;
        }
        private void CaseNewObjectClassInheritance(ObjectClass objClass)
        {
            string assocName = Construct.InheritanceAssociationName(objClass.BaseObjectClass, objClass);
            string tblName = objClass.TableName;

            report.WriteLine("  New ObjectClass Inheritance: {0} -> {1}: {2}", objClass.ClassName, objClass.BaseObjectClass.ClassName, assocName);

            if (db.CheckTableContainsData(tblName))
            {
                report.WriteLine("    ** Warning: Table '{0}' contains data. Unable to add inheritence.", tblName);
                return;
            }

            db.CreateFKConstraint(tblName, objClass.BaseObjectClass.TableName, "ID", assocName);
        }
        #endregion

        #endregion

        #endregion

    }
}
