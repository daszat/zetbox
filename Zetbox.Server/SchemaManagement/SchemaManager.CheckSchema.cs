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
        public async Task CheckSchema(bool withRepair)
        {
            using (Log.DebugTraceMethodCall("CheckSchema"))
            {
                this.repair = withRepair;
                WriteReportHeader(withRepair ? "Check Schema Report with repair" : "Check Schema Report");

                if (schema.GetQuery<Zetbox.App.Base.ObjectClass>().Count() == 0)
                {
                    Log.Error("Current Schema is empty, aborting");
                }
                else
                {
                    await CheckTables();
                    Log.Debug(String.Empty);

                    await CheckUpdateRightsTrigger();
                    Log.Debug(String.Empty);

                    await UpdateProcedures();
                    Log.Debug(String.Empty);

                    await CheckExtraTables();
                    Log.Debug(String.Empty);

                    await CheckRelations();
                    Log.Debug(String.Empty);

                    await CheckInheritance();
                    Log.Debug(String.Empty);

                    if (withRepair)
                        await Workaround_UpdateTPHNotNullCheckConstraint();

                    await CheckExtraRelations();

                    if (withRepair)
                        db.RefreshDbStats();
                }
            }
        }
        public async Task CheckBaseSchema(bool withRepair)
        {
            using (Log.DebugTraceMethodCall("CheckBaseSchema"))
            {
                this.repair = withRepair;
                WriteReportHeader(withRepair ? "Check Base-Schema Report with repair" : "Check Base-Schema Report");

                if (schema.GetQuery<Zetbox.App.Base.ObjectClass>().Count() == 0)
                {
                    Log.Error("Current Schema is empty, aborting");
                }
                else
                {
                    Log.Info("Checking Tables & Columns");
                    Log.Debug("-------------------------");

                    // Checking Tables
                    foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().Where(o => o.Module.Name == "ZetboxBase" || o.Module.Name == "GUI").OrderBy(o => o.Name))
                    {
                        Log.DebugFormat("Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.Name);

                        if (db.CheckTableExists(objClass.GetTableRef(db)))
                        {
                            Log.DebugFormat("  Table: {0}", objClass.GetTableRef(db));
                            await CheckColumns(objClass, objClass.Properties, String.Empty);
                        }
                        else
                        {
                            Log.WarnFormat("Table '{0}' is missing", objClass.GetTableRef(db));
                            if (repair)
                            {
                                Log.Info("Fixing");
                                await Case.DoNewObjectClass(objClass);
                            }
                        }
                    }

                    Log.Info("Checking Relations");
                    Log.Debug("------------------");
                    foreach (Relation rel in schema.GetQuery<Relation>().Where(r => r.Module.Name == "ZetboxBase" || r.Module.Name == "GUI"))
                    {
                        string assocName = rel.GetAssociationName();
                        Log.DebugFormat("Relation: \"{0}\" \"{1} - {2}\"", assocName, rel.A.Type.Name, rel.B.Type.Name);
                        switch (await rel.GetRelationType())
                        {
                            case RelationType.one_one:
                                await Check_1_1_RelationColumns(rel, rel.A, RelationEndRole.A);
                                await Check_1_1_RelationColumns(rel, rel.B, RelationEndRole.B);
                                break;
                            case RelationType.one_n:
                                await Check_1_N_RelationColumns(rel);
                                break;
                            case RelationType.n_m:
                                await Check_N_M_RelationColumns(rel);
                                break;
                        }
                    }
                }
            }
        }

        private async Task CheckUpdateRightsTrigger()
        {
            Log.InfoFormat("Checking UpdateRightsTrigger");
            // Select all ObjectClasses with ACL
            foreach (var objClass in schema.GetQuery<ObjectClass>().ToList().Where(o => o.NeedsRightsTable()).OrderBy(o => o.Module.Namespace).ThenBy(o => o.Name))
            {
                Log.DebugFormat("Table: {0}", objClass.TableName);
                if (repair)
                {
                    await Case.DoCreateOrReplaceUpdateRightsTrigger(objClass);
                }
                else
                {
                    var tblName = objClass.GetTableRef(db);
                    var updateRightsTriggerName = new TriggerRef(tblName, Construct.SecurityRulesUpdateRightsTriggerName(objClass));
                    if (!db.CheckTriggerExists(updateRightsTriggerName))
                    {
                        Log.WarnFormat("Security Rules Trigger '{0}' is missing", updateRightsTriggerName);
                    }
                }
            }

            // Select all n:m Relations that are in a ACL selector
            foreach (Relation rel in await schema.GetQuery<Relation>().ToList()
                .Where(async r => await r.GetRelationType() == RelationType.n_m && schema.GetQuery<RoleMembership>().ToList().Where(rm => rm.Relations.Contains(r)).Count() > 0))
            {
                Log.DebugFormat("Relation: {0}, {1} <-> {2}", rel.Description, rel.A.Type.TableName, rel.B.Type.TableName);

                if (repair)
                {
                    await Case.DoCreateUpdateRightsTrigger(rel);
                }
                else
                {
                    var tblName = db.GetTableName(rel.Module.SchemaName, rel.GetRelationTableName());
                    var updateRightsTriggerName = new TriggerRef(tblName, Construct.SecurityRulesUpdateRightsTriggerName(rel));
                    if (!db.CheckTriggerExists(updateRightsTriggerName))
                    {
                        Log.WarnFormat("Security Rules Trigger '{0}' is missing", updateRightsTriggerName);
                    }
                }
            }

            if (repair)
            {
                var allACLTables = schema.GetQuery<ObjectClass>()
                    .ToList()
                    .Where(o => o.NeedsRightsTable())
                    .OrderBy(o => o.Module.Namespace)
                    .ThenBy(o => o.Name)
                    .ToList();
                await Case.DoCreateRefreshAllRightsProcedure(allACLTables);
            }
        }

        private async Task CheckInheritance()
        {
            Log.Info("Checking Inheritance");
            Log.Debug("--------------------");

            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().Where(o => o.BaseObjectClass != null).OrderBy(o => o.Module.Namespace).ThenBy(o => o.Name))
            {
                if (objClass.GetTableMapping() == TableMapping.TPT)
                {
                    Log.DebugFormat("Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.Name);
                    string assocName = Construct.InheritanceAssociationName(objClass.BaseObjectClass, objClass);
                    var tblName = objClass.GetTableRef(db);
                    if (!db.CheckFKConstraintExists(tblName, assocName))
                    {
                        Log.WarnFormat("FK Constraint to BaseClass is missing on Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.Name);
                        if (repair)
                        {
                            await Case.DoNewObjectClassInheritance(objClass);
                        }
                    }
                }
            }
            Log.Debug(String.Empty);
        }

        private Task CheckExtraTables()
        {
            Log.Info("Checking extra Tables");
            Log.Debug("---------------------");

            // All ObjectClasses
            List<string> tableNames = schema.GetQuery<ObjectClass>().Select(o => o.TableName).ToList();

            // Add ValueTypeListProperties
            tableNames.AddRange(schema.GetQuery<ValueTypeProperty>().Where(p => p.IsList).ToList()
                .Select(p => p.GetCollectionEntryTable()));

            // Add CompoundObjectListProperties
            tableNames.AddRange(schema.GetQuery<CompoundObjectProperty>().Where(p => p.IsList).ToList()
                .Select(p => p.GetCollectionEntryTable()));

            // Add Relations with sep. Storage
            tableNames.AddRange(schema.GetQuery<Relation>()
                            .Where(r => r.Storage == StorageType.Separate)
                            .ToList()
                            .Select(r => r.GetRelationTableName()));

            // All Security Rules Rights Tables
            tableNames.AddRange(schema.GetQuery<ObjectClass>().ToList()
                .Where(o => o.NeedsRightsTable())
                .Select(o => Construct.SecurityRulesTableName(o)));

            // TODO: be schema-aware
            foreach (var tblName in db.GetTableNames().Select(r => r.Name))
            {
                if (!tableNames.Contains(tblName))
                {
                    Log.WarnFormat("Table '{0}' found in database but no ObjectClass was defined", tblName);
                }
            }

            return Task.CompletedTask;
        }

        private void GetExistingColumnNames(ObjectClass objClass, ICollection<Property> properties, string prefix, List<string> columns)
        {
            columns.AddRange(properties.OfType<ValueTypeProperty>().Where(p => !p.IsList).Select(p => Construct.ColumnName(p, prefix)).ToArray());

            foreach (CompoundObjectProperty sprop in properties.OfType<CompoundObjectProperty>().Where(p => !p.IsList))
            {
                columns.Add(Construct.ColumnName(sprop, prefix));
                GetExistingColumnNames(objClass, sprop.CompoundObjectDefinition.Properties, Construct.ColumnName(sprop, prefix), columns);
            }
        }
        private async Task GetRelationColumnNames(ObjectClass objClass, List<string> columns)
        {
            foreach (var relEnd in schema.GetQuery<RelationEnd>().Where(e => e.Type == objClass))
            {
                if (await relEnd.Parent.HasStorage(relEnd.GetRole()))
                {
                    columns.Add(await Construct.ForeignKeyColumnName(await relEnd.Parent.GetOtherEnd(relEnd)));
                    if (await relEnd.Parent.NeedsPositionStorage(relEnd.GetRole()))
                    {
                        columns.Add(Construct.ListPositionColumnName(await relEnd.Parent.GetOtherEnd(relEnd)));
                    }
                }
            }
        }

        private async Task CheckExtraColumns(ObjectClass objClass)
        {
            if (objClass.GetTableMapping() == TableMapping.TPH && objClass.BaseObjectClass != null) return; // Check only base TPH classes

            Log.Debug("Extra Columns: ");
            List<string> columns = new List<string>();
            List<ObjectClass> classes = new List<ObjectClass>(new[] { objClass });

            if (objClass.GetTableMapping() == TableMapping.TPH)
                objClass.CollectChildClasses(classes, true);

            foreach (var cls in classes)
            {
                GetExistingColumnNames(cls, cls.Properties, String.Empty, columns);
                await GetRelationColumnNames(cls, columns);
            }

            foreach (string propName in db.GetTableColumnNames(objClass.GetTableRef(db)))
            {
                if (propName == "ID" || propName == TableMapper.DiscriminatorColumnName)
                    continue;
                if (!columns.Contains(propName))
                {
                    Log.WarnFormat("Column '[{0}].[{1}].[{2}]' found in database but no Property was defined", 
                        objClass.Module.IfNotNull(o => o.SchemaName),
                        objClass.TableName, 
                        propName);
                }
            }
        }

        private async Task CheckExtraRelations()
        {
            Log.Info("Checking extra Relations");
            Log.Debug("------------------------");

            var relations = schema.GetQuery<Relation>().ToList();
            List<string> relationNames = new List<string>();

            relationNames.AddRange((await relations.Where(async r => await r.GetRelationType() == RelationType.one_n)).Select(r => r.GetAssociationName()));
            foreach (var rel in await relations.Where(async r => await r.GetRelationType() == RelationType.n_m))
            {
                relationNames.Add(await rel.GetRelationAssociationName(RelationEndRole.A));
                relationNames.Add(await rel.GetRelationAssociationName(RelationEndRole.B));
            }

            foreach (var rel in await relations.Where(async r => await r.GetRelationType() == RelationType.one_one))
            {
                if (rel.A.Navigator != null && await rel.HasStorage(RelationEndRole.A))
                    relationNames.Add(await rel.GetRelationAssociationName(RelationEndRole.A));
                if (rel.B.Navigator != null && await rel.HasStorage(RelationEndRole.B))
                    relationNames.Add(await rel.GetRelationAssociationName(RelationEndRole.B));
            }

            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().Where(o => o.BaseObjectClass != null))
            {
                relationNames.Add(Construct.InheritanceAssociationName(objClass.BaseObjectClass, objClass));
            }

            foreach (ValueTypeProperty prop in schema.GetQuery<ValueTypeProperty>().Where(p => p.IsList))
            {
                relationNames.Add(await prop.GetAssociationName());
            }

            foreach (CompoundObjectProperty prop in schema.GetQuery<CompoundObjectProperty>().Where(p => p.IsList))
            {
                relationNames.Add(await prop.GetAssociationName());
            }

            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().ToList().Where(o => o.NeedsRightsTable()))
            {
                relationNames.Add(Construct.SecurityRulesFKName(objClass));
            }

            // Quote Identifier to meet the exact name in database. PG will short those names when they exceed 63 chars
            relationNames = relationNames.Select(n => db.QuoteIdentifier(n)).ToList();
            foreach (var rel in db.GetFKConstraintNames())
            {
                if (!relationNames.Contains(db.QuoteIdentifier(rel.ConstraintName)))
                {
                    Log.WarnFormat("'{0}' on table '{1}' found in database but no relation object was defined", rel.ConstraintName, rel.TableName);
                    if (repair)
                    {
                        db.DropFKConstraint(rel.TableName, rel.ConstraintName);
                    }
                }
            }
        }

        private async Task CheckRelations()
        {
            Log.Info("Checking Relations");
            Log.Debug("------------------");
            foreach (Relation rel in schema.GetQuery<Relation>())
            {
                string assocName = rel.GetAssociationName();
                Log.DebugFormat("Relation: \"{0}\" \"{1} - {2}\"", assocName, rel.A.Type.Name, rel.B.Type.Name);
                switch (await rel.GetRelationType())
                {
                    case RelationType.one_one:
                        await Check_1_1_RelationColumns(rel, rel.A, RelationEndRole.A);
                        await Check_1_1_RelationColumns(rel, rel.B, RelationEndRole.B);
                        break;
                    case RelationType.one_n:
                        await Check_1_N_RelationColumns(rel);
                        break;
                    case RelationType.n_m:
                        await Check_N_M_RelationColumns(rel);
                        break;
                }
            }
        }

        private async Task Check_1_1_RelationColumns(Relation rel, RelationEnd relEnd, RelationEndRole role)
        {
            if (await rel.HasStorage(role))
            {
                var tblName = relEnd.Type.GetTableRef(db);
                RelationEnd otherEnd = await rel.GetOtherEnd(relEnd);
                var refTblName = otherEnd.Type.GetTableRef(db);
                string colName = await Construct.ForeignKeyColumnName(otherEnd);
                string assocName = await rel.GetRelationAssociationName(role);
                string idxName = Construct.IndexName(tblName.Name, colName);
                var realIsNullable = otherEnd.IsNullable();
                if (realIsNullable == false)
                    realIsNullable = relEnd.Type.GetTableMapping() == TableMapping.TPH && relEnd.Type.BaseObjectClass != null;

                await CheckColumn(tblName, await Construct.ForeignKeyColumnName(otherEnd), System.Data.DbType.Int32, 0, 0, realIsNullable, null);
                if (!db.CheckFKConstraintExists(tblName, assocName))
                {
                    Log.WarnFormat("FK Constraint '{0}' is missing", assocName);
                    if (repair)
                    {
                        db.CreateFKConstraint(tblName, refTblName, colName, assocName, false);
                    }
                }
                if (!db.CheckIndexExists(tblName, idxName))
                {
                    Log.WarnFormat("Index '{0}' is missing", idxName);
                    if (repair)
                    {
                        db.CreateIndex(tblName, idxName, true, false, colName);
                    }
                }
            }
        }

        private async Task Check_1_N_RelationColumns(Relation rel)
        {
            string assocName = rel.GetAssociationName();

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
            var refTblName = otherEnd.Type.GetTableRef(db);
            bool isIndexed = await rel.NeedsPositionStorage(relEnd.GetRole());

            string colName = await Construct.ForeignKeyColumnName(otherEnd);
            string indexName = Construct.ListPositionColumnName(otherEnd);

            var realIsNullable = otherEnd.IsNullable();
            if (realIsNullable == false)
                realIsNullable = relEnd.Type.GetTableMapping() == TableMapping.TPH && relEnd.Type.BaseObjectClass != null;

            await CheckColumn(tblName, colName, System.Data.DbType.Int32, 0, 0, realIsNullable, null);

            if (!db.CheckFKConstraintExists(tblName, assocName))
            {
                Log.WarnFormat("FK Constraint '{0}' is missing", assocName);
                if (repair)
                {
                    db.CreateFKConstraint(tblName, refTblName, colName, assocName, false);
                }
            }

            if (!db.CheckIndexExists(tblName, Construct.IndexName(tblName.Name, colName)))
            {
                Log.WarnFormat("Index '{0}' is missing", Construct.IndexName(tblName.Name, colName));
                if (repair)
                {
                    db.CreateIndex(tblName, Construct.IndexName(tblName.Name, colName), false, false, colName);
                }
            }

            if (isIndexed)
            {
                await CheckOrderColumn(tblName, colName, indexName);
            }
            if (!isIndexed && db.CheckColumnExists(tblName, indexName))
            {
                Log.WarnFormat("Index Column exists but property is not indexed");
                if (repair)
                {
                    await Case.Do_1_N_RelationChange_FromIndexed_To_NotIndexed(rel);
                }
            }
        }

        private async Task Check_N_M_RelationColumns(Relation rel)
        {
            string assocName = rel.GetAssociationName();

            var tblName = db.GetTableName(rel.Module.SchemaName, rel.GetRelationTableName());
            string fkAName = await Construct.ForeignKeyColumnName(rel.A);
            string fkBName = await Construct.ForeignKeyColumnName(rel.B);
            string assocAName = await rel.GetRelationAssociationName(RelationEndRole.A);
            string assocBName = await rel.GetRelationAssociationName(RelationEndRole.B);
            string fkAIndex = fkAName + Zetbox.API.Helper.PositionSuffix;
            string fkBIndex = fkBName + Zetbox.API.Helper.PositionSuffix;

            Log.DebugFormat("Checking [{0}]", assocName);

            if (!db.CheckTableExists(tblName))
            {
                Log.WarnFormat("Relation table '{0}' is missing for [{1}]", tblName, assocName);
                if (repair)
                {
                    await Case.DoNew_N_M_Relation(rel);
                }
                return;
            }
            await CheckColumn(tblName, fkAName, System.Data.DbType.Int32, 0, 0, false, null);
            await CheckColumn(tblName, fkBName, System.Data.DbType.Int32, 0, 0, false, null);

            if (!db.CheckFKConstraintExists(tblName, assocAName))
            {
                Log.WarnFormat("FK Constraint '{0}' for A is missing for [{1}]", assocAName, assocName);
                if (repair)
                {
                    db.CreateFKConstraint(tblName, rel.A.Type.GetTableRef(db), fkAName, assocAName, true);
                }
            }
            if (!db.CheckFKConstraintExists(tblName, assocBName))
            {
                Log.WarnFormat("FK Constraint '{0}' for B is missing for [{1}]", assocBName, assocName);
                if (repair)
                {
                    db.CreateFKConstraint(tblName, rel.B.Type.GetTableRef(db), fkBName, assocBName, true);
                }
            }

            if (!db.CheckIndexExists(tblName, Construct.IndexName(tblName.Name, fkAName)))
            {
                Log.WarnFormat("Index '{0}' is missing", Construct.IndexName(tblName.Name, fkAName));
                if (repair)
                {
                    db.CreateIndex(tblName, Construct.IndexName(tblName.Name, fkAName), false, false, fkAName);
                }
            }
            if (!db.CheckIndexExists(tblName, Construct.IndexName(tblName.Name, fkBName)))
            {
                Log.WarnFormat("Index '{0}' is missing", Construct.IndexName(tblName.Name, fkBName));
                if (repair)
                {
                    db.CreateIndex(tblName, Construct.IndexName(tblName.Name, fkBName), false, false, fkBName);
                }
            }


            if (await rel.NeedsPositionStorage(RelationEndRole.A))
            {
                await CheckColumn(tblName, fkAIndex, System.Data.DbType.Int32, 0, 0, true, null);
                if (repair)
                {
                    // TODO: Call case
                }
            }
            if (!await rel.NeedsPositionStorage(RelationEndRole.A) && db.CheckColumnExists(tblName, fkAIndex))
            {
                Log.WarnFormat("Navigator A '{0}' Index Column exists but property is not indexed for [{1}]", fkAName, assocName);
                if (repair)
                {
                    // TODO: Call case
                }
            }

            if (await rel.NeedsPositionStorage(RelationEndRole.B))
            {
                await CheckColumn(tblName, fkBIndex, System.Data.DbType.Int32, 0, 0, true, null);
                if (repair)
                {
                    // TODO: Call case
                }
            }
            if (!await rel.NeedsPositionStorage(RelationEndRole.B) && db.CheckColumnExists(tblName, fkBIndex))
            {
                Log.WarnFormat("Navigator B '{0}' Index Column exists but property is not indexed for [{1}]", fkBName, assocName);
                if (repair)
                {
                    // TODO: Call case
                }
            }

            if (rel.A.Type.ImplementsIExportable().Result && rel.B.Type.ImplementsIExportable().Result)
            {
                await CheckColumn(tblName, "ExportGuid", System.Data.DbType.Guid, 0, 0, false, new NewGuidDefaultConstraint());
            }
        }

        private async Task CheckTables()
        {
            Log.Info("Checking Tables & Columns");
            Log.Debug("-------------------------");

            // Checking Tables
            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().OrderBy(o => o.Module.Namespace).ThenBy(o => o.Name))
            {
                Log.DebugFormat("Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.Name);

                if (db.CheckTableExists(objClass.GetTableRef(db)))
                {
                    Log.DebugFormat("  Table: {0}", objClass.TableName);
                    await CheckColumns(objClass, objClass.Properties, String.Empty);
                    await CheckIndexConstraints(objClass);
                    await CheckValueTypeCollections(objClass);
                    await CheckCompoundObjectCollections(objClass);
                    await CheckExtraColumns(objClass);
                    await CheckTableSecurityRules(objClass);
                }
                else
                {
                    Log.WarnFormat("Table '{0}' is missing", objClass.TableName);
                    if (repair)
                        await Case.DoNewObjectClass(objClass);
                }
            }
        }

        private async Task CheckIndexConstraints(ObjectClass objClass)
        {
            foreach (var uc in objClass.Constraints.OfType<IndexConstraint>())
            {
                var isFulltextConstraint = uc is FullTextIndexConstraint;
                if (isFulltextConstraint) continue;

                var tblName = objClass.GetTableRef(db);
                var columns = await Construct.GetUCColNames(uc);
                var idxName = Construct.IndexName(tblName.Name, columns);
                if (!db.CheckIndexExists(tblName, idxName))
                {
                    Log.WarnFormat("Index Constraint '{0}' is missing", idxName);
                    if (repair)
                    {
                        await Case.DoNewIndexConstraint(uc);
                    }
                }
            }
        }

        private async Task CheckTableSecurityRules(ObjectClass objClass)
        {
            if (objClass.NeedsRightsTable())
            {
                var tblName = objClass.GetTableRef(db);
                var tblRightsName = db.GetTableName(objClass.Module.SchemaName, Construct.SecurityRulesTableName(objClass));
                var rightsViewUnmaterializedName = db.GetTableName(objClass.Module.SchemaName, Construct.SecurityRulesRightsViewUnmaterializedName(objClass));
                var refreshRightsOnProcedureName = db.GetProcedureName(objClass.Module.SchemaName, Construct.SecurityRulesRefreshRightsOnProcedureName(objClass));

                if (!db.CheckTableExists(tblRightsName))
                {
                    Log.WarnFormat("Security Rules Table '{0}' is missing", tblRightsName);
                    if (repair)
                    {
                        await Case.DoNewObjectClassACL(objClass);
                    }
                }
                else
                {
                    if (!db.CheckViewExists(rightsViewUnmaterializedName))
                    {
                        Log.WarnFormat("Security Rules unmaterialized View '{0}' is missing", rightsViewUnmaterializedName);
                        if (repair)
                        {
                            await Case.DoCreateRightsViewUnmaterialized(objClass);
                        }
                    }
                    if (!db.CheckProcedureExists(refreshRightsOnProcedureName))
                    {
                        Log.WarnFormat("Security Rules Refresh Procedure '{0}' is missing", refreshRightsOnProcedureName);
                        if (repair)
                        {
                            db.CreateRefreshRightsOnProcedure(refreshRightsOnProcedureName, rightsViewUnmaterializedName, tblName, tblRightsName);
                        }
                    }
                }
            }
        }

        private async Task CheckValueTypeCollections(ObjectClass objClass)
        {
            Log.Debug("ValueType Collections: ");

            foreach (ValueTypeProperty prop in objClass.Properties.OfType<ValueTypeProperty>()
                .Where(p => p.IsList)
                .OrderBy(p => p.Module.Namespace).ThenBy(p => p.Name))
            {
                var tblName = db.GetTableName(prop.Module.SchemaName, prop.GetCollectionEntryTable());
                var fkName = await Construct.ForeignKeyColumnName(prop);
                var valPropName = prop.Name;
                var valPropIndexName = prop.Name + "Index";
                var assocName = await prop.GetAssociationName();
                var refTblName = objClass.GetTableRef(db);
                bool hasPersistentOrder = prop.HasPersistentOrder;
                if (db.CheckTableExists(tblName))
                {
                    Log.DebugFormat("{0}", prop.Name);
                    await CheckColumn(tblName, fkName, System.Data.DbType.Int32, 0, 0, false, null);
                    await CheckColumn(tblName, valPropName, prop.GetDbType(), await prop.GetSize(), await prop.GetScale(), false, SchemaManager.GetDefaultConstraint(prop));

                    if (hasPersistentOrder)
                    {
                        await CheckColumn(tblName, valPropIndexName, System.Data.DbType.Int32, 0, 0, true, null);
                    }
                    if (!hasPersistentOrder && db.CheckColumnExists(tblName, valPropIndexName))
                    {
                        Log.WarnFormat("Index Column '{0}' exists but property is not indexed", valPropIndexName);
                    }
                    if (!db.CheckFKConstraintExists(tblName, assocName))
                    {
                        Log.WarnFormat("FK Constraint is missing", prop.Name);
                        if (repair)
                        {
                            db.CreateFKConstraint(tblName, refTblName, fkName, assocName, true);
                        }
                    }
                }
                else
                {
                    Log.WarnFormat("Table '{0}' for Property '{1}' is missing", tblName, prop.Name);
                    if (repair)
                    {
                        await Case.DoNewValueTypePropertyList(objClass, prop);
                    }
                }
            }
        }

        private async Task CheckCompoundObjectCollections(ObjectClass objClass)
        {
            Log.Debug("CompoundObject Collections: ");

            foreach (CompoundObjectProperty prop in objClass.Properties.OfType<CompoundObjectProperty>()
            .Where(p => p.IsList)
            .OrderBy(p => p.Module.Namespace).ThenBy(p => p.Name))
            {
                var tblName = db.GetTableName(prop.Module.SchemaName, prop.GetCollectionEntryTable());
                var fkName = await Construct.ForeignKeyColumnName(prop);
                var basePropName = prop.Name;
                var valPropIndexName = prop.Name + "Index";
                var assocName = await prop.GetAssociationName();
                var refTblName = objClass.GetTableRef(db);
                bool hasPersistentOrder = prop.HasPersistentOrder;
                if (db.CheckTableExists(tblName))
                {
                    Log.DebugFormat("{0}", prop.Name);

                    await CheckColumn(tblName, fkName, System.Data.DbType.Int32, 0, 0, false, null);
                    // TODO: Support nested CompoundObject
                    foreach (ValueTypeProperty p in prop.CompoundObjectDefinition.Properties)
                    {
                        await CheckColumn(tblName, Construct.ColumnName(p, basePropName), p.GetDbType(), await p.GetSize(), await p.GetScale(), true, null);
                    }
                    if (hasPersistentOrder)
                    {
                        await CheckColumn(tblName, valPropIndexName, System.Data.DbType.Int32, 0, 0, true, null);
                    }
                    if (!hasPersistentOrder && db.CheckColumnExists(tblName, valPropIndexName))
                    {
                        Log.WarnFormat("Index Column '{0}' exists but property is not indexed", valPropIndexName);
                    }
                    if (!db.CheckFKConstraintExists(tblName, assocName))
                    {
                        Log.WarnFormat("FK Constraint is missing", prop.Name);
                        if (repair)
                        {
                            db.CreateFKConstraint(tblName, refTblName, fkName, assocName, true);
                        }
                    }
                }
                else
                {
                    Log.WarnFormat("Table '{0}' for Property '{1}' is missing", tblName, prop.Name);
                    if (repair)
                    {
                        await Case.DoNewCompoundObjectPropertyList(objClass, prop);
                    }
                }
            }
        }

        private Task CheckColumn(TableRef tblName, string colName, System.Data.DbType type, int size, int scale, bool isNullable, DefaultConstraint defConstr)
        {
            if (db.CheckColumnExists(tblName, colName))
            {
                // TODO: Add DataType Check
                {
                    var colIsNullable = db.GetIsColumnNullable(tblName, colName);
                    if (colIsNullable != isNullable)
                    {
                        Log.WarnFormat("Column '{0}'.'{1}' nullable mismatch. Column is {2} but should be {3}", tblName, colName,
                            colIsNullable ? "NULLABLE" : "NOT NULLABLE",
                            isNullable ? "NULLABLE" : "NOT NULLABLE");

                        if (repair)
                        {
                            if (isNullable || (!isNullable && !db.CheckColumnContainsNulls(tblName, colName)))
                            {
                                db.AlterColumn(tblName, colName, type, size, scale, isNullable, defConstr);
                                Log.Info("Fixed");
                            }
                            else if (!isNullable && db.CheckColumnContainsNulls(tblName, colName))
                            {
                                Log.WarnFormat("Column '{0}.{1}' contains NULL values, cannot set NOT NULLABLE", tblName, colName);
                            }
                        }
                    }
                }
                if (type == System.Data.DbType.String)
                {
                    int colSize = db.GetColumnMaxLength(tblName, colName);
                    // TODO: Introduce TextProperty
                    if (size == int.MaxValue)
                    {
                        // TODO: Check if nvarchar(max) or text
                    }
                    else if (colSize != size)
                    {
                        Log.WarnFormat("Column '{0}'.'{1}' length mismatch. Columns length is {2} but should be {3}", tblName, colName, colSize, size);
                        if (repair)
                        {
                            if (size > colSize)
                            {
                                db.AlterColumn(tblName, colName, type, size, scale, isNullable, defConstr);
                                Log.Info("Fixed");
                            }
                            else
                            {
                                Log.WarnFormat("Cannot fix column size as new size is smaller");
                            }
                        }
                    }
                }
                {
                    bool colHasDefValue = db.GetHasColumnDefaultValue(tblName, colName);
                    bool hasDefValue = defConstr != null;
                    if (colHasDefValue != hasDefValue)
                    {
                        Log.WarnFormat("Column '{0}'.'{1}' default value mismatch. Column {2} a default value but should {3}have one", tblName, colName,
                            colHasDefValue ? "has" : "does not have",
                            hasDefValue ? string.Empty : "not ");

                        if (repair)
                        {
                            db.AlterColumn(tblName, colName, type, size, scale, isNullable, defConstr);
                            Log.Info("Fixed");
                        }
                    }
                }
            }
            else
            {
                Log.WarnFormat("Column '{0}'.'{1}' is missing", tblName, colName);
                if (repair)
                {
                    Log.Info("Fixing");
                    if (!isNullable && db.CheckTableContainsData(tblName))
                    {
                        Log.WarnFormat("Table '{0}' contains data, cannot create NOT NULLABLE column '{1}'", tblName, colName);
                        db.CreateColumn(tblName, colName, type, size, scale, true, defConstr);
                    }
                    else
                    {
                        db.CreateColumn(tblName, colName, type, size, scale, isNullable, defConstr);
                    }
                }
            }

            return Task.CompletedTask;
        }

        private async Task CheckOrderColumn(TableRef tblName, string fkName, string indexName)
        {
            await CheckColumn(tblName, indexName, System.Data.DbType.Int32, 0, 0, true, null);
            if (repair)
            {
                using (Log.InfoTraceMethodCallFormat(
                    "CheckOrderColumn",
                    "checking and repairing [{0}].[{1}]",
                    tblName,
                    indexName))
                {
                    db.RepairPositionColumn(tblName, indexName);
                }
            }
            else if (!db.CheckPositionColumnValidity(tblName, fkName, indexName))
            {
                Log.WarnFormat(
                    "Column [{0}][{1}] contains invalid position column entries",
                    tblName,
                    indexName);
            }
        }

        private async Task CheckColumns(ObjectClass objClass, ICollection<Property> properties, string prefix)
        {
            Log.Debug("  Columns: ");
            var tblName = objClass.GetTableRef(db);
            var mapping = objClass.GetTableMapping();

            if (mapping == TableMapping.TPH && objClass.BaseObjectClass == null)
            {
                await CheckColumn(tblName, TableMapper.DiscriminatorColumnName, System.Data.DbType.String, TableMapper.DiscriminatorColumnSize, 0, false, null);
            }

            foreach (ValueTypeProperty prop in properties.OfType<ValueTypeProperty>()
                .Where(p => !p.IsList)
                .OrderBy(p => p.Module.Namespace).ThenBy(p => p.Name))
            {
                var colName = Construct.ColumnName(prop, prefix);
                Log.DebugFormat("    {0}", colName);

                var realIsNullable = await prop.IsNullable();
                if (realIsNullable == false)
                    realIsNullable = mapping == TableMapping.TPH && objClass.BaseObjectClass != null;
                await CheckColumn(tblName, colName, prop.GetDbType(), await prop.GetSize(), await prop.GetScale(), realIsNullable, SchemaManager.GetDefaultConstraint(prop));
            }

            foreach (CompoundObjectProperty sprop in properties.OfType<CompoundObjectProperty>().Where(p => !p.IsList))
            {
                await CheckColumns(objClass, sprop.CompoundObjectDefinition.Properties, Construct.ColumnName(sprop, prefix));
            }
        }
    }
}
