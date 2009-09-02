using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
// TODO: Das gehört angeschaut.
using Kistl.Server.Generators.EntityFramework.Implementation;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Generators;

namespace Kistl.Server.SchemaManagement
{
    internal class Cases : IDisposable
    {
        #region Fields
        private IKistlContext schema;
        private ISchemaProvider db;
        private TextWriter report;

        private IKistlContext _savedSchema;
        public IKistlContext savedSchema
        {
            get
            {
                if (_savedSchema == null)
                {
                    _savedSchema = SchemaManager.GetSavedSchema();
                }

                return _savedSchema;
            }
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            // Do not dispose "schema" -> passed to this class
            if (savedSchema != null) savedSchema.Dispose();
        }
        #endregion


        internal Cases(IKistlContext schema, ISchemaProvider db, TextWriter report)
        {
            this.schema = schema;
            this.db = db;
            this.report = report;
        }

        // Add all IsCase_ + DoCase_ Methods

        #region Cases

        #region DeleteObjectClass
        public bool IsDeleteObjectClass(ObjectClass objClass)
        {
            return schema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid) == null;
        }
        public void DoDeleteObjectClass(ObjectClass objClass)
        {
            report.WriteLine("  Drop Table: {0}", objClass.TableName);
            db.DropTable(objClass.TableName);
        }
        #endregion

        #region NewObjectClass
        public bool IsNewObjectClass(ObjectClass objClass)
        {
            return savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid) == null;
        }
        public void DoNewObjectClass(ObjectClass objClass)
        {
            report.WriteLine("  New Table: {0}", objClass.TableName);
            db.CreateTable(objClass.TableName, objClass.BaseObjectClass == null);
        }
        #endregion

        #region RenameObjectClassTable
        public bool IsRenameObjectClassTable(ObjectClass objClass)
        {
            var saved = savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid);
            if (saved == null) return false;
            return saved.TableName != objClass.TableName;
        }
        public void DoRenameObjectClassTable(ObjectClass objClass)
        {
            var saved = savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid);
            report.WriteLine("  ** Warning: renaming a tablename from '{0}' to '{1}' is not supported yet", saved.TableName, objClass.TableName);
        }
        #endregion

        #region RenameValueTypePropertyName
        public bool IsRenameValueTypePropertyName(ValueTypeProperty prop)
        {
            var saved = savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid);
            if (saved == null) return false;
            return saved.PropertyName != prop.PropertyName;
        }
        public void DoRenameValueTypePropertyName(ObjectClass objClass, ValueTypeProperty prop, string prefix)
        {
            var saved = savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid);
            report.WriteLine("  ** Warning: renaming a Property from '{0}' to '{1}' is not supported yet", saved.PropertyName, prop.PropertyName);
        }
        #endregion

        #region MoveValueTypeProperty
        public bool IsMoveValueTypeProperty(ValueTypeProperty prop)
        {
            var saved = savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid);
            if (saved == null) return false;
            return saved.ObjectClass.ExportGuid != prop.ObjectClass.ExportGuid;
        }
        public void DoMoveValueTypeProperty(ObjectClass objClass, ValueTypeProperty prop, string prefix)
        {
            var saved = savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid);
            report.WriteLine("  ** Warning: moving a Property from '{0}' to '{1}' is not supported yet", saved.ObjectClass.ClassName, prop.ObjectClass.ClassName);
        }
        #endregion

        #region NewValueTypeProperty nullable
        public bool IsNewValueTypePropertyNullable(ValueTypeProperty prop)
        {
            return prop.IsNullable && savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid) == null;
        }
        public void DoNewValueTypePropertyNullable(ObjectClass objClass, ValueTypeProperty prop, string prefix)
        {
            string colName = Construct.NestedColumnName(prop, prefix);
            report.WriteLine("    New nullable ValueType Property: '{0}' ('{1}')", prop.PropertyName, colName);
            db.CreateColumn(objClass.TableName, colName, SchemaManager.GetDbType(prop),
                prop is StringProperty ? ((StringProperty)prop).Length : 0, true);
        }
        #endregion

        #region NewValueTypeProperty not nullable
        public bool IsNewValueTypePropertyNotNullable(ValueTypeProperty prop)
        {
            return !prop.IsNullable && savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid) == null;
        }
        public void DoNewValueTypePropertyNotNullable(ObjectClass objClass, ValueTypeProperty prop, string prefix)
        {
            string tblName = objClass.TableName;
            string colName = Construct.NestedColumnName(prop, prefix);
            report.WriteLine("    New not nullable ValueType Property: {0} ('{1}')", prop.PropertyName, colName);
            if (!db.CheckTableContainsData(tblName))
            {
                db.CreateColumn(tblName, colName, SchemaManager.GetDbType(prop),
                    prop is StringProperty ? ((StringProperty)prop).Length : 0, false);
            }
            else
            {
                report.WriteLine("    ** Warning: unable to create new not nullable ValueType Property '{0}' when table '{1}' contains data", colName, tblName);
            }
        }
        #endregion

        #region RenameValueTypePropertyListName
        public bool IsRenameValueTypePropertyListName(ValueTypeProperty prop)
        {
            var saved = savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid);
            if (saved == null) return false;
            return saved.PropertyName != prop.PropertyName;
        }
        public void DoRenameValueTypePropertyListName(ObjectClass objClass, ValueTypeProperty prop)
        {
            var saved = savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid);
            report.WriteLine("  ** Warning: renaming a Property from '{0}' to '{1}' is not supported yet", saved.PropertyName, prop.PropertyName);
        }
        #endregion

        #region MoveValueTypePropertyList
        public bool IsMoveValueTypePropertyList(ValueTypeProperty prop)
        {
            var saved = savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid);
            if (saved == null) return false;
            return saved.ObjectClass.ExportGuid != prop.ObjectClass.ExportGuid;
        }
        public void DoMoveValueTypePropertyList(ObjectClass objClass, ValueTypeProperty prop)
        {
            var saved = savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid);
            report.WriteLine("  ** Warning: moving a Property from '{0}' to '{1}' is not supported yet", saved.ObjectClass.ClassName, prop.ObjectClass.ClassName);
        }
        #endregion

        #region NewValueTypePropertyList
        public bool IsNewValueTypePropertyList(ValueTypeProperty prop)
        {
            return savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid) == null;
        }
        public void DoNewValueTypePropertyList(ObjectClass objClass, ValueTypeProperty prop)
        {
            report.WriteLine("    New ValueType Property List: {0}", prop.PropertyName);
            string tblName = prop.GetCollectionEntryTable();
            string fkName = "fk_" + prop.ObjectClass.ClassName;
            string valPropName = prop.PropertyName;
            string valPropIndexName = prop.PropertyName + "Index";
            string assocName = prop.GetAssociationName();

            db.CreateTable(tblName, true);

            db.CreateColumn(tblName, fkName, System.Data.DbType.Int32, 0, false);
            db.CreateColumn(tblName, valPropName, SchemaManager.GetDbType(prop), prop is StringProperty ? ((StringProperty)prop).Length : 0, false);
            if (prop.IsIndexed)
            {
                db.CreateColumn(tblName, valPropIndexName, System.Data.DbType.Int32, 0, false);
            }
            db.CreateFKConstraint(tblName, objClass.TableName, fkName, prop.GetAssociationName(), true);
        }
        #endregion

        #region 1_N_RelationChange_FromNotIndexed_To_Indexed
        public bool Is_1_N_RelationChange_FromNotIndexed_To_Indexed(Relation rel)
        {
            var savedRel = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);
            if (savedRel == null) return false;
            return (rel.NeedsPositionStorage(RelationEndRole.A) && !savedRel.NeedsPositionStorage(RelationEndRole.A)) ||
                    (rel.NeedsPositionStorage(RelationEndRole.B) && !savedRel.NeedsPositionStorage(RelationEndRole.B));
        }
        public void Do_1_N_RelationChange_FromNotIndexed_To_Indexed(Relation rel)
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
        public bool Is_1_N_RelationChange_FromIndexed_To_NotIndexed(Relation rel)
        {
            var savedRel = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);
            if (savedRel == null) return false;
            return (!rel.NeedsPositionStorage(RelationEndRole.A) && savedRel.NeedsPositionStorage(RelationEndRole.A)) ||
                    (!rel.NeedsPositionStorage(RelationEndRole.B) && savedRel.NeedsPositionStorage(RelationEndRole.B));
        }
        public void Do_1_N_RelationChange_FromIndexed_To_NotIndexed(Relation rel)
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

        #region Delete_1_N_Relation
        public bool IsDelete_1_N_Relation(Relation rel)
        {
            return schema.FindPersistenceObject<Relation>(rel.ExportGuid) == null;
        }
        public void DoDelete_1_N_Relation(Relation rel)
        {
            string assocName = rel.GetAssociationName();
            report.WriteLine("  Deleting 1:N Relation: {0}", assocName);

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
                report.WriteLine("    ** Warning: Relation '{0}' has no Navigator", assocName);
                return;
            }

            db.DropFKConstraint(tblName, assocName);

            string colName = Construct.ForeignKeyColumnName(nav);
            db.DropColumn(tblName, colName);
            if (isIndexed)
            {
                db.DropColumn(tblName, Construct.ListPositionColumnName(nav));
            }
        }
        #endregion

        #region New_1_N_Relation
        public bool IsNew_1_N_Relation(Relation rel)
        {
            return savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid) == null;
        }
        public void DoNew_1_N_Relation(Relation rel)
        {
            string assocName = rel.GetAssociationName();
            report.WriteLine("  New 1:N Relation: {0}", assocName);

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
                report.WriteLine("    ** Warning: Relation '{0}' has no Navigator", assocName);
                return;
            }

            string colName = Construct.ForeignKeyColumnName(nav);
            db.CreateColumn(tblName, colName, System.Data.DbType.Int32, 0, nav.IsNullable);
            db.CreateFKConstraint(tblName, refTblName, colName, assocName, false);

            if (isIndexed)
            {
                db.CreateColumn(tblName, Construct.ListPositionColumnName(nav), System.Data.DbType.Int32, 0, nav.IsNullable);
            }
        }
        #endregion

        #region N_M_RelationChange_FromNotIndexed_To_Indexed
        public bool Is_N_M_RelationChange_FromNotIndexed_To_Indexed(Relation rel, RelationEndRole role)
        {
            var savedRel = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);
            if (savedRel == null) return false;
            return rel.NeedsPositionStorage(role) && !savedRel.NeedsPositionStorage(role);
        }
        public void Do_N_M_RelationChange_FromNotIndexed_To_Indexed(Relation rel, RelationEndRole role)
        {
            string assocName = rel.GetAssociationName();
            report.WriteLine("  Create N:M Relation {1} PositionStorage: {0}", assocName, role);

            string tblName = rel.GetRelationTableName();
            string fkName = rel.GetRelationFkColumnName(role);

            db.CreateColumn(tblName, fkName + Kistl.API.Helper.PositionSuffix, System.Data.DbType.Int32, 0, true);
        }
        #endregion

        #region N_M_RelationChange_FromIndexed_To_NotIndexed
        public bool Is_N_M_RelationChange_FromIndexed_To_NotIndexed(Relation rel, RelationEndRole role)
        {
            var savedRel = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);
            if (savedRel == null) return false;
            return !rel.NeedsPositionStorage(role) && savedRel.NeedsPositionStorage(role);
        }
        public void Do_N_M_RelationChange_FromIndexed_To_NotIndexed(Relation rel, RelationEndRole role)
        {
            string assocName = rel.GetAssociationName();
            report.WriteLine("  Drop N:M Relation {1} PositionStorage: {0}", assocName, role);

            string tblName = rel.GetRelationTableName();
            string fkName = rel.GetRelationFkColumnName(role);

            db.DropColumn(tblName, fkName + Kistl.API.Helper.PositionSuffix);
        }
        #endregion

        #region Delete_N_M_Relation
        public bool IsDelete_N_M_Relation(Relation rel)
        {
            return schema.FindPersistenceObject<Relation>(rel.ExportGuid) == null;
        }
        public void DoDelete_N_M_Relation(Relation rel)
        {
            string assocName = rel.GetAssociationName();
            report.WriteLine("  Deleting N:M Relation: {0}", assocName);

            string tblName = rel.GetRelationTableName();
            string fkAName = rel.GetRelationFkColumnName(RelationEndRole.A);
            string fkBName = rel.GetRelationFkColumnName(RelationEndRole.B);

            db.DropFKConstraint(tblName, rel.GetRelationAssociationName(RelationEndRole.A));
            db.DropFKConstraint(tblName, rel.GetRelationAssociationName(RelationEndRole.B));

            db.DropTable(tblName);
        }
        #endregion

        #region New_N_M_Relation
        public bool IsNew_N_M_Relation(Relation rel)
        {
            return savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid) == null;
        }
        public void DoNew_N_M_Relation(Relation rel)
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
                db.CreateColumn(tblName, fkAName + Kistl.API.Helper.PositionSuffix, System.Data.DbType.Int32, 0, false);
            }

            db.CreateColumn(tblName, fkBName, System.Data.DbType.Int32, 0, false);
            if (rel.NeedsPositionStorage(RelationEndRole.B))
            {
                db.CreateColumn(tblName, fkBName + Kistl.API.Helper.PositionSuffix, System.Data.DbType.Int32, 0, false);
            }

            if (rel.A.Type.ImplementsIExportable(schema) && rel.B.Type.ImplementsIExportable(schema))
            {
                db.CreateColumn(tblName, "ExportGuid", System.Data.DbType.Guid, 0, false);
            }

            db.CreateFKConstraint(tblName, rel.A.Type.TableName, fkAName, rel.GetRelationAssociationName(RelationEndRole.A), false);
            db.CreateFKConstraint(tblName, rel.B.Type.TableName, fkBName, rel.GetRelationAssociationName(RelationEndRole.B), false);
        }
        #endregion

        #region Delete_1_1_Relation
        public bool IsDelete_1_1_Relation(Relation rel)
        {
            return schema.FindPersistenceObject<Relation>(rel.ExportGuid) == null;
        }
        public void DoDelete_1_1_Relation(Relation rel)
        {
            report.WriteLine("  Deleting 1:1 Relation: {0}", rel.GetAssociationName());

            if (rel.A.Navigator.HasStorage())
            {
                Delete_1_1_Relation_DropColumns(rel, rel.A, rel.B, RelationEndRole.A);
            }
            // Difference to 1:N. 1:1 may have storage 'Replicate'
            if (rel.B.Navigator.HasStorage())
            {
                Delete_1_1_Relation_DropColumns(rel, rel.B, rel.A, RelationEndRole.B);
            }
        }

        private void Delete_1_1_Relation_DropColumns(Relation rel, RelationEnd end, RelationEnd otherEnd, RelationEndRole role)
        {
            string tblName = end.Type.TableName;
            string refTblName = otherEnd.Type.TableName;
            string colName = Construct.ForeignKeyColumnName(end.Navigator);
            string assocName = rel.GetRelationAssociationName(role);

            db.DropFKConstraint(tblName, assocName);

            db.DropColumn(tblName, colName);

            if (rel.NeedsPositionStorage(role))
            {
                // TODO: Dont use Navigator IsNullable! Check Multiplicity on RelationEnd. But first check Multiplicity in Schema
                db.DropColumn(tblName, Construct.ListPositionColumnName(end.Navigator));
            }
        }
        #endregion

        #region New_1_1_Relation
        public bool IsNew_1_1_Relation(Relation rel)
        {
            return savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid) == null;
        }
        public void DoNew_1_1_Relation(Relation rel)
        {
            report.WriteLine("  New 1:1 Relation: {0}", rel.GetAssociationName());

            if (rel.Storage == StorageType.MergeIntoA || rel.Storage == StorageType.Replicate)
            {
                New_1_1_Relation_CreateColumns(rel, rel.A, rel.B, RelationEndRole.A);
            }

            if (rel.Storage == StorageType.MergeIntoB || rel.Storage == StorageType.Replicate)
            {
                New_1_1_Relation_CreateColumns(rel, rel.B, rel.A, RelationEndRole.B);
            }
        }

        private void New_1_1_Relation_CreateColumns(Relation rel, RelationEnd end, RelationEnd otherEnd, RelationEndRole role)
        {
            string tblName = end.Type.TableName;
            string refTblName = otherEnd.Type.TableName;
            string colName = Construct.ForeignKeyColumnName(end.Navigator);
            string assocName = rel.GetRelationAssociationName(role);

            db.CreateColumn(tblName, colName, System.Data.DbType.Int32, 0, end.Navigator.IsNullable);
            db.CreateFKConstraint(tblName, refTblName, colName, assocName, false);

            if (rel.NeedsPositionStorage(role))
            {
                // TODO: Dont use Navigator IsNullable! Check Multiplicity on RelationEnd. But first check Multiplicity in Schema
                db.CreateColumn(tblName, Construct.ListPositionColumnName(end.Navigator), System.Data.DbType.Int32, 0, end.Navigator.IsNullable);
            }
        }
        #endregion

        #region NewObjectClassInheritance
        public bool IsNewObjectClassInheritance(ObjectClass objClass)
        {
            if (objClass.BaseObjectClass == null) return false;

            ObjectClass savedObjClass = savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid);
            return savedObjClass == null || savedObjClass.BaseObjectClass == null;
        }
        public void DoNewObjectClassInheritance(ObjectClass objClass)
        {
            string assocName = Construct.InheritanceAssociationName(objClass.BaseObjectClass, objClass);
            string tblName = objClass.TableName;

            report.WriteLine("  New ObjectClass Inheritance: {0} -> {1}: {2}", objClass.ClassName, objClass.BaseObjectClass.ClassName, assocName);

            if (db.CheckTableContainsData(tblName))
            {
                report.WriteLine("    ** Warning: Table '{0}' contains data. Unable to add inheritence.", tblName);
                return;
            }

            db.CreateFKConstraint(tblName, objClass.BaseObjectClass.TableName, "ID", assocName, false);
        }
        #endregion

        #region ChangeObjectClassInheritance
        public bool IsChangeObjectClassInheritance(ObjectClass objClass)
        {
            if (objClass.BaseObjectClass == null) return false;

            ObjectClass savedObjClass = savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid);
            if (savedObjClass == null) return false;

            if (savedObjClass.BaseObjectClass == null) return false;
            return savedObjClass.BaseObjectClass.ExportGuid != objClass.BaseObjectClass.ExportGuid;
        }
        public void DoChangeObjectClassInheritance(ObjectClass objClass)
        {
            report.WriteLine("  ** Warning: Changing ObjectClass inheritance is not supported yet");
        }
        #endregion

        #region RemoveObjectClassInheritance
        public bool IsRemoveObjectClassInheritance(ObjectClass objClass)
        {
            ObjectClass savedObjClass = savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid);
            if (savedObjClass == null) return false;

            return savedObjClass.BaseObjectClass != null && objClass.BaseObjectClass == null;
        }
        public void DoRemoveObjectClassInheritance(ObjectClass objClass)
        {
            report.WriteLine("  ** Warning: Removing ObjectClass inheritance is not supported yet");
        }
        #endregion

        #region DeleteValueTypeProperty
        public bool IsDeleteValueTypeProperty(ValueTypeProperty prop)
        {
            return schema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid) == null;
        }

        public void DoDeleteValueTypeProperty(ObjectClass objClass, ValueTypeProperty prop, string prefix)
        {
            string tblName = objClass.TableName;
            string colName = Construct.NestedColumnName(prop, prefix);
            report.WriteLine("  Drop Column: {0}.{1}", tblName, colName);
            db.DropColumn(tblName, colName);
        }
        #endregion

        #endregion
    }
}
