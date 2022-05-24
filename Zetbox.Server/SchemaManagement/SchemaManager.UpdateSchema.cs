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
    using System.Threading.Tasks;
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
        public async Task UpdateSchema()
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

                    await DropVolatileObjects();

                    await UpdateDatabaseSchemas();
                    await UpdateTables();
                    await UpdateRelations();
                    await UpdateInheritance();
                    await UpdateSecurityTables();
                    await UpdateConstraints();

                    await UpdateDeletedRelations();
                    await UpdateDeletedTables();

                    await UpdateProcedures();

                    await CreateFinalCheckConstraints();
                    await CreateFinalRightsInfrastructure();

                    await SaveSchema(schema);

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

        private async Task CreateFinalRightsInfrastructure()
        {
            Log.Info("Recreating Rights Infrastructure");

            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().OrderBy(o => o.Module.Namespace).ThenBy(o => o.Name))
            {
                if (!objClass.NeedsRightsTable()) continue;

                var tblRightsName = db.GetTableName(objClass.Module.SchemaName, Construct.SecurityRulesTableName(objClass));
                var tblName = objClass.GetTableRef(db);
                var rightsViewUnmaterializedName = db.GetTableName(objClass.Module.SchemaName, Construct.SecurityRulesRightsViewUnmaterializedName(objClass));
                var refreshRightsOnProcedureName = db.GetProcedureName(objClass.Module.SchemaName, Construct.SecurityRulesRefreshRightsOnProcedureName(objClass));

                Log.InfoFormat("Refreshing ObjectClass Security Rules: {0}", objClass.Name);

                if (!db.CheckViewExists(rightsViewUnmaterializedName))
                    await Case.DoCreateRightsViewUnmaterialized(objClass);
                if (!db.CheckProcedureExists(refreshRightsOnProcedureName))
                    db.CreateRefreshRightsOnProcedure(refreshRightsOnProcedureName, rightsViewUnmaterializedName, tblName, tblRightsName);

                await Case.DoCreateOrReplaceUpdateRightsTrigger(objClass);
            }
            // Either we just removed it to keep the dependency away, then nothing has changed
            // or, the case has triggered the execution and we do it now that everything is in place again.
            await Case.ExecuteTriggeredRefreshRights();
        }

        private async Task CreateFinalCheckConstraints()
        {
            Log.Info("Recreating Check Constraints");

            // this does more than required (dropping already existing), but good enough for now.
            // has to be revisited if someone else creates check constraints.
            await Workaround_UpdateTPHNotNullCheckConstraint();
        }

        private Task DropVolatileObjects()
        {
            Log.Info("Dropping volatile objects");

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

            return Task.CompletedTask;
        }

        private async Task Workaround_UpdateTPHNotNullCheckConstraint()
        {
            foreach (var prop in schema.GetQuery<ValueTypeProperty>())
            {
                var cls = prop.ObjectClass as ObjectClass;
                if (cls == null)
                {
                    // TODO: handle compounds
                    continue;
                }
                var tblName = cls.GetTableRef(db);
                var colName = Construct.ColumnName(prop, string.Empty);
                var checkConstraintName = Construct.CheckConstraintName(tblName.Name, colName);

                if (db.CheckCheckConstraintExists(tblName, checkConstraintName))
                {
                    db.DropCheckConstraint(tblName, checkConstraintName);
                }

                if (!await prop.IsNullable() && cls != null && cls.BaseObjectClass != null && cls.GetTableMapping() == TableMapping.TPH)
                {
                    await Case.CreateTPHNotNullCheckConstraint(tblName, colName, cls);
                }
            }

            foreach (var rel in schema.GetQuery<Relation>())
            {
                if (await rel.GetRelationType() == RelationType.one_n)
                {
                    string assocName, colName, listPosName;
                    RelationEnd relEnd, otherEnd;
                    TableRef tblName, refTblName;
                    bool hasPersistentOrder;
                    if (Case.TryInspect_1_N_Relation(rel, out assocName, out relEnd, out otherEnd, out tblName, out refTblName, out colName, out hasPersistentOrder, out listPosName))
                    {
                        var checkConstraintName = Construct.CheckConstraintName(tblName.Name, colName);
                        if (db.CheckCheckConstraintExists(tblName, checkConstraintName))
                        {
                            db.DropCheckConstraint(tblName, checkConstraintName);
                        }

                        var isNullable = otherEnd.IsNullable();
                        var checkNotNull = !isNullable;
                        if (checkNotNull && (relEnd.Type.GetTableMapping() == TableMapping.TPH && relEnd.Type.BaseObjectClass != null))
                        {
                            await Case.CreateTPHNotNullCheckConstraint(tblName, colName, relEnd.Type);
                        }
                    }
                }
                else if (await rel.GetRelationType() == RelationType.one_one)
                {
                    // TODO: ....
                }
            }
        }

        private async Task UpdateDatabaseSchemas()
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
                if (!await Case.IsNewSchema(moduleName))
                {
                    await Case.DoNewSchema(moduleName);
                }
            }
        }

        private async Task UpdateConstraints()
        {
            Log.Info("Updating Constraints");
            Log.Debug("--------------------");

            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().OrderBy(o => o.Module.Namespace).ThenBy(o => o.Name))
            {
                Log.DebugFormat("Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.Name);
                await UpdateIndexConstraints(objClass);
            }
            Log.Debug(String.Empty);
        }

        private async Task UpdateIndexConstraints(ObjectClass objClass)
        {
            foreach (IndexConstraint uc in Case.savedSchema.GetQuery<IndexConstraint>().Where(p => p.Constrained.ExportGuid == objClass.ExportGuid))
            {
                if (await Case.IsDeleteIndexConstraint(uc))
                {
                    await Case.DoDeleteIndexConstraint(uc);
                }
            }

            foreach (var uc in objClass.Constraints.OfType<IndexConstraint>())
            {
                if (await Case.IsChangeIndexConstraint(uc))
                {
                    await Case.DoChangeIndexConstraint(uc);
                }
            }

            foreach (var uc in objClass.Constraints.OfType<IndexConstraint>())
            {
                if (await Case.IsNewIndexConstraint(uc))
                {
                    await Case.DoNewIndexConstraint(uc);
                }
            }
        }

        private Task UpdateProcedures()
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
                .ToList()
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
                .ToList()
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
                    refSpec => new KeyValuePair<TableRef, string>(db.GetTableName(refSpec.refSchemaName, refSpec.refTableName), Construct.ForeignKeyColumnName(refSpec.OtherEnd).Result));

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

            return Task.CompletedTask;
        }

        private async Task UpdateDeletedTables()
        {
            Log.Info("Updating deleted Tables");
            Log.Debug("-----------------------");

            foreach (ObjectClass objClass in Case.savedSchema.GetQuery<ObjectClass>().OrderBy(o => o.Module.Namespace).ThenBy(o => o.Name))
            {
                Log.DebugFormat("Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.Name);
                if (await Case.IsDeleteObjectClass(objClass))
                {
                    await Case.DoDeleteObjectClass(objClass);
                }
            }
            Log.Debug(String.Empty);
        }

        private async Task UpdateSecurityTables()
        {
            Log.Info("Updating Security Tables");
            Log.Debug("-------------------------");

            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().OrderBy(o => o.Module.Namespace).ThenBy(o => o.Name))
            {
                if (await Case.IsNewObjectClassACL(objClass))
                {
                    await Case.DoNewObjectClassACL(objClass);
                }
                if (await Case.IsRenameObjectClassACL(objClass))
                {
                    await Case.DoRenameObjectClassACL(objClass);
                }
                if (await Case.IsChangeObjectClassACL(objClass))
                {
                    await Case.DoChangeObjectClassACL(objClass);
                }
                if (await Case.IsDeleteObjectClassSecurityRules(objClass))
                {
                    await Case.DoDeleteObjectClassSecurityRules(objClass);
                }
            }

            var allACLTables = schema.GetQuery<ObjectClass>()
                .ToList()
                .Where(o => o.NeedsRightsTable())
                .OrderBy(o => o.Module.Namespace)
                .ThenBy(o => o.Name)
                .ToList();
            await Case.DoCreateRefreshAllRightsProcedure(allACLTables);
        }

        private async Task UpdateTables()
        {
            Log.Info("Updating Tables & Columns");
            Log.Debug("-------------------------");

            // The following actions have to be sequenced separately to avoid stepping on each other.

            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().OrderBy(o => o.Module.Namespace).ThenBy(o => o.Name))
            {
                Log.DebugFormat("Deleting Columns in Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.Name);

                // Delete early to avoid collisions with newly created columns (like changing data type)
                // Note: migration of data types is not supported now. Only chance is to delete and recreate a column
                await UpdateDeletedColumns(objClass, String.Empty);
            }

            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().Where(o => o.BaseObjectClass == null).OrderBy(o => o.Module.Namespace).ThenBy(o => o.Name))
            {
                Log.DebugFormat("TPH/TPT migrations for Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.Name);

                if (await Case.IsChangeTphToTpt(objClass))
                {
                    await Case.DoChangeTphToTpt(objClass);
                }
                if (await Case.IsChangeTptToTph(objClass))
                {
                    await Case.DoChangeTptToTph(objClass);
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
                if (await Case.IsRenameObjectClassTable(objClass))
                {
                    await Case.DoRenameObjectClassTable(objClass);
                }
            }

            foreach (ObjectClass objClass in classes)
            {
                Log.DebugFormat("Managing Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.Name);

                if (await Case.IsNewObjectClass(objClass))
                {
                    await Case.DoNewObjectClass(objClass);
                }
            }

            foreach (ObjectClass objClass in classes)
            {
                await UpdateColumns(objClass, objClass.Properties, String.Empty);
            }

            Log.Debug(String.Empty);
        }

        private async Task UpdateColumns(ObjectClass objClass, ICollection<Property> properties, string prefix)
        {
            foreach (ValueTypeProperty prop in properties.OfType<ValueTypeProperty>().Where(p => !p.IsList))
            {
                if (await Case.IsNewValueTypePropertyNullable(prop))
                {
                    await Case.DoNewValueTypePropertyNullable(objClass, prop, prefix);
                }
                if (await Case.IsNewValueTypePropertyNotNullable(prop))
                {
                    await Case.DoNewValueTypePropertyNotNullable(objClass, prop, prefix);
                }
                if (await Case.IsRenameValueTypePropertyName(objClass, prop, prefix))
                {
                    await Case.DoRenameValueTypePropertyName(objClass, prop, prefix);
                }
                if (await Case.IsMoveValueTypeProperty(prop))
                {
                    await Case.DoMoveValueTypeProperty(objClass, prop, prefix);
                }
                if (await Case.IsChangeValueTypeProperty_To_NotNullable(prop))
                {
                    await Case.DoChangeValueTypeProperty_To_NotNullable(objClass, prop, prefix);
                }
                if (await Case.IsChangeValueTypeProperty_To_Nullable(prop))
                {
                    await Case.DoChangeValueTypeProperty_To_Nullable(objClass, prop, prefix);
                }
                if (await Case.IsChangeDefaultValue(prop))
                {
                    await Case.DoChangeDefaultValue(objClass, prop, prefix);
                }
            }

            foreach (CompoundObjectProperty sprop in properties.OfType<CompoundObjectProperty>().Where(p => !p.IsList))
            {
                if (await Case.IsNewCompoundObjectProperty(sprop))
                {
                    await Case.DoNewCompoundObjectProperty(objClass, sprop, prefix);
                }
                else
                {
                    // See if the CompoundObject self has changed
                    await UpdateColumns(objClass, sprop.CompoundObjectDefinition.Properties, Construct.ColumnName(sprop, prefix));
                }
            }

            foreach (ValueTypeProperty prop in properties.OfType<ValueTypeProperty>().Where(p => p.IsList))
            {
                if (await Case.IsNewValueTypePropertyList(prop))
                {
                    await Case.DoNewValueTypePropertyList(objClass, prop);
                }
                if (await Case.IsRenameValueTypePropertyListName(prop))
                {
                    await Case.DoRenameValueTypePropertyListName(objClass, prop);
                }
                if (await Case.IsMoveValueTypePropertyList(prop))
                {
                    await Case.DoMoveValueTypePropertyList(objClass, prop);
                }
            }

            foreach (CompoundObjectProperty prop in properties.OfType<CompoundObjectProperty>().Where(p => p.IsList))
            {
                if (await Case.IsNewCompoundObjectPropertyList(prop))
                {
                    await Case.DoNewCompoundObjectPropertyList(objClass, prop);
                }
                if (await Case.IsRenameCompoundObjectPropertyListName(prop))
                {
                    await Case.DoRenameCompoundObjectPropertyListName(objClass, prop);
                }
                if (await Case.IsMoveCompoundObjectPropertyList(prop))
                {
                    await Case.DoMoveCompoundObjectPropertyList(objClass, prop);
                }
            }
        }

        private async Task UpdateDeletedColumns(ObjectClass objClass, string prefix)
        {
            foreach (ValueTypeProperty savedProp in Case.savedSchema.GetQuery<ValueTypeProperty>().Where(p => p.ObjectClass.ExportGuid == objClass.ExportGuid))
            {
                if (await Case.IsDeleteValueTypeProperty(savedProp))
                {
                    await Case.DoDeleteValueTypeProperty(objClass, savedProp, prefix);
                }
                if (await Case.IsDeleteValueTypePropertyList(savedProp))
                {
                    await Case.DoDeleteValueTypePropertyList(objClass, savedProp, prefix);
                }
            }

            foreach (CompoundObjectProperty savedCProp in Case.savedSchema.GetQuery<CompoundObjectProperty>().Where(p => p.ObjectClass.ExportGuid == objClass.ExportGuid))
            {
                if (await Case.IsDeleteCompoundObjectProperty(savedCProp))
                {
                    await Case.DoDeleteCompoundObjectProperty(objClass, savedCProp, prefix);
                }
                if (await Case.IsDeleteCompoundObjectPropertyList(savedCProp))
                {
                    await Case.DoDeleteCompoundObjectPropertyList(objClass, savedCProp, prefix);
                }
            }
        }

        private async Task UpdateDeletedRelations()
        {
            Log.Info("Updating deleted Relations");
            Log.Debug("--------------------------");

            foreach (Relation rel in Case.savedSchema.GetQuery<Relation>().OrderBy(r => r.Module.Namespace))
            {
                Log.DebugFormat("Relation: {0} ({1})", rel.GetAssociationName(), rel.GetRelationType());

                if (await rel.GetRelationType() == RelationType.one_n)
                {
                    if (await Case.IsDelete_1_N_Relation(rel))
                    {
                        await Case.DoDelete_1_N_Relation(rel);
                    }
                }
                else if (await rel.GetRelationType() == RelationType.n_m)
                {
                    if (await Case.IsDelete_N_M_Relation(rel))
                    {
                        await Case.DoDelete_N_M_Relation(rel);
                    }
                }
                else if (await rel.GetRelationType() == RelationType.one_one)
                {
                    if (await Case.IsDelete_1_1_Relation(rel))
                    {
                        await Case.DoDelete_1_1_Relation(rel);
                    }
                }
            }
            Log.Debug(String.Empty);
        }

        private async Task UpdateRelations()
        {
            Log.Info("Updating Relations");
            Log.Debug("------------------");
            var relations = schema.GetQuery<Relation>().OrderBy(r => r.Module.Namespace);

            foreach (Relation rel in relations)
            {
                if (await Case.IsChangeRelationName(rel))
                {
                    await Case.DoChangeRelationName(rel);
                }
                if (await Case.IsChangeRelationEndTypes(rel))
                {
                    await Case.DoChangeRelationEndTypes(rel);
                }
            }

            foreach (Relation rel in relations)
            {
                if (await Case.IsChangeRelationType(rel))
                {
                    if (await Case.IsChangeRelationType_from_1_1_to_1_n(rel))
                    {
                        await Case.DoChangeRelationType_from_1_1_to_1_n(rel);
                    }
                    else if (await Case.IsChangeRelationType_from_1_1_to_n_m(rel))
                    {
                        await Case.DoChangeRelationType_from_1_1_to_n_m(rel);
                    }
                    else if (await Case.IsChangeRelationType_from_1_n_to_1_1(rel))
                    {
                        await Case.DoChangeRelationType_from_1_n_to_1_1(rel);
                    }
                    else if (await Case.IsChangeRelationType_from_1_n_to_n_m(rel))
                    {
                        await Case.DoChangeRelationType_from_1_n_to_n_m(rel);
                    }
                    else if (await Case.IsChangeRelationType_from_n_m_to_1_1(rel))
                    {
                        await Case.DoChangeRelationType_from_n_m_to_1_1(rel);
                    }
                    else if (await Case.IsChangeRelationType_from_n_m_to_1_n(rel))
                    {
                        await Case.DoChangeRelationType_from_n_m_to_1_n(rel);
                    }
                }
            }
            foreach (Relation rel in relations)
            {
                if (await rel.GetRelationType() == RelationType.one_n)
                {
                    if (await Case.Is_1_N_RelationChange_FromIndexed_To_NotIndexed(rel))
                    {
                        await Case.Do_1_N_RelationChange_FromIndexed_To_NotIndexed(rel);
                    }
                    if (await Case.Is_1_N_RelationChange_FromNotIndexed_To_Indexed(rel))
                    {
                        await Case.Do_1_N_RelationChange_FromNotIndexed_To_Indexed(rel);
                    }
                    if (await Case.Is_1_N_RelationChange_FromNullable_To_NotNullable(rel))
                    {
                        await Case.Do_1_N_RelationChange_FromNullable_To_NotNullable(rel);
                    }
                    if (await Case.Is_1_N_RelationChange_FromNotNullable_To_Nullable(rel))
                    {
                        await Case.Do_1_N_RelationChange_FromNotNullable_To_Nullable(rel);
                    }
                }
                else if (await rel.GetRelationType() == RelationType.n_m)
                {
                    if (await Case.Is_N_M_RelationChange_FromIndexed_To_NotIndexed(rel, RelationEndRole.A))
                    {
                        await Case.Do_N_M_RelationChange_FromIndexed_To_NotIndexed(rel, RelationEndRole.A);
                    }
                    if (await Case.Is_N_M_RelationChange_FromIndexed_To_NotIndexed(rel, RelationEndRole.B))
                    {
                        await Case.Do_N_M_RelationChange_FromIndexed_To_NotIndexed(rel, RelationEndRole.B);
                    }
                    if (await Case.Is_N_M_RelationChange_FromNotIndexed_To_Indexed(rel, RelationEndRole.A))
                    {
                        await Case.Do_N_M_RelationChange_FromNotIndexed_To_Indexed(rel, RelationEndRole.A);
                    }
                    if (await Case.Is_N_M_RelationChange_FromNotIndexed_To_Indexed(rel, RelationEndRole.B))
                    {
                        await Case.Do_N_M_RelationChange_FromNotIndexed_To_Indexed(rel, RelationEndRole.B);
                    }
                }
                else if (await rel.GetRelationType() == RelationType.one_one)
                {
                    if (await Case.IsChange_1_1_Storage(rel))
                    {
                        await Case.DoChange_1_1_Storage(rel);
                    }

                    if (await Case.Is_1_1_RelationChange_FromNotNullable_To_Nullable(rel, RelationEndRole.A))
                    {
                        await Case.Do_1_1_RelationChange_FromNotNullable_To_Nullable(rel, RelationEndRole.A);
                    }
                    if (await Case.Is_1_1_RelationChange_FromNotNullable_To_Nullable(rel, RelationEndRole.B))
                    {
                        await Case.Do_1_1_RelationChange_FromNotNullable_To_Nullable(rel, RelationEndRole.B);
                    }

                    if (await Case.Is_1_1_RelationChange_FromNullable_To_NotNullable(rel, RelationEndRole.A))
                    {
                        await Case.Do_1_1_RelationChange_FromNullable_To_NotNullable(rel, RelationEndRole.A);
                    }
                    if (await Case.Is_1_1_RelationChange_FromNullable_To_NotNullable(rel, RelationEndRole.B))
                    {
                        await Case.Do_1_1_RelationChange_FromNullable_To_NotNullable(rel, RelationEndRole.B);
                    }
                }
            }

            foreach (Relation rel in relations)
            {
                if (await rel.GetRelationType() == RelationType.one_n)
                {
                    if (await Case.IsNew_1_N_Relation(rel))
                    {
                        await Case.DoNew_1_N_Relation(rel);
                    }
                }
                else if (await rel.GetRelationType() == RelationType.n_m)
                {
                    if (await Case.IsNew_N_M_Relation(rel))
                    {
                        await Case.DoNew_N_M_Relation(rel);
                    }
                }
                else if (await rel.GetRelationType() == RelationType.one_one)
                {
                    if (await Case.IsNew_1_1_Relation(rel))
                    {
                        await Case.DoNew_1_1_Relation(rel);
                    }
                }
            }
            Log.Debug(String.Empty);
        }

        private async Task UpdateInheritance()
        {
            Log.Info("Updating Inheritance");
            Log.Debug("--------------------");

            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().OrderBy(o => o.Module.Namespace).ThenBy(o => o.Name))
            {
                var mapping = objClass.GetTableMapping();
                Log.DebugFormat("Objectclass: {0}.{1}, {2}", objClass.Module.Namespace, objClass.Name, mapping);
                if (mapping == TableMapping.TPT)
                {
                    if (await Case.IsNewObjectClassInheritance(objClass))
                    {
                        await Case.DoNewObjectClassInheritance(objClass);
                    }
                    if (await Case.IsChangeObjectClassInheritance(objClass))
                    {
                        await Case.DoChangeObjectClassInheritance(objClass);
                    }
                    if (await Case.IsRemoveObjectClassInheritance(objClass))
                    {
                        await Case.DoRemoveObjectClassInheritance(objClass);
                    }
                }
            }
            Log.Debug(String.Empty);
        }
    }
}
