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
        public void UpdateSchema()
        {
            using (Logging.Log.TraceMethodCall())
            {
                WriteReportHeader("Update Schema Report");

                db.BeginTransaction();
                try
                {
                    UpdateTables();
                    UpdateRelations();
                    UpdateInheritance();

                    UpdateDeletedRelations();
                    UpdateDeletedTables();

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
        }

        private void UpdateDeletedTables()
        {
            report.WriteLine("Updating deleted Tables");
            report.WriteLine("-----------------------");

            foreach (ObjectClass objClass in Case.savedSchema.GetQuery<ObjectClass>().OrderBy(o => o.Module.Namespace).ThenBy(o => o.ClassName))
            {
                report.WriteLine("Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.ClassName);
                if (Case.IsDeleteObjectClass(objClass))
                {
                    Case.DoDeleteObjectClass(objClass);
                }
            }
            report.WriteLine();
        }


        private void UpdateTables()
        {
            report.WriteLine("Updating Tables & Columns");
            report.WriteLine("-------------------------");

            foreach (ObjectClass objClass in schema.GetQuery<ObjectClass>().OrderBy(o => o.Module.Namespace).ThenBy(o => o.ClassName))
            {
                report.WriteLine("Objectclass: {0}.{1}", objClass.Module.Namespace, objClass.ClassName);
                if (Case.IsNewObjectClass(objClass))
                {
                    Case.DoNewObjectClass(objClass);
                }
                if (Case.IsRenameObjectClassTable(objClass))
                {
                    Case.DoRenameObjectClassTable(objClass);
                }

                UpdateColumns(objClass, objClass.Properties, "");
            }
            report.WriteLine();
        }

        private void UpdateColumns(ObjectClass objClass, ICollection<Property> properties, string prefix)
        {
            foreach (ValueTypeProperty prop in properties.OfType<ValueTypeProperty>().Where(p => !p.IsList && p.HasStorage()))
            {
                if (Case.IsNewValueTypePropertyNullable(prop))
                {
                    Case.DoNewValueTypePropertyNullable(objClass, prop, prefix);
                }
                if (Case.IsNewValueTypePropertyNotNullable(prop))
                {
                    Case.DoNewValueTypePropertyNotNullable(objClass, prop, prefix);
                }
                if (Case.IsRenameValueTypePropertyName(prop))
                {
                    Case.DoRenameValueTypePropertyName(objClass, prop, prefix);
                }
                if (Case.IsMoveValueTypeProperty(prop))
                {
                    Case.DoMoveValueTypeProperty(objClass, prop, prefix);
                }
            }

            foreach (StructProperty sprop in properties.OfType<StructProperty>().Where(p => !p.IsList && p.HasStorage()))
            {
                UpdateColumns(objClass, sprop.StructDefinition.Properties, Construct.NestedColumnName(sprop, prefix));
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
        }

        private void UpdateDeletedRelations()
        {
            report.WriteLine("Updating deleted Relations");
            report.WriteLine("--------------------------");

            foreach (Relation rel in Case.savedSchema.GetQuery<Relation>().OrderBy(r => r.Module.Namespace))
            {
                report.WriteLine("Relation: {0} ({1})", rel.GetAssociationName(), rel.GetRelationType());

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
            report.WriteLine();
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
            report.WriteLine();
        }
    }
}
