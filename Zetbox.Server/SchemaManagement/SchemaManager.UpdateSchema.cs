
namespace Zetbox.Server.SchemaManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
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

                    UpdateDatabaseSchemas();
                    UpdateTables();
                    UpdateRelations();
                    UpdateInheritance();
                    UpdateSecurityTables();
                    UpdateConstraints();

                    UpdateDeletedRelations();
                    UpdateDeletedTables();

                    UpdateProcedures();

                    SaveSchema(schema);

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

            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().OrderBy(o => o.Module.Namespace).ThenBy(o => o.Name))
            {
                Log.DebugFormat("Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.Name);

                // Delete early to avoid collisions with newly created columns (like changing data type)
                // Note: migration of data types is not supported now. Only chance is to delete and recreate a column
                UpdateDeletedColumns(objClass, String.Empty);

                if (Case.IsNewObjectClass(objClass))
                {
                    Case.DoNewObjectClass(objClass);
                }
                if (Case.IsRenameObjectClassTable(objClass))
                {
                    Case.DoRenameObjectClassTable(objClass);
                }

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
                    UpdateColumns(objClass, sprop.CompoundObjectDefinition.Properties, Construct.NestedColumnName(sprop, prefix));
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
            foreach (ValueTypeProperty prop in Case.savedSchema.GetQuery<ValueTypeProperty>().Where(p => p.ObjectClass.ExportGuid == objClass.ExportGuid && !p.IsList))
            {
                if (Case.IsDeleteValueTypeProperty(prop))
                {
                    Case.DoDeleteValueTypeProperty(objClass, prop, prefix);
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

            foreach (Relation rel in schema.GetQuery<Relation>().OrderBy(r => r.Module.Namespace))
            {
                Log.DebugFormat("Relation: {0} ({1})", rel.GetAssociationName(), rel.GetRelationType());

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
                else
                {
                    if (rel.GetRelationType() == RelationType.one_n)
                    {
                        if (Case.IsNew_1_N_Relation(rel))
                        {
                            Case.DoNew_1_N_Relation(rel);
                        }
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
                        if (Case.IsNew_N_M_Relation(rel))
                        {
                            Case.DoNew_N_M_Relation(rel);
                        }
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
                        if (Case.IsNew_1_1_Relation(rel))
                        {
                            Case.DoNew_1_1_Relation(rel);
                        }
                        if (Case.IsChange_1_1_Storage(rel))
                        {
                            Case.DoChange_1_1_Storage(rel);
                        }

                        if (Case.Is1_1_RelationChange_FromNotNullable_To_Nullable(rel, RelationEndRole.A))
                        {
                            Case.Do1_1_RelationChange_FromNotNullable_To_Nullable(rel, RelationEndRole.A);
                        }
                        if (Case.Is1_1_RelationChange_FromNotNullable_To_Nullable(rel, RelationEndRole.B))
                        {
                            Case.Do1_1_RelationChange_FromNotNullable_To_Nullable(rel, RelationEndRole.B);
                        }

                        if (Case.Is1_1_RelationChange_FromNullable_To_NotNullable(rel, RelationEndRole.A))
                        {
                            Case.Do1_1_RelationChange_FromNullable_To_NotNullable(rel, RelationEndRole.A);
                        }
                        if (Case.Is1_1_RelationChange_FromNullable_To_NotNullable(rel, RelationEndRole.B))
                        {
                            Case.Do1_1_RelationChange_FromNullable_To_NotNullable(rel, RelationEndRole.B);
                        }
                    }

                    if (Case.IsChangeRelationEndTypes(rel))
                    {
                        Case.DoChangeRelationEndTypes(rel);
                    }

                    if (Case.IsChangeRelationName(rel))
                    {
                        Case.DoChangeRelationName(rel);
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
                Log.DebugFormat("Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.Name);
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
            Log.Debug(String.Empty);
        }
    }
}
