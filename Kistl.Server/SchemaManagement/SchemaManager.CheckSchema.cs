
namespace Kistl.Server.SchemaManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Server;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Generator;
    using Kistl.Generator.Extensions;

    public partial class SchemaManager
    {
        public void CheckSchema(bool withRepair)
        {
            using (Log.DebugTraceMethodCall())
            {
                this.repair = withRepair;
                WriteReportHeader(withRepair ? "Check Schema Report with repair" : "Check Schema Report");

                if (schema.GetQuery<Kistl.App.Base.ObjectClass>().Count() == 0)
                {
                    Log.Error("Current Schema is empty, aborting");
                }
                else
                {
                    CheckTables();
                    Log.Debug(String.Empty);

                    CheckUpdateRightsTrigger();
                    Log.Debug(String.Empty);

                    UpdateProcedures();
                    Log.Debug(String.Empty);

                    CheckExtraTables();
                    Log.Debug(String.Empty);

                    CheckRelations();
                    Log.Debug(String.Empty);

                    CheckInheritance();
                    Log.Debug(String.Empty);

                    CheckExtraRelations();

                    if (withRepair)
                        db.RefreshDbStats();
                }
            }
        }

        private void CheckUpdateRightsTrigger()
        {
            Log.InfoFormat("Checking UpdateRightsTrigger");
            // Select all ObjectClasses with ACL
            foreach (var objClass in schema.GetQuery<ObjectClass>().ToList().Where(o => o.NeedsRightsTable()).OrderBy(o => o.Module.Namespace).ThenBy(o => o.Name))
            {
                Log.DebugFormat("Table: {0}", objClass.TableName);
                if (repair)
                {
                    Case.DoCreateUpdateRightsTrigger(objClass);
                }
                else
                {
                    var updateRightsTriggerName = Construct.SecurityRulesUpdateRightsTriggerName(objClass);
                    var tblName = db.GetQualifiedTableName(objClass.TableName);
                    if (!db.CheckTriggerExists(tblName, updateRightsTriggerName))
                    {
                        Log.WarnFormat("Security Rules Trigger '{0}' is missing", updateRightsTriggerName);
                    }
                }
            }

            // Select all n:m Relations that are in a ACL selector
            foreach (Relation rel in schema.GetQuery<Relation>().ToList()
                .Where(r => r.GetRelationType() == RelationType.n_m && schema.GetQuery<RoleMembership>().Where(rm => rm.Relations.Contains(r)).Count() > 0))
            {
                Log.DebugFormat("Relation: {0}, {1} <-> {2}", rel.Description, rel.A.Type.TableName, rel.B.Type.TableName);

                if (repair)
                {
                    Case.DoCreateUpdateRightsTrigger(rel);
                }
                else
                {
                    var updateRightsTriggerName = Construct.SecurityRulesUpdateRightsTriggerName(rel);
                    var tblName = db.GetQualifiedTableName(rel.GetRelationTableName());
                    if (!db.CheckTriggerExists(tblName, updateRightsTriggerName))
                    {
                        Log.WarnFormat("Security Rules Trigger '{0}' is missing", updateRightsTriggerName);
                    }
                }
            }
        }

        private void CheckInheritance()
        {
            Log.Info("Checking Inheritance");
            Log.Debug("--------------------");

            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().Where(o => o.BaseObjectClass != null).OrderBy(o => o.Module.Namespace).ThenBy(o => o.Name))
            {
                Log.DebugFormat("Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.Name);
                string assocName = Construct.InheritanceAssociationName(objClass.BaseObjectClass, objClass);
                var tblName = db.GetQualifiedTableName(objClass.TableName);
                if (!db.CheckFKConstraintExists(tblName, assocName))
                {
                    Log.WarnFormat("FK Constraint to BaseClass is missing on Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.Name);
                    if (repair)
                    {
                        Case.DoNewObjectClassInheritance(objClass);
                    }
                }
            }
            Log.Debug(String.Empty);
        }

        private void CheckExtraTables()
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
        }

        private void GetExistingColumnNames(ObjectClass objClass, ICollection<Property> properties, string prefix, List<string> columns)
        {
            columns.AddRange(properties.OfType<ValueTypeProperty>().Where(p => !p.IsList).Select(p => Construct.NestedColumnName(p, prefix)).ToArray());

            foreach (CompoundObjectProperty sprop in properties.OfType<CompoundObjectProperty>().Where(p => !p.IsList))
            {
                columns.Add(Construct.NestedColumnName(sprop, prefix));
                GetExistingColumnNames(objClass, sprop.CompoundObjectDefinition.Properties, Construct.NestedColumnName(sprop, prefix), columns);
            }
        }
        private void GetRelationColumnNames(ObjectClass objClass, List<string> columns)
        {
            foreach (var relEnd in schema.GetQuery<RelationEnd>().Where(e => e.Type == objClass))
            {
                if (relEnd.Parent.HasStorage(relEnd.GetRole()))
                {
                    columns.Add(Construct.ForeignKeyColumnName(relEnd.Parent.GetOtherEnd(relEnd)));
                    if (relEnd.Parent.NeedsPositionStorage(relEnd.GetRole()))
                    {
                        columns.Add(Construct.ListPositionColumnName(relEnd.Parent.GetOtherEnd(relEnd)));
                    }
                }
            }
        }

        private void CheckExtraColumns(ObjectClass objClass)
        {
            Log.Debug("Extra Columns: ");
            List<string> columns = new List<string>();
            GetExistingColumnNames(objClass, objClass.Properties, String.Empty, columns);
            GetRelationColumnNames(objClass, columns);

            foreach (string propName in db.GetTableColumnNames(db.GetQualifiedTableName(objClass.TableName)))
            {
                if (propName == "ID")
                    continue;
                if (!columns.Contains(propName))
                {
                    Log.WarnFormat("Column '[{0}].[{1}]' found in database but no Property was defined", objClass.TableName, propName);
                }
            }
        }

        private void CheckExtraRelations()
        {
            Log.Info("Checking extra Relations");
            Log.Debug("------------------------");

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
                if (rel.A.Navigator != null && rel.HasStorage(RelationEndRole.A))
                    relationNames.Add(rel.GetRelationAssociationName(RelationEndRole.A));
                if (rel.B.Navigator != null && rel.HasStorage(RelationEndRole.B))
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

            foreach (CompoundObjectProperty prop in schema.GetQuery<CompoundObjectProperty>().Where(p => p.IsList))
            {
                relationNames.Add(prop.GetAssociationName());
            }

            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().ToList().Where(o => o.NeedsRightsTable()))
            {
                relationNames.Add(Construct.SecurityRulesFKName(objClass));
            }

            foreach (var rel in db.GetFKConstraintNames())
            {
                if (!relationNames.Contains(rel.ConstraintName))
                {
                    Log.WarnFormat("'{0}' on table '{1}' found in database but no relation object was defined", rel.ConstraintName, rel.TableName);
                    if (repair)
                    {
                        db.DropFKConstraint(rel.TableName, rel.ConstraintName);
                    }
                }
            }
        }

        private void CheckRelations()
        {
            Log.Info("Checking Relations");
            Log.Debug("------------------");
            foreach (Relation rel in schema.GetQuery<Relation>())
            {
                string assocName = rel.GetAssociationName();
                Log.DebugFormat("Relation: \"{0}\" \"{1} - {2}\"", assocName, rel.A.Type.Name, rel.B.Type.Name);
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
            if (rel.HasStorage(role))
            {
                var tblName = db.GetQualifiedTableName(relEnd.Type.TableName);
                RelationEnd otherEnd = rel.GetOtherEnd(relEnd);
                var refTblName = db.GetQualifiedTableName(otherEnd.Type.TableName);
                string colName = Construct.ForeignKeyColumnName(otherEnd);
                string assocName = rel.GetRelationAssociationName(role);
                string idxName = Construct.IndexName(tblName.Name, colName);

                CheckColumn(tblName, Construct.ForeignKeyColumnName(otherEnd), System.Data.DbType.Int32, 0, 0, otherEnd.IsNullable(), null);
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

        private void Check_1_N_RelationColumns(Relation rel)
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

            var tblName = db.GetQualifiedTableName(relEnd.Type.TableName);
            var refTblName = db.GetQualifiedTableName(otherEnd.Type.TableName);
            bool isIndexed = rel.NeedsPositionStorage(relEnd.GetRole());

            string colName = Construct.ForeignKeyColumnName(otherEnd);
            string indexName = Construct.ListPositionColumnName(otherEnd);

            CheckColumn(tblName, colName, System.Data.DbType.Int32, 0, 0, otherEnd.IsNullable(), null);

            if (!db.CheckFKConstraintExists(tblName, assocName))
            {
                Log.WarnFormat("FK Constraint '{0}' is missing", assocName);
                if (repair)
                {
                    db.CreateFKConstraint(tblName, refTblName, colName, assocName, false);
                }
            }

            if (isIndexed)
            {
                CheckOrderColumn(tblName, indexName);
            }
            if (!isIndexed && db.CheckColumnExists(tblName, indexName))
            {
                Log.WarnFormat("Index Column exists but property is not indexed");
                if (repair)
                {
                    Case.Do_1_N_RelationChange_FromIndexed_To_NotIndexed(rel);
                }
            }
        }

        private void Check_N_M_RelationColumns(Relation rel)
        {
            string assocName = rel.GetAssociationName();

            var tblName = db.GetQualifiedTableName(rel.GetRelationTableName());
            string fkAName = rel.GetRelationFkColumnName(RelationEndRole.A);
            string fkBName = rel.GetRelationFkColumnName(RelationEndRole.B);
            string assocAName = rel.GetRelationAssociationName(RelationEndRole.A);
            string assocBName = rel.GetRelationAssociationName(RelationEndRole.B);
            string fkAIndex = fkAName + Kistl.API.Helper.PositionSuffix;
            string fkBIndex = fkBName + Kistl.API.Helper.PositionSuffix;

            Log.DebugFormat("Checking [{0}]", assocName);

            if (!db.CheckTableExists(tblName))
            {
                Log.WarnFormat("Relation table '{0}' is missing for [{1}]", tblName, assocName);
                if (repair)
                {
                    Case.DoNew_N_M_Relation(rel);
                }
                return;
            }
            CheckColumn(tblName, fkAName, System.Data.DbType.Int32, 0, 0, false, null);
            CheckColumn(tblName, fkBName, System.Data.DbType.Int32, 0, 0, false, null);

            if (!db.CheckFKConstraintExists(tblName, assocAName))
            {
                Log.WarnFormat("FK Constraint '{0}' for A is missing for [{1}]", assocAName, assocName);
                if (repair)
                {
                    db.CreateFKConstraint(tblName, db.GetQualifiedTableName(rel.A.Type.TableName), fkAName, assocAName, true);
                }
            }
            if (!db.CheckFKConstraintExists(tblName, assocBName))
            {
                Log.WarnFormat("FK Constraint '{0}' for B is missing for [{1}]", assocBName, assocName);
                if (repair)
                {
                    db.CreateFKConstraint(tblName, db.GetQualifiedTableName(rel.B.Type.TableName), fkBName, assocBName, true);
                }
            }

            if (rel.NeedsPositionStorage(RelationEndRole.A))
            {
                CheckColumn(tblName, fkAIndex, System.Data.DbType.Int32, 0, 0, true, null);
                if (repair)
                {
                    // TODO: Call case
                }
            }
            if (!rel.NeedsPositionStorage(RelationEndRole.A) && db.CheckColumnExists(tblName, fkAIndex))
            {
                Log.WarnFormat("Navigator A '{0}' Index Column exists but property is not indexed for [{1}]", fkAName, assocName);
                if (repair)
                {
                    // TODO: Call case
                }
            }

            if (rel.NeedsPositionStorage(RelationEndRole.B))
            {
                CheckColumn(tblName, fkBIndex, System.Data.DbType.Int32, 0, 0, true, null);
                if (repair)
                {
                    // TODO: Call case
                }
            }
            if (!rel.NeedsPositionStorage(RelationEndRole.B) && db.CheckColumnExists(tblName, fkBIndex))
            {
                Log.WarnFormat("Navigator B '{0}' Index Column exists but property is not indexed for [{1}]", fkBName, assocName);
                if (repair)
                {
                    // TODO: Call case
                }
            }

            if (rel.A.Type.ImplementsIExportable() && rel.B.Type.ImplementsIExportable())
            {
                CheckColumn(tblName, "ExportGuid", System.Data.DbType.Guid, 0, 0, false, new NewGuidDefaultConstraint());
            }
        }

        private void CheckTables()
        {
            Log.Info("Checking Tables & Columns");
            Log.Debug("-------------------------");

            // Checking Tables
            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().OrderBy(o => o.Module.Namespace).ThenBy(o => o.Name))
            {
                Log.DebugFormat("Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.Name);

                if (db.CheckTableExists(db.GetQualifiedTableName(objClass.TableName)))
                {
                    Log.DebugFormat("  Table: {0}", objClass.TableName);
                    CheckColumns(objClass, objClass.Properties, String.Empty);
                    CheckUniqueConstraints(objClass);
                    CheckValueTypeCollections(objClass);
                    CheckCompoundObjectCollections(objClass);
                    CheckExtraColumns(objClass);
                    CheckTableSecurityRules(objClass);
                }
                else
                {
                    Log.WarnFormat("Table '{0}' is missing", objClass.TableName);
                }
            }
        }

        private void CheckUniqueConstraints(ObjectClass objClass)
        {
            foreach (var uc in objClass.Constraints.OfType<UniqueConstraint>())
            {
                var tblName = db.GetQualifiedTableName(objClass.TableName);
                var columns = Cases.GetUCColNames(uc);
                var idxName = Construct.UniqueIndexName(tblName.Name, columns);
                if (!db.CheckIndexExists(tblName, idxName))
                {
                    Log.WarnFormat("Unique Constraint '{0}' is missing", idxName);
                    if (repair)
                    {
                        Case.DoNewUniqueConstraint(uc);
                    }
                }
            }
        }

        private void CheckTableSecurityRules(ObjectClass objClass)
        {
            if (objClass.NeedsRightsTable())
            {
                var tblName = db.GetQualifiedTableName(objClass.TableName);
                var tblRightsName = db.GetQualifiedTableName(Construct.SecurityRulesTableName(objClass));
                var updateRightsTriggerName = Construct.SecurityRulesUpdateRightsTriggerName(objClass);
                var rightsViewUnmaterializedName = db.GetQualifiedTableName(Construct.SecurityRulesRightsViewUnmaterializedName(objClass));
                var refreshRightsOnProcedureName = db.GetQualifiedProcedureName(Construct.SecurityRulesRefreshRightsOnProcedureName(objClass));

                if (!db.CheckTableExists(tblRightsName))
                {
                    Log.WarnFormat("Security Rules Table '{0}' is missing", tblRightsName);
                    if (repair)
                    {
                        Case.DoNewObjectClassACL(objClass);
                    }
                }
                else
                {
                    if (!db.CheckViewExists(rightsViewUnmaterializedName))
                    {
                        Log.WarnFormat("Security Rules unmaterialized View '{0}' is missing", rightsViewUnmaterializedName);
                        if (repair)
                        {
                            Case.DoCreateRightsViewUnmaterialized(objClass);
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

        private void CheckValueTypeCollections(ObjectClass objClass)
        {
            Log.Debug("ValueType Collections: ");

            foreach (ValueTypeProperty prop in objClass.Properties.OfType<ValueTypeProperty>()
            .Where(p => p.IsList)
            .OrderBy(p => p.Module.Namespace).ThenBy(p => p.Name))
            {
                var tblName = db.GetQualifiedTableName(prop.GetCollectionEntryTable());
                var fkName = "fk_" + prop.ObjectClass.Name;
                var valPropName = prop.Name;
                var valPropIndexName = prop.Name + "Index";
                var assocName = prop.GetAssociationName();
                var refTblName = db.GetQualifiedTableName(objClass.TableName);
                bool hasPersistentOrder = prop.HasPersistentOrder;
                if (db.CheckTableExists(tblName))
                {
                    Log.DebugFormat("{0}", prop.Name);
                    CheckColumn(tblName, fkName, System.Data.DbType.Int32, 0, 0, false, null);
                    CheckColumn(tblName, valPropName, prop.GetDbType(), prop.GetSize(), prop.GetScale(), false, SchemaManager.GetDefaultContraint(prop));

                    if (hasPersistentOrder)
                    {
                        CheckColumn(tblName, valPropIndexName, System.Data.DbType.Int32, 0, 0, true, null);
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
                        Case.DoNewValueTypePropertyList(objClass, prop);
                    }
                }
            }
        }

        private void CheckCompoundObjectCollections(ObjectClass objClass)
        {
            Log.Debug("CompoundObject Collections: ");

            foreach (CompoundObjectProperty prop in objClass.Properties.OfType<CompoundObjectProperty>()
            .Where(p => p.IsList)
            .OrderBy(p => p.Module.Namespace).ThenBy(p => p.Name))
            {
                var tblName = db.GetQualifiedTableName(prop.GetCollectionEntryTable());
                var fkName = "fk_" + prop.ObjectClass.Name;
                var valPropName = prop.Name;
                var valPropIndexName = prop.Name + "Index";
                var assocName = prop.GetAssociationName();
                var refTblName = db.GetQualifiedTableName(objClass.TableName);
                bool hasPersistentOrder = prop.HasPersistentOrder;
                if (db.CheckTableExists(tblName))
                {
                    Log.DebugFormat("{0}", prop.Name);

                    // Check isnull column
                    // TODO: Support neested CompoundObject
                    string colName = Construct.NestedColumnName(prop, string.Empty);
                    CheckColumn(tblName, colName, System.Data.DbType.Boolean, 0, 0, false, null);

                    CheckColumn(tblName, fkName, System.Data.DbType.Int32, 0, 0, false, null);
                    // TODO: Support neested CompoundObject
                    foreach (ValueTypeProperty p in prop.CompoundObjectDefinition.Properties)
                    {
                        CheckColumn(tblName, valPropName + "_" + p.Name, p.GetDbType(), p.GetSize(), p.GetScale(), true, SchemaManager.GetDefaultContraint(p));
                    }
                    if (hasPersistentOrder)
                    {
                        CheckColumn(tblName, valPropIndexName, System.Data.DbType.Int32, 0, 0, true, null);
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
                        Case.DoNewCompoundObjectPropertyList(objClass, prop);
                    }
                }
            }
        }

        private void CheckColumn(TableRef tblName, string colName, System.Data.DbType type, int size, int scale, bool isNullable, DefaultConstraint defConstr)
        {
            if (db.CheckColumnExists(tblName, colName))
            {
                // TODO: Add DataType Check
                {
                    bool colIsNullable = db.GetIsColumnNullable(tblName, colName);
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
                        // TODO: Check if ntext
                    }
                    else if (colSize != size)
                    {
                        Log.WarnFormat("Column '{0}'.'{1}' length mismatch. Columns length is {2} but should be {3}", tblName, colName, colSize, size);
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
        }

        private void CheckOrderColumn(TableRef tblName, string indexName)
        {
            CheckColumn(tblName, indexName, System.Data.DbType.Int32, 0, 0, true, null);
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
            else if (!db.CheckPositionColumnValidity(tblName, indexName))
            {
                Log.WarnFormat(
                    "Column [{0}][{1}] contains invalid position column entries",
                    tblName,
                    indexName);
            }
        }

        private void CheckColumns(ObjectClass objClass, ICollection<Property> properties, string prefix)
        {
            Log.Debug("  Columns: ");
            foreach (ValueTypeProperty prop in properties.OfType<ValueTypeProperty>()
                .Where(p => !p.IsList)
                .OrderBy(p => p.Module.Namespace).ThenBy(p => p.Name))
            {
                var tblName = db.GetQualifiedTableName(objClass.TableName);
                var colName = Construct.NestedColumnName(prop, prefix);
                Log.DebugFormat("    {0}", colName);
                CheckColumn(tblName, colName, prop.GetDbType(), prop.GetSize(), prop.GetScale(), prop.IsNullable(), SchemaManager.GetDefaultContraint(prop));
            }

            foreach (CompoundObjectProperty sprop in properties.OfType<CompoundObjectProperty>().Where(p => !p.IsList))
            {
                // Check isnull column
                var tblName = db.GetQualifiedTableName(objClass.TableName);
                var colName_IsNull = Construct.NestedColumnName(sprop, prefix);
                Log.DebugFormat("    {0}", colName_IsNull);
                CheckColumn(tblName, colName_IsNull, System.Data.DbType.Boolean, 0, 0, false, new BoolDefaultConstraint() { Value = true });

                // Check other columns
                CheckColumns(objClass, sprop.CompoundObjectDefinition.Properties, Construct.NestedColumnName(sprop, prefix));
            }
        }
    }
}
