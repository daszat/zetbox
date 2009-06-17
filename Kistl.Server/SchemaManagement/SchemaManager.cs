using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Generators;
// TODO: Das gehört angeschaut.
using Kistl.Server.Generators.EntityFramework.Implementation;

namespace Kistl.Server.SchemaManagement
{
    public partial class SchemaManager : IDisposable
    {
        #region Fields
        private IKistlContext schema;
        private IKistlContext savedSchema;
        private ISchemaProvider db;
        private TextWriter report;
        private bool repair = false;
        private Cases Case { get; set; }
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

        #region RenameObjectClassTable
        private bool IsCaseRenameObjectClassTable(ObjectClass objClass)
        {
            var saved = savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid);
            if (saved == null) return false;
            return saved.TableName != objClass.TableName;
        }
        private void CaseRenameObjectClassTable(ObjectClass objClass)
        {
            var saved = savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid);            
            report.WriteLine("  ** Warning: renaming a tablename from '{0}' to '{1}' is not supported yet", saved.TableName, objClass.TableName);
        }
        #endregion

        #region RenameValueTypePropertyName
        private bool IsCaseRenameValueTypePropertyName(ValueTypeProperty prop)
        {
            var saved = savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid);
            if (saved == null) return false;
            return saved.PropertyName != prop.PropertyName;
        }
        private void CaseRenameValueTypePropertyName(ObjectClass objClass, ValueTypeProperty prop, string prefix)
        {
            var saved = savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid);
            report.WriteLine("  ** Warning: renaming a Property from '{0}' to '{1}' is not supported yet", saved.PropertyName, prop.PropertyName);
        }
        #endregion

        #region MoveValueTypeProperty
        private bool IsCaseMoveValueTypeProperty(ValueTypeProperty prop)
        {
            var saved = savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid);
            if (saved == null) return false;
            return saved.ObjectClass.ExportGuid != prop.ObjectClass.ExportGuid;
        }
        private void CaseMoveValueTypeProperty(ObjectClass objClass, ValueTypeProperty prop, string prefix)
        {
            var saved = savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid);
            report.WriteLine("  ** Warning: moving a Property from '{0}' to '{1}' is not supported yet", saved.ObjectClass.ClassName, prop.ObjectClass.ClassName);
        }
        #endregion

        #region NewValueTypeProperty nullable
        private bool IsCaseNewValueTypePropertyNullable(ValueTypeProperty prop)
        {
            return prop.IsNullable && savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid) == null;
        }
        private void CaseNewValueTypePropertyNullable(ObjectClass objClass, ValueTypeProperty prop, string prefix)
        {
            string colName = Construct.NestedColumnName(prop, prefix);
            report.WriteLine("    New nullable ValueType Property: '{0}' ('{1}')", prop.PropertyName, colName);
            db.CreateColumn(objClass.TableName, colName, GetDbType(prop),
                prop is StringProperty ? ((StringProperty)prop).Length : 0, true);
        }
        #endregion

        #region NewValueTypeProperty not nullable
        private bool IsCaseNewValueTypePropertyNotNullable(ValueTypeProperty prop)
        {
            return !prop.IsNullable && savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid) == null;
        }
        private void CaseNewValueTypePropertyNotNullable(ObjectClass objClass, ValueTypeProperty prop, string prefix)
        {
            string tblName = objClass.TableName;
            string colName = Construct.NestedColumnName(prop, prefix);
            report.WriteLine("    New not nullable ValueType Property: {0} ('{1}')", prop.PropertyName, colName);
            if (!db.CheckTableContainsData(tblName))
            {
                db.CreateColumn(tblName, colName, GetDbType(prop),
                    prop is StringProperty ? ((StringProperty)prop).Length : 0, false);
            }
            else
            {
                report.WriteLine("    ** Warning: unable to create new not nullable ValueType Property '{0}' when table '{1}' contains data", colName, tblName);
            }
        }
        #endregion

        #region RenameValueTypePropertyListName
        private bool IsCaseRenameValueTypePropertyListName(ValueTypeProperty prop)
        {
            var saved = savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid);
            if (saved == null) return false;
            return saved.PropertyName != prop.PropertyName;
        }
        private void CaseRenameValueTypePropertyListName(ObjectClass objClass, ValueTypeProperty prop)
        {
            var saved = savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid);
            report.WriteLine("  ** Warning: renaming a Property from '{0}' to '{1}' is not supported yet", saved.PropertyName, prop.PropertyName);
        }
        #endregion

        #region MoveValueTypePropertyList
        private bool IsCaseMoveValueTypePropertyList(ValueTypeProperty prop)
        {
            var saved = savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid);
            if (saved == null) return false;
            return saved.ObjectClass.ExportGuid != prop.ObjectClass.ExportGuid;
        }
        private void CaseMoveValueTypePropertyList(ObjectClass objClass, ValueTypeProperty prop)
        {
            var saved = savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid);
            report.WriteLine("  ** Warning: moving a Property from '{0}' to '{1}' is not supported yet", saved.ObjectClass.ClassName, prop.ObjectClass.ClassName);
        }
        #endregion

        #region NewValueTypePropertyList
        private bool IsCaseNewValueTypePropertyList(ValueTypeProperty prop)
        {
            return savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid) == null;
        }
        private void CaseNewValueTypePropertyList(ObjectClass objClass, ValueTypeProperty prop)
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
            db.CreateFKConstraint(tblName, objClass.TableName, fkName, prop.GetAssociationName());
        }
        #endregion

        #region 1_N_RelationChange_FromNotIndexed_To_Indexed
        private bool IsCase_1_N_RelationChange_FromNotIndexed_To_Indexed(Relation rel)
        {
            var savedRel = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);
            if (savedRel == null) return false;
            return  (rel.NeedsPositionStorage(RelationEndRole.A) && !savedRel.NeedsPositionStorage(RelationEndRole.A)) ||
                    (rel.NeedsPositionStorage(RelationEndRole.B) && !savedRel.NeedsPositionStorage(RelationEndRole.B));
        }
        private void Case_1_N_RelationChange_FromNotIndexed_To_Indexed(Relation rel)
        {
            string assocName = rel.GetAssociationName();
            report.WriteLine("  Create 1:N Relation Position Storage: {0}", assocName);

            ObjectReferenceProperty nav = null;
            string tblName = "";
            if (rel.A.Navigator != null && rel.A.Navigator.HasStorage())
            {
                nav = rel.A.Navigator;
                tblName = rel.A.Type.TableName;
            }
            else if (rel.B.Navigator != null && rel.B.Navigator.HasStorage())
            {
                nav = rel.B.Navigator;
                tblName = rel.B.Type.TableName;
            }

            if (nav == null)
            {
                report.WriteLine("    ** Warning: Relation '{0}' has no Navigator", assocName);
                return;
            } 
            
            db.CreateColumn(tblName, Construct.ListPositionColumnName(nav), System.Data.DbType.Int32, 0, nav.IsNullable);
        }
        #endregion

        #region 1_N_RelationChange_FromIndexed_To_NotIndexed
        private bool IsCase_1_N_RelationChange_FromIndexed_To_NotIndexed(Relation rel)
        {
            var savedRel = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);
            if (savedRel == null) return false;
            return  (!rel.NeedsPositionStorage(RelationEndRole.A) && savedRel.NeedsPositionStorage(RelationEndRole.A)) ||
                    (!rel.NeedsPositionStorage(RelationEndRole.B) && savedRel.NeedsPositionStorage(RelationEndRole.B));
        }
        private void Case_1_N_RelationChange_FromIndexed_To_NotIndexed(Relation rel)
        {
            string assocName = rel.GetAssociationName();
            report.WriteLine("  Drop 1:N Relation Position Storage: {0}", assocName);

            ObjectReferenceProperty nav = null;
            string tblName = "";
            if (rel.A.Navigator != null && rel.A.Navigator.HasStorage())
            {
                nav = rel.A.Navigator;
                tblName = rel.A.Type.TableName;
            }
            else if (rel.B.Navigator != null && rel.B.Navigator.HasStorage())
            {
                nav = rel.B.Navigator;
                tblName = rel.B.Type.TableName;
            }

            if (nav == null)
            {
                report.WriteLine("    ** Warning: Relation '{0}' has no Navigator", assocName);
                return;
            }

            db.DropColumn(tblName, Construct.ListPositionColumnName(nav));
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

        #region N_M_RelationChange_FromNotIndexed_To_Indexed
        private bool IsCase_N_M_RelationChange_FromNotIndexed_To_Indexed(Relation rel, RelationEndRole role)
        {
            var savedRel = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);
            if (savedRel == null) return false;
            return rel.NeedsPositionStorage(role) && !savedRel.NeedsPositionStorage(role);
        }
        private void Case_N_M_RelationChange_FromNotIndexed_To_Indexed(Relation rel, RelationEndRole role)
        {
            string assocName = rel.GetAssociationName();
            report.WriteLine("  Create N:M Relation {1} PositionStorage: {0}", assocName, role);

            string tblName = rel.GetRelationTableName();
            string fkName = rel.GetRelationFkColumnName(role);

            db.CreateColumn(tblName, fkName + Kistl.API.Helper.PositionSuffix, System.Data.DbType.Int32, 0, true);
        }
        #endregion

        #region N_M_RelationChange_FromIndexed_To_NotIndexed
        private bool IsCase_N_M_RelationChange_FromIndexed_To_NotIndexed(Relation rel, RelationEndRole role)
        {
            var savedRel = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);
            if (savedRel == null) return false;
            return !rel.NeedsPositionStorage(role) && savedRel.NeedsPositionStorage(role);
        }
        private void Case_N_M_RelationChange_FromIndexed_To_NotIndexed(Relation rel, RelationEndRole role)
        {
            string assocName = rel.GetAssociationName();
            report.WriteLine("  Drop N:M Relation {1} PositionStorage: {0}", assocName, role);

            string tblName = rel.GetRelationTableName();
            string fkName = rel.GetRelationFkColumnName(role);

            db.DropColumn(tblName, fkName + Kistl.API.Helper.PositionSuffix);
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
                // TODO: Dont use Navigator IsNullable! Check Multiplicity on RelationEnd. But first check Multiplicity in Schema
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

        #region ChangeObjectClassInheritance
        private bool IsCaseChangeObjectClassInheritance(ObjectClass objClass)
        {
            if (objClass.BaseObjectClass == null) return false;

            ObjectClass savedObjClass = savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid);
            if (savedObjClass == null) return false;

            if (savedObjClass.BaseObjectClass == null) return false;
            return savedObjClass.BaseObjectClass.ExportGuid != objClass.BaseObjectClass.ExportGuid;
        }
        private void CaseChangeObjectClassInheritance(ObjectClass objClass)
        {
            report.WriteLine("  ** Warning: Changing ObjectClass inheritance is not supported yet");
        }
        #endregion

        #region RemoveObjectClassInheritance
        private bool IsCaseRemoveObjectClassInheritance(ObjectClass objClass)
        {
            ObjectClass savedObjClass = savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid);
            if (savedObjClass == null) return false;

            return savedObjClass.BaseObjectClass != null && objClass.BaseObjectClass == null;
        }
        private void CaseRemoveObjectClassInheritance(ObjectClass objClass)
        {
            report.WriteLine("  ** Warning: Removing ObjectClass inheritance is not supported yet");
        }
        #endregion

        #endregion
    }
}
