using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
// TODO: Das gehört angeschaut.
using Kistl.Server.Generators.EntityFramework.Implementation;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Generators;
using Kistl.API.Utils;

namespace Kistl.Server.SchemaManagement
{
    public partial class SchemaManager
    {
        public void CheckSchema(bool withRepair)
        {
            using (Logging.Log.TraceMethodCall())
            {
                this.repair = withRepair;
                WriteReportHeader(withRepair ? "Check Schema Report with repair" : "Check Schema Report");

                if (schema.GetQuery<Kistl.App.Base.ObjectClass>().Count() == 0)
                {
                    report.WriteLine("** Error: Current Schema is empty");
                }
                else
                {
                    CheckTables();
                    report.WriteLine();

                    CheckExtraTables();
                    report.WriteLine();

                    CheckRelations();
                    report.WriteLine();

                    CheckInheritance();
                    report.WriteLine();

                    CheckExtraRelations();
                }
            }
        }

        private void CheckInheritance()
        {
            report.WriteLine("Checking Inheritance");
            report.WriteLine("--------------------");

            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().Where(o => o.BaseObjectClass != null).OrderBy(o => o.Module.Namespace).ThenBy(o => o.ClassName))
            {
                report.WriteLine("Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.ClassName);
                string assocName = Construct.InheritanceAssociationName(objClass.BaseObjectClass, objClass);
                if (!db.CheckFKConstraintExists(assocName))
                {
                    report.WriteLine("** Warning: FK Contraint to BaseClass is missing");
                    if (repair)
                    {
                        Case.DoNewObjectClassInheritance(objClass);
                    }
                }
            }
            report.WriteLine();
        }


        private void CheckExtraTables()
        {
            report.WriteLine("Checking extra Tables");
            report.WriteLine("---------------------");

            // All ObjectClasses
            List<string> tableNames = schema.GetQuery<ObjectClass>().Select(o => o.TableName).ToList();

            // Add ValueTypeProperties
            tableNames.AddRange(schema.GetQuery<ValueTypeProperty>().Where(p => p.IsList).ToList()
                .Select(p => p.GetCollectionEntryTable()));

            // Add Relations with sep. Storage
            tableNames.AddRange(schema.GetQuery<Relation>()
                            .Where(r => (int)r.Storage == (int)StorageType.Separate)
                            .ToList()
                            .Select(r => r.GetRelationTableName()));

            foreach (string tblName in db.GetTableNames())
            {
                if (!tableNames.Contains(tblName))
                {
                    report.WriteLine("** Warning: Table '{0}' found in database but no ObjectClass was defined", tblName);
                }
            }
        }

        private void GetExistingColumnNames(ObjectClass objClass, ICollection<Property> properties, string prefix, List<string> columns)
        {
            columns.AddRange(properties.OfType<ValueTypeProperty>().Where(p => !p.IsList && p.HasStorage()).Select(p => Construct.NestedColumnName(p, prefix)).ToArray());

            foreach (StructProperty sprop in properties.OfType<StructProperty>().Where(p => !p.IsList && p.HasStorage()))
            {
                GetExistingColumnNames(objClass, sprop.StructDefinition.Properties, Construct.NestedColumnName(sprop, prefix), columns);
            }
        }

        private void CheckExtraColumns(ObjectClass objClass)
        {
            report.WriteLine("  Extra Columns: ");
            List<string> columns = new List<string>();
            GetExistingColumnNames(objClass, objClass.Properties, "", columns);

            foreach (string propName in db.GetTableColumnNames(objClass.TableName))
            {
                if (propName == "ID") continue;
                if (propName.StartsWith("fk_")) continue;
                if (!columns.Contains(propName))
                {
                    report.WriteLine("    ** Warning: Column '{0}' found in database but no Property was defined", propName);
                }
            }
        }

        private void CheckExtraRelations()
        {
            report.WriteLine("Checking extra Relations");
            report.WriteLine("------------------------");

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
                if (rel.A.Navigator != null && rel.A.Navigator.HasStorage())
                    relationNames.Add(rel.GetRelationAssociationName(RelationEndRole.A));
                if (rel.B.Navigator != null && rel.B.Navigator.HasStorage())
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

            foreach (var rel in db.GetFKConstraintNames())
            {
                if (!relationNames.Contains(rel.ConstraintName))
                {
                    report.WriteLine("** Warning: Relation '{0}' on table '{1}' found in database but no relation object was defined", rel.ConstraintName, rel.TableName);
                    if (repair)
                    {
                        db.DropFKConstraint(rel.TableName, rel.ConstraintName);
                    }
                }
            }
        }

        private void CheckRelations()
        {
            report.WriteLine("Checking Relations");
            report.WriteLine("------------------");
            foreach (Relation rel in schema.GetQuery<Relation>())
            {
                string assocName = rel.GetAssociationName();
                report.WriteLine("Relation: \"{0}\" \"{1} - {2}\"", assocName, rel.A.Type.ClassName, rel.B.Type.ClassName);
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
            if (relEnd.Navigator != null && relEnd.Navigator.HasStorage())
            {
                string tblName = relEnd.Type.TableName;
                RelationEnd otherEnd = rel.GetOtherEnd(relEnd);
                string refTblName = otherEnd.Type.TableName;
                string colName = Construct.ForeignKeyColumnName(relEnd.Navigator);
                string assocName = rel.GetRelationAssociationName(role);

                if (!db.CheckFKConstraintExists(assocName))
                {
                    report.WriteLine("  ** Warning: FK Constraint '{0}' is missing", assocName);
                    if (repair)
                    {
                        db.CreateFKConstraint(tblName, refTblName, colName, assocName, false);
                    }
                }
                CheckColumn(relEnd.Type.TableName, Construct.ForeignKeyColumnName(relEnd.Navigator), System.Data.DbType.Int32, 0, otherEnd.IsNullable());
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
                    report.WriteLine("    ** Warning: Relation '{0}' has unsupported Storage set: {1}", assocName, rel.Storage);
                    return;
            }

            ObjectReferenceProperty nav = relEnd.Navigator;
            string tblName = relEnd.Type.TableName;
            string refTblName = otherEnd.Type.TableName;
            bool isIndexed = rel.NeedsPositionStorage(relEnd.GetRole());

            if (nav == null)
            {
                report.WriteLine("    ** Warning: Relation '{0}' has no Navigator", assocName);
                return;
            }

            string colName = Construct.ForeignKeyColumnName(nav);
            string indexName = Construct.ListPositionColumnName(nav);

            if (!db.CheckFKConstraintExists(assocName))
            {
                report.WriteLine("  ** Warning: FK Constraint '{0}' is missing", assocName);
                if (repair)
                {
                    db.CreateFKConstraint(tblName, refTblName, colName, assocName, false);
                }
            }

            CheckColumn(tblName, colName, System.Data.DbType.Int32, 0, otherEnd.IsNullable());
            if (isIndexed)
            {
                CheckColumn(tblName, indexName, System.Data.DbType.Int32, 0, otherEnd.IsNullable());
            }
            if (!isIndexed && db.CheckColumnExists(tblName, indexName))
            {
                report.WriteLine("  ** Warning: Index Column exists but property is not indexed");
                if (repair)
                {
                    Case.Do_1_N_RelationChange_FromIndexed_To_NotIndexed(rel);
                }
            }
        }

        private void Check_N_M_RelationColumns(Relation rel)
        {
            string assocName = rel.GetAssociationName();

            string tblName = rel.GetRelationTableName();
            string fkAName = rel.GetRelationFkColumnName(RelationEndRole.A);
            string fkBName = rel.GetRelationFkColumnName(RelationEndRole.B);
            string assocAName = rel.GetRelationAssociationName(RelationEndRole.A);
            string assocBName = rel.GetRelationAssociationName(RelationEndRole.B);
            string fkAIndex = fkAName + Kistl.API.Helper.PositionSuffix;
            string fkBIndex = fkBName + Kistl.API.Helper.PositionSuffix;

            if (!db.CheckTableExists(tblName))
            {
                report.WriteLine("  ** Warning: Relation table '{0}' is missing", tblName);
                return;
            }

            if (!db.CheckFKConstraintExists(assocAName))
            {
                report.WriteLine("  ** Warning: FK Constraint '{0}' for A is missing", assocAName);
                if (repair)
                {
                    db.CreateFKConstraint(tblName, rel.A.Type.TableName, fkAName, assocAName, false);
                }
            }
            if (!db.CheckFKConstraintExists(assocBName))
            {
                report.WriteLine("  ** Warning: FK Constraint '{0}' for B is missing", assocBName);
                if (repair)
                {
                    db.CreateFKConstraint(tblName, rel.B.Type.TableName, fkBName, assocBName, false);
                }
            }

            CheckColumn(tblName, fkAName, System.Data.DbType.Int32, 0, false);
            if (rel.NeedsPositionStorage(RelationEndRole.A))
            {
                CheckColumn(tblName, fkAIndex, System.Data.DbType.Int32, 0, false);
                if (repair)
                {
                    // TODO: Call case
                }
            }
            if (!rel.NeedsPositionStorage(RelationEndRole.A) && db.CheckColumnExists(tblName, fkAIndex))
            {
                report.WriteLine("  ** Warning: Navigator A '{0}' Index Column exists but property is not indexed", fkAName);
                if (repair)
                {
                    // TODO: Call case
                }
            }

            CheckColumn(tblName, fkBName, System.Data.DbType.Int32, 0, false);
            if (rel.NeedsPositionStorage(RelationEndRole.B))
            {
                CheckColumn(tblName, fkBIndex, System.Data.DbType.Int32, 0, false);
                if (repair)
                {
                    // TODO: Call case
                }
            }
            if (!rel.NeedsPositionStorage(RelationEndRole.B) && db.CheckColumnExists(tblName, fkBIndex))
            {
                report.WriteLine("  ** Warning: Navigator B '{0}' Index Column exists but property is not indexed", fkBName);
                if (repair)
                {
                    // TODO: Call case
                }
            }

            if (rel.A.Type.ImplementsIExportable(schema) && rel.B.Type.ImplementsIExportable(schema))
            {
                CheckColumn(tblName, "ExportGuid", System.Data.DbType.Guid, 0, false);
            }
        }

        private void CheckTables()
        {
            report.WriteLine("Checking Tables & Columns");
            report.WriteLine("-------------------------");
            // Checking Tables
            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().OrderBy(o => o.Module.Namespace).ThenBy(o => o.ClassName))
            {
                report.WriteLine("Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.ClassName);

                if (db.CheckTableExists(objClass.TableName))
                {
                    report.WriteLine("  Table: {0}", objClass.TableName);
                    CheckColumns(objClass, objClass.Properties, "");
                    CheckValueTypeCollections(objClass);
                    CheckExtraColumns(objClass);
                }
                else
                {
                    report.WriteLine("  ** Warning: Table '{0}' is missing", objClass.TableName);
                }
            }
        }
        private void CheckValueTypeCollections(ObjectClass objClass)
        {
            report.WriteLine("  ValueType Collections: ");
            foreach (ValueTypeProperty prop in objClass.Properties.OfType<ValueTypeProperty>()
                .Where(p => p.IsList)
                .OrderBy(p => p.Module.Namespace).ThenBy(p => p.PropertyName))
            {
                string tblName = prop.GetCollectionEntryTable();
                string fkName = "fk_" + prop.ObjectClass.ClassName;
                string valPropName = prop.PropertyName;
                string valPropIndexName = prop.PropertyName + "Index";
                string assocName = prop.GetAssociationName();
                string refTblName = objClass.TableName;
                if (db.CheckTableExists(tblName))
                {
                    report.WriteLine("    {0}", prop.PropertyName);
                    CheckColumn(tblName, fkName, System.Data.DbType.Int32, 0, false);
                    CheckColumn(tblName, valPropName, GetDbType(prop), prop is StringProperty ? ((StringProperty)prop).Length : 0, false);
                    if (prop.IsIndexed)
                    {
                        CheckColumn(tblName, valPropIndexName, System.Data.DbType.Int32, 0, false);
                    }
                    if (!prop.IsIndexed && db.CheckColumnExists(tblName, valPropIndexName))
                    {
                        report.WriteLine("      ** Warning: Index Column '{0}' exists but property is not indexed", valPropIndexName);
                    }
                    if (!db.CheckFKConstraintExists(assocName))
                    {
                        report.WriteLine("      ** Warning: FK Constraint is missing", prop.PropertyName);
                        if (repair)
                        {
                            db.CreateFKConstraint(tblName, refTblName, fkName, assocName, true);
                        }
                    }
                }
                else
                {
                    report.WriteLine("    ** Warning: Table '{0}' for Property '{1}' is missing", tblName, prop.PropertyName);
                    if (repair)
                    {
                        Case.DoNewValueTypePropertyList(objClass, prop);
                    }
                }
            }
        }

        private void CheckColumn(string tblName, string colName, System.Data.DbType type, int size, bool isNullable)
        {
            if (db.CheckColumnExists(tblName, colName))
            {
                // TODO: Add DataType Check
                bool colIsNullable = db.GetIsColumnNullable(tblName, colName);
                if (colIsNullable != isNullable)
                {
                    report.WriteLine("      ** Warning: Column '{0}'.'{1}' nullable mismatch. Column is {2} but should be {3}", tblName, colName,
                        colIsNullable ? "NULLABLE" : "NOT NULLABLE",
                        isNullable ? "NULLABLE" : "NOT NULLABLE");

                    if (repair)
                    {
                        if (isNullable || (!isNullable && !db.CheckColumnContainsNulls(tblName, colName)))
                        {
                            // not calling case because we already have all neccessary information
                            db.AlterColumn(tblName, colName, type, size, isNullable);
                            report.WriteLine("      ** Fixed.");
                        }
                        else if (!isNullable && db.CheckColumnContainsNulls(tblName, colName))
                        {
                            report.WriteLine("      ** Warning: column '{0}.{1}' contains NULL values, cannot set NOT NULLABLE", tblName, colName);
                        }
                    }
                }
                if (type == System.Data.DbType.String)
                {
                    int colSize = db.GetColumnMaxLength(tblName, colName);
                    // TODO: Introduce TextProperty
                    if (size > 1000)
                    {
                        // TODO: Check if ntext
                    }
                    else if (colSize != size)
                    {
                        report.WriteLine("      ** Warning: Column '{0}'.'{1}' length mismatch. Columns length is {2} but should be {3}", tblName, colName, colSize, size);
                    }
                }
            }
            else
            {
                report.WriteLine("    ** Warning: Column '{0}'.'{1}' is missing", tblName, colName);
            }
        }

        private void CheckColumns(ObjectClass objClass, ICollection<Property> properties, string prefix)
        {
            report.WriteLine("  Columns: ");
            foreach (ValueTypeProperty prop in properties.OfType<ValueTypeProperty>()
                .Where(p => !p.IsList && p.HasStorage())
                .OrderBy(p => p.Module.Namespace).ThenBy(p => p.PropertyName))
            {
                string tblName = objClass.TableName;
                string colName = Construct.NestedColumnName(prop, prefix);
                report.WriteLine("    {0}", colName);
                CheckColumn(tblName, colName, GetDbType(prop), prop is StringProperty ? ((StringProperty)prop).Length : 0, prop.IsNullable());
            }

            foreach (StructProperty sprop in properties.OfType<StructProperty>().Where(p => !p.IsList && p.HasStorage()))
            {
                CheckColumns(objClass, sprop.StructDefinition.Properties, Construct.NestedColumnName(sprop, prefix));
            }
        }
    }
}
