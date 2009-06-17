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

namespace Kistl.Server.SchemaManagement
{
	public partial class SchemaManager
	{
        public void UpdateSchema()
        {
            savedSchema = GetSavedSchema();
            WriteReportHeader("Update Schema Report");

            db.BeginTransaction();
            try
            {
                UpdateTables();
                UpdateRelations();
                UpdateInheritance();

                SaveSchema(schema);

                db.CommitTransaction();
            }
            catch (Exception ex)
            {
                db.RollbackTransaction();

                report.WriteLine();
                report.WriteLine("** ERROR:");
                report.WriteLine(ex.ToString());
                report.Flush();

                throw;
            }
        }

        private void UpdateTables()
        {
            report.WriteLine("Updating Tables & Columns");
            report.WriteLine("-------------------------");

            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().OrderBy(o => o.Module.Namespace).ThenBy(o => o.ClassName))
            {
                report.WriteLine("Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.ClassName);
                if (IsCaseNewObjectClass(objClass))
                {
                    CaseNewObjectClass(objClass);
                }
                if (IsCaseRenameObjectClassTable(objClass))
                {
                    CaseRenameObjectClassTable(objClass);
                }

                UpdateColumns(objClass, objClass.Properties, "");
            }
            report.WriteLine();
        }

        private void UpdateColumns(ObjectClass objClass, ICollection<Property> properties, string prefix)
        {
            foreach (ValueTypeProperty prop in properties.OfType<ValueTypeProperty>().Where(p => !p.IsList && p.HasStorage()))
            {
                if (IsCaseNewValueTypePropertyNullable(prop))
                {
                    CaseNewValueTypePropertyNullable(objClass, prop, prefix);
                }
                if (IsCaseNewValueTypePropertyNotNullable(prop))
                {
                    CaseNewValueTypePropertyNotNullable(objClass, prop, prefix);
                }
                if (IsCaseRenameValueTypePropertyName(prop))
                {
                    CaseRenameValueTypePropertyName(objClass, prop, prefix);
                }
                if (IsCaseMoveValueTypeProperty(prop))
                {
                    CaseMoveValueTypeProperty(objClass, prop, prefix);
                }
            }

            foreach (StructProperty sprop in properties.OfType<StructProperty>().Where(p => !p.IsList && p.HasStorage()))
            {
                UpdateColumns(objClass, sprop.StructDefinition.Properties, Construct.NestedColumnName(sprop, prefix));
            }

            foreach (ValueTypeProperty prop in properties.OfType<ValueTypeProperty>().Where(p => p.IsList))
            {
                if (IsCaseNewValueTypePropertyList(prop))
                {
                    CaseNewValueTypePropertyList(objClass, prop);
                }
                if (IsCaseRenameValueTypePropertyListName(prop))
                {
                    CaseRenameValueTypePropertyListName(objClass, prop);
                }
                if (IsCaseMoveValueTypePropertyList(prop))
                {
                    CaseMoveValueTypePropertyList(objClass, prop);
                }
            }
        }

        private void UpdateRelations()
        {
            report.WriteLine("Updating Relations");
            report.WriteLine("------------------");

            foreach (Relation rel in schema.GetQuery<Relation>().OrderBy(r => r.Module.Namespace))
            {
                report.WriteLine("Relation: {0} ({1})", rel.GetAssociationName(), rel.GetRelationType());

                if (rel.GetRelationType() == RelationType.one_n)
                {
                    if (IsCaseNew_1_N_Relation(rel))
                    {
                        CaseNew_1_N_Relation(rel);
                    }
                    if (IsCase_1_N_RelationChange_FromIndexed_To_NotIndexed(rel))
                    {
                        Case_1_N_RelationChange_FromIndexed_To_NotIndexed(rel);
                    }
                    if (IsCase_1_N_RelationChange_FromNotIndexed_To_Indexed(rel))
                    {
                        Case_1_N_RelationChange_FromNotIndexed_To_Indexed(rel);
                    }
                }
                else if (rel.GetRelationType() == RelationType.n_m)
                {
                    if (IsCaseNew_N_M_Relation(rel))
                    {
                        CaseNew_N_M_Relation(rel);
                    }
                    if (IsCase_N_M_RelationChange_FromIndexed_To_NotIndexed(rel, RelationEndRole.A))
                    {
                        Case_N_M_RelationChange_FromIndexed_To_NotIndexed(rel, RelationEndRole.A);
                    }
                    if (IsCase_N_M_RelationChange_FromIndexed_To_NotIndexed(rel, RelationEndRole.B))
                    {
                        Case_N_M_RelationChange_FromIndexed_To_NotIndexed(rel, RelationEndRole.B);
                    }
                    if (IsCase_N_M_RelationChange_FromNotIndexed_To_Indexed(rel, RelationEndRole.A))
                    {
                        Case_N_M_RelationChange_FromNotIndexed_To_Indexed(rel, RelationEndRole.A);
                    }
                    if (IsCase_N_M_RelationChange_FromNotIndexed_To_Indexed(rel, RelationEndRole.B))
                    {
                        Case_N_M_RelationChange_FromNotIndexed_To_Indexed(rel, RelationEndRole.B);
                    }
                }
                else if (rel.GetRelationType() == RelationType.one_one)
                {
                    if (IsCaseNew_1_1_Relation(rel))
                    {
                        CaseNew_1_1_Relation(rel);
                    }
                }
            }
            report.WriteLine();
        }

        private void UpdateInheritance()
        {
            report.WriteLine("Updating Inheritance");
            report.WriteLine("--------------------");

            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().OrderBy(o => o.Module.Namespace).ThenBy(o => o.ClassName))
            {
                report.WriteLine("Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.ClassName);
                if (IsCaseNewObjectClassInheritance(objClass))
                {
                    CaseNewObjectClassInheritance(objClass);
                }
                if (IsCaseChangeObjectClassInheritance(objClass))
                {
                    CaseChangeObjectClassInheritance(objClass);
                }
                if (IsCaseRemoveObjectClassInheritance(objClass))
                {
                    CaseRemoveObjectClassInheritance(objClass);
                }
            }
            report.WriteLine();
        }
    }
}
