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
    using System.Linq.Expressions;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.SchemaManagement;
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
        private readonly Dictionary<Tuple<ClassMigrationEventType, Guid>, IClassMigrationFragment> _classMigrationFragments;
        private readonly Dictionary<Tuple<PropertyMigrationEventType, Guid>, IPropertyMigrationFragment> _propertyMigrationFragments;
        private readonly Dictionary<Tuple<RelationMigrationEventType, Guid>, IRelationMigrationFragment> _relationMigrationFragments;

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

        internal Cases(IZetboxContext schema, ISchemaProvider db, IZetboxContext savedSchema, IEnumerable<IMigrationFragment> migrationFragments)
        {
            this.schema = schema;
            this.db = db;
            this._savedSchema = savedSchema;
            try
            {
                this._classMigrationFragments = migrationFragments.OfType<IClassMigrationFragment>().ToDictionary(cmf => new Tuple<ClassMigrationEventType, Guid>(cmf.ClassEventType, cmf.Target));
                this._propertyMigrationFragments = migrationFragments.OfType<IPropertyMigrationFragment>().ToDictionary(cmf => new Tuple<PropertyMigrationEventType, Guid>(cmf.PropertyEventType, cmf.Target));
                this._relationMigrationFragments = migrationFragments.OfType<IRelationMigrationFragment>().ToDictionary(cmf => new Tuple<RelationMigrationEventType, Guid>(cmf.RelationEventType, cmf.Target));
            }
            catch (ArgumentException ex)
            {
                throw new InvalidOperationException("Initializing migration framents. An ArgumentException has been catched. This usually indicates, that more than one migration fragment is registrated for a specific migration.", ex);
            }
        }

        // Add all IsCase_ + DoCase_ Methods

        #region Cases

        #region DeleteObjectClass
        public bool IsDeleteObjectClass(ObjectClass savedObjClass)
        {
            return schema.FindPersistenceObject<ObjectClass>(savedObjClass.ExportGuid) == null;
        }
        public void DoDeleteObjectClass(ObjectClass savedObjClass)
        {
            if (!PreMigration(ClassMigrationEventType.Delete, savedObjClass, null))
                return;

            if (savedObjClass.NeedsRightsTable())
            {
                DoDeleteObjectClassSecurityRules(savedObjClass);
            }

            var mapping = savedObjClass.GetTableMapping();
            if (mapping == TableMapping.TPT)
            {
                var tbl = savedObjClass.GetTableRef(db);
                Log.InfoFormat("Drop Table: {0}", tbl);
                if (db.CheckTableExists(tbl))
                    db.DropTable(tbl);
            }
            else if (mapping == TableMapping.TPH)
            {
                // TODO: Do delete all columns
            }
            else
            {
                throw new NotSupportedException(string.Format("Mapping {0} is not supported", mapping));
            }

            PostMigration(ClassMigrationEventType.Delete, savedObjClass, null);
        }
        #endregion

        #region NewObjectClass
        public bool IsNewObjectClass(ObjectClass objClass)
        {
            return savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid) == null;
        }
        public void DoNewObjectClass(ObjectClass objClass)
        {
            if (!PreMigration(ClassMigrationEventType.Add, null, objClass))
                return;

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

            PostMigration(ClassMigrationEventType.Add, null, objClass);
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
            var savedObjClass = savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid);

            if (!PreMigration(ClassMigrationEventType.RenameTable, savedObjClass, objClass))
                return;

            var mapping = objClass.GetTableMapping();
            if (mapping == TableMapping.TPT || (mapping == TableMapping.TPH && objClass.BaseObjectClass == null))
            {
                db.RenameTable(savedObjClass.GetTableRef(db), objClass.GetTableRef(db));
            }

            PostMigration(ClassMigrationEventType.RenameTable, savedObjClass, objClass);
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
            var savedProp = savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid);

            if (!PreMigration(PropertyMigrationEventType.Rename, savedProp, prop))
                return;

            // TODO: What if prefix has changed
            db.RenameColumn(objClass.GetTableRef(db), Construct.ColumnName(savedProp, prefix), Construct.ColumnName(prop, prefix));

            PostMigration(PropertyMigrationEventType.Rename, savedProp, prop);
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
            var savedProp = savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid);

            if (!PreMigration(PropertyMigrationEventType.Move, savedProp, prop))
                return;

            // Reflected changed hierarchie
            var currentOriginObjClass = schema.FindPersistenceObject<ObjectClass>(savedProp.ObjectClass.ExportGuid);
            var movedUp = IsParentOf(objClass, currentOriginObjClass);
            var movedDown = IsParentOf(currentOriginObjClass, objClass);

            var tblName = objClass.GetTableRef(db);
            var srcTblName = ((ObjectClass)savedProp.ObjectClass).GetTableRef(db);
            var colName = Construct.ColumnName(prop, prefix);
            var srcColName = Construct.ColumnName(savedProp, prefix); // TODO: What if prefix has changed
            var dbType = prop.GetDbType();
            var size = prop.GetSize();
            var scale = prop.GetScale();
            var defConstr = SchemaManager.GetDefaultConstraint(prop);

            if (movedUp)
            {
                Log.InfoFormat("Moving property '{0}' from '{1}' up to '{2}'", prop.Name, savedProp.ObjectClass.Name, objClass.Name);
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
                Log.InfoFormat("Moving property '{0}' from '{1}' down to '{2}' (dataloss possible)", prop.Name, savedProp.ObjectClass.Name, objClass.Name);
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
                Log.ErrorFormat("Moving property '{2}' from '{0}' to '{1}' is not supported. ObjectClasses are not in the same hierarchy. Will only create destination column.", savedProp.ObjectClass.Name, prop.ObjectClass.Name, prop.Name);
                db.CreateColumn(tblName, colName, dbType, size, scale, true, defConstr);
            }

            PostMigration(PropertyMigrationEventType.Move, savedProp, prop);
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
            if (!PreMigration(PropertyMigrationEventType.Add, null, prop))
                return;

            string colName = Construct.ColumnName(prop, prefix);
            Log.InfoFormat("New nullable ValueType Property: '{0}' ('{1}')", prop.Name, colName);
            CheckValueTypePropertyHasWarnings(prop);

            CreateValueTypePropertyNullable(objClass.GetTableRef(db), prop, colName, true);

            PostMigration(PropertyMigrationEventType.Add, null, prop);
        }

        private void CreateValueTypePropertyNullable(TableRef tblName, ValueTypeProperty prop, string colName, bool withDefault)
        {
            db.CreateColumn(tblName, colName, prop.GetDbType(), prop.GetSize(), prop.GetScale(), true, withDefault ? SchemaManager.GetDefaultConstraint(prop) : null);
        }

        #endregion

        #region NewValueTypeProperty not nullable
        public bool IsNewValueTypePropertyNotNullable(ValueTypeProperty prop)
        {
            return !prop.IsNullable() && savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid) == null;
        }
        public void DoNewValueTypePropertyNotNullable(ObjectClass objClass, ValueTypeProperty prop, string prefix)
        {
            if (!PreMigration(PropertyMigrationEventType.Add, null, prop))
                return;

            var tblName = objClass.GetTableRef(db);
            var colName = Construct.ColumnName(prop, prefix);
            var dbType = prop.GetDbType();
            var size = prop.GetSize();
            var scale = prop.GetScale();
            var def = SchemaManager.GetDefaultConstraint(prop);
            var isSimplyCheckable = objClass.GetTableMapping() == TableMapping.TPT || objClass.BaseObjectClass == null;
            bool updateDone = false;

            // classes that do have this property
            var classes = objClass.AndChildren(c => c.SubClasses).Select(cls => Construct.DiscriminatorValue(cls)).ToList();

            Log.InfoFormat("New not nullable ValueType Property: [{0}.{1}] (col:{2})", prop.ObjectClass.Name, prop.Name, colName);

            CheckValueTypePropertyHasWarnings(prop);

            if (db.CheckTableContainsData(tblName, isSimplyCheckable ? null : classes))
            {
                db.CreateColumn(tblName, colName, dbType, size, scale, true, isSimplyCheckable ? def : null);
                updateDone = WriteDefaultValue(tblName, colName, def, isSimplyCheckable ? null : classes);
            }
            else
            {
                db.CreateColumn(tblName, colName, dbType, size, scale, !isSimplyCheckable, isSimplyCheckable ? def : null);
                updateDone = true;
            }

            if (updateDone && isSimplyCheckable)
            {
                db.AlterColumn(tblName, colName, dbType, size, scale, false, def);
            }
            else if (updateDone && !isSimplyCheckable)
            {
                CreateTPHNotNullCheckConstraint(tblName, colName, objClass);
            }
            else if (!updateDone && isSimplyCheckable)
            {
                Log.ErrorFormat("unable to set ValueType Property '{0}' to NOT NULL when table '{1}' contains data: No supported default constraint found", colName, tblName);
            }
            else if (!updateDone && !isSimplyCheckable)
            {
                Log.ErrorFormat("unable to create CHECK constraint on ValueType Property '{0}' when table '{1}' contains data: No supported default constraint found", colName, tblName);
            }

            PostMigration(PropertyMigrationEventType.Add, null, prop);
        }

        private static void CheckValueTypePropertyHasWarnings(ValueTypeProperty prop)
        {
            if (prop is StringProperty)
            {
                if (((StringProperty)prop).GetLengthConstraint() == null)
                {
                    Log.Warn("String property must have a string range constraint");
                }
            }
        }

        internal void CreateTPHNotNullCheckConstraint(TableRef tblName, string colName, ObjectClass objClass)
        {
            // classes that do have this property
            var classes = objClass.AndChildren(c => c.SubClasses).Select(cls => Construct.DiscriminatorValue(cls)).ToList();

            // classes that do not have this property
            var otherClasses = objClass
                .BaseObjectClass // step once up, which is allowed, since we're not base (else isSimplyCheckable would be true) and necessary to let the skip work (see below)
                .AndParents(cls => cls.BaseObjectClass)
                .SelectMany(cls => cls
                    .AndChildren(c => c.SubClasses
                        .Where(child => child != objClass))) // skip self (and thus its children)
                .Select(cls => Construct.DiscriminatorValue(cls)).ToList();

            var checkExpressions = new Dictionary<List<string>, Expression<Func<string, bool>>>()
                    {
                        { classes, s => s != null },
                        { otherClasses, s => s == null },
                    };

            var checkConstraintName = Construct.CheckConstraintName(tblName.Name, colName);
            if (db.CheckCheckConstraintPossible(tblName, colName, checkConstraintName, checkExpressions))
                db.CreateCheckConstraint(tblName, colName, checkConstraintName, checkExpressions);
            else
                Log.ErrorFormat("unable to create CHECK constraint for ValueType Property '{0}' in '{1}': column contains invalid NULLs or superfluous values", colName, tblName);
        }

        private bool WriteDefaultValue(TableRef tblName, string colName, DefaultConstraint def, IEnumerable<string> discriminatorFilter)
        {
            if (def is NewGuidDefaultConstraint)
            {
                db.WriteGuidDefaultValue(tblName, colName, discriminatorFilter);
                return true;
            }
            else if (def is DateTimeDefaultConstraint)
            {
                if (((DateTimeDefaultConstraint)def).Precision == DateTimeDefaultConstraintPrecision.Date)
                    db.WriteDefaultValue(tblName, colName, DateTime.Today, discriminatorFilter);
                else
                    db.WriteDefaultValue(tblName, colName, DateTime.Now, discriminatorFilter);
                return true;
            }
            else if (def is BoolDefaultConstraint)
            {
                db.WriteDefaultValue(tblName, colName, ((BoolDefaultConstraint)def).Value, discriminatorFilter);
                return true;
            }
            else if (def is IntDefaultConstraint)
            {
                db.WriteDefaultValue(tblName, colName, ((IntDefaultConstraint)def).Value, discriminatorFilter);
                return true;
            }
            else if (def is DecimalDefaultConstraint)
            {
                db.WriteDefaultValue(tblName, colName, ((DecimalDefaultConstraint)def).Value, discriminatorFilter);
                return true;
            }
            return false;
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
            var savedProp = savedSchema.FindPersistenceObject<Property>(prop.ExportGuid);

            if (!PreMigration(PropertyMigrationEventType.ChangeDefaultValueDefinition, savedProp, prop))
                return;

            var tblName = objClass.GetTableRef(db);
            var colName = Construct.ColumnName(prop, prefix);

            // Use current nullable definition. 
            // Another case is responsible to change that.
            var currentIsNullable = db.GetIsColumnNullable(tblName, colName);

            db.AlterColumn(tblName, colName, prop.GetDbType(), prop.GetSize(), prop.GetScale(), currentIsNullable, SchemaManager.GetDefaultConstraint(prop));

            PostMigration(PropertyMigrationEventType.ChangeDefaultValueDefinition, savedProp, prop);
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
            var savedProp = savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid);

            if (!PreMigration(PropertyMigrationEventType.ChangeToNotNullable, savedProp, prop))
                return;

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

            PostMigration(PropertyMigrationEventType.ChangeToNotNullable, savedProp, prop);
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
            var savedProp = savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid);

            if (!PreMigration(PropertyMigrationEventType.ChangeToNullable, savedProp, prop))
                return;

            var tblName = objClass.GetTableRef(db);
            var colName = Construct.ColumnName(prop, prefix);

            db.AlterColumn(tblName, colName, prop.GetDbType(), prop.GetSize(), prop.GetScale(), prop.IsNullable(), null);

            PostMigration(PropertyMigrationEventType.ChangeToNullable, savedProp, prop);
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
            var savedProp = savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid);

            if (!PreMigration(PropertyMigrationEventType.Rename, savedProp, prop))
                return;

            Log.ErrorFormat("renaming a Property from '{0}' to '{1}' is not supported yet", savedProp.Name, prop.Name);

            PostMigration(PropertyMigrationEventType.Rename, savedProp, prop);
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
            var savedProp = savedSchema.FindPersistenceObject<ValueTypeProperty>(prop.ExportGuid);

            if (!PreMigration(PropertyMigrationEventType.Move, savedProp, prop))
                return;

            Log.ErrorFormat("moving a Property from '{0}' to '{1}' is not supported yet", savedProp.ObjectClass.Name, prop.ObjectClass.Name);

            PostMigration(PropertyMigrationEventType.Move, savedProp, prop);
        }
        #endregion

        #region NewValueTypePropertyList
        public bool IsNewValueTypePropertyList(ValueTypeProperty prop)
        {
            return savedSchema.FindPersistenceObject<Property>(prop.ExportGuid) == null;
        }
        public void DoNewValueTypePropertyList(ObjectClass objClass, ValueTypeProperty prop)
        {
            if (!PreMigration(PropertyMigrationEventType.Add, null, prop))
                return;

            Log.InfoFormat("New ValueType Property List: {0}", prop.Name);
            CheckValueTypePropertyHasWarnings(prop);

            var tblName = db.GetTableName(prop.Module.SchemaName, prop.GetCollectionEntryTable());
            string fkName = Construct.ForeignKeyColumnName(prop);
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

            PostMigration(PropertyMigrationEventType.Add, null, prop);
        }
        #endregion

        #region DeleteValueTypePropertyList
        public bool IsDeleteValueTypePropertyList(ValueTypeProperty savedProp)
        {
            return savedProp.IsList && schema.FindPersistenceObject<ValueTypeProperty>(savedProp.ExportGuid) == null;
        }

        public void DoDeleteValueTypePropertyList(ObjectClass objClass, ValueTypeProperty savedProp, string prefix)
        {
            if (!PreMigration(PropertyMigrationEventType.Delete, savedProp, null))
                return;

            Log.InfoFormat("Delete ValueType Property List: {0}", savedProp.Name);
            var tblName = db.GetTableName(savedProp.Module.SchemaName, savedProp.GetCollectionEntryTable());
            db.DropTable(tblName);

            PostMigration(PropertyMigrationEventType.Delete, savedProp, null);
        }
        #endregion

        #region NewCompoundObjectPropertyList
        public bool IsNewCompoundObjectPropertyList(CompoundObjectProperty prop)
        {
            return savedSchema.FindPersistenceObject<Property>(prop.ExportGuid) == null;
        }
        public void DoNewCompoundObjectPropertyList(ObjectClass objClass, CompoundObjectProperty cprop)
        {
            if (!PreMigration(PropertyMigrationEventType.Add, null, cprop))
                return;

            Log.InfoFormat("New CompoundObject Property List: {0}", cprop.Name);
            var tblName = db.GetTableName(cprop.Module.SchemaName, cprop.GetCollectionEntryTable());
            string fkName = Construct.ForeignKeyColumnName(cprop);

            // TODO: Support nested CompoundObject
            string valPropIndexName = cprop.Name + "Index";
            string assocName = cprop.GetAssociationName();
            bool hasPersistentOrder = cprop.HasPersistentOrder;

            db.CreateTable(tblName, true);
            db.CreateColumn(tblName, fkName, System.Data.DbType.Int32, 0, 0, false);

            foreach (ValueTypeProperty p in cprop.CompoundObjectDefinition.Properties)
            {
                CheckValueTypePropertyHasWarnings(p);
                db.CreateColumn(tblName, Construct.ColumnName(p, cprop.Name), p.GetDbType(), p.GetSize(), p.GetScale(), true, null);
            }

            if (hasPersistentOrder)
            {
                db.CreateColumn(tblName, valPropIndexName, System.Data.DbType.Int32, 0, 0, false);
            }
            db.CreateFKConstraint(tblName, objClass.GetTableRef(db), fkName, assocName, true);
            db.CreateIndex(tblName, Construct.IndexName(tblName.Name, fkName), false, false, fkName);

            PostMigration(PropertyMigrationEventType.Add, null, cprop);
        }
        #endregion

        #region DeleteCompoundObjectPropertyList
        public bool IsDeleteCompoundObjectPropertyList(CompoundObjectProperty savedCProp)
        {
            return savedCProp.IsList && schema.FindPersistenceObject<CompoundObjectProperty>(savedCProp.ExportGuid) == null;
        }
        public void DoDeleteCompoundObjectPropertyList(ObjectClass objClass, CompoundObjectProperty savedCProp, string prefix)
        {
            if (!PreMigration(PropertyMigrationEventType.Delete, savedCProp, null))
                return;

            Log.InfoFormat("Delete CompoundObject Property List: {0}", savedCProp.Name);
            var tblName = db.GetTableName(savedCProp.Module.SchemaName, savedCProp.GetCollectionEntryTable());
            db.DropTable(tblName);

            PostMigration(PropertyMigrationEventType.Delete, savedCProp, null);
        }
        #endregion

        #region RenameCompoundObjectPropertyListName
        public bool IsRenameCompoundObjectPropertyListName(CompoundObjectProperty prop)
        {
            var saved = savedSchema.FindPersistenceObject<CompoundObjectProperty>(prop.ExportGuid);
            if (saved == null) return false;
            return saved.Name != prop.Name;
        }
        public void DoRenameCompoundObjectPropertyListName(ObjectClass objClass, CompoundObjectProperty cprop)
        {
            var savedProp = savedSchema.FindPersistenceObject<ValueTypeProperty>(cprop.ExportGuid);

            if (!PreMigration(PropertyMigrationEventType.Rename, savedProp, cprop))
                return;

            Log.ErrorFormat("renaming a Property from '{0}' to '{1}' is not supported yet", savedProp.Name, cprop.Name);

            PostMigration(PropertyMigrationEventType.Rename, savedProp, cprop);
        }
        #endregion

        #region MoveCompoundObjectPropertyList
        public bool IsMoveCompoundObjectPropertyList(CompoundObjectProperty prop)
        {
            var saved = savedSchema.FindPersistenceObject<CompoundObjectProperty>(prop.ExportGuid);
            if (saved == null) return false;
            return saved.ObjectClass.ExportGuid != prop.ObjectClass.ExportGuid;
        }
        public void DoMoveCompoundObjectPropertyList(ObjectClass objClass, CompoundObjectProperty cprop)
        {
            var savedProp = savedSchema.FindPersistenceObject<ValueTypeProperty>(cprop.ExportGuid);

            if (!PreMigration(PropertyMigrationEventType.Move, savedProp, cprop))
                return;

            Log.ErrorFormat("moving a Property from '{0}' to '{1}' is not supported yet", savedProp.ObjectClass.Name, cprop.ObjectClass.Name);

            PostMigration(PropertyMigrationEventType.Move, savedProp, cprop);
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
            var savedRel = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);

            if (!PreMigration(RelationMigrationEventType.ChangeHasPositionStorage, savedRel, rel))
                return;

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

            PostMigration(RelationMigrationEventType.ChangeHasPositionStorage, savedRel, rel);
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
            var savedRel = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);

            if (!PreMigration(RelationMigrationEventType.ChangeHasPositionStorage, savedRel, rel))
                return;

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

            PostMigration(RelationMigrationEventType.ChangeHasPositionStorage, savedRel, rel);
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
            var savedRel = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);

            if (!PreMigration(RelationMigrationEventType.ChangeToNullable, savedRel, rel))
                return;

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

            PostMigration(RelationMigrationEventType.ChangeToNullable, savedRel, rel);
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
            var savedRel = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);

            if (!PreMigration(RelationMigrationEventType.ChangeToNotNullable, savedRel, rel))
                return;

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

            PostMigration(RelationMigrationEventType.ChangeToNotNullable, savedRel, rel);
        }
        #endregion

        #region Delete_1_N_Relation
        public bool IsDelete_1_N_Relation(Relation rel)
        {
            return schema.FindPersistenceObject<Relation>(rel.ExportGuid) == null;
        }
        public void DoDelete_1_N_Relation(Relation savedRel)
        {
            if (!PreMigration(RelationMigrationEventType.Delete, savedRel, null))
                return;

            string assocName = savedRel.GetAssociationName();
            Log.InfoFormat("Deleting 1:N Relation: {0}", assocName);

            TableRef tblName;
            bool isIndexed = false;
            RelationEnd otherEnd;
            if (savedRel.HasStorage(RelationEndRole.A))
            {
                tblName = savedRel.A.Type.GetTableRef(db);
                isIndexed = savedRel.NeedsPositionStorage(RelationEndRole.A);
                otherEnd = savedRel.B;
            }
            else if (savedRel.HasStorage(RelationEndRole.B))
            {
                tblName = savedRel.B.Type.GetTableRef(db);
                isIndexed = savedRel.NeedsPositionStorage(RelationEndRole.B);
                otherEnd = savedRel.A;
            }
            else
            {
                Log.ErrorFormat("Relation '{0}' has unsupported Storage set: {1}, skipped", assocName, savedRel.Storage);
                return;
            }

            if (db.CheckFKConstraintExists(tblName, assocName))
                db.DropFKConstraint(tblName, assocName);

            string colName = Construct.ForeignKeyColumnName(otherEnd);

            if (db.CheckColumnExists(tblName, colName))
                db.DropColumn(tblName, colName);

            if (isIndexed && db.CheckColumnExists(tblName, Construct.ListPositionColumnName(otherEnd)))
                db.DropColumn(tblName, Construct.ListPositionColumnName(otherEnd));

            PostMigration(RelationMigrationEventType.Delete, savedRel, null);
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
            var savedRel = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);

            if (!PreMigration(RelationMigrationEventType.ChangeType, savedRel, rel))
                return;

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
            if (savedRel.HasStorage(RelationEndRole.A))
            {
                srcSchemaName = savedRel.A.Type.Module.SchemaName;
                srcTblName = savedRel.A.Type.TableName;
                srcColName = Construct.ForeignKeyColumnName(savedRel.B);
            }
            if (savedRel.HasStorage(RelationEndRole.B) && (string.IsNullOrEmpty(srcSchemaName) || string.IsNullOrEmpty(srcTblName) || db.GetTableName(srcSchemaName, srcTblName) != destTblRef))
            {
                srcSchemaName = savedRel.B.Type.Module.SchemaName;
                srcTblName = savedRel.B.Type.TableName;
                srcColName = Construct.ForeignKeyColumnName(savedRel.A);
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
            if (savedRel.HasStorage(RelationEndRole.A))
            {
                srcTblName = savedRel.A.Type.TableName;
                srcColName = Construct.ForeignKeyColumnName(savedRel.B);
                var srcAssocName = savedRel.GetRelationAssociationName(RelationEndRole.A);

                if (db.CheckFKConstraintExists(srcTblRef, srcAssocName))
                    db.DropFKConstraint(srcTblRef, srcAssocName);
                if (srcTblRef != destTblRef || srcColName != destColName)
                {
                    if (db.CheckColumnExists(srcTblRef, srcColName))
                        db.DropColumn(srcTblRef, srcColName);
                }
            }
            if (savedRel.HasStorage(RelationEndRole.B))
            {
                srcTblName = savedRel.B.Type.TableName;
                srcColName = Construct.ForeignKeyColumnName(savedRel.A);
                var srcAssocName = savedRel.GetRelationAssociationName(RelationEndRole.B);

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
            var savedRel = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);

            if (!PreMigration(RelationMigrationEventType.ChangeType, savedRel, rel))
                return; string srcAssocName = savedRel.GetAssociationName();

            RelationEnd relEnd, otherEnd;

            switch (savedRel.Storage)
            {
                case StorageType.Replicate:
                case StorageType.MergeIntoA:
                    relEnd = savedRel.A;
                    otherEnd = savedRel.B;
                    break;
                case StorageType.MergeIntoB:
                    otherEnd = savedRel.A;
                    relEnd = savedRel.B;
                    break;
                default:
                    Log.ErrorFormat("Relation '{0}' has unsupported Storage set: {1}, skipped", srcAssocName, rel.Storage);
                    return;
            }

            var aType = rel.A.Type;
            var bType = rel.B.Type;
            var savedAType = savedRel.A.Type;
            var savedBType = savedRel.B.Type;

            var srcTblName = relEnd.Type.GetTableRef(db);
            var srcColName = Construct.ForeignKeyColumnName(otherEnd);

            var destTbl = db.GetTableName(rel.Module.SchemaName, rel.GetRelationTableName());
            var destCol = Construct.ForeignKeyColumnName(relEnd);
            var destFKCol = Construct.ForeignKeyColumnName(otherEnd);

            // Drop relations first as 1:1 and n:m relations share the same names
            var srcAssocA = savedRel.GetRelationAssociationName(RelationEndRole.A);
            if (db.CheckFKConstraintExists(aType.GetTableRef(db), srcAssocA))
                db.DropFKConstraint(aType.GetTableRef(db), srcAssocA);
            var srcAssocB = savedRel.GetRelationAssociationName(RelationEndRole.B);
            if (db.CheckFKConstraintExists(bType.GetTableRef(db), srcAssocB))
                db.DropFKConstraint(bType.GetTableRef(db), srcAssocB);

            DoNew_N_M_Relation(rel);
            db.InsertFKs(srcTblName, srcColName, destTbl, destCol, destFKCol);

            // Drop columns
            if (savedRel.Storage == StorageType.MergeIntoA || savedRel.Storage == StorageType.Replicate)
            {
                if (db.CheckColumnExists(savedAType.GetTableRef(db), Construct.ForeignKeyColumnName(savedRel.B)))
                    db.DropColumn(savedAType.GetTableRef(db), Construct.ForeignKeyColumnName(savedRel.B));
            }
            if (savedRel.Storage == StorageType.MergeIntoB || savedRel.Storage == StorageType.Replicate)
            {
                if (db.CheckColumnExists(savedBType.GetTableRef(db), Construct.ForeignKeyColumnName(savedRel.A)))
                    db.DropColumn(savedBType.GetTableRef(db), Construct.ForeignKeyColumnName(savedRel.A));
            }

            PostMigration(RelationMigrationEventType.ChangeType, savedRel, rel);
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
            var savedRel = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);

            if (!PreMigration(RelationMigrationEventType.ChangeType, savedRel, rel))
                return; string srcAssocName = savedRel.GetAssociationName();

            RelationEnd relEnd, otherEnd;

            switch (savedRel.Storage)
            {
                case StorageType.MergeIntoA:
                    relEnd = savedRel.A;
                    otherEnd = savedRel.B;
                    break;
                case StorageType.MergeIntoB:
                    otherEnd = savedRel.A;
                    relEnd = savedRel.B;
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

            PostMigration(RelationMigrationEventType.ChangeType, savedRel, rel);
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
            var savedRel = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);

            if (!PreMigration(RelationMigrationEventType.ChangeType, savedRel, rel))
                return;

            string srcAssocName = rel.GetAssociationName();

            RelationEnd relEnd, otherEnd;

            switch (savedRel.Storage)
            {
                case StorageType.MergeIntoA:
                    relEnd = savedRel.A;
                    otherEnd = savedRel.B;
                    break;
                case StorageType.MergeIntoB:
                    otherEnd = savedRel.A;
                    relEnd = savedRel.B;
                    break;
                default:
                    Log.ErrorFormat("Relation '{0}' has unsupported Storage set: {1}, skipped", srcAssocName, rel.Storage);
                    return;
            }

            var srcTblName = relEnd.Type.GetTableRef(db);
            var srcColName = Construct.ForeignKeyColumnName(otherEnd);

            var destTbl = db.GetTableName(rel.Module.SchemaName, rel.GetRelationTableName());
            var destCol = Construct.ForeignKeyColumnName(relEnd);
            var destFKCol = Construct.ForeignKeyColumnName(otherEnd);

            DoNew_N_M_Relation(rel);
            db.InsertFKs(srcTblName, srcColName, destTbl, destCol, destFKCol);
            DoDelete_1_N_Relation(savedRel);

            PostMigration(RelationMigrationEventType.ChangeType, savedRel, rel);
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
            var savedRel = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);

            if (!PreMigration(RelationMigrationEventType.ChangeType, savedRel, rel))
                return;

            string destAssocName = rel.GetAssociationName();

            var srcTblName = db.GetTableName(savedRel.Module.SchemaName, savedRel.GetRelationTableName());

            // Drop relations first as 1:1 and n:m relations share the same names
            var srcAssocA = savedRel.GetRelationAssociationName(RelationEndRole.A);
            if (db.CheckFKConstraintExists(srcTblName, srcAssocA))
                db.DropFKConstraint(srcTblName, srcAssocA);
            var srcAssocB = savedRel.GetRelationAssociationName(RelationEndRole.B);
            if (db.CheckFKConstraintExists(srcTblName, srcAssocB))
                db.DropFKConstraint(srcTblName, srcAssocB);

            DoNew_1_1_Relation(rel);

            if (rel.HasStorage(RelationEndRole.A))
            {
                var destTblName = rel.A.Type.GetTableRef(db);
                var destColName = Construct.ForeignKeyColumnName(rel.B);
                var srcColName = Construct.ForeignKeyColumnName(rel.B);
                var srcFKColName = Construct.ForeignKeyColumnName(rel.A);

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
                var srcColName = Construct.ForeignKeyColumnName(rel.A);
                var srcFKColName = Construct.ForeignKeyColumnName(rel.B);

                if (!db.CheckFKColumnContainsUniqueValues(srcTblName, srcColName))
                {
                    Log.ErrorFormat("Unable to change Relation '{0}' from n:m to 1:1. Data is not unique", destAssocName);
                    return;
                }
                db.CopyFKs(srcTblName, srcColName, destTblName, destColName, srcFKColName);
            }

            if (db.CheckTableExists(srcTblName))
                db.DropTable(srcTblName);

            PostMigration(RelationMigrationEventType.ChangeType, savedRel, rel);
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
            var savedRel = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);

            if (!PreMigration(RelationMigrationEventType.ChangeType, savedRel, rel))
                return;

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

            var srcTbl = db.GetTableName(savedRel.Module.SchemaName, savedRel.GetRelationTableName());
            // translate ends to savedEnds
            var srcCol = Construct.ForeignKeyColumnName(savedRel.GetEndFromRole(otherEnd.GetRole()));
            var srcFKCol = Construct.ForeignKeyColumnName(savedRel.GetEndFromRole(relEnd.GetRole()));

            if (!db.CheckFKColumnContainsUniqueValues(srcTbl, srcCol))
            {
                Log.ErrorFormat("Unable to change Relation '{0}' from n:m to 1:n. Data is not unique", destAssocName);
                return;
            }

            var destTblName = relEnd.Type.GetTableRef(db);
            var destColName = Construct.ForeignKeyColumnName(otherEnd);

            DoNew_1_N_Relation(rel);
            db.CopyFKs(srcTbl, srcCol, destTblName, destColName, srcFKCol);
            DoDelete_N_M_Relation(savedRel);

            PostMigration(RelationMigrationEventType.ChangeType, savedRel, rel);
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
            var savedRel = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);

            if (!PreMigration(RelationMigrationEventType.ChangeEndType, savedRel, rel))
                return;

            if (rel.GetRelationType() == RelationType.n_m)
            {
                var oldTblName = db.GetTableName(savedRel.Module.SchemaName, savedRel.GetRelationTableName());
                if (db.CheckTableContainsData(oldTblName))
                {
                    if (savedRel.A.Type.AndParents(cls => cls.BaseObjectClass).Select(cls => cls.ExportGuid).Contains(rel.A.Type.ExportGuid)
                        && savedRel.B.Type.AndParents(cls => cls.BaseObjectClass).Select(cls => cls.ExportGuid).Contains(rel.B.Type.ExportGuid))
                    {
                        string assocName = rel.GetAssociationName();
                        Log.InfoFormat("Rewiring N:M Relation: {0}", assocName);

                        if (db.CheckFKConstraintExists(oldTblName, savedRel.GetRelationAssociationName(RelationEndRole.A)))
                            db.DropFKConstraint(oldTblName, savedRel.GetRelationAssociationName(RelationEndRole.A));
                        if (db.CheckFKConstraintExists(oldTblName, savedRel.GetRelationAssociationName(RelationEndRole.B)))
                            db.DropFKConstraint(oldTblName, savedRel.GetRelationAssociationName(RelationEndRole.B));

                        // renaming is handled by DoChangeRelationName
                        //db.RenameTable(oldTblName, newTblName);

                        var fkAName = Construct.ForeignKeyColumnName(savedRel.A);
                        var fkBName = Construct.ForeignKeyColumnName(savedRel.B);
                        db.CreateFKConstraint(oldTblName, rel.A.Type.GetTableRef(db), fkAName, savedRel.GetRelationAssociationName(RelationEndRole.A), false);
                        db.CreateFKConstraint(oldTblName, rel.B.Type.GetTableRef(db), fkBName, savedRel.GetRelationAssociationName(RelationEndRole.B), false);
                    }
                    else
                    {
                        Log.WarnFormat("Unable to drop old relation. Relation has some instances. Table: " + oldTblName);
                    }
                }
                else
                {
                    DoDelete_N_M_Relation(savedRel);
                    DoNew_N_M_Relation(rel);
                }
            }
            else if (rel.GetRelationType() == RelationType.one_n)
            {
                RelationEnd relEnd, otherEnd;

                switch (rel.Storage)
                {
                    case StorageType.MergeIntoA:
                        relEnd = savedRel.A;
                        otherEnd = savedRel.B;
                        break;
                    case StorageType.MergeIntoB:
                        otherEnd = savedRel.A;
                        relEnd = savedRel.B;
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
                    DoDelete_1_N_Relation(savedRel);
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
                        relEnd = savedRel.A;
                        otherEnd = savedRel.B;
                        break;
                    case StorageType.MergeIntoB:
                        otherEnd = savedRel.A;
                        relEnd = savedRel.B;
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
                    DoDelete_1_1_Relation(savedRel);
                    DoNew_1_1_Relation(rel);
                }
            }

            PostMigration(RelationMigrationEventType.ChangeEndType, savedRel, rel);
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
            var savedRel = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);

            if (!PreMigration(RelationMigrationEventType.Rename, savedRel, rel))
                return;

            var fkAName = Construct.ForeignKeyColumnName(rel.A);
            var fkBName = Construct.ForeignKeyColumnName(rel.B);

            var old_fkAName = Construct.ForeignKeyColumnName(savedRel.A);
            var old_fkBName = Construct.ForeignKeyColumnName(savedRel.B);

            var aType = rel.A.Type;
            var bType = rel.B.Type;

            if (rel.GetRelationType() == RelationType.n_m)
            {
                var srcRelTbl = db.GetTableName(savedRel.Module.SchemaName, savedRel.GetRelationTableName());
                var destRelTbl = db.GetTableName(rel.Module.SchemaName, rel.GetRelationTableName());

                db.RenameFKConstraint(srcRelTbl, savedRel.GetRelationAssociationName(RelationEndRole.A),
                    aType.GetTableRef(db), old_fkAName, rel.GetRelationAssociationName(RelationEndRole.A), false);
                db.RenameFKConstraint(srcRelTbl, savedRel.GetRelationAssociationName(RelationEndRole.B),
                    bType.GetTableRef(db), old_fkBName, rel.GetRelationAssociationName(RelationEndRole.B), false);

                db.RenameTable(srcRelTbl, destRelTbl);

                db.RenameColumn(destRelTbl, old_fkAName, fkAName);
                db.RenameColumn(destRelTbl, old_fkBName, fkBName);
            }
            else if (rel.GetRelationType() == RelationType.one_n)
            {
                if (savedRel.HasStorage(RelationEndRole.A) &&
                    old_fkBName != fkBName)
                {
                    var tbl = aType.GetTableRef(db);
                    db.RenameFKConstraint(tbl, savedRel.GetAssociationName(),
                       aType.GetTableRef(db), old_fkBName, rel.GetAssociationName(), false);
                    db.RenameColumn(tbl, old_fkBName, fkBName);
                }
                else if (savedRel.HasStorage(RelationEndRole.B) &&
                    old_fkAName != fkAName)
                {
                    var tbl = bType.GetTableRef(db);
                    db.RenameFKConstraint(tbl, savedRel.GetAssociationName(),
                       bType.GetTableRef(db), old_fkAName, rel.GetAssociationName(), false);
                    db.RenameColumn(tbl, old_fkAName, fkAName);
                }
            }
            else if (rel.GetRelationType() == RelationType.one_one)
            {
                if (savedRel.HasStorage(RelationEndRole.A))
                {
                    var tbl = aType.GetTableRef(db);
                    db.RenameFKConstraint(tbl, savedRel.GetRelationAssociationName(RelationEndRole.A),
                        aType.GetTableRef(db), old_fkAName, rel.GetRelationAssociationName(RelationEndRole.A), false);
                    if (old_fkBName != fkBName)
                    {
                        db.RenameColumn(tbl, old_fkBName, fkBName);
                    }
                }
                if (savedRel.HasStorage(RelationEndRole.B))
                {
                    var tbl = bType.GetTableRef(db);
                    db.RenameFKConstraint(tbl, savedRel.GetRelationAssociationName(RelationEndRole.B),
                        bType.GetTableRef(db), old_fkBName, rel.GetRelationAssociationName(RelationEndRole.B), false);
                    if (old_fkAName != fkAName)
                    {
                        db.RenameColumn(tbl, old_fkAName, fkAName);
                    }
                }
            }

            PostMigration(RelationMigrationEventType.Rename, savedRel, rel);
        }
        #endregion

        #region New_1_N_Relation
        public bool IsNew_1_N_Relation(Relation rel)
        {
            return savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid) == null;
        }
        public void DoNew_1_N_Relation(Relation rel)
        {
            if (!PreMigration(RelationMigrationEventType.Add, null, rel))
                return;

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

            PostMigration(RelationMigrationEventType.Add, null, rel);
        }

        /// <summary>
        /// Tries to dissect a relation for the 1_N case.
        /// </summary>
        /// <returns>false if something is wrong with the relation definition. The out parameters are only filled correctly when the function returns true.</returns>
        internal bool TryInspect_1_N_Relation(
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
            var savedRel = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);

            if (!PreMigration(RelationMigrationEventType.ChangeHasPositionStorage, savedRel, rel))
                return;

            string assocName = rel.GetAssociationName();
            Log.InfoFormat("Create N:M Relation {1} PositionStorage: {0}", assocName, role);

            var tblName = db.GetTableName(rel.Module.SchemaName, rel.GetRelationTableName());
            var fkName = Construct.ForeignKeyColumnName(rel.GetEndFromRole(role));

            db.CreateColumn(tblName, fkName + Zetbox.API.Helper.PositionSuffix, System.Data.DbType.Int32, 0, 0, true);

            PostMigration(RelationMigrationEventType.ChangeHasPositionStorage, savedRel, rel);
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
            var savedRel = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);

            if (!PreMigration(RelationMigrationEventType.ChangeHasPositionStorage, savedRel, rel))
                return;

            string assocName = rel.GetAssociationName();
            Log.InfoFormat("Drop N:M Relation {1} PositionStorage: {0}", assocName, role);

            var tblName = db.GetTableName(rel.Module.SchemaName, rel.GetRelationTableName());
            var fkName = Construct.ForeignKeyColumnName(rel.GetEndFromRole(role));

            if (db.CheckColumnExists(tblName, fkName + Zetbox.API.Helper.PositionSuffix))
                db.DropColumn(tblName, fkName + Zetbox.API.Helper.PositionSuffix);

            PostMigration(RelationMigrationEventType.ChangeHasPositionStorage, savedRel, rel);
        }
        #endregion

        #region Delete_N_M_Relation
        public bool IsDelete_N_M_Relation(Relation savedRel)
        {
            return schema.FindPersistenceObject<Relation>(savedRel.ExportGuid) == null;
        }
        public void DoDelete_N_M_Relation(Relation savedRel)
        {
            if (!PreMigration(RelationMigrationEventType.Delete, savedRel, null))
                return;

            string assocName = savedRel.GetAssociationName();
            Log.InfoFormat("Deleting N:M Relation: {0}", assocName);

            var tblName = db.GetTableName(savedRel.Module.SchemaName, savedRel.GetRelationTableName());

            if (db.CheckFKConstraintExists(tblName, savedRel.GetRelationAssociationName(RelationEndRole.A)))
                db.DropFKConstraint(tblName, savedRel.GetRelationAssociationName(RelationEndRole.A));
            if (db.CheckFKConstraintExists(tblName, savedRel.GetRelationAssociationName(RelationEndRole.B)))
                db.DropFKConstraint(tblName, savedRel.GetRelationAssociationName(RelationEndRole.B));

            if (db.CheckTableExists(tblName))
                db.DropTable(tblName);

            PostMigration(RelationMigrationEventType.Delete, savedRel, null);
        }
        #endregion

        #region New_N_M_Relation
        public bool IsNew_N_M_Relation(Relation rel)
        {
            return savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid) == null;
        }
        public void DoNew_N_M_Relation(Relation rel)
        {
            if (!PreMigration(RelationMigrationEventType.Add, null, rel))
                return;

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

            if (schema.GetQuery<RoleMembership>().Where(rm => rm.Relations.Contains(rel)).Count() > 0)
            {
                // Relation is in a ACL selector
                DoCreateUpdateRightsTrigger(rel);
            }

            PostMigration(RelationMigrationEventType.Add, null, rel);
        }

        private bool TryInspect_N_M_Relation(Relation rel, out string assocName, out TableRef tblName, out string fkAName, out string fkBName, out ObjectClass aType, out ObjectClass bType)
        {
            assocName = rel.GetAssociationName();

            tblName = db.GetTableName(rel.Module.SchemaName, rel.GetRelationTableName());
            fkAName = Construct.ForeignKeyColumnName(rel.A);
            fkBName = Construct.ForeignKeyColumnName(rel.B);
            aType = rel.A.Type;
            bType = rel.B.Type;

            return true;
        }
        #endregion

        #region Delete_1_1_Relation
        public bool IsDelete_1_1_Relation(Relation savedRel)
        {
            return schema.FindPersistenceObject<Relation>(savedRel.ExportGuid) == null;
        }
        public void DoDelete_1_1_Relation(Relation savedRel)
        {
            if (!PreMigration(RelationMigrationEventType.Delete, savedRel, null))
                return;

            Log.InfoFormat("Deleting 1:1 Relation: {0}", savedRel.GetAssociationName());

            if (savedRel.HasStorage(RelationEndRole.A))
            {
                Delete_1_1_Relation_DropColumns(savedRel, savedRel.A, savedRel.B, RelationEndRole.A);
            }
            // Difference to 1:N. 1:1 may have storage 'Replicate'
            if (savedRel.HasStorage(RelationEndRole.B))
            {
                Delete_1_1_Relation_DropColumns(savedRel, savedRel.B, savedRel.A, RelationEndRole.B);
            }

            PostMigration(RelationMigrationEventType.Delete, savedRel, null);
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
            if (!PreMigration(RelationMigrationEventType.Add, null, rel))
                return;

            Log.InfoFormat("New 1:1 Relation: {0}", rel.GetAssociationName());

            if (rel.Storage == StorageType.MergeIntoA || rel.Storage == StorageType.Replicate)
            {
                New_1_1_Relation_CreateColumns(rel, rel.A, rel.B, RelationEndRole.A);
            }

            if (rel.Storage == StorageType.MergeIntoB || rel.Storage == StorageType.Replicate)
            {
                New_1_1_Relation_CreateColumns(rel, rel.B, rel.A, RelationEndRole.B);
            }

            PostMigration(RelationMigrationEventType.Add, null, rel);
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

        internal bool TryInspect_1_1_Relation(Relation rel, RelationEnd relEnd, RelationEnd otherEnd, RelationEndRole role, out TableRef tblName, out TableRef refTblName, out string assocName, out string colName, out string idxName)
        {
            tblName = relEnd.Type.GetTableRef(db);
            refTblName = otherEnd.Type.GetTableRef(db);
            assocName = rel.GetRelationAssociationName(role);
            colName = Construct.ForeignKeyColumnName(otherEnd);
            idxName = Construct.IndexName(tblName.Name, colName);

            return true;
        }

        /// <summary>
        /// Creates a fk column "colName" on table "tblName", pointing to "otherEnd".
        /// </summary>
        private void CreateFKColumn(RelationEnd otherEnd, TableRef tblName, string colName, bool forceTPH = false)
        {
            var relEnd = otherEnd.GetParent().GetOtherEnd(otherEnd);

            var isNullable = otherEnd.IsNullable();
            var checkNotNull = !isNullable;
            var createCheckConstraint = false;
            string errorMsg = null;
            if (checkNotNull && (forceTPH || (relEnd.Type.GetTableMapping() == TableMapping.TPH && relEnd.Type.BaseObjectClass != null)))
            {
                isNullable = true;
                createCheckConstraint = true;
            }
            else if (checkNotNull && db.CheckTableContainsData(tblName))
            {
                isNullable = true;
                errorMsg = "Unable to create NOT NULL column ({1}), since table ({0}) contains data. Created nullable column instead";
            }

            db.CreateColumn(tblName, colName, System.Data.DbType.Int32, 0, 0, isNullable);
            if (createCheckConstraint)
            {
                CreateTPHNotNullCheckConstraint(tblName, colName, relEnd.Type);
            }

            if (errorMsg != null)
            {
                Log.ErrorFormat(errorMsg, tblName, colName);
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

            var savedRel = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);

            if (!PreMigration(RelationMigrationEventType.ChangeStorage, savedRel, rel))
                return;

            Log.InfoFormat("Changing 1:1 Relation Storage: {0}", rel.GetAssociationName());

            if (savedRel.Storage == StorageType.Replicate)
            {
                // To MergeIntoA or MergeIntoB
                if (rel.HasStorage(RelationEndRole.B))
                {
                    Delete_1_1_Relation_DropColumns(savedRel, savedRel.A, savedRel.B, RelationEndRole.A);
                }
                else if (rel.HasStorage(RelationEndRole.A))
                {
                    Delete_1_1_Relation_DropColumns(savedRel, savedRel.B, savedRel.A, RelationEndRole.B);
                }
            }
            else
            {
                RelationEnd relEnd;
                RelationEnd otherEnd;
                RelationEndRole role;
                RelationEndRole otherRole;
                if (savedRel.Storage == StorageType.MergeIntoA)
                {
                    // To MergeIntoB or Replicate
                    relEnd = rel.B;
                    otherEnd = rel.A;
                    role = RelationEndRole.B;
                    otherRole = RelationEndRole.A;

                }
                else if (savedRel.Storage == StorageType.MergeIntoB)
                {
                    // To MergeIntoA or Replicate
                    relEnd = rel.A;
                    otherEnd = rel.B;
                    role = RelationEndRole.A;
                    otherRole = RelationEndRole.B;
                }
                else
                {
                    throw new InvalidOperationException("Unexpected saved stroage type " + savedRel.Storage.ToString());
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

            PostMigration(RelationMigrationEventType.ChangeStorage, savedRel, rel);
        }
        #endregion

        #region 1_1_RelationChange_FromNullable_To_NotNullable
        public bool Is_1_1_RelationChange_FromNullable_To_NotNullable(Relation rel, RelationEndRole role)
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
        public void Do_1_1_RelationChange_FromNullable_To_NotNullable(Relation rel, RelationEndRole role)
        {
            var savedRel = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);

            if (!PreMigration(RelationMigrationEventType.ChangeToNotNullable, savedRel, rel))
                return;

            RelationEnd relEnd = rel.GetEndFromRole(role);
            RelationEnd otherEnd = rel.GetOtherEndFromRole(role);

            var tblName = relEnd.Type.GetTableRef(db);
            var colName = Construct.ForeignKeyColumnName(otherEnd);

            db.AlterColumn(tblName, colName, System.Data.DbType.Int32, 0, 0, otherEnd.IsNullable(), null);

            PostMigration(RelationMigrationEventType.ChangeToNotNullable, savedRel, rel);
        }
        #endregion

        #region 1_1_RelationChange_FromNotNullable_To_Nullable
        public bool Is_1_1_RelationChange_FromNotNullable_To_Nullable(Relation rel, RelationEndRole role)
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
        public void Do_1_1_RelationChange_FromNotNullable_To_Nullable(Relation rel, RelationEndRole role)
        {
            var savedRel = savedSchema.FindPersistenceObject<Relation>(rel.ExportGuid);

            if (!PreMigration(RelationMigrationEventType.ChangeToNullable, savedRel, rel))
                return;

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

            PostMigration(RelationMigrationEventType.ChangeToNullable, savedRel, rel);
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
            var savedObjClass = savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid);

            if (!PreMigration(ClassMigrationEventType.ChangeBase, savedObjClass, objClass))
                return;

            var assocName = Construct.InheritanceAssociationName(objClass.BaseObjectClass, objClass);
            var tblName = objClass.GetTableRef(db);

            Log.InfoFormat("New ObjectClass Inheritance: {0} -> {1}: {2}", objClass.Name, objClass.BaseObjectClass.Name, assocName);

            if (db.CheckTableContainsData(tblName))
            {
                Log.ErrorFormat("Table '{0}' contains data. Unable to add inheritence.", tblName);
                return;
            }

            db.CreateFKConstraint(tblName, objClass.BaseObjectClass.GetTableRef(db), "ID", assocName, false);

            PostMigration(ClassMigrationEventType.ChangeBase, savedObjClass, objClass);
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
            var savedObjClass = savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid);

            if (!PreMigration(ClassMigrationEventType.ChangeBase, savedObjClass, objClass))
                return;

            Log.InfoFormat("Changing ObjectClass Inheritance: {0} -> {1}", objClass.Name, objClass.BaseObjectClass.Name);
            DoRemoveObjectClassInheritance(objClass);
            DoNewObjectClassInheritance(objClass);

            PostMigration(ClassMigrationEventType.ChangeBase, savedObjClass, objClass);
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
            var savedObjClass = savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid);

            if (!PreMigration(ClassMigrationEventType.ChangeBase, savedObjClass, objClass))
                return;

            string assocName = Construct.InheritanceAssociationName(savedObjClass.BaseObjectClass, savedObjClass);
            var tblName = objClass.GetTableRef(db);

            Log.InfoFormat("Remove ObjectClass Inheritance: {0} -> {1}: {2}", savedObjClass.Name, savedObjClass.BaseObjectClass.Name, assocName);

            if (db.CheckFKConstraintExists(tblName, assocName))
                db.DropFKConstraint(tblName, assocName);

            PostMigration(ClassMigrationEventType.ChangeBase, savedObjClass, objClass);
        }
        #endregion

        #region ChangeTptToTph
        public bool IsChangeTptToTph(ObjectClass objClass)
        {
            ObjectClass savedObjClass = savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid);
            if (savedObjClass == null) return false;

            return savedObjClass.GetTableMapping() == TableMapping.TPT && objClass.GetTableMapping() == TableMapping.TPH;
        }

        /// <summary>
        /// Changes table layout from table-per-type to table-per-hierarchy. This operates only on the saved schema to avoid doing work of other cases, specifically the Do(New/Change)*Property cases.
        /// </summary>
        /// <remarks>
        /// After this has run the database should look as if the saved schema already was TPH, but nothing else had changed.
        /// </remarks>
        public void DoChangeTptToTph(ObjectClass objClass)
        {
            var savedObjClass = savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid);

            if (!PreMigration(ClassMigrationEventType.ChangeMapping, savedObjClass, objClass))
                return;

            var baseTblName = db.GetTableName(savedObjClass.GetRootClass().Module.SchemaName, savedObjClass.GetRootClass().TableName);

            if (savedObjClass.BaseObjectClass == null)
            {
                // create and initialize discriminator
                db.CreateColumn(
                    baseTblName, TableMapper.DiscriminatorColumnName,
                    System.Data.DbType.String, TableMapper.DiscriminatorColumnSize, 0,
                    true, null);
                db.WriteDefaultValue(baseTblName, TableMapper.DiscriminatorColumnName, Construct.DiscriminatorValue(savedObjClass));
                db.AlterColumn(
                    baseTblName, TableMapper.DiscriminatorColumnName,
                    System.Data.DbType.String, TableMapper.DiscriminatorColumnSize, 0,
                    false, null);
            }
            else
            {
                var colNamesList = new List<string>();

                #region create new columns in base table

                #region Value and Compound Properties

                foreach (ValueTypeProperty savedProp in savedObjClass.Properties.OfType<ValueTypeProperty>().Where(p => !p.IsList))
                {
                    CreateValueTypePropertyNullable(baseTblName, savedProp, Construct.NestedColumnName(savedProp.Name, savedObjClass.TableName), false);
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
                    string fkName = Construct.ForeignKeyColumnName(savedProp);
                    string assocName = savedProp.GetAssociationName();

                    db.DropFKConstraint(tblName, assocName);
                    // TPH downside: constraint doesn't restrict to subclass any more
                    db.CreateFKConstraint(tblName, baseTblName, fkName, assocName, true);
                }
                foreach (CompoundObjectProperty savedProp in savedObjClass.Properties.OfType<CompoundObjectProperty>().Where(p => p.IsList))
                {
                    // relink fk column on tblName
                    var tblName = db.GetTableName(savedProp.Module.SchemaName, savedProp.GetCollectionEntryTable());
                    string fkName = Construct.ForeignKeyColumnName(savedProp);
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
                                RelationEnd savedRelEnd, savedOtherEnd;
                                TableRef tblName, refTblName;
                                bool hasPersistentOrder;
                                if (TryInspect_1_N_Relation(savedRel, out assocName, out savedRelEnd, out savedOtherEnd, out tblName, out refTblName, out colName, out hasPersistentOrder, out listPosName))
                                {
                                    // Only do this when we are N-side
                                    if (savedRelEnd.Type == savedObjClass)
                                    {
                                        CreateFKColumn(savedOtherEnd, baseTblName, Construct.NestedColumnName(colName, savedObjClass.TableName), true);
                                        colNamesList.Add(colName);
                                        if (hasPersistentOrder)
                                        {
                                            db.CreateColumn(baseTblName, Construct.NestedColumnName(listPosName, savedObjClass.TableName), System.Data.DbType.Int32, 0, 0, true);
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
                                    CreateFKColumn(savedRel.B, baseTblName, Construct.NestedColumnName(colName, savedObjClass.TableName), true);
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
                                    CreateFKColumn(savedRel.A, baseTblName, Construct.NestedColumnName(colName, savedObjClass.TableName), true);
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

                var srcTblName = db.GetTableName(savedObjClass.Module.SchemaName, savedObjClass.TableName);
                var srcColNames = colNamesList.ToArray();
                var dstColNames = colNamesList.Select(n => Construct.NestedColumnName(n, savedObjClass.TableName)).ToArray();
                db.CopyColumnData(srcTblName, srcColNames, baseTblName, dstColNames, Construct.DiscriminatorValue(savedObjClass));

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
                                    colName = Construct.NestedColumnName(colName, savedObjClass.TableName);
                                    if (db.CheckFKConstraintExists(tblName, assocName)) db.DropFKConstraint(tblName, assocName);
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
                                    colName = Construct.NestedColumnName(colName, savedObjClass.TableName);
                                    if (db.CheckFKConstraintExists(tblName, assocName)) db.DropFKConstraint(tblName, assocName);
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
                                    colName = Construct.NestedColumnName(colName, savedObjClass.TableName);
                                    if (db.CheckFKConstraintExists(tblName, assocName)) db.DropFKConstraint(tblName, assocName);
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

                var oldTblName = savedObjClass.GetTableRef(db);
                var unknownColumns = db.GetTableColumnNames(oldTblName).Except(colNamesList).ToList();
                unknownColumns.Remove("ID");

                if (unknownColumns.Count > 0)
                {
                    Log.ErrorFormat("Keeping old table {0} while migrating to TPH: Unknown Columns detected: {1}", oldTblName, string.Join(", ", unknownColumns));
                }
                else
                {
                    // Break children's references, they'll go away soon
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

                #endregion
            }

            // process all children
            foreach (var child in savedObjClass.SubClasses)
            {
                DoChangeTptToTph(child);
            }

            // "fix" saved schema, so other cases accessing the "old" schema see the already transformed base table
            savedObjClass.TableMapping = TableMapping.TPH;

            PostMigration(ClassMigrationEventType.ChangeMapping, savedObjClass, objClass);
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
            var savedObjClass = savedSchema.FindPersistenceObject<ObjectClass>(objClass.ExportGuid);

            if (!PreMigration(ClassMigrationEventType.ChangeMapping, savedObjClass, objClass))
                return;

            // create new derived tables
            // copy data from base table to derived tables
            // drop old columns for derived classes in base table

            PostMigration(ClassMigrationEventType.ChangeMapping, savedObjClass, objClass);
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
        public bool IsDeleteValueTypeProperty(ValueTypeProperty savedProp)
        {
            return !savedProp.IsList && schema.FindPersistenceObject<ValueTypeProperty>(savedProp.ExportGuid) == null;
        }

        public void DoDeleteValueTypeProperty(ObjectClass objClass, ValueTypeProperty savedProp, string prefix)
        {
            if (!PreMigration(PropertyMigrationEventType.Delete, savedProp, null))
                return;

            var tblName = objClass.GetTableRef(db);
            var colName = Construct.ColumnName(savedProp, prefix);
            Log.InfoFormat("Drop Column: {0}.{1}", tblName, colName);
            if (db.CheckColumnExists(tblName, colName))
                db.DropColumn(tblName, colName);

            PostMigration(PropertyMigrationEventType.Delete, savedProp, null);
        }
        #endregion

        #region NewCompoundObjectProperty
        public bool IsNewCompoundObjectProperty(CompoundObjectProperty prop)
        {
            return savedSchema.FindPersistenceObject<CompoundObjectProperty>(prop.ExportGuid) == null;
        }
        public void DoNewCompoundObjectProperty(ObjectClass objClass, CompoundObjectProperty cprop, string prefix)
        {
            if (!PreMigration(PropertyMigrationEventType.Add, null, cprop))
                return;

            var tblName = objClass.GetTableRef(db);
            CreateCompoundObjectProperty(tblName, cprop, prefix, true);

            PostMigration(PropertyMigrationEventType.Add, null, cprop);
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
                CheckValueTypePropertyHasWarnings(valProp);
                db.CreateColumn(
                    tblName,
                    colName,
                    valProp.GetDbType(),
                    valProp.GetSize(),
                    valProp.GetScale(),
                    hasData || valProp.IsNullable(),
                    null); // CP-Objects does not have a default value. could be nullable or deep in a TPH hierarchy
            }

            // TODO: Add nested CompoundObjectProperty
        }
        #endregion

        #region DeleteCompoundObjectProperty
        public bool IsDeleteCompoundObjectProperty(CompoundObjectProperty savedCProp)
        {
            return !savedCProp.IsList && schema.FindPersistenceObject<CompoundObjectProperty>(savedCProp.ExportGuid) == null;
        }

        public void DoDeleteCompoundObjectProperty(ObjectClass objClass, CompoundObjectProperty savedCProp, string prefix)
        {
            if (!PreMigration(PropertyMigrationEventType.Delete, savedCProp, null))
                return;

            Log.InfoFormat("deleting CompoundObject Property: '{0}'", savedCProp.Name);

            var tblName = objClass.GetTableRef(db);
            string baseColName = Construct.ColumnName(savedCProp, prefix);

            foreach (var valProp in savedCProp.CompoundObjectDefinition.Properties.OfType<ValueTypeProperty>())
            {
                var colName = Construct.ColumnName(valProp, baseColName);
                Log.InfoFormat("  deleting ValueType Property: '{0}' ('{1}')", valProp.Name, colName);
                db.DropColumn(tblName, colName);
            }

            // TODO: Add nested CompoundObjectProperty

            PostMigration(PropertyMigrationEventType.Delete, savedCProp, null);
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

        #region Migration Infrastructure

        private bool PreMigration(ClassMigrationEventType classMigrationEventType, ObjectClass savedObjClass, ObjectClass objClass)
        {
            IClassMigrationFragment result;
            if (_classMigrationFragments.TryGetValue(new Tuple<ClassMigrationEventType, Guid>(classMigrationEventType, (savedObjClass ?? objClass).ExportGuid), out result))
            {
                return result.PreMigration(db, savedObjClass, objClass);
            }
            else
            {
                return true;
            }
        }

        private void PostMigration(ClassMigrationEventType classMigrationEventType, ObjectClass savedObjClass, ObjectClass objClass)
        {
            IClassMigrationFragment result;
            if (_classMigrationFragments.TryGetValue(new Tuple<ClassMigrationEventType, Guid>(classMigrationEventType, (savedObjClass ?? objClass).ExportGuid), out result))
            {
                result.PostMigration(db, savedObjClass, objClass);
            }
        }

        private bool PreMigration(PropertyMigrationEventType propertyMigrationEventType, Property savedObjProperty, Property objProperty)
        {
            IPropertyMigrationFragment result;
            if (_propertyMigrationFragments.TryGetValue(new Tuple<PropertyMigrationEventType, Guid>(propertyMigrationEventType, (savedObjProperty ?? objProperty).ExportGuid), out result))
            {
                return result.PreMigration(db, savedObjProperty, objProperty);
            }
            else
            {
                return true;
            }
        }

        private void PostMigration(PropertyMigrationEventType propertyMigrationEventType, Property savedObjProperty, Property objProperty)
        {
            IPropertyMigrationFragment result;
            if (_propertyMigrationFragments.TryGetValue(new Tuple<PropertyMigrationEventType, Guid>(propertyMigrationEventType, (savedObjProperty ?? objProperty).ExportGuid), out result))
            {
                result.PostMigration(db, savedObjProperty, objProperty);
            }
        }

        private bool PreMigration(RelationMigrationEventType relationMigrationEventType, Relation savedObjRelation, Relation objRelation)
        {
            IRelationMigrationFragment result;
            if (_relationMigrationFragments.TryGetValue(new Tuple<RelationMigrationEventType, Guid>(relationMigrationEventType, (savedObjRelation ?? objRelation).ExportGuid), out result))
            {
                return result.PreMigration(db, savedObjRelation, objRelation);
            }
            else
            {
                return true;
            }
        }

        private void PostMigration(RelationMigrationEventType relationMigrationEventType, Relation savedObjRelation, Relation objRelation)
        {
            IRelationMigrationFragment result;
            if (_relationMigrationFragments.TryGetValue(new Tuple<RelationMigrationEventType, Guid>(relationMigrationEventType, (savedObjRelation ?? objRelation).ExportGuid), out result))
            {
                result.PostMigration(db, savedObjRelation, objRelation);
            }
        }
        #endregion
    }
}
