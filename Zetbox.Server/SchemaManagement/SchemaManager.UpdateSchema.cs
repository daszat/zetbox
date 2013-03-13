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
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.SchemaManagement;
    using Zetbox.API.Server;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator;
    using Zetbox.Generator.Extensions;

    public partial class SchemaManager
    {
        public void UpdateSchema()
        {
            using (Log.DebugTraceMethodCall("UpdateSchema"))
            {
                WriteReportHeader("Update Schema Report");

                db.BeginTransaction();
                try
                {
                    db.EnsureInfrastructure();

                    foreach (var gmf in _globalMigrationFragments)
                    {
                        gmf.PreMigration(db);
                    }

                    DropVolatileObjects();

                    UpdateDatabaseSchemas();
                    UpdateTables();
                    UpdateRelations();
                    UpdateInheritance();
                    UpdateSecurityTables();
                    UpdateConstraints();

                    UpdateDeletedRelations();
                    UpdateDeletedTables();

                    UpdateProcedures();

                    CreateFinalCheckConstraints();
                    CreateFinalRightsInfrastructure();

                    SaveSchema(schema);

                    foreach (var gmf in _globalMigrationFragments)
                    {
                        gmf.PostMigration(db);
                    }

                    db.CommitTransaction();
                    db.RefreshDbStats();
                }
                catch (Exception ex)
                {
                    db.RollbackTransaction();
                    Log.Debug(String.Empty);
                    Log.Error("An error ocurred while updating the schema", ex);
                    throw;
                }
            }
        }

        private void CreateFinalRightsInfrastructure()
        {
            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().OrderBy(o => o.Module.Namespace).ThenBy(o => o.Name))
            {
                if (!objClass.NeedsRightsTable()) continue;

                var tblRightsName = db.GetTableName(objClass.Module.SchemaName, Construct.SecurityRulesTableName(objClass));
                var tblName = objClass.GetTableRef(db);
                var rightsViewUnmaterializedName = db.GetTableName(objClass.Module.SchemaName, Construct.SecurityRulesRightsViewUnmaterializedName(objClass));
                var refreshRightsOnProcedureName = db.GetProcedureName(objClass.Module.SchemaName, Construct.SecurityRulesRefreshRightsOnProcedureName(objClass));

                if (db.CheckViewExists(rightsViewUnmaterializedName)) continue;

                Log.InfoFormat("New ObjectClass Security Rules: {0}", objClass.Name);

                Case.DoCreateOrReplaceUpdateRightsTrigger(objClass);
                Case.DoCreateRightsViewUnmaterialized(objClass);
                if (!db.CheckProcedureExists(refreshRightsOnProcedureName))
                    db.CreateRefreshRightsOnProcedure(refreshRightsOnProcedureName, rightsViewUnmaterializedName, tblName, tblRightsName);
                // Either the procedure has to be called, then it was done by the respective case
                // or, we just removed it to keep the dependency away, then nothing has changed.
                //db.ExecRefreshRightsOnProcedure(refreshRightsOnProcedureName);
            }
        }

        private void CreateFinalCheckConstraints()
        {
            // this does more than required (dropping already existing), but good enough for now.
            // has to be revisited if someone else creates check constraints.
            Workaround_UpdateTPHNotNullCheckConstraint();
        }

        private void DropVolatileObjects()
        {
            // .ToList() -> There is already an open DataReader associated with this Command which must be closed first
            foreach (var triggerName in db.GetTriggerNames().ToList())
            {
                db.DropTrigger(triggerName);
            }

            foreach (var viewName in db.GetViewNames().ToList())
            {
                db.DropView(viewName);
            }

            // only drop refreshrights procedures
            foreach (var procName in db.GetProcedureNames().ToList().Where(procRef => procRef.Name.StartsWith(Construct.SecurityRulesRefreshRightsOnProcedurePrefix())))
            {
                db.DropProcedure(procName);
            }
        }

        private void Workaround_UpdateTPHNotNullCheckConstraint()
        {
            foreach (var prop in schema.GetQuery<ValueTypeProperty>())
            {
                var cls = prop.ObjectClass as ObjectClass;
                if (!prop.IsNullable() && cls != null && cls.BaseObjectClass != null && cls.GetTableMapping() == TableMapping.TPH)
                {
                    var tblName = cls.GetTableRef(db);
                    var colName = Construct.ColumnName(prop, string.Empty);
                    var checkConstraintName = Construct.CheckConstraintName(tblName.Name, colName);

                    if (db.CheckCheckConstraintExists(tblName, checkConstraintName))
                    {
                        db.DropCheckConstraint(tblName, checkConstraintName);
                    }

                    Case.CreateTPHNotNullCheckConstraint(tblName, colName, cls);
                }
            }

            foreach (var rel in schema.GetQuery<Relation>())
            {
                if (rel.GetRelationType() == RelationType.one_n)
                {
                    string assocName, colName, listPosName;
                    RelationEnd relEnd, otherEnd;
                    TableRef tblName, refTblName;
                    bool hasPersistentOrder;
                    if (Case.TryInspect_1_N_Relation(rel, out assocName, out relEnd, out otherEnd, out tblName, out refTblName, out colName, out hasPersistentOrder, out listPosName))
                    {
                        var isNullable = otherEnd.IsNullable();
                        var checkNotNull = !isNullable;
                        if (checkNotNull && (relEnd.Type.GetTableMapping() == TableMapping.TPH && relEnd.Type.BaseObjectClass != null))
                        {
                            var checkConstraintName = Construct.CheckConstraintName(tblName.Name, colName);
                            if (db.CheckCheckConstraintExists(tblName, checkConstraintName))
                            {
                                db.DropCheckConstraint(tblName, checkConstraintName);
                            }
                            Case.CreateTPHNotNullCheckConstraint(tblName, colName, relEnd.Type);
                        }
                    }
                }
                else if (rel.GetRelationType() == RelationType.one_one)
                {
                    // TODO: ....
                }
            }
        }

        private void UpdateDatabaseSchemas()
        {
            Log.Info("Updating database schemas");
            Log.Debug("-------------------------");

            // Create/Check dbo shema
            if (!db.CheckSchemaExists("dbo"))
            {
                db.CreateSchema("dbo");
            }

            foreach (var moduleName in schema.GetQuery<Module>().Select(m => m.SchemaName))
            {
                if (!Case.IsNewSchema(moduleName))
                {
                    Case.DoNewSchema(moduleName);
                }
            }
        }

        private void UpdateConstraints()
        {
            Log.Info("Updating Constraints");
            Log.Debug("--------------------");

            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().OrderBy(o => o.Module.Namespace).ThenBy(o => o.Name))
            {
                Log.DebugFormat("Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.Name);

                UpdateIndexConstraints(objClass);
                UpdateDeletedIndexConstraints(objClass);
            }
            Log.Debug(String.Empty);
        }

        private void UpdateIndexConstraints(ObjectClass objClass)
        {
            foreach (var uc in objClass.Constraints.OfType<IndexConstraint>())
            {
                if (Case.IsNewIndexConstraint(uc))
                {
                    Case.DoNewIndexConstraint(uc);
                }
                else if (Case.IsChangeIndexConstraint(uc))
                {
                    Case.DoChangeIndexConstraint(uc);
                }
            }
        }

        private void UpdateDeletedIndexConstraints(ObjectClass objClass)
        {
            foreach (IndexConstraint uc in Case.savedSchema.GetQuery<IndexConstraint>().Where(p => p.Constrained.ExportGuid == objClass.ExportGuid))
            {
                if (Case.IsDeleteIndexConstraint(uc))
                {
                    Case.DoDeleteIndexConstraint(uc);
                }
            }
        }

        private void UpdateProcedures()
        {
            Log.Info("Updating Procedures");
            Log.Debug("-------------------");

            var refSpecsASide = schema.GetQuery<RelationEnd>()
                .Where(relEnd =>
                    relEnd.Multiplicity == Multiplicity.ZeroOrMore
                    && relEnd.HasPersistentOrder
                    && relEnd.AParent != null
                    && (relEnd.AParent.B.Multiplicity == Multiplicity.One
                        || relEnd.AParent.B.Multiplicity == Multiplicity.ZeroOrOne))
                .Select(relEnd => new
                {
                    OtherEnd = relEnd.AParent.B,
                    schemaName = relEnd.Type.Module.SchemaName,
                    tblName = relEnd.Type.TableName,
                    refSchemaName = relEnd.AParent.B.Type.Module.SchemaName,
                    refTableName = relEnd.AParent.B.Type.TableName,
                })
                .ToList();

            var refSpecsBSide = schema.GetQuery<RelationEnd>()
                .Where(relEnd =>
                    relEnd.Multiplicity == Multiplicity.ZeroOrMore
                    && relEnd.HasPersistentOrder
                    && relEnd.BParent != null
                    && (relEnd.BParent.A.Multiplicity == Multiplicity.One
                        || relEnd.BParent.A.Multiplicity == Multiplicity.ZeroOrOne))
                .Select(relEnd => new
                {
                    OtherEnd = relEnd.BParent.A,
                    schemaName = relEnd.Type.Module.SchemaName,
                    tblName = relEnd.Type.TableName,
                    refSchemaName = relEnd.BParent.A.Type.Module.SchemaName,
                    refTableName = relEnd.BParent.A.Type.TableName,
                })
                .ToList();

            var refSpecs = refSpecsASide.Concat(refSpecsBSide)
                .ToLookup(
                    refSpec => db.GetTableName(refSpec.schemaName, refSpec.tblName),
                    refSpec => new KeyValuePair<TableRef, string>(db.GetTableName(refSpec.refSchemaName, refSpec.refTableName), Construct.ForeignKeyColumnName(refSpec.OtherEnd)));

            db.CreatePositionColumnValidCheckProcedures(refSpecs);

            var getSequenceNumberRef = db.GetProcedureName("dbo", "GetSequenceNumber");
            if (!db.CheckProcedureExists(getSequenceNumberRef))
            {
                db.CreateSequenceNumberProcedure();
            }
            else if (repair)
            {
                db.DropProcedure(getSequenceNumberRef);
                db.CreateSequenceNumberProcedure();
            }

            var getContinuousSequenceNumberRef = db.GetProcedureName("dbo", "GetContinuousSequenceNumber");
            if (!db.CheckProcedureExists(getContinuousSequenceNumberRef))
            {
                db.CreateContinuousSequenceNumberProcedure();
            }
            else if (repair)
            {
                db.DropProcedure(getContinuousSequenceNumberRef);
                db.CreateContinuousSequenceNumberProcedure();
            }
        }

        private void UpdateDeletedTables()
        {
            Log.Info("Updating deleted Tables");
            Log.Debug("-----------------------");

            foreach (ObjectClass objClass in Case.savedSchema.GetQuery<ObjectClass>().OrderBy(o => o.Module.Namespace).ThenBy(o => o.Name))
            {
                Log.DebugFormat("Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.Name);
                if (Case.IsDeleteObjectClass(objClass))
                {
                    Case.DoDeleteObjectClass(objClass);
                }
            }
            Log.Debug(String.Empty);
        }

        private void UpdateSecurityTables()
        {
            Log.Info("Updating Security Tables");
            Log.Debug("-------------------------");

            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().OrderBy(o => o.Module.Namespace).ThenBy(o => o.Name))
            {
                if (Case.IsNewObjectClassACL(objClass))
                {
                    Case.DoNewObjectClassACL(objClass);
                }
                if (Case.IsChangeObjectClassACL(objClass))
                {
                    Case.DoChangeObjectClassACL(objClass);
                }
                if (Case.IsDeleteObjectClassSecurityRules(objClass))
                {
                    Case.DoDeleteObjectClassSecurityRules(objClass);
                }
            }

            var allACLTables = schema.GetQuery<ObjectClass>()
                .ToList()
                .Where(o => o.NeedsRightsTable())
                .OrderBy(o => o.Module.Namespace)
                .ThenBy(o => o.Name)
                .ToList();
            Case.DoCreateRefreshAllRightsProcedure(allACLTables);
        }

        private void UpdateTables()
        {
            Log.Info("Updating Tables & Columns");
            Log.Debug("-------------------------");

            // The following actions have to be sequenced separately to avoid stepping on each other.

            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().OrderBy(o => o.Module.Namespace).ThenBy(o => o.Name))
            {
                Log.DebugFormat("Deleting Columns in Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.Name);

                // Delete early to avoid collisions with newly created columns (like changing data type)
                // Note: migration of data types is not supported now. Only chance is to delete and recreate a column
                UpdateDeletedColumns(objClass, String.Empty);
            }

            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().Where(o => o.BaseObjectClass == null).OrderBy(o => o.Module.Namespace).ThenBy(o => o.Name))
            {
                Log.DebugFormat("TPH/TPT migrations for Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.Name);

                if (Case.IsChangeTphToTpt(objClass))
                {
                    Case.DoChangeTphToTpt(objClass);
                }
                if (Case.IsChangeTptToTph(objClass))
                {
                    Case.DoChangeTptToTph(objClass);
                }
            }

            var classes = schema.GetQuery<ObjectClass>()
                .Select(o => new { Class = o, Generation = o.AndParents(c => c.BaseObjectClass).Count() })
                .OrderBy(o => o.Generation)
                .ThenBy(o => o.Class.Module.Namespace)
                .ThenBy(o => o.Class.Name)
                .Select(o => o.Class)
                .ToList();

            foreach (ObjectClass objClass in classes)
            {
                if (Case.IsRenameObjectClassTable(objClass))
                {
                    Case.DoRenameObjectClassTable(objClass);
                }
            }

            foreach (ObjectClass objClass in classes)
            {
                Log.DebugFormat("Managing Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.Name);

                if (Case.IsNewObjectClass(objClass))
                {
                    Case.DoNewObjectClass(objClass);
                }
            }

            foreach (ObjectClass objClass in classes)
            {
                UpdateColumns(objClass, objClass.Properties, String.Empty);
            }

            Log.Debug(String.Empty);
        }

        private void UpdateColumns(ObjectClass objClass, ICollection<Property> properties, string prefix)
        {
            foreach (ValueTypeProperty prop in properties.OfType<ValueTypeProperty>().Where(p => !p.IsList))
            {
                if (Case.IsNewValueTypePropertyNullable(prop))
                {
                    Case.DoNewValueTypePropertyNullable(objClass, prop, prefix);
                }
                if (Case.IsNewValueTypePropertyNotNullable(prop))
                {
                    Case.DoNewValueTypePropertyNotNullable(objClass, prop, prefix);
                }
                if (Case.IsRenameValueTypePropertyName(objClass, prop, prefix))
                {
                    Case.DoRenameValueTypePropertyName(objClass, prop, prefix);
                }
                if (Case.IsMoveValueTypeProperty(prop))
                {
                    Case.DoMoveValueTypeProperty(objClass, prop, prefix);
                }
                if (Case.IsChangeValueTypeProperty_To_NotNullable(prop))
                {
                    Case.DoChangeValueTypeProperty_To_NotNullable(objClass, prop, prefix);
                }
                if (Case.IsChangeValueTypeProperty_To_Nullable(prop))
                {
                    Case.DoChangeValueTypeProperty_To_Nullable(objClass, prop, prefix);
                }
                if (Case.IsChangeDefaultValue(prop))
                {
                    Case.DoChangeDefaultValue(objClass, prop, prefix);
                }
            }

            foreach (CompoundObjectProperty sprop in properties.OfType<CompoundObjectProperty>().Where(p => !p.IsList))
            {
                if (Case.IsNewCompoundObjectProperty(sprop))
                {
                    Case.DoNewCompoundObjectProperty(objClass, sprop, prefix);
                }
                else
                {
                    // See if the CompoundObject self has changed
                    UpdateColumns(objClass, sprop.CompoundObjectDefinition.Properties, Construct.ColumnName(sprop, prefix));
                }
            }

            foreach (ValueTypeProperty prop in properties.OfType<ValueTypeProperty>().Where(p => p.IsList))
            {
                if (Case.IsNewValueTypePropertyList(prop))
                {
                    Case.DoNewValueTypePropertyList(objClass, prop);
                }
                if (Case.IsRenameValueTypePropertyListName(prop))
                {
                    Case.DoRenameValueTypePropertyListName(objClass, prop);
                }
                if (Case.IsMoveValueTypePropertyList(prop))
                {
                    Case.DoMoveValueTypePropertyList(objClass, prop);
                }
            }

            foreach (CompoundObjectProperty prop in properties.OfType<CompoundObjectProperty>().Where(p => p.IsList))
            {
                if (Case.IsNewCompoundObjectPropertyList(prop))
                {
                    Case.DoNewCompoundObjectPropertyList(objClass, prop);
                }
                if (Case.IsRenameCompoundObjectPropertyListName(prop))
                {
                    Case.DoRenameCompoundObjectPropertyListName(objClass, prop);
                }
                if (Case.IsMoveCompoundObjectPropertyList(prop))
                {
                    Case.DoMoveCompoundObjectPropertyList(objClass, prop);
                }
            }
        }

        private void UpdateDeletedColumns(ObjectClass objClass, string prefix)
        {
            foreach (ValueTypeProperty savedProp in Case.savedSchema.GetQuery<ValueTypeProperty>().Where(p => p.ObjectClass.ExportGuid == objClass.ExportGuid))
            {
                if (Case.IsDeleteValueTypeProperty(savedProp))
                {
                    Case.DoDeleteValueTypeProperty(objClass, savedProp, prefix);
                }
                if (Case.IsDeleteValueTypePropertyList(savedProp))
                {
                    Case.DoDeleteValueTypePropertyList(objClass, savedProp, prefix);
                }
            }

            foreach (CompoundObjectProperty savedCProp in Case.savedSchema.GetQuery<CompoundObjectProperty>().Where(p => p.ObjectClass.ExportGuid == objClass.ExportGuid))
            {
                if (Case.IsDeleteCompoundObjectProperty(savedCProp))
                {
                    Case.DoDeleteCompoundObjectProperty(objClass, savedCProp, prefix);
                }
                if (Case.IsDeleteCompoundObjectPropertyList(savedCProp))
                {
                    Case.DoDeleteCompoundObjectPropertyList(objClass, savedCProp, prefix);
                }
            }
        }

        private void UpdateDeletedRelations()
        {
            Log.Info("Updating deleted Relations");
            Log.Debug("--------------------------");

            foreach (Relation rel in Case.savedSchema.GetQuery<Relation>().OrderBy(r => r.Module.Namespace))
            {
                Log.DebugFormat("Relation: {0} ({1})", rel.GetAssociationName(), rel.GetRelationType());

                if (rel.GetRelationType() == RelationType.one_n)
                {
                    if (Case.IsDelete_1_N_Relation(rel))
                    {
                        Case.DoDelete_1_N_Relation(rel);
                    }
                }
                else if (rel.GetRelationType() == RelationType.n_m)
                {
                    if (Case.IsDelete_N_M_Relation(rel))
                    {
                        Case.DoDelete_N_M_Relation(rel);
                    }
                }
                else if (rel.GetRelationType() == RelationType.one_one)
                {
                    if (Case.IsDelete_1_1_Relation(rel))
                    {
                        Case.DoDelete_1_1_Relation(rel);
                    }
                }
            }
            Log.Debug(String.Empty);
        }

        private void UpdateRelations()
        {
            Log.Info("Updating Relations");
            Log.Debug("------------------");
            var relations = schema.GetQuery<Relation>().OrderBy(r => r.Module.Namespace);

            foreach (Relation rel in relations)
            {
                if (Case.IsChangeRelationName(rel))
                {
                    Case.DoChangeRelationName(rel);
                }
                if (Case.IsChangeRelationEndTypes(rel))
                {
                    Case.DoChangeRelationEndTypes(rel);
                }
            }

            foreach (Relation rel in relations)
            {
                if (Case.IsChangeRelationType(rel))
                {
                    if (Case.IsChangeRelationType_from_1_1_to_1_n(rel))
                    {
                        Case.DoChangeRelationType_from_1_1_to_1_n(rel);
                    }
                    else if (Case.IsChangeRelationType_from_1_1_to_n_m(rel))
                    {
                        Case.DoChangeRelationType_from_1_1_to_n_m(rel);
                    }
                    else if (Case.IsChangeRelationType_from_1_n_to_1_1(rel))
                    {
                        Case.DoChangeRelationType_from_1_n_to_1_1(rel);
                    }
                    else if (Case.IsChangeRelationType_from_1_n_to_n_m(rel))
                    {
                        Case.DoChangeRelationType_from_1_n_to_n_m(rel);
                    }
                    else if (Case.IsChangeRelationType_from_n_m_to_1_1(rel))
                    {
                        Case.DoChangeRelationType_from_n_m_to_1_1(rel);
                    }
                    else if (Case.IsChangeRelationType_from_n_m_to_1_n(rel))
                    {
                        Case.DoChangeRelationType_from_n_m_to_1_n(rel);
                    }
                }
            }
            foreach (Relation rel in relations)
            {
                if (rel.GetRelationType() == RelationType.one_n)
                {
                    if (Case.Is_1_N_RelationChange_FromIndexed_To_NotIndexed(rel))
                    {
                        Case.Do_1_N_RelationChange_FromIndexed_To_NotIndexed(rel);
                    }
                    if (Case.Is_1_N_RelationChange_FromNotIndexed_To_Indexed(rel))
                    {
                        Case.Do_1_N_RelationChange_FromNotIndexed_To_Indexed(rel);
                    }
                    if (Case.Is_1_N_RelationChange_FromNullable_To_NotNullable(rel))
                    {
                        Case.Do_1_N_RelationChange_FromNullable_To_NotNullable(rel);
                    }
                    if (Case.Is_1_N_RelationChange_FromNotNullable_To_Nullable(rel))
                    {
                        Case.Do_1_N_RelationChange_FromNotNullable_To_Nullable(rel);
                    }
                }
                else if (rel.GetRelationType() == RelationType.n_m)
                {
                    if (Case.Is_N_M_RelationChange_FromIndexed_To_NotIndexed(rel, RelationEndRole.A))
                    {
                        Case.Do_N_M_RelationChange_FromIndexed_To_NotIndexed(rel, RelationEndRole.A);
                    }
                    if (Case.Is_N_M_RelationChange_FromIndexed_To_NotIndexed(rel, RelationEndRole.B))
                    {
                        Case.Do_N_M_RelationChange_FromIndexed_To_NotIndexed(rel, RelationEndRole.B);
                    }
                    if (Case.Is_N_M_RelationChange_FromNotIndexed_To_Indexed(rel, RelationEndRole.A))
                    {
                        Case.Do_N_M_RelationChange_FromNotIndexed_To_Indexed(rel, RelationEndRole.A);
                    }
                    if (Case.Is_N_M_RelationChange_FromNotIndexed_To_Indexed(rel, RelationEndRole.B))
                    {
                        Case.Do_N_M_RelationChange_FromNotIndexed_To_Indexed(rel, RelationEndRole.B);
                    }
                }
                else if (rel.GetRelationType() == RelationType.one_one)
                {
                    if (Case.IsChange_1_1_Storage(rel))
                    {
                        Case.DoChange_1_1_Storage(rel);
                    }

                    if (Case.Is_1_1_RelationChange_FromNotNullable_To_Nullable(rel, RelationEndRole.A))
                    {
                        Case.Do_1_1_RelationChange_FromNotNullable_To_Nullable(rel, RelationEndRole.A);
                    }
                    if (Case.Is_1_1_RelationChange_FromNotNullable_To_Nullable(rel, RelationEndRole.B))
                    {
                        Case.Do_1_1_RelationChange_FromNotNullable_To_Nullable(rel, RelationEndRole.B);
                    }

                    if (Case.Is_1_1_RelationChange_FromNullable_To_NotNullable(rel, RelationEndRole.A))
                    {
                        Case.Do_1_1_RelationChange_FromNullable_To_NotNullable(rel, RelationEndRole.A);
                    }
                    if (Case.Is_1_1_RelationChange_FromNullable_To_NotNullable(rel, RelationEndRole.B))
                    {
                        Case.Do_1_1_RelationChange_FromNullable_To_NotNullable(rel, RelationEndRole.B);
                    }
                }
            }

            foreach (Relation rel in relations)
            {
                if (rel.GetRelationType() == RelationType.one_n)
                {
                    if (Case.IsNew_1_N_Relation(rel))
                    {
                        Case.DoNew_1_N_Relation(rel);
                    }
                }
                else if (rel.GetRelationType() == RelationType.n_m)
                {
                    if (Case.IsNew_N_M_Relation(rel))
                    {
                        Case.DoNew_N_M_Relation(rel);
                    }
                }
                else if (rel.GetRelationType() == RelationType.one_one)
                {
                    if (Case.IsNew_1_1_Relation(rel))
                    {
                        Case.DoNew_1_1_Relation(rel);
                    }
                }
            }
            Log.Debug(String.Empty);
        }

        private void UpdateInheritance()
        {
            Log.Info("Updating Inheritance");
            Log.Debug("--------------------");

            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().OrderBy(o => o.Module.Namespace).ThenBy(o => o.Name))
            {
                var mapping = objClass.GetTableMapping();
                Log.DebugFormat("Objectclass: {0}.{1}, {2}", objClass.Module.Namespace, objClass.Name, mapping);
                if (mapping == TableMapping.TPT)
                {
                    if (Case.IsNewObjectClassInheritance(objClass))
                    {
                        Case.DoNewObjectClassInheritance(objClass);
                    }
                    if (Case.IsChangeObjectClassInheritance(objClass))
                    {
                        Case.DoChangeObjectClassInheritance(objClass);
                    }
                    if (Case.IsRemoveObjectClassInheritance(objClass))
                    {
                        Case.DoRemoveObjectClassInheritance(objClass);
                    }
                }
            }
            Log.Debug(String.Empty);
        }
    }
}
