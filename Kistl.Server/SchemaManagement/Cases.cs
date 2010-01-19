using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Generators;

namespace Kistl.Server.SchemaManagement
{
    internal class Cases
        : IDisposable
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server.Schema.Cases");

        #region Fields
        private readonly IKistlContext schema;
        private readonly ISchemaProvider db;

        private readonly IKistlContext _savedSchema;
        public IKistlContext savedSchema
        {
            get
            {
                return _savedSchema;
            }
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            // Do not dispose "schema" -> passed to this class
            // Do not dispose "db" -> passed to this class
            if (_savedSchema != null) _savedSchema.Dispose();
        }
        #endregion

        internal Cases(IKistlContext schema, ISchemaProvider db, IKistlContext savedSchema)
        {
            this.schema = schema;
            this.db = db;
            this._savedSchema = savedSchema;
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
            if (objClass.HasSecurityRules(false))
            {
                DoDeleteObjectClassSecurityRules(objClass);
            }

            Log.InfoFormat("Drop Table: {0}", objClass.TableName);
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
            Log.InfoFormat("  New Table: {0}", objClass.TableName);
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
            Log.ErrorFormat("renaming a table from '{0}' to '{1}' is not supported yet", saved.TableName, objClass.TableName);
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
            Log.ErrorFormat("renaming a property from '{0}' to '{1}' is not supported yet", saved.PropertyName, prop.PropertyName);
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

            // TODO: What will happen if the object hierarchie is also changed?
            var movedUp = IsParentOf(objClass, (ObjectClass)saved.ObjectClass);
            var movedDown = IsParentOf((ObjectClass)saved.ObjectClass, objClass);

            var tblName = objClass.TableName;
            var srcTblName = ((ObjectClass)saved.ObjectClass).TableName;
            var colName = Construct.NestedColumnName(prop, prefix);
            var srcColName = Construct.NestedColumnName(saved, prefix); // TODO: What if prefix has changed
            var dbType = SchemaManager.GetDbType(prop);
            var size = prop is StringProperty ? ((StringProperty)prop).GetMaxLength() : 0;

            if (movedUp)
            {
                Log.InfoFormat("Moving property '{0}' from '{1}' up to '{2}'", prop.PropertyName, saved.ObjectClass.ClassName, objClass.ClassName);
                db.CreateColumn(tblName, colName, dbType, size, true);

                db.CopyColumnData(srcTblName, srcColName, tblName, colName);

                if (!prop.IsNullable())
                {
                    if (db.CheckColumnContainsNulls(tblName, colName))
                    {
                        Log.ErrorFormat("column '{0}.{1}' contains NULL values, cannot set NOT NULLABLE", tblName, colName);
                    }
                    else
                    {
                        db.AlterColumn(tblName, colName, dbType, size, prop.IsNullable());
                    }
                }

                db.DropColumn(srcTblName, srcColName);
            }
            else if (movedDown)
            {
                Log.InfoFormat("Moving property '{0}' from '{1}' down to '{2}' (dataloss possible)", prop.PropertyName, saved.ObjectClass.ClassName, objClass.ClassName);
                db.CreateColumn(tblName, colName, dbType, size, true);

                db.CopyColumnData(srcTblName, srcColName, tblName, colName);

                if (!prop.IsNullable())
                {
                    db.AlterColumn(tblName, colName, dbType, size, prop.IsNullable());
                }

                db.DropColumn(srcTblName, srcColName);
            }
            else
            {
                Log.ErrorFormat("moving a Property from '{0}' to '{1}' is not supported. ObjectClasses are not in the same hierarchy.", saved.ObjectClass.ClassName, prop.ObjectClass.ClassName);
            }
        }

        private static bool IsParentOf(ObjectClass objClass, ObjectClass child)
        {
            var cls = child;
            while (cls != null)
            {
                if (cls.ExportGuid == objClass.ExportGuid)
                {
                    return true;
                }
                cls = cls.BaseObjectClass;
            }
            return false;
        }
        #endregion

        #region NewValueTypeProperty nullable
        public bool IsNewValueTypePropertyNullable(ValueTypeProperty prop)
        {
            return prop.IsNullable() && savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid) == null;
        }
        public void DoNewValueTypePropertyNullable(ObjectClass objClass, ValueTypeProperty prop, string prefix)
        {
            string colName = Construct.NestedColumnName(prop, prefix);
            Log.InfoFormat("New nullable ValueType Property: '{0}' ('{1}')", prop.PropertyName, colName);
            db.CreateColumn(objClass.TableName, colName, SchemaManager.GetDbType(prop),
                prop is StringProperty ? ((StringProperty)prop).GetMaxLength() : 0, true);
        }
        #endregion

        #region NewValueTypeProperty not nullable
        public bool IsNewValueTypePropertyNotNullable(ValueTypeProperty prop)
        {
            return !prop.IsNullable() && savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid) == null;
        }
        public void DoNewValueTypePropertyNotNullable(ObjectClass objClass, ValueTypeProperty prop, string prefix)
        {
            string tblName = objClass.TableName;
            string colName = Construct.NestedColumnName(prop, prefix);
            var dbType = SchemaManager.GetDbType(prop);
            var size = prop is StringProperty ? ((StringProperty)prop).GetMaxLength() : 0;
            Log.InfoFormat("New not nullable ValueType Property: [{0}.{1}] (col:{2})", prop.ObjectClass.ClassName, prop.PropertyName, colName);
            if (!db.CheckTableContainsData(tblName))
            {
                db.CreateColumn(tblName, colName, dbType, size, false);
            }
            else
            {
                db.CreateColumn(tblName, colName, dbType, size, true);
                Log.ErrorFormat("unable to create new not nullable ValueType Property '{0}' when table '{1}' contains data. Created nullable column instead.", colName, tblName);
            }
        }
        #endregion

        #region ChangeValueTypeProperty_To_NotNullable
        public bool IsChangeValueTypeProperty_To_NotNullable(ValueTypeProperty prop)
        {
            var saved = savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid);
            if (saved == null) return false;
            return saved.IsNullable() && !prop.IsNullable();
        }
        public void DoChangeValueTypeProperty_To_NotNullable(ObjectClass objClass, ValueTypeProperty prop, string prefix)
        {
            string tblName = objClass.TableName;
            string colName = Construct.NestedColumnName(prop, prefix);

            if (db.CheckColumnContainsNulls(tblName, colName))
            {
                Log.ErrorFormat("column '{0}.{1}' contains NULL values, cannot set NOT NULLABLE", tblName, colName);
            }
            else
            {
                db.AlterColumn(tblName, colName, SchemaManager.GetDbType(prop),
                    prop is StringProperty ? ((StringProperty)prop).GetMaxLength() : 0, prop.IsNullable());
            }
        }
        #endregion

        #region ChangeValueTypeProperty_To_Nullable
        public bool IsChangeValueTypeProperty_To_Nullable(ValueTypeProperty prop)
        {
            var saved = savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid);
            if (saved == null) return false;
            return !saved.IsNullable() && prop.IsNullable();
        }
        public void DoChangeValueTypeProperty_To_Nullable(ObjectClass objClass, ValueTypeProperty prop, string prefix)
        {
            string tblName = objClass.TableName;
            string colName = Construct.NestedColumnName(prop, prefix);

            db.AlterColumn(tblName, colName, SchemaManager.GetDbType(prop),
                prop is StringProperty ? ((StringProperty)prop).GetMaxLength() : 0, prop.IsNullable());
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
            Log.ErrorFormat("renaming a Property from '{0}' to '{1}' is not supported yet", saved.PropertyName, prop.PropertyName);
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
            Log.ErrorFormat("moving a Property from '{0}' to '{1}' is not supported yet", saved.ObjectClass.ClassName, prop.ObjectClass.ClassName);
        }
        #endregion

        #region NewValueTypePropertyList
        public bool IsNewValueTypePropertyList(ValueTypeProperty prop)
        {
            return savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid) == null;
        }
        public void DoNewValueTypePropertyList(ObjectClass objClass, ValueTypeProperty prop)
        {
            Log.InfoFormat("New ValueType Property List: {0}", prop.PropertyName);
            string tblName = prop.GetCollectionEntryTable();
            string fkName = "fk_" + prop.ObjectClass.ClassName;
            string valPropName = prop.PropertyName;
            string valPropIndexName = prop.PropertyName + "Index";
            string assocName = prop.GetAssociationName();

            db.CreateTable(tblName, true);

            db.CreateColumn(tblName, fkName, System.Data.DbType.Int32, 0, false);
            db.CreateColumn(tblName, valPropName, SchemaManager.GetDbType(prop), prop is StringProperty ? ((StringProperty)prop).GetMaxLength() : 0, false);
            if (prop.HasPersistentOrder)
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
            Log.InfoFormat("Create 1:N Relation Position Storage: {0}", assocName);

            RelationEnd relEnd, otherEnd;

            switch (rel.Storage)
            {
                case StorageType.MergeIntoA:
                    relEnd = rel.A;
                    otherEnd = rel.B;
                    break;
                case StorageType.MergeIntoB:
                    otherEnd = rel.A;
                    relEnd = rel.B;
                    break;
                default:
                    Log.ErrorFormat("Relation '{0}' has unsupported Storage set: {1}, skipped", assocName, rel.Storage);
                    return;
            }

            string tblName = relEnd.Type.TableName;
            db.CreateColumn(tblName, Construct.ListPositionColumnName(otherEnd), System.Data.DbType.Int32, 0, otherEnd.IsNullable());
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
            Log.InfoFormat("Drop 1:N Relation Position Storage: {0}", assocName);

            string tblName = String.Empty;
            RelationEnd otherEnd;
            if (rel.HasStorage(RelationEndRole.A))
            {
                tblName = rel.A.Type.TableName;
                otherEnd = rel.B;
            }
            else if (rel.HasStorage(RelationEndRole.B))
            {
                tblName = rel.B.Type.TableName;
                otherEnd = rel.A;
            }
            else
            {
                Log.ErrorFormat("Relation '{0}' has unsupported Storage set: {1}, skipped", assocName, rel.Storage);
                return;
            }

            db.DropColumn(tblName, Construct.ListPositionColumnName(otherEnd));
        }
        #endregion

        #region 1_N_RelationChange_FromNotNullable_To_Nullable
        public bool Is_1_N_RelationChange_FromNotNullable_To_Nullable(Relation rel)
        {
            var savedRel = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);
            if (savedRel == null) return false;
            return (rel.A.IsNullable() && !savedRel.A.IsNullable()) ||
                    (rel.B.IsNullable() && !savedRel.B.IsNullable());
        }
        public void Do_1_N_RelationChange_FromNotNullable_To_Nullable(Relation rel)
        {
            string assocName = rel.GetAssociationName();
            Log.InfoFormat("Make 1:N relation optional: {0}", assocName);

            RelationEnd relEnd, otherEnd;

            switch (rel.Storage)
            {
                case StorageType.MergeIntoA:
                    relEnd = rel.A;
                    otherEnd = rel.B;
                    break;
                case StorageType.MergeIntoB:
                    otherEnd = rel.A;
                    relEnd = rel.B;
                    break;
                default:
                    Log.ErrorFormat("Relation '{0}' has unsupported Storage set: {1}, skipped", assocName, rel.Storage);
                    return;
            }

            string tblName = relEnd.Type.TableName;
            string colName = Construct.ForeignKeyColumnName(otherEnd);

            db.AlterColumn(tblName, colName, System.Data.DbType.Int32, 0, otherEnd.IsNullable());
        }
        #endregion

        #region 1_N_RelationChange_FromNullable_To_NotNullable
        public bool Is_1_N_RelationChange_FromNullable_To_NotNullable(Relation rel)
        {
            var savedRel = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);
            if (savedRel == null) return false;
            return (!rel.A.IsNullable() && savedRel.A.IsNullable()) ||
                    (!rel.B.IsNullable() && savedRel.B.IsNullable());
        }
        public void Do_1_N_RelationChange_FromNullable_To_NotNullable(Relation rel)
        {
            string assocName = rel.GetAssociationName();
            Log.InfoFormat("Make 1:N relation mandatory: {0}", assocName);

            RelationEnd relEnd, otherEnd;

            switch (rel.Storage)
            {
                case StorageType.MergeIntoA:
                    relEnd = rel.A;
                    otherEnd = rel.B;
                    break;
                case StorageType.MergeIntoB:
                    otherEnd = rel.A;
                    relEnd = rel.B;
                    break;
                default:
                    Log.ErrorFormat("Relation '{0}' has unsupported Storage set: {1}, skipped", assocName, rel.Storage);
                    return;
            }

            string tblName = relEnd.Type.TableName;
            string colName = Construct.ForeignKeyColumnName(otherEnd);

            if (db.CheckColumnContainsNulls(tblName, colName))
            {
                Log.ErrorFormat("column '{0}.{1}' contains NULL values, cannot set NOT NULLABLE", tblName, colName);
            }
            else
            {
                db.AlterColumn(tblName, colName, System.Data.DbType.Int32, 0, otherEnd.IsNullable());
            }
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
            Log.InfoFormat("Deleting 1:N Relation: {0}", assocName);

            string tblName = String.Empty;
            string refTblName = String.Empty;
            bool isIndexed = false;
            RelationEnd otherEnd;
            if (rel.HasStorage(RelationEndRole.A))
            {
                tblName = rel.A.Type.TableName;
                refTblName = rel.B.Type.TableName;
                isIndexed = rel.NeedsPositionStorage(RelationEndRole.A);
                otherEnd = rel.B;
            }
            else if (rel.HasStorage(RelationEndRole.B))
            {
                tblName = rel.B.Type.TableName;
                refTblName = rel.A.Type.TableName;
                isIndexed = rel.NeedsPositionStorage(RelationEndRole.B);
                otherEnd = rel.A;
            }
            else
            {
                Log.ErrorFormat("Relation '{0}' has unsupported Storage set: {1}, skipped", assocName, rel.Storage);
                return;
            }

            db.DropFKConstraint(tblName, assocName);

            string colName = Construct.ForeignKeyColumnName(otherEnd);
            db.DropColumn(tblName, colName);
            if (isIndexed)
            {
                db.DropColumn(tblName, Construct.ListPositionColumnName(otherEnd));
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
            Log.InfoFormat("New 1:N Relation: {0}", assocName);

            RelationEnd relEnd, otherEnd;

            switch (rel.Storage)
            {
                case StorageType.MergeIntoA:
                    relEnd = rel.A;
                    otherEnd = rel.B;
                    break;
                case StorageType.MergeIntoB:
                    otherEnd = rel.A;
                    relEnd = rel.B;
                    break;
                default:
                    Log.ErrorFormat("Relation '{0}' has unsupported Storage set: {1}, skipped", assocName, rel.Storage);
                    return;
            }

            string tblName = relEnd.Type.TableName;
            string refTblName = otherEnd.Type.TableName;
            bool isIndexed = rel.NeedsPositionStorage(relEnd.GetRole());

            string colName = Construct.ForeignKeyColumnName(otherEnd);
            string indexName = Construct.ListPositionColumnName(otherEnd);

            CreateNotNullableColumn(otherEnd, tblName, colName);
            db.CreateFKConstraint(tblName, refTblName, colName, assocName, false);

            if (isIndexed)
            {
                db.CreateColumn(tblName, indexName, System.Data.DbType.Int32, 0, otherEnd.IsNullable());
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
            Log.InfoFormat("Create N:M Relation {1} PositionStorage: {0}", assocName, role);

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
            Log.InfoFormat("Drop N:M Relation {1} PositionStorage: {0}", assocName, role);

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
            Log.InfoFormat("Deleting N:M Relation: {0}", assocName);

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
            Log.InfoFormat("New N:M Relation: {0}", assocName);

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

            if (rel.A.Type.ImplementsIExportable() && rel.B.Type.ImplementsIExportable())
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
            Log.InfoFormat("Deleting 1:1 Relation: {0}", rel.GetAssociationName());

            if (rel.HasStorage(RelationEndRole.A))
            {
                Delete_1_1_Relation_DropColumns(rel, rel.A, rel.B, RelationEndRole.A);
            }
            // Difference to 1:N. 1:1 may have storage 'Replicate'
            if (rel.HasStorage(RelationEndRole.B))
            {
                Delete_1_1_Relation_DropColumns(rel, rel.B, rel.A, RelationEndRole.B);
            }
        }

        private void Delete_1_1_Relation_DropColumns(Relation rel, RelationEnd end, RelationEnd otherEnd, RelationEndRole role)
        {
            string tblName = end.Type.TableName;
            string refTblName = otherEnd.Type.TableName;
            string colName = Construct.ForeignKeyColumnName(otherEnd);
            string assocName = rel.GetRelationAssociationName(role);

            db.DropFKConstraint(tblName, assocName);

            db.DropColumn(tblName, colName);

            if (rel.NeedsPositionStorage(role))
            {
                db.DropColumn(tblName, Construct.ListPositionColumnName(otherEnd));
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
            Log.InfoFormat("New 1:1 Relation: {0}", rel.GetAssociationName());

            if (rel.Storage == StorageType.MergeIntoA || rel.Storage == StorageType.Replicate)
            {
                New_1_1_Relation_CreateColumns(rel, rel.A, rel.B, RelationEndRole.A);
            }

            if (rel.Storage == StorageType.MergeIntoB || rel.Storage == StorageType.Replicate)
            {
                New_1_1_Relation_CreateColumns(rel, rel.B, rel.A, RelationEndRole.B);
            }
        }

        private void New_1_1_Relation_CreateColumns(Relation rel, RelationEnd relEnd, RelationEnd otherEnd, RelationEndRole role)
        {
            string tblName = relEnd.Type.TableName;
            string refTblName = otherEnd.Type.TableName;
            string colName = Construct.ForeignKeyColumnName(otherEnd);
            string assocName = rel.GetRelationAssociationName(role);

            CreateNotNullableColumn(otherEnd, tblName, colName);
            db.CreateFKConstraint(tblName, refTblName, colName, assocName, false);

            if (rel.NeedsPositionStorage(role))
            {
                Log.ErrorFormat("1:1 Relation should never need position storage, but this one does!");
            }
        }

        private void CreateNotNullableColumn(RelationEnd otherEnd, string tblName, string colName)
        {
            Log.InfoFormat("Creating new column '{0}.{1}'", tblName, colName);

            if (otherEnd.IsNullable() || !db.CheckTableContainsData(tblName))
            {
                db.CreateColumn(tblName, colName, System.Data.DbType.Int32, 0, otherEnd.IsNullable());
            }
            else
            {
                db.CreateColumn(tblName, colName, System.Data.DbType.Int32, 0, true);
                Log.ErrorFormat("Unable to create NOT NULL column, since table contains data. Created nullable column instead");
            }
        }
        #endregion

        #region 1_1_RelationChange_FromNullable_To_NotNullable
        public bool Is1_1_RelationChange_FromNullable_To_NotNullable(Relation rel, RelationEndRole role)
        {
            Relation savedRel = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);
            if (savedRel == null)
            {
                return false;
            }
            return savedRel.GetOtherEndFromRole(role).IsNullable() && !rel.GetOtherEndFromRole(role).IsNullable()
                && ((rel.Storage == StorageType.MergeIntoA && role == RelationEndRole.A)
                    || (rel.Storage == StorageType.MergeIntoB && role == RelationEndRole.B)
                    || (rel.Storage == StorageType.Replicate));
        }
        public void Do1_1_RelationChange_FromNullable_To_NotNullable(Relation rel, RelationEndRole role)
        {
            RelationEnd relEnd = rel.GetEndFromRole(role);
            RelationEnd otherEnd = rel.GetOtherEndFromRole(role);

            string tblName = relEnd.Type.TableName;
            string colName = Construct.ForeignKeyColumnName(otherEnd);

            db.AlterColumn(tblName, colName, System.Data.DbType.Int32, 0, otherEnd.IsNullable());
        }
        #endregion

        #region 1_1_RelationChange_FromNotNullable_To_Nullable
        public bool Is1_1_RelationChange_FromNotNullable_To_Nullable(Relation rel, RelationEndRole role)
        {
            Relation savedRel = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);
            if (savedRel == null)
            {
                return false;
            }
            return !savedRel.GetOtherEndFromRole(role).IsNullable() && rel.GetOtherEndFromRole(role).IsNullable()
                && ((rel.Storage == StorageType.MergeIntoA && role == RelationEndRole.A)
                    || (rel.Storage == StorageType.MergeIntoB && role == RelationEndRole.B)
                    || (rel.Storage == StorageType.Replicate));
        }
        public void Do1_1_RelationChange_FromNotNullable_To_Nullable(Relation rel, RelationEndRole role)
        {
            RelationEnd relEnd = rel.GetEndFromRole(role);
            RelationEnd otherEnd = rel.GetOtherEndFromRole(role);

            string tblName = relEnd.Type.TableName;
            string colName = Construct.ForeignKeyColumnName(otherEnd);

            if (db.CheckColumnContainsNulls(tblName, colName))
            {
                Log.ErrorFormat("column '{0}.{1}' contains NULL values, cannot set NOT NULLABLE", tblName, colName);
            }
            else
            {
                db.AlterColumn(tblName, colName, System.Data.DbType.Int32, 0, otherEnd.IsNullable());
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

            Log.InfoFormat("New ObjectClass Inheritance: {0} -> {1}: {2}", objClass.ClassName, objClass.BaseObjectClass.ClassName, assocName);

            if (db.CheckTableContainsData(tblName))
            {
                Log.ErrorFormat("Table '{0}' contains data. Unable to add inheritence.", tblName);
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
            Log.InfoFormat("Changing ObjectClass Inheritance: {0} -> {1}", objClass.ClassName, objClass.BaseObjectClass.ClassName);
            DoRemoveObjectClassInheritance(objClass);
            DoNewObjectClassInheritance(objClass);
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
            ObjectClass savedObjClass = savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid);
            string assocName = Construct.InheritanceAssociationName(savedObjClass.BaseObjectClass, savedObjClass);
            string tblName = objClass.TableName;

            Log.InfoFormat("Remove ObjectClass Inheritance: {0} -> {1}: {2}", savedObjClass.ClassName, savedObjClass.BaseObjectClass.ClassName, assocName);

            db.DropFKConstraint(tblName, assocName);
        }
        #endregion

        #region NewObjectClassSecurityRules
        public bool IsNewObjectClassSecurityRules(ObjectClass objClass)
        {
            if (!objClass.HasSecurityRules(false)) return false;
            ObjectClass savedObjClass = savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid);
            return savedObjClass == null || !savedObjClass.HasSecurityRules(false);
        }
        public void DoNewObjectClassSecurityRules(ObjectClass objClass)
        {
            Log.InfoFormat("New ObjectClass Security Rules: {0}", objClass.ClassName);
            string tblRightsName = Construct.SecurityRulesTableName(objClass);

            db.CreateTable(tblRightsName, false, false);
            db.CreateColumn(tblRightsName, "Identity", System.Data.DbType.Int32, 0, false);
            db.CreateColumn(tblRightsName, "Right", System.Data.DbType.Int32, 0, false);

            db.CreateIndex(tblRightsName, Construct.SecurityRulesIndexName(objClass), true, true, "ID", "Identity");
            db.CreateFKConstraint(tblRightsName, objClass.TableName, "ID", Construct.SecurityRulesFKName(objClass), true);

            var tblName = objClass.TableName;
            var updateRightsTriggerName = Construct.SecurityRulesUpdateRightsTriggerName(objClass);
            var rightsViewUnmaterializedName = Construct.SecurityRulesRightsViewUnmaterializedName(objClass);
            var refreshRightsOnProcedureName = Construct.SecurityRulesRefreshRightsOnProcedureName(objClass);

            db.CreateUpdateRightsTrigger(updateRightsTriggerName, rightsViewUnmaterializedName, tblName, tblRightsName);
            db.CreateRightsViewUnmaterialized(rightsViewUnmaterializedName, tblName, tblRightsName);
            db.CreateRefreshRightsOnProcedure(refreshRightsOnProcedureName, rightsViewUnmaterializedName, tblName, tblRightsName);
        }
        #endregion

        #region DeleteObjectClassSecurityRules
        public bool IsDeleteObjectClassSecurityRules(ObjectClass objClass)
        {
            if (objClass.HasSecurityRules(false)) return false;
            ObjectClass savedObjClass = savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid);
            return savedObjClass != null && savedObjClass.HasSecurityRules(false);
        }
        public void DoDeleteObjectClassSecurityRules(ObjectClass objClass)
        {
            var tblRightsName = Construct.SecurityRulesTableName(objClass);
            var rightsViewUnmaterializedName = Construct.SecurityRulesRightsViewUnmaterializedName(objClass);
            var refreshRightsOnProcedureName = Construct.SecurityRulesRefreshRightsOnProcedureName(objClass);

            Log.InfoFormat("Delete ObjectClass Security Rules: {0}", objClass.ClassName);

            db.DropProcedure(refreshRightsOnProcedureName);
            db.DropView(rightsViewUnmaterializedName);
            db.DropTable(tblRightsName);
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
            Log.InfoFormat("Drop Column: {0}.{1}", tblName, colName);
            db.DropColumn(tblName, colName);
        }
        #endregion

        #endregion
    }
}
