// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.Server.SchemaManagement
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Server;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator;
    using Zetbox.Generator.Extensions;

    internal class Cases
        : IDisposable
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Server.Schema.Cases");

        #region Fields
        private readonly IZetboxContext schema;
        private readonly ISchemaProvider db;

        private readonly IZetboxContext _savedSchema;
        public IZetboxContext savedSchema
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

        internal Cases(IZetboxContext schema, ISchemaProvider db, IZetboxContext savedSchema)
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
            if (objClass.NeedsRightsTable())
            {
                DoDeleteObjectClassSecurityRules(objClass);
            }

            var mapping = objClass.GetTableMapping();
            if (mapping == TableMapping.TPT)
            {
                var tbl = objClass.GetTableRef(db);
                Log.InfoFormat("Drop Table: {0}", tbl);
                if (db.CheckTableExists(tbl))
                    db.DropTable(tbl);
            }
            else if (mapping == TableMapping.TPH)
            {
                // Do delete all columns
            }
            else
            {
                throw new NotSupportedException(string.Format("Mapping {0} is not supported", mapping));
            }
        }
        #endregion

        #region NewObjectClass
        public bool IsNewObjectClass(ObjectClass objClass)
        {
            return savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid) == null;
        }
        public void DoNewObjectClass(ObjectClass objClass)
        {
            TableMapping mapping = objClass.GetTableMapping();
            if (mapping == TableMapping.TPT || (mapping == TableMapping.TPH && objClass.BaseObjectClass == null))
            {
                var tblName = objClass.GetTableRef(db);
                Log.InfoFormat("New Table: {0}", tblName);
                if (!db.CheckTableExists(tblName))
                {
                    db.CreateTable(tblName, objClass.BaseObjectClass == null);
                    if (mapping == TableMapping.TPH)
                    {
                        db.CreateColumn(tblName, TableMapper.DiscriminatorColumnName, System.Data.DbType.String, TableMapper.DiscriminatorColumnSize, 0, false);
                    }
                }
                else
                {
                    Log.ErrorFormat("Table {0} already exists", tblName);
                }
            }
            else
            {
                Log.DebugFormat("Skipping table for TPH child {0}", objClass.Name);
            }
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
            var mapping = objClass.GetTableMapping();
            if (mapping == TableMapping.TPT || (mapping == TableMapping.TPH && objClass.BaseObjectClass == null))
            {
                var saved = savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid);
                db.RenameTable(saved.GetTableRef(db), objClass.GetTableRef(db));
            }
        }
        #endregion

        #region RenameValueTypePropertyName
        public bool IsRenameValueTypePropertyName(ObjectClass objClass, ValueTypeProperty prop, string prefix)
        {
            var saved = savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid);
            if (saved == null) return false;
            // TODO: What if prefix has changed
            return saved.Name != prop.Name;
        }
        public void DoRenameValueTypePropertyName(ObjectClass objClass, ValueTypeProperty prop, string prefix)
        {
            var saved = savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid);
            // TODO: What if prefix has changed
            db.RenameColumn(objClass.GetTableRef(db), Construct.ColumnName(saved, prefix), Construct.ColumnName(prop, prefix));
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

            // Reflected changed hierarchie
            var currentOriginObjClass = schema.FindPersistenceObject<ObjectClass>(saved.ObjectClass.ExportGuid);
            var movedUp = IsParentOf(objClass, currentOriginObjClass);
            var movedDown = IsParentOf(currentOriginObjClass, objClass);

            var tblName = objClass.GetTableRef(db);
            var srcTblName = ((ObjectClass)saved.ObjectClass).GetTableRef(db);
            var colName = Construct.ColumnName(prop, prefix);
            var srcColName = Construct.ColumnName(saved, prefix); // TODO: What if prefix has changed
            var dbType = prop.GetDbType();
            var size = prop.GetSize();
            var scale = prop.GetScale();
            var defConstr = SchemaManager.GetDefaultConstraint(prop);

            if (movedUp)
            {
                Log.InfoFormat("Moving property '{0}' from '{1}' up to '{2}'", prop.Name, saved.ObjectClass.Name, objClass.Name);
                db.CreateColumn(tblName, colName, dbType, size, scale, true, defConstr);

                db.CopyColumnData(srcTblName, srcColName, tblName, colName);

                if (!prop.IsNullable())
                {
                    if (db.CheckColumnContainsNulls(tblName, colName))
                    {
                        Log.ErrorFormat("column '{0}.{1}' contains NULL values, cannot set NOT NULLABLE", tblName, colName);
                    }
                    else
                    {
                        db.AlterColumn(tblName, colName, dbType, size, scale, prop.IsNullable(), defConstr);
                    }
                }

                if (db.CheckColumnExists(srcTblName, srcColName))
                    db.DropColumn(srcTblName, srcColName);
            }
            else if (movedDown)
            {
                Log.InfoFormat("Moving property '{0}' from '{1}' down to '{2}' (dataloss possible)", prop.Name, saved.ObjectClass.Name, objClass.Name);
                db.CreateColumn(tblName, colName, dbType, size, scale, true, defConstr);

                db.CopyColumnData(srcTblName, srcColName, tblName, colName);

                if (!prop.IsNullable())
                {
                    db.AlterColumn(tblName, colName, dbType, size, scale, prop.IsNullable(), defConstr);
                }

                if (db.CheckColumnExists(srcTblName, srcColName))
                    db.DropColumn(srcTblName, srcColName);
            }
            else
            {
                Log.ErrorFormat("Moving property '{2}' from '{0}' to '{1}' is not supported. ObjectClasses are not in the same hierarchy. Will only create destination column.", saved.ObjectClass.Name, prop.ObjectClass.Name, prop.Name);
                db.CreateColumn(tblName, colName, dbType, size, scale, true, defConstr);
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
            string colName = Construct.ColumnName(prop, prefix);
            Log.InfoFormat("New nullable ValueType Property: '{0}' ('{1}')", prop.Name, colName);
            CreateValueTypePropertyNullable(objClass.GetTableRef(db), prop, colName);
        }

        private void CreateValueTypePropertyNullable(TableRef tblName, ValueTypeProperty prop, string colName)
        {
            db.CreateColumn(tblName, colName, prop.GetDbType(), prop.GetSize(), prop.GetScale(), true, SchemaManager.GetDefaultConstraint(prop));
        }

        #endregion

        #region NewValueTypeProperty not nullable
        public bool IsNewValueTypePropertyNotNullable(ValueTypeProperty prop)
        {
            return !prop.IsNullable() && savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid) == null;
        }
        public void DoNewValueTypePropertyNotNullable(ObjectClass objClass, ValueTypeProperty prop, string prefix)
        {
            var tblName = objClass.GetTableRef(db);
            var colName = Construct.ColumnName(prop, prefix);
            var dbType = prop.GetDbType();
            var size = prop.GetSize();
            var scale = prop.GetScale();
            var def = SchemaManager.GetDefaultConstraint(prop);
            var realIsNullable = objClass.GetTableMapping() == TableMapping.TPH && objClass.BaseObjectClass != null;
            // todo: add special is null check contraint for TableMapping.TPH
            Log.InfoFormat("New not nullable ValueType Property: [{0}.{1}] (col:{2})", prop.ObjectClass.Name, prop.Name, colName);
            if (!db.CheckTableContainsData(tblName))
            {
                db.CreateColumn(tblName, colName, dbType, size, scale, realIsNullable, def);
            }
            else
            {
                db.CreateColumn(tblName, colName, dbType, size, scale, true, def);
                bool updateDone = false;
                if (def is NewGuidDefaultConstraint)
                {
                    db.WriteGuidDefaultValue(tblName, colName);
                    updateDone = true;
                }
                else if (def is DateTimeDefaultConstraint)
                {
                    if (((DateTimeDefaultConstraint)def).Precision == DateTimeDefaultConstraintPrecision.Date)
                        db.WriteDefaultValue(tblName, colName, DateTime.Today);
                    else
                        db.WriteDefaultValue(tblName, colName, DateTime.Now);
                    updateDone = true;
                }
                else if (def is BoolDefaultConstraint)
                {
                    db.WriteDefaultValue(tblName, colName, ((BoolDefaultConstraint)def).Value);
                    updateDone = true;
                }
                else if (def is IntDefaultConstraint)
                {
                    db.WriteDefaultValue(tblName, colName, ((IntDefaultConstraint)def).Value);
                    updateDone = true;
                }

                if (updateDone)
                {
                    db.CreateColumn(tblName, colName, dbType, size, scale, realIsNullable, def);
                }
                else
                {
                    Log.ErrorFormat("unable to create new not nullable ValueType Property '{0}' when table '{1}' contains data. No supported default contraint found. Created nullable column instead.", colName, tblName);
                }
            }
        }
        #endregion

        #region ChangeDefaultValue
        public bool IsChangeDefaultValue(Property prop)
        {
            var saved = savedSchema.FindPersistenceObject<Property>(prop.ExportGuid);
            if (saved == null) return false;
            if (saved.DefaultValue == null && prop.DefaultValue == null) return false;
            return
                (saved.DefaultValue != null && prop.DefaultValue == null)
                || (saved.DefaultValue == null && prop.DefaultValue != null)
                || (saved.DefaultValue.ExportGuid != prop.DefaultValue.ExportGuid);
        }
        public void DoChangeDefaultValue(ObjectClass objClass, ValueTypeProperty prop, string prefix)
        {
            var tblName = objClass.GetTableRef(db);
            var colName = Construct.ColumnName(prop, prefix);

            // Use current nullable definition. 
            // Another case is responsible to change that.
            var currentIsNullable = db.GetIsColumnNullable(tblName, colName);

            db.AlterColumn(tblName, colName, prop.GetDbType(), prop.GetSize(), prop.GetScale(), currentIsNullable, SchemaManager.GetDefaultConstraint(prop));
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
            var tblName = objClass.GetTableRef(db);
            var colName = Construct.ColumnName(prop, prefix);

            if (db.CheckColumnContainsNulls(tblName, colName))
            {
                Log.ErrorFormat("column '{0}.{1}' contains NULL values, cannot set NOT NULLABLE", tblName, colName);
            }
            else
            {
                db.AlterColumn(tblName, colName, prop.GetDbType(), prop.GetSize(), prop.GetScale(), prop.IsNullable(), null);
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
            var tblName = objClass.GetTableRef(db);
            var colName = Construct.ColumnName(prop, prefix);

            db.AlterColumn(tblName, colName, prop.GetDbType(), prop.GetSize(), prop.GetScale(), prop.IsNullable(), null);
        }
        #endregion

        #region RenameValueTypePropertyListName
        public bool IsRenameValueTypePropertyListName(ValueTypeProperty prop)
        {
            var saved = savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid);
            if (saved == null) return false;
            return saved.Name != prop.Name;
        }
        public void DoRenameValueTypePropertyListName(ObjectClass objClass, ValueTypeProperty prop)
        {
            var saved = savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid);
            Log.ErrorFormat("renaming a Property from '{0}' to '{1}' is not supported yet", saved.Name, prop.Name);
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
            Log.ErrorFormat("moving a Property from '{0}' to '{1}' is not supported yet", saved.ObjectClass.Name, prop.ObjectClass.Name);
        }
        #endregion

        #region NewValueTypePropertyList
        public bool IsNewValueTypePropertyList(ValueTypeProperty prop)
        {
            return savedSchema.FindPersistenceObject<Property>(prop.ExportGuid) == null;
        }
        public void DoNewValueTypePropertyList(ObjectClass objClass, ValueTypeProperty prop)
        {
            Log.InfoFormat("New ValueType Property List: {0}", prop.Name);
            var tblName = db.GetTableName(prop.Module.SchemaName, prop.GetCollectionEntryTable());
            string fkName = prop.GetCollectionEntryReverseKeyColumnName();
            string valPropName = prop.Name;
            string valPropIndexName = prop.Name + "Index";
            string assocName = prop.GetAssociationName();
            bool hasPersistentOrder = prop.HasPersistentOrder;

            db.CreateTable(tblName, true);
            db.CreateColumn(tblName, fkName, System.Data.DbType.Int32, 0, 0, false);

            db.CreateColumn(tblName, valPropName, prop.GetDbType(), prop.GetSize(), prop.GetScale(), false, SchemaManager.GetDefaultConstraint(prop));

            if (hasPersistentOrder)
            {
                db.CreateColumn(tblName, valPropIndexName, System.Data.DbType.Int32, 0, 0, false);
            }
            db.CreateFKConstraint(tblName, objClass.GetTableRef(db), fkName, assocName, true);
            db.CreateIndex(tblName, Construct.IndexName(tblName.Name, fkName), false, false, fkName);
        }
        #endregion

        #region NewCompoundObjectPropertyList
        public bool IsNewCompoundObjectPropertyList(CompoundObjectProperty prop)
        {
            return savedSchema.FindPersistenceObject<Property>(prop.ExportGuid) == null;
        }
        public void DoNewCompoundObjectPropertyList(ObjectClass objClass, CompoundObjectProperty cprop)
        {
            Log.InfoFormat("New CompoundObject Property List: {0}", cprop.Name);
            var tblName = db.GetTableName(cprop.Module.SchemaName, cprop.GetCollectionEntryTable());
            string fkName = cprop.GetCollectionEntryReverseKeyColumnName();

            // TODO: Support nested CompoundObject
            string valPropIndexName = cprop.Name + "Index";
            string assocName = cprop.GetAssociationName();
            bool hasPersistentOrder = cprop.HasPersistentOrder;

            db.CreateTable(tblName, true);
            db.CreateColumn(tblName, fkName, System.Data.DbType.Int32, 0, 0, false);

            foreach (ValueTypeProperty p in cprop.CompoundObjectDefinition.Properties)
            {
                db.CreateColumn(tblName, Construct.ColumnName(p, cprop.Name), p.GetDbType(), p.GetSize(), p.GetScale(), true, SchemaManager.GetDefaultConstraint(cprop));
            }

            if (hasPersistentOrder)
            {
                db.CreateColumn(tblName, valPropIndexName, System.Data.DbType.Int32, 0, 0, false);
            }
            db.CreateFKConstraint(tblName, objClass.GetTableRef(db), fkName, assocName, true);
            db.CreateIndex(tblName, Construct.IndexName(tblName.Name, fkName), false, false, fkName);
        }
        #endregion

        #region RenameCompoundObjectPropertyListName
        public bool IsRenameCompoundObjectPropertyListName(CompoundObjectProperty prop)
        {
            var saved = savedSchema.FindPersistenceObject<CompoundObjectProperty>(prop.ExportGuid);
            if (saved == null) return false;
            return saved.Name != prop.Name;
        }
        public void DoRenameCompoundObjectPropertyListName(ObjectClass objClass, CompoundObjectProperty prop)
        {
            var saved = savedSchema.FindPersistenceObject<CompoundObjectProperty>(prop.ExportGuid);
            Log.ErrorFormat("renaming a Property from '{0}' to '{1}' is not supported yet", saved.Name, prop.Name);
        }
        #endregion

        #region MoveCompoundObjectPropertyList
        public bool IsMoveCompoundObjectPropertyList(CompoundObjectProperty prop)
        {
            var saved = savedSchema.FindPersistenceObject<CompoundObjectProperty>(prop.ExportGuid);
            if (saved == null) return false;
            return saved.ObjectClass.ExportGuid != prop.ObjectClass.ExportGuid;
        }
        public void DoMoveCompoundObjectPropertyList(ObjectClass objClass, CompoundObjectProperty prop)
        {
            var saved = savedSchema.FindPersistenceObject<CompoundObjectProperty>(prop.ExportGuid);
            Log.ErrorFormat("moving a Property from '{0}' to '{1}' is not supported yet", saved.ObjectClass.Name, prop.ObjectClass.Name);
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

            var tblName = relEnd.Type.GetTableRef(db);
            string colName = Construct.ListPositionColumnName(otherEnd);
            // always allow nulls for items missing a definite order
            db.CreateColumn(tblName, colName, System.Data.DbType.Int32, 0, 0, true);
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

            TableRef tblName;
            RelationEnd otherEnd;
            if (rel.HasStorage(RelationEndRole.A))
            {
                tblName = rel.A.Type.GetTableRef(db);
                otherEnd = rel.B;
            }
            else if (rel.HasStorage(RelationEndRole.B))
            {
                tblName = rel.B.Type.GetTableRef(db);
                otherEnd = rel.A;
            }
            else
            {
                Log.ErrorFormat("Relation '{0}' has unsupported Storage set: {1}, skipped", assocName, rel.Storage);
                return;
            }

            if (db.CheckColumnExists(tblName, Construct.ListPositionColumnName(otherEnd)))
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

            var tblName = relEnd.Type.GetTableRef(db);
            var colName = Construct.ForeignKeyColumnName(otherEnd);

            db.AlterColumn(tblName, colName, System.Data.DbType.Int32, 0, 0, otherEnd.IsNullable(), null);
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

            var tblName = relEnd.Type.GetTableRef(db);
            var colName = Construct.ForeignKeyColumnName(otherEnd);

            if (db.CheckColumnContainsNulls(tblName, colName))
            {
                Log.ErrorFormat("column '{0}.{1}' contains NULL values, cannot set NOT NULLABLE", tblName, colName);
            }
            else
            {
                db.AlterColumn(tblName, colName, System.Data.DbType.Int32, 0, 0, otherEnd.IsNullable(), null);
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

            TableRef tblName;
            bool isIndexed = false;
            RelationEnd otherEnd;
            if (rel.HasStorage(RelationEndRole.A))
            {
                tblName = rel.A.Type.GetTableRef(db);
                isIndexed = rel.NeedsPositionStorage(RelationEndRole.A);
                otherEnd = rel.B;
            }
            else if (rel.HasStorage(RelationEndRole.B))
            {
                tblName = rel.B.Type.GetTableRef(db);
                isIndexed = rel.NeedsPositionStorage(RelationEndRole.B);
                otherEnd = rel.A;
            }
            else
            {
                Log.ErrorFormat("Relation '{0}' has unsupported Storage set: {1}, skipped", assocName, rel.Storage);
                return;
            }

            if (db.CheckFKConstraintExists(tblName, assocName))
                db.DropFKConstraint(tblName, assocName);

            string colName = Construct.ForeignKeyColumnName(otherEnd);

            if (db.CheckColumnExists(tblName, colName))
                db.DropColumn(tblName, colName);

            if (isIndexed && db.CheckColumnExists(tblName, Construct.ListPositionColumnName(otherEnd)))
                db.DropColumn(tblName, Construct.ListPositionColumnName(otherEnd));
        }
        #endregion

        #region ChangeRelationType
        public bool IsChangeRelationType(Relation rel)
        {
            var saved = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);
            if (saved == null) return false;
            return saved.GetRelationType() != rel.GetRelationType();
        }

        // public void DoChangeRelationType(Relation rel) { no implementaion }

        #region ChangeRelationType_from_1_1_to_1_n
        public bool IsChangeRelationType_from_1_1_to_1_n(Relation rel)
        {
            var saved = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);
            if (saved == null) return false;
            return
                saved.GetRelationType() == RelationType.one_one &&
                rel.GetRelationType() == RelationType.one_n;
        }
        public void DoChangeRelationType_from_1_1_to_1_n(Relation rel)
        {
            var saved = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);

            string destAssocName = rel.GetAssociationName();
            Log.InfoFormat("Changing 1:1 Relation to 1:N: {0}", destAssocName);

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
                    Log.ErrorFormat("Relation '{0}' has unsupported Storage set: {1}, skipped", destAssocName, rel.Storage);
                    return;
            }

            var destTblRef = relEnd.Type.GetTableRef(db);
            var destRefTblName = otherEnd.Type.GetTableRef(db);
            bool isIndexed = rel.NeedsPositionStorage(relEnd.GetRole());

            string destColName = Construct.ForeignKeyColumnName(otherEnd);
            string destIndexName = Construct.ListPositionColumnName(otherEnd);

            string srcSchemaName = string.Empty;
            string srcTblName = string.Empty;
            string srcColName = string.Empty;

            // Difference to 1:N. 1:1 may have storage 'Replicate'
            // use best matching
            if (saved.HasStorage(RelationEndRole.A))
            {
                srcSchemaName = saved.A.Type.Module.SchemaName;
                srcTblName = saved.A.Type.TableName;
                srcColName = Construct.ForeignKeyColumnName(saved.B);
            }
            if (saved.HasStorage(RelationEndRole.B) && (string.IsNullOrEmpty(srcSchemaName) || string.IsNullOrEmpty(srcTblName) || db.GetTableName(srcSchemaName, srcTblName) != destTblRef))
            {
                srcSchemaName = saved.B.Type.Module.SchemaName;
                srcTblName = saved.B.Type.TableName;
                srcColName = Construct.ForeignKeyColumnName(saved.A);
            }

            var srcTblRef = db.GetTableName(srcSchemaName, srcTblName);
            var idxName = Construct.IndexName(srcTblName, srcColName);

            if (srcTblRef == destTblRef && srcColName == destColName)
            {
                if (db.CheckIndexExists(srcTblRef, idxName))
                    db.DropIndex(srcTblRef, idxName);
            }
            else if (srcTblRef == destTblRef && srcColName != destColName)
            {
                db.RenameColumn(srcTblRef, srcColName, destColName);
                if (db.CheckIndexExists(srcTblRef, idxName))
                    db.DropIndex(srcTblRef, idxName);
            }
            else
            {
                db.CreateColumn(destTblRef, destColName, System.Data.DbType.Int32, 0, 0, true);
                db.MigrateFKs(srcTblRef, srcColName, destTblRef, destColName);
                if (!otherEnd.IsNullable())
                {
                    if (!db.CheckColumnContainsNulls(destTblRef, destColName))
                    {
                        db.AlterColumn(destTblRef, destColName, System.Data.DbType.Int32, 0, 0, false, null);
                    }
                    else
                    {
                        Log.ErrorFormat("Unable to alter NOT NULL column, since table contains data. Leaving nullable column instead");
                    }
                }
            }

            db.CreateFKConstraint(destTblRef, destRefTblName, destColName, destAssocName, false);
            db.CreateIndex(destTblRef, Construct.IndexName(destTblRef.Name, destColName), false, false, destColName);
            if (isIndexed)
            {
                Log.InfoFormat("Creating position column '{0}.{1}'", destTblRef, destIndexName);
                db.CreateColumn(destTblRef, destIndexName, System.Data.DbType.Int32, 0, 0, true);
            }

            // Cleanup
            if (saved.HasStorage(RelationEndRole.A))
            {
                srcTblName = saved.A.Type.TableName;
                srcColName = Construct.ForeignKeyColumnName(saved.B);
                var srcAssocName = saved.GetRelationAssociationName(RelationEndRole.A);

                if (db.CheckFKConstraintExists(srcTblRef, srcAssocName))
                    db.DropFKConstraint(srcTblRef, srcAssocName);
                if (srcTblRef != destTblRef || srcColName != destColName)
                {
                    if (db.CheckColumnExists(srcTblRef, srcColName))
                        db.DropColumn(srcTblRef, srcColName);
                }
            }
            if (saved.HasStorage(RelationEndRole.B))
            {
                srcTblName = saved.B.Type.TableName;
                srcColName = Construct.ForeignKeyColumnName(saved.A);
                var srcAssocName = saved.GetRelationAssociationName(RelationEndRole.B);

                if (db.CheckFKConstraintExists(srcTblRef, srcAssocName))
                    db.DropFKConstraint(srcTblRef, srcAssocName);
                if (srcTblRef != destTblRef || srcColName != destColName)
                {
                    if (db.CheckColumnExists(srcTblRef, srcColName))
                        db.DropColumn(srcTblRef, srcColName);
                }
            }
        }
        #endregion

        #region ChangeRelationType_from_1_1_to_n_m
        public bool IsChangeRelationType_from_1_1_to_n_m(Relation rel)
        {
            var saved = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);
            if (saved == null) return false;
            return
                saved.GetRelationType() == RelationType.one_one &&
                rel.GetRelationType() == RelationType.n_m;
        }
        public void DoChangeRelationType_from_1_1_to_n_m(Relation rel)
        {
            var saved = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);
            string srcAssocName = saved.GetAssociationName();

            RelationEnd relEnd, otherEnd;

            switch (saved.Storage)
            {
                case StorageType.Replicate:
                case StorageType.MergeIntoA:
                    relEnd = saved.A;
                    otherEnd = saved.B;
                    break;
                case StorageType.MergeIntoB:
                    otherEnd = saved.A;
                    relEnd = saved.B;
                    break;
                default:
                    Log.ErrorFormat("Relation '{0}' has unsupported Storage set: {1}, skipped", srcAssocName, rel.Storage);
                    return;
            }

            var aType = rel.A.Type;
            var bType = rel.B.Type;
            var savedAType = saved.A.Type;
            var savedBType = saved.B.Type;

            var srcTblName = relEnd.Type.GetTableRef(db);
            var srcColName = Construct.ForeignKeyColumnName(otherEnd);

            var destTbl = db.GetTableName(rel.Module.SchemaName, rel.GetRelationTableName());
            var destCol = rel.GetRelationFkColumnName(relEnd.GetRole());
            var destFKCol = rel.GetRelationFkColumnName(otherEnd.GetRole());

            // Drop relations first as 1:1 and n:m relations share the same names
            var srcAssocA = saved.GetRelationAssociationName(RelationEndRole.A);
            if (db.CheckFKConstraintExists(aType.GetTableRef(db), srcAssocA))
                db.DropFKConstraint(aType.GetTableRef(db), srcAssocA);
            var srcAssocB = saved.GetRelationAssociationName(RelationEndRole.B);
            if (db.CheckFKConstraintExists(bType.GetTableRef(db), srcAssocB))
                db.DropFKConstraint(bType.GetTableRef(db), srcAssocB);

            DoNew_N_M_Relation(rel);
            db.InsertFKs(srcTblName, srcColName, destTbl, destCol, destFKCol);

            // Drop columns
            if (saved.Storage == StorageType.MergeIntoA || saved.Storage == StorageType.Replicate)
            {
                if (db.CheckColumnExists(savedAType.GetTableRef(db), Construct.ForeignKeyColumnName(saved.B)))
                    db.DropColumn(savedAType.GetTableRef(db), Construct.ForeignKeyColumnName(saved.B));
            }
            if (saved.Storage == StorageType.MergeIntoB || saved.Storage == StorageType.Replicate)
            {
                if (db.CheckColumnExists(savedBType.GetTableRef(db), Construct.ForeignKeyColumnName(saved.A)))
                    db.DropColumn(savedBType.GetTableRef(db), Construct.ForeignKeyColumnName(saved.A));
            }
        }
        #endregion

        #region ChangeRelationType_from_1_n_to_1_1
        public bool IsChangeRelationType_from_1_n_to_1_1(Relation rel)
        {
            var saved = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);
            if (saved == null) return false;
            return
                saved.GetRelationType() == RelationType.one_n &&
                rel.GetRelationType() == RelationType.one_one;
        }
        public void DoChangeRelationType_from_1_n_to_1_1(Relation rel)
        {
            var saved = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);
            string srcAssocName = saved.GetAssociationName();

            RelationEnd relEnd, otherEnd;

            switch (saved.Storage)
            {
                case StorageType.MergeIntoA:
                    relEnd = saved.A;
                    otherEnd = saved.B;
                    break;
                case StorageType.MergeIntoB:
                    otherEnd = saved.A;
                    relEnd = saved.B;
                    break;
                default:
                    Log.ErrorFormat("Relation '{0}' has unsupported Storage set: {1}, skipped", srcAssocName, rel.Storage);
                    return;
            }

            var srcTblName = relEnd.Type.GetTableRef(db);
            string srcColName = Construct.ForeignKeyColumnName(otherEnd);
            bool srcIsIndexed = rel.NeedsPositionStorage(relEnd.GetRole());
            string srcIndexName = Construct.ListPositionColumnName(otherEnd);

            if (!db.CheckFKColumnContainsUniqueValues(srcTblName, srcColName))
            {
                Log.ErrorFormat("Unable to change Relation '{0}' from 1:n to 1:1. Data is not unique", srcAssocName);
                return;
            }

            if (db.CheckFKConstraintExists(srcTblName, srcAssocName))
                db.DropFKConstraint(srcTblName, srcAssocName);
            if (srcIsIndexed && db.CheckColumnExists(srcTblName, srcIndexName))
                db.DropColumn(srcTblName, srcIndexName);
            if (db.CheckIndexExists(srcTblName, Construct.IndexName(srcTblName.Name, srcColName)))
                db.DropIndex(srcTblName, Construct.IndexName(srcTblName.Name, srcColName));

            bool aCreated = false;
            bool bCreated = false;

            var a = rel.A;
            var b = rel.B;
            var aType = a.Type;
            var bType = b.Type;

            // Difference to 1:N. 1:1 may have storage 'Replicate'
            // First try to migrate columns
            // And only migrate because the source data might be used twice
            if (rel.HasStorage(RelationEndRole.A))
            {
                var destTblName = aType.GetTableRef(db);
                var destColName = Construct.ForeignKeyColumnName(b);
                if (destTblName != srcTblName)
                {
                    New_1_1_Relation_CreateColumns(rel, a, b, RelationEndRole.A);
                    db.MigrateFKs(srcTblName, srcColName, destTblName, destColName);
                    aCreated = true;
                }
            }
            if (rel.HasStorage(RelationEndRole.B))
            {
                var destTblName = bType.GetTableRef(db);
                var destColName = Construct.ForeignKeyColumnName(a);
                if (destTblName != srcTblName)
                {
                    New_1_1_Relation_CreateColumns(rel, b, a, RelationEndRole.B);
                    db.MigrateFKs(srcTblName, srcColName, destTblName, destColName);
                    bCreated = true;
                }
            }
            bool srcColWasReused = false;
            // Then try to rename columns
            if (rel.HasStorage(RelationEndRole.A) && !aCreated)
            {
                var destTblName = aType.GetTableRef(db);
                var destColName = Construct.ForeignKeyColumnName(b);
                if (destTblName == srcTblName && destColName != srcColName)
                {
                    db.RenameColumn(destTblName, srcColName, destColName);
                }
                var assocName = rel.GetRelationAssociationName(RelationEndRole.A);
                var refTblName = bType.GetTableRef(db);
                db.CreateFKConstraint(destTblName, refTblName, destColName, assocName, false);
                db.CreateIndex(destTblName, Construct.IndexName(destTblName.Name, destColName), true, false, destColName);
                srcColWasReused = true;
            }
            if (rel.HasStorage(RelationEndRole.B) && !bCreated)
            {
                var destTblName = bType.GetTableRef(db);
                var destColName = Construct.ForeignKeyColumnName(a);
                if (destTblName == srcTblName && destColName != srcColName)
                {
                    db.RenameColumn(destTblName, srcColName, destColName);
                }
                var assocName = rel.GetRelationAssociationName(RelationEndRole.B);
                var refTblName = aType.GetTableRef(db);
                db.CreateFKConstraint(destTblName, refTblName, destColName, assocName, false);
                db.CreateIndex(destTblName, Construct.IndexName(destTblName.Name, destColName), true, false, destColName);
                srcColWasReused = true;
            }

            if (!srcColWasReused && db.CheckColumnExists(srcTblName, srcColName))
                db.DropColumn(srcTblName, srcColName);
        }
        #endregion

        #region ChangeRelationType_from_1_n_to_n_m
        public bool IsChangeRelationType_from_1_n_to_n_m(Relation rel)
        {
            var saved = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);
            if (saved == null) return false;
            return
                saved.GetRelationType() == RelationType.one_n &&
                rel.GetRelationType() == RelationType.n_m;
        }
        public void DoChangeRelationType_from_1_n_to_n_m(Relation rel)
        {
            string srcAssocName = rel.GetAssociationName();
            var saved = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);

            RelationEnd relEnd, otherEnd;

            switch (saved.Storage)
            {
                case StorageType.MergeIntoA:
                    relEnd = saved.A;
                    otherEnd = saved.B;
                    break;
                case StorageType.MergeIntoB:
                    otherEnd = saved.A;
                    relEnd = saved.B;
                    break;
                default:
                    Log.ErrorFormat("Relation '{0}' has unsupported Storage set: {1}, skipped", srcAssocName, rel.Storage);
                    return;
            }

            var srcTblName = relEnd.Type.GetTableRef(db);
            var srcColName = Construct.ForeignKeyColumnName(otherEnd);

            var destTbl = db.GetTableName(rel.Module.SchemaName, rel.GetRelationTableName());
            var destCol = rel.GetRelationFkColumnName(relEnd.GetRole());
            var destFKCol = rel.GetRelationFkColumnName(otherEnd.GetRole());

            DoNew_N_M_Relation(rel);
            db.InsertFKs(srcTblName, srcColName, destTbl, destCol, destFKCol);
            DoDelete_1_N_Relation(saved);
        }
        #endregion

        #region ChangeRelationType_from_n_m_to_1_1
        public bool IsChangeRelationType_from_n_m_to_1_1(Relation rel)
        {
            var saved = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);
            if (saved == null) return false;
            return
                saved.GetRelationType() == RelationType.n_m &&
                rel.GetRelationType() == RelationType.one_one;
        }
        public void DoChangeRelationType_from_n_m_to_1_1(Relation rel)
        {
            string destAssocName = rel.GetAssociationName();
            var saved = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);

            var srcTblName = db.GetTableName(saved.Module.SchemaName, saved.GetRelationTableName());

            // Drop relations first as 1:1 and n:m relations share the same names
            var srcAssocA = saved.GetRelationAssociationName(RelationEndRole.A);
            if (db.CheckFKConstraintExists(srcTblName, srcAssocA))
                db.DropFKConstraint(srcTblName, srcAssocA);
            var srcAssocB = saved.GetRelationAssociationName(RelationEndRole.B);
            if (db.CheckFKConstraintExists(srcTblName, srcAssocB))
                db.DropFKConstraint(srcTblName, srcAssocB);

            DoNew_1_1_Relation(rel);

            if (rel.HasStorage(RelationEndRole.A))
            {
                var destTblName = rel.A.Type.GetTableRef(db);
                var destColName = Construct.ForeignKeyColumnName(rel.B);
                var srcColName = rel.GetRelationFkColumnName(RelationEndRole.B);
                var srcFKColName = rel.GetRelationFkColumnName(RelationEndRole.A);

                if (!db.CheckFKColumnContainsUniqueValues(srcTblName, srcColName))
                {
                    Log.ErrorFormat("Unable to change Relation '{0}' from n:m to 1:1. Data is not unique", destAssocName);
                    return;
                }
                db.CopyFKs(srcTblName, srcColName, destTblName, destColName, srcFKColName);
            }
            if (rel.HasStorage(RelationEndRole.B))
            {
                var destTblName = rel.B.Type.GetTableRef(db);
                var destColName = Construct.ForeignKeyColumnName(rel.A);
                var srcColName = rel.GetRelationFkColumnName(RelationEndRole.A);
                var srcFKColName = rel.GetRelationFkColumnName(RelationEndRole.B);

                if (!db.CheckFKColumnContainsUniqueValues(srcTblName, srcColName))
                {
                    Log.ErrorFormat("Unable to change Relation '{0}' from n:m to 1:1. Data is not unique", destAssocName);
                    return;
                }
                db.CopyFKs(srcTblName, srcColName, destTblName, destColName, srcFKColName);
            }

            if (db.CheckTableExists(srcTblName))
                db.DropTable(srcTblName);
        }
        #endregion

        #region ChangeRelationType_from_n_m_to_1_n
        public bool IsChangeRelationType_from_n_m_to_1_n(Relation rel)
        {
            var saved = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);
            if (saved == null) return false;
            return
                saved.GetRelationType() == RelationType.n_m &&
                rel.GetRelationType() == RelationType.one_n;
        }
        public void DoChangeRelationType_from_n_m_to_1_n(Relation rel)
        {
            string destAssocName = rel.GetAssociationName();

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
                    Log.ErrorFormat("Relation '{0}' has unsupported Storage set: {1}, skipped", destAssocName, rel.Storage);
                    return;
            }

            var saved = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);

            var srcTbl = db.GetTableName(saved.Module.SchemaName, saved.GetRelationTableName());
            var srcCol = saved.GetRelationFkColumnName(otherEnd.GetRole());
            var srcFKCol = saved.GetRelationFkColumnName(relEnd.GetRole());

            if (!db.CheckFKColumnContainsUniqueValues(srcTbl, srcCol))
            {
                Log.ErrorFormat("Unable to change Relation '{0}' from n:m to 1:n. Data is not unique", destAssocName);
                return;
            }

            var destTblName = relEnd.Type.GetTableRef(db);
            var destColName = Construct.ForeignKeyColumnName(otherEnd);

            DoNew_1_N_Relation(rel);
            db.CopyFKs(srcTbl, srcCol, destTblName, destColName, srcFKCol);
            DoDelete_N_M_Relation(saved);
        }
        #endregion

        #endregion

        #region ChangeRelationEndTypes
        public bool IsChangeRelationEndTypes(Relation rel)
        {
            var saved = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);
            if (saved == null) return false;
            return saved.A.Type.ExportGuid != rel.A.Type.ExportGuid || saved.B.Type.ExportGuid != rel.B.Type.ExportGuid;
        }

        public void DoChangeRelationEndTypes(Relation rel)
        {
            var saved = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);

            if (rel.GetRelationType() == RelationType.n_m)
            {
                var oldTblName = db.GetTableName(saved.Module.SchemaName, saved.GetRelationTableName());
                if (db.CheckTableContainsData(oldTblName))
                {
                    if (saved.A.Type.AndParents(cls => cls.BaseObjectClass).Select(cls => cls.ExportGuid).Contains(rel.A.Type.ExportGuid)
                        && saved.B.Type.AndParents(cls => cls.BaseObjectClass).Select(cls => cls.ExportGuid).Contains(rel.B.Type.ExportGuid))
                    {
                        string assocName = rel.GetAssociationName();
                        Log.InfoFormat("Rewiring N:M Relation: {0}", assocName);

                        if (db.CheckFKConstraintExists(oldTblName, saved.GetRelationAssociationName(RelationEndRole.A)))
                            db.DropFKConstraint(oldTblName, saved.GetRelationAssociationName(RelationEndRole.A));
                        if (db.CheckFKConstraintExists(oldTblName, saved.GetRelationAssociationName(RelationEndRole.B)))
                            db.DropFKConstraint(oldTblName, saved.GetRelationAssociationName(RelationEndRole.B));

                        // renaming is handled by DoChangeRelationName
                        //db.RenameTable(oldTblName, newTblName);

                        var fkAName = saved.GetRelationFkColumnName(RelationEndRole.A);
                        var fkBName = saved.GetRelationFkColumnName(RelationEndRole.B);
                        db.CreateFKConstraint(oldTblName, rel.A.Type.GetTableRef(db), fkAName, saved.GetRelationAssociationName(RelationEndRole.A), false);
                        db.CreateFKConstraint(oldTblName, rel.B.Type.GetTableRef(db), fkBName, saved.GetRelationAssociationName(RelationEndRole.B), false);
                    }
                    else
                    {
                        Log.WarnFormat("Unable to drop old relation. Relation has some instances. Table: " + oldTblName);
                    }
                }
                else
                {
                    DoDelete_N_M_Relation(saved);
                    DoNew_N_M_Relation(rel);
                }
            }
            else if (rel.GetRelationType() == RelationType.one_n)
            {
                RelationEnd relEnd, otherEnd;

                switch (rel.Storage)
                {
                    case StorageType.MergeIntoA:
                        relEnd = saved.A;
                        otherEnd = saved.B;
                        break;
                    case StorageType.MergeIntoB:
                        otherEnd = saved.A;
                        relEnd = saved.B;
                        break;
                    default:
                        Log.ErrorFormat("Relation '{0}' has unsupported Storage set: {1}, skipped", rel.GetAssociationName(), rel.Storage);
                        return;
                }

                var tblName = relEnd.Type.GetTableRef(db);
                var colName = Construct.ForeignKeyColumnName(otherEnd);

                if (db.CheckColumnContainsValues(tblName, colName))
                {
                    Log.WarnFormat("Unable to drop old relation. Relation has some instances. Table: " + tblName);
                }
                else
                {
                    DoDelete_1_N_Relation(saved);
                    DoNew_1_N_Relation(rel);
                }
            }
            else if (rel.GetRelationType() == RelationType.one_one)
            {
                RelationEnd relEnd, otherEnd;

                switch (rel.Storage)
                {
                    case StorageType.MergeIntoA:
                    case StorageType.Replicate:
                        relEnd = saved.A;
                        otherEnd = saved.B;
                        break;
                    case StorageType.MergeIntoB:
                        otherEnd = saved.A;
                        relEnd = saved.B;
                        break;
                    default:
                        Log.ErrorFormat("Relation '{0}' has unsupported Storage set: {1}, skipped", rel.GetAssociationName(), rel.Storage);
                        return;
                }

                var tblName = relEnd.Type.GetTableRef(db);
                var colName = Construct.ForeignKeyColumnName(otherEnd);

                if (db.CheckColumnContainsValues(tblName, colName))
                {
                    Log.WarnFormat("Unable to drop old relation. Relation has some instances. Table: " + tblName);
                }
                else
                {
                    DoDelete_1_1_Relation(saved);
                    DoNew_1_1_Relation(rel);
                }
            }

        }
        #endregion

        #region ChangeRelationName
        public bool IsChangeRelationName(Relation rel)
        {
            var saved = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);
            if (saved == null) return false;
            // GetAssociationName and GetRelationAssociationName contains both ARoleName, Verb and BRoleName
            return saved.GetAssociationName() != rel.GetAssociationName();
        }
        public void DoChangeRelationName(Relation rel)
        {
            var saved = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);

            var fkAName = saved.GetRelationFkColumnName(RelationEndRole.A);
            var fkBName = saved.GetRelationFkColumnName(RelationEndRole.B);

            var aType = rel.A.Type;
            var bType = rel.B.Type;

            if (rel.GetRelationType() == RelationType.n_m)
            {
                var srcRelTbl = db.GetTableName(saved.Module.SchemaName, saved.GetRelationTableName());
                var destRelTbl = db.GetTableName(rel.Module.SchemaName, rel.GetRelationTableName());

                db.RenameFKConstraint(srcRelTbl, saved.GetRelationAssociationName(RelationEndRole.A),
                    aType.GetTableRef(db), fkAName, rel.GetRelationAssociationName(RelationEndRole.A), false);
                db.RenameFKConstraint(srcRelTbl, saved.GetRelationAssociationName(RelationEndRole.B),
                    bType.GetTableRef(db), fkBName, rel.GetRelationAssociationName(RelationEndRole.B), false);

                db.RenameTable(srcRelTbl, destRelTbl);

                db.RenameColumn(destRelTbl, saved.GetRelationFkColumnName(RelationEndRole.A), rel.GetRelationFkColumnName(RelationEndRole.A));
                db.RenameColumn(destRelTbl, saved.GetRelationFkColumnName(RelationEndRole.B), rel.GetRelationFkColumnName(RelationEndRole.B));
            }
            else if (rel.GetRelationType() == RelationType.one_n)
            {
                if (saved.HasStorage(RelationEndRole.A) &&
                    Construct.ForeignKeyColumnName(saved.B) != Construct.ForeignKeyColumnName(rel.B))
                {
                    var tbl = aType.GetTableRef(db);
                    db.RenameFKConstraint(tbl, saved.GetAssociationName(),
                        aType.GetTableRef(db), fkAName, rel.GetAssociationName(), false);
                    db.RenameColumn(tbl, Construct.ForeignKeyColumnName(saved.B), Construct.ForeignKeyColumnName(rel.B));
                }
                else if (saved.HasStorage(RelationEndRole.B) &&
                    Construct.ForeignKeyColumnName(saved.A) != Construct.ForeignKeyColumnName(rel.A))
                {
                    var tbl = bType.GetTableRef(db);
                    db.RenameFKConstraint(tbl, saved.GetAssociationName(),
                        bType.GetTableRef(db), fkBName, rel.GetAssociationName(), false);
                    db.RenameColumn(tbl, Construct.ForeignKeyColumnName(saved.A), Construct.ForeignKeyColumnName(rel.A));
                }
            }
            else if (rel.GetRelationType() == RelationType.one_one)
            {
                if (saved.HasStorage(RelationEndRole.A))
                {
                    var tbl = aType.GetTableRef(db);
                    db.RenameFKConstraint(tbl, saved.GetRelationAssociationName(RelationEndRole.A),
                        aType.GetTableRef(db), fkAName, rel.GetRelationAssociationName(RelationEndRole.A), false);
                    if (Construct.ForeignKeyColumnName(saved.B) != Construct.ForeignKeyColumnName(rel.B))
                    {
                        db.RenameColumn(tbl, Construct.ForeignKeyColumnName(saved.B), Construct.ForeignKeyColumnName(rel.B));
                    }
                }
                if (saved.HasStorage(RelationEndRole.B))
                {
                    var tbl = bType.GetTableRef(db);
                    db.RenameFKConstraint(tbl, saved.GetRelationAssociationName(RelationEndRole.B),
                        bType.GetTableRef(db), fkBName, rel.GetRelationAssociationName(RelationEndRole.B), false);
                    if (Construct.ForeignKeyColumnName(saved.A) != Construct.ForeignKeyColumnName(rel.A))
                    {
                        db.RenameColumn(tbl, Construct.ForeignKeyColumnName(saved.A), Construct.ForeignKeyColumnName(rel.A));
                    }
                }
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
            string assocName, colName, listPosName;
            RelationEnd relEnd, otherEnd;
            TableRef tblName, refTblName;
            bool hasPersistentOrder;
            if (!TryInspect_1_N_Relation(rel, out assocName, out relEnd, out otherEnd, out tblName, out refTblName, out colName, out hasPersistentOrder, out listPosName))
            {
                return;
            }

            Log.InfoFormat("New 1:N Relation: {0}", assocName);

            CreateFKColumn(otherEnd, tblName, colName);
            db.CreateFKConstraint(tblName, refTblName, colName, assocName, false);
            db.CreateIndex(tblName, Construct.IndexName(tblName.Name, colName), false, false, colName);

            if (hasPersistentOrder)
            {
                Log.InfoFormat("Creating position column '{0}.{1}'", tblName, listPosName);
                db.CreateColumn(tblName, listPosName, System.Data.DbType.Int32, 0, 0, true);
            }
        }

        /// <summary>
        /// Tries to dissect a relation for the 1_N case.
        /// </summary>
        /// <returns>false if something is wrong with the relation definition. The out parameters are only filled correctly when the function returns true.</returns>
        private bool TryInspect_1_N_Relation(
            Relation rel, out string assocName,
            out RelationEnd relEnd, out RelationEnd otherEnd,
            out TableRef tblName, out TableRef refTblName,
            out string colName,
            out bool hasPersistentOrder, out string listPosName)
        {
            assocName = rel.GetAssociationName();

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
                    Log.ErrorFormat("Skipping Relation '{0}': unsupported Storage set: {1}", assocName, rel.Storage);
                    otherEnd = null;
                    relEnd = null;
                    tblName = null;
                    refTblName = null;
                    hasPersistentOrder = default(bool);
                    colName = null;
                    listPosName = null;
                    return false;
            }

            tblName = relEnd.Type.GetTableRef(db);
            refTblName = otherEnd.Type.GetTableRef(db);
            hasPersistentOrder = rel.NeedsPositionStorage(relEnd.GetRole());

            colName = Construct.ForeignKeyColumnName(otherEnd);
            listPosName = Construct.ListPositionColumnName(otherEnd);

            return true;
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

            var tblName = db.GetTableName(rel.Module.SchemaName, rel.GetRelationTableName());
            var fkName = rel.GetRelationFkColumnName(role);

            db.CreateColumn(tblName, fkName + Zetbox.API.Helper.PositionSuffix, System.Data.DbType.Int32, 0, 0, true);
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

            var tblName = db.GetTableName(rel.Module.SchemaName, rel.GetRelationTableName());
            var fkName = rel.GetRelationFkColumnName(role);

            if (db.CheckColumnExists(tblName, fkName + Zetbox.API.Helper.PositionSuffix))
                db.DropColumn(tblName, fkName + Zetbox.API.Helper.PositionSuffix);
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

            var tblName = db.GetTableName(rel.Module.SchemaName, rel.GetRelationTableName());

            if (db.CheckFKConstraintExists(tblName, rel.GetRelationAssociationName(RelationEndRole.A)))
                db.DropFKConstraint(tblName, rel.GetRelationAssociationName(RelationEndRole.A));
            if (db.CheckFKConstraintExists(tblName, rel.GetRelationAssociationName(RelationEndRole.B)))
                db.DropFKConstraint(tblName, rel.GetRelationAssociationName(RelationEndRole.B));

            if (db.CheckTableExists(tblName))
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
            string assocName, fkAName, fkBName;
            TableRef tblName;
            ObjectClass aType, bType;
            if (!TryInspect_N_M_Relation(rel, out assocName, out tblName, out fkAName, out fkBName, out aType, out bType))
            {
                return;
            }

            Log.InfoFormat("New N:M Relation: {0}", assocName);
            if (db.CheckTableExists(tblName))
            {
                Log.ErrorFormat("Relation table {0} already exists", tblName);
                return;
            }

            db.CreateTable(tblName, true);

            db.CreateColumn(tblName, fkAName, System.Data.DbType.Int32, 0, 0, false);
            if (rel.NeedsPositionStorage(RelationEndRole.A))
            {
                db.CreateColumn(tblName, fkAName + Zetbox.API.Helper.PositionSuffix, System.Data.DbType.Int32, 0, 0, true);
            }

            db.CreateColumn(tblName, fkBName, System.Data.DbType.Int32, 0, 0, false);
            if (rel.NeedsPositionStorage(RelationEndRole.B))
            {
                db.CreateColumn(tblName, fkBName + Zetbox.API.Helper.PositionSuffix, System.Data.DbType.Int32, 0, 0, true);
            }

            if (aType.ImplementsIExportable() && bType.ImplementsIExportable())
            {
                db.CreateColumn(tblName, "ExportGuid", System.Data.DbType.Guid, 0, 0, false, new NewGuidDefaultConstraint());
            }

            db.CreateFKConstraint(tblName, aType.GetTableRef(db), fkAName, rel.GetRelationAssociationName(RelationEndRole.A), false);
            db.CreateIndex(tblName, Construct.IndexName(tblName.Name, fkAName), false, false, fkAName);
            db.CreateFKConstraint(tblName, bType.GetTableRef(db), fkBName, rel.GetRelationAssociationName(RelationEndRole.B), false);
            db.CreateIndex(tblName, Construct.IndexName(tblName.Name, fkBName), false, false, fkBName);
        }

        private bool TryInspect_N_M_Relation(Relation rel, out string assocName, out TableRef tblName, out string fkAName, out string fkBName, out ObjectClass aType, out ObjectClass bType)
        {
            assocName = rel.GetAssociationName();

            tblName = db.GetTableName(rel.Module.SchemaName, rel.GetRelationTableName());
            fkAName = rel.GetRelationFkColumnName(RelationEndRole.A);
            fkBName = rel.GetRelationFkColumnName(RelationEndRole.B);
            aType = rel.A.Type;
            bType = rel.B.Type;

            return true;
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

        private void Delete_1_1_Relation_DropColumns(Relation rel, RelationEnd relEnd, RelationEnd otherEnd, RelationEndRole role)
        {
            var tblName = relEnd.Type.GetTableRef(db);
            var colName = Construct.ForeignKeyColumnName(otherEnd);
            var assocName = rel.GetRelationAssociationName(role);

            if (db.CheckFKConstraintExists(tblName, assocName))
                db.DropFKConstraint(tblName, assocName);
            if (db.CheckColumnExists(tblName, colName))
                db.DropColumn(tblName, colName);

            if (rel.NeedsPositionStorage(role) && db.CheckColumnExists(tblName, Construct.ListPositionColumnName(otherEnd)))
                db.DropColumn(tblName, Construct.ListPositionColumnName(otherEnd));
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
            TableRef tblName, refTblName;
            string assocName, colName, idxName;
            if (!TryInspect_1_1_Relation(rel, relEnd, otherEnd, role, out tblName, out refTblName, out assocName, out colName, out idxName))
            {
                return;
            }

            CreateFKColumn(otherEnd, tblName, colName);
            db.CreateFKConstraint(tblName, refTblName, colName, assocName, false);
            if (db.CheckIndexPossible(tblName, idxName, true, false, colName))
                db.CreateIndex(tblName, idxName, true, false, colName);
            else
                Log.WarnFormat("Cannot create index: {0}", idxName);

            if (rel.NeedsPositionStorage(role))
            {
                Log.ErrorFormat("1:1 Relation should never need position storage, but this one does!");
            }
        }

        private bool TryInspect_1_1_Relation(Relation rel, RelationEnd relEnd, RelationEnd otherEnd, RelationEndRole role, out TableRef tblName, out TableRef refTblName, out string assocName, out string colName, out string idxName)
        {
            tblName = relEnd.Type.GetTableRef(db);
            refTblName = otherEnd.Type.GetTableRef(db);
            assocName = rel.GetRelationAssociationName(role);
            colName = Construct.ForeignKeyColumnName(otherEnd);
            idxName = Construct.IndexName(tblName.Name, colName);

            return true;
        }

        private void CreateFKColumn(RelationEnd otherEnd, TableRef tblName, string colName)
        {
            if (otherEnd.IsNullable() || !db.CheckTableContainsData(tblName))
            {
                db.CreateColumn(tblName, colName, System.Data.DbType.Int32, 0, 0, otherEnd.IsNullable());
            }
            else
            {
                db.CreateColumn(tblName, colName, System.Data.DbType.Int32, 0, 0, true);
                Log.ErrorFormat("Unable to create NOT NULL column, since table contains data. Created nullable column instead");
            }
        }
        #endregion

        #region Change_1_1_Storage
        public bool IsChange_1_1_Storage(Relation rel)
        {
            var saved = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);
            if (saved == null) return false;
            return rel.Storage != saved.Storage;
        }
        public void DoChange_1_1_Storage(Relation rel)
        {
            Log.InfoFormat("Changing 1:1 Relation Storage: {0}", rel.GetAssociationName());
            var saved = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);

            if (saved.Storage == StorageType.Replicate)
            {
                // To MergeIntoA or MergeIntoB
                if (rel.HasStorage(RelationEndRole.B))
                {
                    Delete_1_1_Relation_DropColumns(saved, saved.A, saved.B, RelationEndRole.A);
                }
                else if (rel.HasStorage(RelationEndRole.A))
                {
                    Delete_1_1_Relation_DropColumns(saved, saved.B, saved.A, RelationEndRole.B);
                }
            }
            else
            {
                RelationEnd relEnd;
                RelationEnd otherEnd;
                RelationEndRole role;
                RelationEndRole otherRole;
                if (saved.Storage == StorageType.MergeIntoA)
                {
                    // To MergeIntoB or Replicate
                    relEnd = rel.B;
                    otherEnd = rel.A;
                    role = RelationEndRole.B;
                    otherRole = RelationEndRole.A;

                }
                else if (saved.Storage == StorageType.MergeIntoB)
                {
                    // To MergeIntoA or Replicate
                    relEnd = rel.A;
                    otherEnd = rel.B;
                    role = RelationEndRole.A;
                    otherRole = RelationEndRole.B;
                }
                else
                {
                    throw new InvalidOperationException("Unexpected saved stroage type " + saved.Storage.ToString());
                }

                New_1_1_Relation_CreateColumns(rel, relEnd, otherEnd, role);
                var srcTbl = otherEnd.Type.GetTableRef(db);
                var srcCol = Construct.ForeignKeyColumnName(relEnd);
                var destTbl = relEnd.Type.GetTableRef(db);
                var destCol = Construct.ForeignKeyColumnName(otherEnd);
                db.MigrateFKs(srcTbl, srcCol, destTbl, destCol);
                if (!relEnd.IsNullable())
                {
                    if (!db.CheckColumnContainsNulls(destTbl, destCol))
                    {
                        db.AlterColumn(destTbl, destCol, System.Data.DbType.Int32, 0, 0, false, null);
                    }
                    else
                    {
                        Log.ErrorFormat("Unable to alter NOT NULL column, since table contains data. Leaving nullable column instead");
                    }
                }

                if (rel.Storage != StorageType.Replicate)
                {
                    Delete_1_1_Relation_DropColumns(rel, otherEnd, relEnd, otherRole);
                }
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

            var tblName = relEnd.Type.GetTableRef(db);
            var colName = Construct.ForeignKeyColumnName(otherEnd);

            db.AlterColumn(tblName, colName, System.Data.DbType.Int32, 0, 0, otherEnd.IsNullable(), null);
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

            var tblName = relEnd.Type.GetTableRef(db);
            var colName = Construct.ForeignKeyColumnName(otherEnd);

            if (db.CheckColumnContainsNulls(tblName, colName))
            {
                Log.ErrorFormat("column '{0}.{1}' contains NULL values, cannot set NOT NULLABLE", tblName, colName);
            }
            else
            {
                db.AlterColumn(tblName, colName, System.Data.DbType.Int32, 0, 0, otherEnd.IsNullable(), null);
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
            var assocName = Construct.InheritanceAssociationName(objClass.BaseObjectClass, objClass);
            var tblName = objClass.GetTableRef(db);

            Log.InfoFormat("New ObjectClass Inheritance: {0} -> {1}: {2}", objClass.Name, objClass.BaseObjectClass.Name, assocName);

            if (db.CheckTableContainsData(tblName))
            {
                Log.ErrorFormat("Table '{0}' contains data. Unable to add inheritence.", tblName);
                return;
            }

            db.CreateFKConstraint(tblName, objClass.BaseObjectClass.GetTableRef(db), "ID", assocName, false);
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
            Log.InfoFormat("Changing ObjectClass Inheritance: {0} -> {1}", objClass.Name, objClass.BaseObjectClass.Name);
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
            var tblName = objClass.GetTableRef(db);

            Log.InfoFormat("Remove ObjectClass Inheritance: {0} -> {1}: {2}", savedObjClass.Name, savedObjClass.BaseObjectClass.Name, assocName);

            if (db.CheckFKConstraintExists(tblName, assocName))
                db.DropFKConstraint(tblName, assocName);
        }
        #endregion

        #region ChangeTptToTph
        public bool IsChangeTptToTph(ObjectClass objClass)
        {
            // only migrate derived classes
            if (objClass.BaseObjectClass == null) return false;

            ObjectClass savedObjClass = savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid);
            if (savedObjClass == null) return false;

            return savedObjClass.GetTableMapping() == TableMapping.TPT && objClass.GetTableMapping() == TableMapping.TPH;
        }

        /// <summary>
        /// Changes table layout from table-per-type to table-per-hierarchy. This operates only on the saved schema to avoid doing work of other cases, specifically the Do(New/Change)*Property cases.
        /// </summary>
        /// <param name="objClass"></param>
        public void DoChangeTptToTph(ObjectClass objClass)
        {
            ObjectClass savedObjClass = savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid);
            var colNamesList = new List<string>();

            #region create new columns in base table

            var baseTblName = objClass.GetTableRef(db);

            #region Value and Compound Properties

            foreach (ValueTypeProperty savedProp in savedObjClass.Properties.OfType<ValueTypeProperty>().Where(p => !p.IsList))
            {
                CreateValueTypePropertyNullable(baseTblName, savedProp, Construct.NestedColumnName(savedProp.Name, savedObjClass.TableName));
                colNamesList.Add(savedProp.Name);
            }
            foreach (CompoundObjectProperty savedProp in savedObjClass.Properties.OfType<CompoundObjectProperty>().Where(p => !p.IsList))
            {
                CreateCompoundObjectProperty(baseTblName, savedProp, string.Empty, false);

                var baseColName = Construct.ColumnName(savedProp, string.Empty);
                colNamesList.AddRange(savedProp.CompoundObjectDefinition.Properties.OfType<ValueTypeProperty>().Select(p => Construct.ColumnName(p, baseColName)));
            }
            foreach (ValueTypeProperty savedProp in savedObjClass.Properties.OfType<ValueTypeProperty>().Where(p => p.IsList))
            {
                // relink fk column on tblName
                var tblName = db.GetTableName(savedProp.Module.SchemaName, savedProp.GetCollectionEntryTable());
                string fkName = savedProp.GetCollectionEntryReverseKeyColumnName();
                string assocName = savedProp.GetAssociationName();

                db.DropFKConstraint(tblName, assocName);
                // TPH downside: constraint doesn't restrict to subclass any more
                db.CreateFKConstraint(tblName, baseTblName, fkName, assocName, true);
            }
            foreach (CompoundObjectProperty savedProp in savedObjClass.Properties.OfType<CompoundObjectProperty>().Where(p => p.IsList))
            {
                // relink fk column on tblName
                var tblName = db.GetTableName(savedProp.Module.SchemaName, savedProp.GetCollectionEntryTable());
                string fkName = savedProp.GetCollectionEntryReverseKeyColumnName();
                string assocName = savedProp.GetAssociationName();

                db.DropFKConstraint(tblName, assocName);
                // TPH downside: constraint doesn't restrict to subclass any more
                db.CreateFKConstraint(tblName, baseTblName, fkName, assocName, true);
            }

            #endregion

            #region Relations

            foreach (Relation savedRel in savedSchema.GetQuery<Relation>().Where(r => r.A.Type == savedObjClass || r.B.Type == savedObjClass).OrderBy(r => r.Module.Namespace))
            {
                switch (savedRel.GetRelationType())
                {
                    case RelationType.one_n:
                        // create new columns on base table
                        {
                            string assocName, colName, listPosName;
                            RelationEnd relEnd, otherEnd;
                            TableRef tblName, refTblName;
                            bool hasPersistentOrder;
                            if (TryInspect_1_N_Relation(savedRel, out assocName, out relEnd, out otherEnd, out tblName, out refTblName, out colName, out hasPersistentOrder, out listPosName))
                            {
                                // Only do this when we are N-side
                                if (relEnd.Type == savedObjClass)
                                {
                                    CreateFKColumn(otherEnd, baseTblName, Construct.NestedColumnName(colName, objClass.TableName));
                                    colNamesList.Add(colName);
                                    if (hasPersistentOrder)
                                    {
                                        db.CreateColumn(baseTblName, Construct.NestedColumnName(listPosName, objClass.TableName), System.Data.DbType.Int32, 0, 0, true);
                                        colNamesList.Add(listPosName);
                                    }
                                }
                            }
                        }
                        break;
                    case RelationType.n_m:
                        // rewire fk on RelationEntry
                        {
                            string assocName, fkAName, fkBName;
                            TableRef tblName;
                            ObjectClass aType, bType;
                            if (TryInspect_N_M_Relation(savedRel, out assocName, out tblName, out fkAName, out fkBName, out aType, out bType))
                            {
                                if (aType == savedObjClass)
                                {
                                    db.DropFKConstraint(tblName, savedRel.GetRelationAssociationName(RelationEndRole.A));
                                    db.CreateFKConstraint(tblName, baseTblName, fkAName, savedRel.GetRelationAssociationName(RelationEndRole.A), false);
                                }
                                if (bType == savedObjClass)
                                {
                                    db.DropFKConstraint(tblName, savedRel.GetRelationAssociationName(RelationEndRole.B));
                                    db.CreateFKConstraint(tblName, baseTblName, fkBName, savedRel.GetRelationAssociationName(RelationEndRole.B), false);
                                }
                            }
                        }
                        break;
                    case RelationType.one_one:
                        // create new columns on base table
                        if (savedRel.Storage == StorageType.MergeIntoA || savedRel.Storage == StorageType.Replicate)
                        {
                            TableRef tblName, refTblName;
                            string assocName, colName, idxName;
                            if (TryInspect_1_1_Relation(savedRel, savedRel.A, savedRel.B, RelationEndRole.A, out tblName, out refTblName, out assocName, out colName, out idxName))
                            {
                                db.DropFKConstraint(tblName, assocName);
                                CreateFKColumn(savedRel.B, baseTblName, Construct.NestedColumnName(colName, objClass.TableName));
                                colNamesList.Add(colName);
                            }
                        }

                        if (savedRel.Storage == StorageType.MergeIntoB || savedRel.Storage == StorageType.Replicate)
                        {
                            TableRef tblName, refTblName;
                            string assocName, colName, idxName;
                            if (TryInspect_1_1_Relation(savedRel, savedRel.B, savedRel.A, RelationEndRole.B, out tblName, out refTblName, out assocName, out colName, out idxName))
                            {
                                db.DropFKConstraint(tblName, assocName);
                                CreateFKColumn(savedRel.A, baseTblName, Construct.NestedColumnName(colName, objClass.TableName));
                                colNamesList.Add(colName);
                            }
                        }
                        break;
                    default:
                        Log.ErrorFormat("Skipping Relation '{0}': unsupported RelationType: {1}", savedRel.GetRelationType());
                        break;
                }

            }

            #endregion

            #endregion

            #region copy data from derived tables to base table

            if (colNamesList.Count > 0)
            {
                var srcTblName = db.GetTableName(objClass.Module.SchemaName, objClass.TableName);
                var srcColNames = colNamesList.ToArray();
                var dstColNames = colNamesList.Select(n => Construct.NestedColumnName(n, objClass.TableName)).ToArray();
                db.CopyColumnData(srcTblName, srcColNames, baseTblName, dstColNames);
            }

            #endregion

            #region create constraints and references

            foreach (Relation savedRel in savedSchema.GetQuery<Relation>().Where(r => r.A.Type == savedObjClass || r.B.Type == savedObjClass).OrderBy(r => r.Module.Namespace))
            {
                switch (savedRel.GetRelationType())
                {
                    case RelationType.one_n:
                        {
                            string assocName, colName, listPosName;
                            RelationEnd relEnd, otherEnd;
                            TableRef tblName, refTblName;
                            bool hasPersistentOrder;
                            if (TryInspect_1_N_Relation(savedRel, out assocName, out relEnd, out otherEnd, out tblName, out refTblName, out colName, out hasPersistentOrder, out listPosName))
                            {
                                colName = Construct.NestedColumnName(colName, objClass.TableName);
                                db.CreateFKConstraint(baseTblName, refTblName, colName, assocName, false);
                                db.CreateIndex(baseTblName, Construct.IndexName(baseTblName.Name, colName), false, false, colName);
                            }
                        }
                        break;
                    case RelationType.n_m:
                        // nothing to do
                        break;
                    case RelationType.one_one:
                        if (savedRel.Storage == StorageType.MergeIntoA || savedRel.Storage == StorageType.Replicate)
                        {
                            TableRef tblName, refTblName;
                            string assocName, colName, idxName;
                            if (!TryInspect_1_1_Relation(savedRel, savedRel.A, savedRel.B, RelationEndRole.A, out tblName, out refTblName, out assocName, out colName, out idxName))
                            {
                                colName = Construct.NestedColumnName(colName, objClass.TableName);
                                db.CreateFKConstraint(tblName, baseTblName, colName, assocName, false);
                                idxName = Construct.IndexName(baseTblName.Name, colName);
                                if (db.CheckIndexPossible(baseTblName, idxName, true, false, colName))
                                    db.CreateIndex(baseTblName, idxName, true, false, colName);
                                else
                                    Log.WarnFormat("Cannot create index: {0}", idxName);
                            }
                        }

                        if (savedRel.Storage == StorageType.MergeIntoB || savedRel.Storage == StorageType.Replicate)
                        {
                            TableRef tblName, refTblName;
                            string assocName, colName, idxName;
                            if (!TryInspect_1_1_Relation(savedRel, savedRel.B, savedRel.A, RelationEndRole.B, out tblName, out refTblName, out assocName, out colName, out idxName))
                            {
                                colName = Construct.NestedColumnName(colName, objClass.TableName);
                                db.CreateFKConstraint(tblName, baseTblName, colName, assocName, false);
                                idxName = Construct.IndexName(baseTblName.Name, colName);
                                if (db.CheckIndexPossible(baseTblName, idxName, true, false, colName))
                                    db.CreateIndex(baseTblName, idxName, true, false, colName);
                                else
                                    Log.WarnFormat("Cannot create index: {0}", idxName);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            #endregion

            #region drop old tables for derived classes

            if (objClass.BaseObjectClass != null)
            {
                var oldTblName = savedObjClass.GetTableRef(db);
                var unknownColumns = db.GetTableColumnNames(oldTblName).Except(colNamesList).ToList();
                unknownColumns.Remove("ID");

                if (unknownColumns.Count > 0)
                {
                    Log.ErrorFormat("Keeping old table {0} while migrating to TPH: Unknown Columns detected: {1}", oldTblName, string.Join(", ", unknownColumns));
                }
                else
                {
                    // Break children's references, they'll go away soon, if they haven't already
                    foreach (var savedSubClass in savedObjClass.SubClasses)
                    {
                        string assocName = Construct.InheritanceAssociationName(savedObjClass, savedSubClass);
                        var tblName = savedSubClass.GetTableRef(db);
                        if (db.CheckFKConstraintExists(tblName, assocName))
                        {
                            db.DropFKConstraint(tblName, assocName);
                        }
                    }
                    db.DropTable(oldTblName);
                }
            }

            #endregion
        }
        #endregion

        #region ChangeTphToTpt
        public bool IsChangeTphToTpt(ObjectClass objClass)
        {
            // only migrate on the RootClass
            if (objClass.BaseObjectClass != null) return false;

            ObjectClass savedObjClass = savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid);
            if (savedObjClass == null) return false;

            return savedObjClass.GetTableMapping() == TableMapping.TPH && objClass.GetTableMapping() == TableMapping.TPT;
        }
        /// <summary>
        /// Changes table layout from table-per-hierarchy to table-per-type. This operates only on the saved schema to avoid doing work of other cases, specifically the Do(New/Change)*Property cases.
        /// </summary>
        /// <param name="objClass"></param>
        public void DoChangeTphToTpt(ObjectClass objClass)
        {
            // create new derived tables
            // copy data from base table to derived tables
            // drop old columns for derived classes in base table
        }
        #endregion

        #region NewObjectClassACL
        public bool IsNewObjectClassACL(ObjectClass objClass)
        {
            if (!objClass.NeedsRightsTable()) return false;
            ObjectClass savedObjClass = savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid);
            return savedObjClass == null || !savedObjClass.NeedsRightsTable();
        }
        public void DoNewObjectClassACL(ObjectClass objClass)
        {
            Log.InfoFormat("New ObjectClass Security Rules: {0}", objClass.Name);
            var tblRightsName = db.GetTableName(objClass.Module.SchemaName, Construct.SecurityRulesTableName(objClass));

            db.CreateTable(tblRightsName, false, false);
            db.CreateColumn(tblRightsName, "Identity", System.Data.DbType.Int32, 0, 0, false);
            db.CreateColumn(tblRightsName, "Right", System.Data.DbType.Int32, 0, 0, false);

            db.CreateIndex(tblRightsName, Construct.SecurityRulesIndexName(objClass), true, true, "ID", "Identity");
            db.CreateFKConstraint(tblRightsName, objClass.GetTableRef(db), "ID", Construct.SecurityRulesFKName(objClass), true);

            var tblName = objClass.GetTableRef(db);
            var rightsViewUnmaterializedName = db.GetTableName(objClass.Module.SchemaName, Construct.SecurityRulesRightsViewUnmaterializedName(objClass));
            var refreshRightsOnProcedureName = db.GetProcedureName(objClass.Module.SchemaName, Construct.SecurityRulesRefreshRightsOnProcedureName(objClass));

            DoCreateUpdateRightsTrigger(objClass);
            DoCreateRightsViewUnmaterialized(objClass);
            db.CreateRefreshRightsOnProcedure(refreshRightsOnProcedureName, rightsViewUnmaterializedName, tblName, tblRightsName);
            db.ExecRefreshRightsOnProcedure(refreshRightsOnProcedureName);
        }

        public void DoCreateRightsViewUnmaterialized(ObjectClass objClass)
        {
            var tblName = objClass.GetTableRef(db);
            var tblRightsName = db.GetTableName(objClass.Module.SchemaName, Construct.SecurityRulesTableName(objClass));
            var rightsViewUnmaterializedName = db.GetTableName(objClass.Module.SchemaName, Construct.SecurityRulesRightsViewUnmaterializedName(objClass));

            if (objClass.AccessControlList.Count == 0)
            {
                Log.ErrorFormat("Unable to create RightsViewUnmaterialized: ObjectClass '{0}' has an empty AccessControlList", objClass.Name);
                db.CreateEmptyRightsViewUnmaterialized(rightsViewUnmaterializedName);
                return;
            }


            List<ACL> viewAcls = new List<ACL>();
            foreach (var ac in objClass.AccessControlList.OfType<RoleMembership>())
            {
                if (ac.Relations.Count == 0)
                {
                    Log.ErrorFormat("Unable to create RightsViewUnmaterialized: RoleMembership '{0}' has no relations", ac.Name);
                    db.CreateEmptyRightsViewUnmaterialized(rightsViewUnmaterializedName);
                    return;
                }

                var viewAcl = new ACL();
                viewAcls.Add(viewAcl);
                viewAcl.Right = (Zetbox.API.AccessRights)ac.Rights;
                try
                {
                    viewAcl.Relations.AddRange(SchemaManager.CreateJoinList(db, objClass, ac.Relations));
                }
                catch (SchemaManager.JoinListException ex)
                {
                    Log.ErrorFormat(ex.Message);
                    db.CreateEmptyRightsViewUnmaterialized(rightsViewUnmaterializedName);
                    return;
                }
            }

            db.CreateRightsViewUnmaterialized(rightsViewUnmaterializedName, tblName, tblRightsName, viewAcls);
        }

        public void DoCreateUpdateRightsTrigger(ObjectClass objClass)
        {
            var updateRightsTriggerName = Construct.SecurityRulesUpdateRightsTriggerName(objClass);
            var tblName = objClass.GetTableRef(db);
            if (db.CheckTriggerExists(tblName, updateRightsTriggerName))
                db.DropTrigger(tblName, updateRightsTriggerName);

            var tblList = new List<RightsTrigger>();
            tblList.Add(new RightsTrigger()
            {
                TblName = objClass.GetTableRef(db),
                TblNameRights = db.GetTableName(objClass.Module.SchemaName, Construct.SecurityRulesTableName(objClass)),
                ViewUnmaterializedName = db.GetTableName(objClass.Module.SchemaName, Construct.SecurityRulesRightsViewUnmaterializedName(objClass))
            });

            // Get all ObjectClasses that depends on current object class
            var list = schema.GetQuery<ObjectClass>()
                .Where(o => o.AccessControlList.OfType<RoleMembership>()
                    .Where(rm => rm.Relations
                        .Where(r => r.A.Type == objClass || r.B.Type == objClass).Count() > 0).Count() > 0)
                .Distinct().ToList().Where(o => o.NeedsRightsTable() && o != objClass);
            foreach (var dep in list)
            {
                Log.DebugFormat("  Additional update Table: {0}", dep.TableName);
                foreach (var ac in dep.AccessControlList.OfType<RoleMembership>())
                {
                    var rel = ac.Relations.FirstOrDefault(r => r.A.Type == objClass || r.B.Type == objClass);
                    if (rel != null)
                    {
                        var rt = new RightsTrigger()
                        {
                            TblName = dep.GetTableRef(db),
                            TblNameRights = db.GetTableName(dep.Module.SchemaName, Construct.SecurityRulesTableName(dep)),
                            ViewUnmaterializedName = db.GetTableName(dep.Module.SchemaName, Construct.SecurityRulesRightsViewUnmaterializedName(dep)),
                        };
                        try
                        {
                            rt.Relations.AddRange(SchemaManager.CreateJoinList(db, dep, ac.Relations, rel));
                        }
                        catch (Zetbox.Server.SchemaManagement.SchemaManager.JoinListException ex)
                        {
                            Log.Warn("Unable to create UpdateRightsTrigger on " + objClass, ex);
                            return;
                        }
                        tblList.Add(rt);
                    }
                }
            }

            // do not check fk_ChangedBy since it always changes, even when only recalculations were done.
            // ACLs MUST never use ChangedBy information
            var fkCols = objClass.GetRelationEndsWithLocalStorage()
                .Where(r => !(r.Type.ImplementsIChangedBy() && r.Navigator != null && r.Navigator.Name == "ChangedBy"))
                .Select(r => Construct.ForeignKeyColumnName(r.GetParent().GetOtherEnd(r)))
                .ToList();
            db.CreateUpdateRightsTrigger(updateRightsTriggerName, tblName, tblList, fkCols);
        }

        public void DoCreateUpdateRightsTrigger(Relation rel)
        {
            var updateRightsTriggerName = Construct.SecurityRulesUpdateRightsTriggerName(rel);
            var tblName = db.GetTableName(rel.Module.SchemaName, rel.GetRelationTableName());
            if (db.CheckTriggerExists(tblName, updateRightsTriggerName))
                db.DropTrigger(tblName, updateRightsTriggerName);

            var tblList = new List<RightsTrigger>();

            // Get all ObjectClasses that depends on current relation
            var list = schema.GetQuery<ObjectClass>()
                .Where(o => o.AccessControlList.OfType<RoleMembership>()
                    .Where(rm => rm.Relations
                        .Where(r => r == rel).Count() > 0).Count() > 0)
                .Distinct().ToList().Where(o => o.NeedsRightsTable());

            foreach (var dep in list)
            {
                Log.DebugFormat("  Additional update Table: {0}", dep.TableName);
                foreach (var ac in dep.AccessControlList.OfType<RoleMembership>())
                {
                    if (ac.Relations.Contains(rel))
                    {
                        var rt = new RightsTrigger()
                        {
                            TblName = dep.GetTableRef(db),
                            TblNameRights = db.GetTableName(dep.Module.SchemaName, Construct.SecurityRulesTableName(dep)),
                            ViewUnmaterializedName = db.GetTableName(dep.Module.SchemaName, Construct.SecurityRulesRightsViewUnmaterializedName(dep)),
                        };
                        try
                        {
                            // Ignore last one - this is our n:m end
                            var joinList = SchemaManager.CreateJoinList(db, dep, ac.Relations, rel);
                            rt.Relations.AddRange(joinList.Take(joinList.Count - 1));
                        }
                        catch (Zetbox.Server.SchemaManagement.SchemaManager.JoinListException ex)
                        {
                            Log.Warn("Unable to create UpdateRightsTrigger on " + rel, ex);
                            return;
                        }

                        tblList.Add(rt);
                    }
                }
            }

            db.CreateUpdateRightsTrigger(updateRightsTriggerName, tblName, tblList, new List<string>() { Construct.ForeignKeyColumnName(rel.A), Construct.ForeignKeyColumnName(rel.B) });
        }
        #endregion

        #region ChangeObjectClassACL
        public bool IsChangeObjectClassACL(ObjectClass objClass)
        {
            if (objClass == null) throw new ArgumentNullException("objClass");

            // Basic checks
            if (!objClass.NeedsRightsTable()) return false;
            ObjectClass savedObjClass = savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid);
            if (savedObjClass == null) return false;
            if (!savedObjClass.NeedsRightsTable()) return false;

            // Check each AccessControl
            var acl = objClass.AccessControlList;
            var savedAcl = savedObjClass.AccessControlList;

            if (acl.Count != savedAcl.Count) return true;

            foreach (var ac in acl.OfType<RoleMembership>())
            {
                var sac = savedAcl.OfType<RoleMembership>().FirstOrDefault(i => i.ExportGuid == ac.ExportGuid);
                if (sac == null) return true;

                if (ac.Relations.Count != sac.Relations.Count) return true;

                for (int i = 0; i < ac.Relations.Count; i++)
                {
                    if (ac.Relations[i].ExportGuid != sac.Relations[i].ExportGuid) return true;
                }
            }

            return false;
        }

        public void DoChangeObjectClassACL(ObjectClass objClass)
        {
            var rightsViewUnmaterializedName = db.GetTableName(objClass.Module.SchemaName, Construct.SecurityRulesRightsViewUnmaterializedName(objClass));
            var refreshRightsOnProcedureName = db.GetProcedureName(objClass.Module.SchemaName, Construct.SecurityRulesRefreshRightsOnProcedureName(objClass));

            if (db.CheckViewExists(rightsViewUnmaterializedName))
                db.DropView(rightsViewUnmaterializedName);
            DoCreateRightsViewUnmaterialized(objClass);
            db.ExecRefreshRightsOnProcedure(refreshRightsOnProcedureName);
        }
        #endregion

        #region DeleteObjectClassSecurityRules
        public bool IsDeleteObjectClassSecurityRules(ObjectClass objClass)
        {
            if (objClass.NeedsRightsTable()) return false;
            ObjectClass savedObjClass = savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid);
            return savedObjClass != null && savedObjClass.NeedsRightsTable();
        }
        public void DoDeleteObjectClassSecurityRules(ObjectClass objClass)
        {
            var tblName = objClass.GetTableRef(db);
            var tblRightsName = db.GetTableName(objClass.Module.SchemaName, Construct.SecurityRulesTableName(objClass));
            var rightsViewUnmaterializedName = db.GetTableName(objClass.Module.SchemaName, Construct.SecurityRulesRightsViewUnmaterializedName(objClass));
            var refreshRightsOnProcedureName = Construct.SecurityRulesRefreshRightsOnProcedureName(objClass);
            var updateRightsTriggerName = Construct.SecurityRulesUpdateRightsTriggerName(objClass);

            Log.InfoFormat("Delete ObjectClass Security Rules: {0}", objClass.Name);

            if (db.CheckTriggerExists(tblName, updateRightsTriggerName))
                db.DropTrigger(tblName, updateRightsTriggerName);

            if (db.CheckProcedureExists(db.GetProcedureName(objClass.Module.SchemaName, refreshRightsOnProcedureName)))
                db.DropProcedure(db.GetProcedureName(objClass.Module.SchemaName, refreshRightsOnProcedureName));

            if (db.CheckViewExists(rightsViewUnmaterializedName))
                db.DropView(rightsViewUnmaterializedName);

            if (db.CheckTableExists(tblRightsName))
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
            var tblName = objClass.GetTableRef(db);
            var colName = Construct.ColumnName(prop, prefix);
            Log.InfoFormat("Drop Column: {0}.{1}", tblName, colName);
            if (db.CheckColumnExists(tblName, colName))
                db.DropColumn(tblName, colName);
        }
        #endregion

        #region NewValueTypeProperty nullable
        public bool IsNewCompoundObjectProperty(CompoundObjectProperty prop)
        {
            return savedSchema.FindPersistenceObject<CompoundObjectProperty>(prop.ExportGuid) == null;
        }
        public void DoNewCompoundObjectProperty(ObjectClass objClass, CompoundObjectProperty cprop, string prefix)
        {
            var tblName = objClass.GetTableRef(db);
            CreateCompoundObjectProperty(tblName, cprop, prefix, true);
        }

        private void CreateCompoundObjectProperty(TableRef tblName, CompoundObjectProperty cprop, string prefix, bool logAsNew)
        {
            string baseColName = Construct.ColumnName(cprop, prefix);
            if (logAsNew) Log.InfoFormat("New is null column for CompoundObject Property: '{0}'", cprop.Name);
            var hasData = db.CheckTableContainsData(tblName);

            foreach (var valProp in cprop.CompoundObjectDefinition.Properties.OfType<ValueTypeProperty>())
            {
                var colName = Construct.ColumnName(valProp, baseColName);
                if (logAsNew) Log.InfoFormat("New nullable ValueType Property: '{0}' ('{1}')", valProp.Name, colName);
                db.CreateColumn(
                    tblName,
                    colName,
                    valProp.GetDbType(),
                    valProp.GetSize(),
                    valProp.GetScale(),
                    hasData || valProp.IsNullable(),
                    SchemaManager.GetDefaultConstraint(valProp));
            }

            // TODO: Add nested CompoundObjectProperty
        }
        #endregion

        #region NewIndexConstraint
        public bool IsNewIndexConstraint(IndexConstraint uc)
        {
            return uc.Constrained is ObjectClass && savedSchema.FindPersistenceObject<IndexConstraint>(uc.ExportGuid) == null;
        }
        public void DoNewIndexConstraint(IndexConstraint uc)
        {
            var objClass = (ObjectClass)uc.Constrained;
            var tblName = objClass.GetTableRef(db);
            var columns = GetUCColNames(uc);
            Log.InfoFormat("New Index Constraint: {0} on {1}({2})", uc.Reason, tblName, string.Join(", ", columns));
            if (db.CheckIndexPossible(tblName, Construct.IndexName(objClass.TableName, columns), uc.IsUnique, false, columns))
                db.CreateIndex(tblName, Construct.IndexName(objClass.TableName, columns), uc.IsUnique, false, columns);
            else
                Log.WarnFormat("Cannot create Index Constraint: {0} on {1}({2})", uc.Reason, tblName, string.Join(", ", columns));
        }

        internal static string[] GetUCColNames(IndexConstraint uc)
        {
            var vt_columns = uc.Properties.OfType<ValueTypeProperty>().Select(p => Construct.ColumnName(p, null)).ToArray();
            var columns = vt_columns.Union(uc.Properties.OfType<ObjectReferenceProperty>().Select(p => Construct.ForeignKeyColumnName(p.RelationEnd.Parent.GetOtherEnd(p.RelationEnd)))).OrderBy(n => n).ToArray();
            return columns;
        }
        #endregion

        #region DeleteIndexConstraint
        public bool IsDeleteIndexConstraint(IndexConstraint uc)
        {
            return uc.Constrained is ObjectClass && schema.FindPersistenceObject<IndexConstraint>(uc.ExportGuid) == null;
        }
        public void DoDeleteIndexConstraint(IndexConstraint uc)
        {
            var objClass = (ObjectClass)uc.Constrained;
            var tblName = objClass.GetTableRef(db);
            var columns = GetUCColNames(uc);
            if (db.CheckIndexExists(tblName, Construct.IndexName(objClass.TableName, columns)))
            {
                Log.InfoFormat("Drop Index Constraint: {0} on {1}({2})", uc.Reason, objClass.TableName, string.Join(", ", columns));
                if (db.CheckIndexExists(tblName, Construct.IndexName(objClass.TableName, columns)))
                    db.DropIndex(tblName, Construct.IndexName(objClass.TableName, columns));
            }
        }
        #endregion

        #region ChangeIndexConstraint
        public bool IsChangeIndexConstraint(IndexConstraint uc)
        {
            if (!(uc is IndexConstraint)) return false;
            var saved = savedSchema.FindPersistenceObject<IndexConstraint>(uc.ExportGuid);
            if (saved == null) return false;

            if (uc.IsUnique != saved.IsUnique) return true;

            var newCols = GetUCColNames(uc);
            var savedCols = GetUCColNames(saved);
            if (newCols.Length != savedCols.Length) return true;
            foreach (var c in newCols)
            {
                if (!savedCols.Contains(c)) return true;
            }
            return false;
        }
        public void DoChangeIndexConstraint(IndexConstraint uc)
        {
            var saved = savedSchema.FindPersistenceObject<IndexConstraint>(uc.ExportGuid);
            DoDeleteIndexConstraint(saved);
            DoNewIndexConstraint(uc);
        }
        #endregion

        #region NewSchema
        internal bool IsNewSchema(string schemaName)
        {
            return db.CheckSchemaExists(schemaName);
        }

        internal void DoNewSchema(string schemaName)
        {
            Log.InfoFormat("New Schema: {0}", schemaName);
            db.CreateSchema(schemaName);
        }
        #endregion

        #endregion

        #region RefreshRights
        public void DoCreateRefreshAllRightsProcedure(List<ObjectClass> allACLTables)
        {
            var procName = db.GetProcedureName("dbo", Construct.SecurityRulesRefreshAllRightsProcedureName());
            if (db.CheckProcedureExists(procName))
                db.DropProcedure(procName);

            var refreshProcNames = allACLTables
                .Select(i => db.GetProcedureName(i.Module.SchemaName, Construct.SecurityRulesRefreshRightsOnProcedureName(i)))
                .ToList();
            db.CreateRefreshAllRightsProcedure(refreshProcNames);
        }
        #endregion


    }
}
