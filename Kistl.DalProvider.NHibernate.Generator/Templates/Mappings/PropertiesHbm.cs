
namespace Kistl.DalProvider.NHibernate.Generator.Templates.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Generator;
    using Kistl.Generator.Extensions;

    public partial class PropertiesHbm
    {
        protected virtual void ApplyObjectReferenceProperty(string prefix, ObjectReferenceProperty prop)
        {
            this.WriteLine("<!-- ObjectReferenceProperty -->");

            var rel = Kistl.App.Extensions.RelationExtensions.Lookup(ctx, prop);
            var relEnd = rel.GetEnd(prop);
            var otherEnd = rel.GetOtherEnd(relEnd);

            string nameAttr = String.Format("name=\"{0}\"", prop.Name);
            string classAttr = String.Format("class=\"{0}\"",
                ObjectClassHbm.GetAssemblyQualifiedProxy(otherEnd.Type, this.Settings));
            //string tableAttr = String.Format("table =\"`{0}`\" ", rel.GetAssociationName());

            switch (rel.GetRelationType())
            {
                case RelationType.one_one:
                    if (rel.HasStorage(relEnd.GetRole()))
                    {
                        string columnAttr = String.Format("column=\"`{0}`\"", Construct.ForeignKeyColumnName(otherEnd, prefix));
                        this.WriteObjects("        <many-to-one ", nameAttr, " ", columnAttr, " ", classAttr, " unique=\"true\" ");
                        if (prop.EagerLoading)
                        {
                            // TODO: re-think and re-test eagerloading
                            //this.WriteObjects("fetch=\"join\" ");
                        }
                        this.WriteLine("/>");
                    }
                    else
                    {
                        this.WriteObjects("        <one-to-one ", nameAttr, " ", classAttr,
                            " constrained=\"false\" ", // constrained must be false, because else the reference is not optional(!)
                            // TODO: re-think and re-test eagerloading
                            //prop.EagerLoading ? "fetch=\"join\" " : String.Empty,
                            "property-ref=\"" + (otherEnd.Navigator != null ? otherEnd.Navigator.Name : "(no nav)") + "\" />");
                    }
                    break;
                case RelationType.one_n:
                    if (otherEnd.Multiplicity.UpperBound() > 1) // we are 1-side
                    {
                        // always map as set, the wrapper has to translate/order the elements
                        this.WriteObjects("        <set ", nameAttr, " batch-size=\"100\" cascade=\"none\" inverse=\"true\" ");
                        if (prop.EagerLoading)
                        {
                            // TODO: re-think and re-test eagerloading
                            //this.WriteObjects("lazy=\"false\" fetch=\"join\" ");
                        }
                        this.WriteLine(">");
                        string columnAttr = String.Format("column=\"`{0}`\"", Construct.ForeignKeyColumnName(relEnd, prefix));
                        this.WriteObjects("            <key ", columnAttr, " />");
                        this.WriteLine();
                        this.WriteObjects("            <one-to-many ", classAttr, " />");
                        this.WriteLine();
                        this.WriteLine("        </set>");
                    }
                    else // we are n-side
                    {
                        string columnAttr = String.Format("column=\"`{0}`\"", Construct.ForeignKeyColumnName(otherEnd, prefix));
                        this.WriteObjects("        <many-to-one ", nameAttr, " ", columnAttr, " ", classAttr, " ");
                        if (prop.EagerLoading)
                        {
                            // TODO: re-think and re-test eagerloading
                            //this.WriteObjects("fetch=\"join\" ");
                        }
                        this.WriteLine("/>");
                        if (rel.NeedsPositionStorage(relEnd.GetRole()))
                        {
                            string posNameAttr = String.Format("name=\"{0}\"", Construct.ListPositionPropertyName(relEnd));
                            string posColumnAttr = String.Format("column=\"`{0}`\"", Construct.ListPositionColumnName(otherEnd, prefix));
                            this.WriteObjects("        <property ", posNameAttr, " ", posColumnAttr, " />");
                            this.WriteLine();
                        }
                    }
                    break;
                case RelationType.n_m:
                    ApplyNMProperty(rel, relEnd, otherEnd, prop);
                    break;
                default:
                    throw new NotImplementedException(String.Format("Unknown RelationType {0} found", rel.GetRelationType()));
            }

            this.WriteLine();

        }

        private void ApplyNMProperty(
            Relation rel, RelationEnd relEnd, RelationEnd otherEnd,
            ObjectReferenceProperty prop)
        {
            this.WriteLine("        <!-- NMProperty -->");
            this.WriteLine("        <!-- rel={0} -->", rel.GetRelationClassName());
            this.WriteLine("        <!-- relEnd={0} otherEnd={1} -->", relEnd.RoleName, otherEnd.RoleName);

            string nameAttr = String.Format("name=\"{0}\"", prop.Name);
            string tableName = rel.GetRelationTableName();
            string tableAttr = String.Format("table=\"`{0}`\"", tableName);

            string relationEntryClassAttr = String.Format("class=\"{0}.{1}{2}+{1}Proxy,Kistl.Objects.NHibernateImpl\"",
                rel.Module.Namespace,
                rel.GetRelationClassName(),
                ImplementationSuffix);

            string fkThisColumnAttr = String.Format("column=\"`{0}`\"", Construct.ForeignKeyColumnName(relEnd));
            //string fkOtherColumnAttr = String.Format("column=\"`{0}`\"", Construct.ForeignKeyColumnName(otherEnd));

            // always map as set, the wrapper has to translate/order the elements
            this.WriteObjects("        <set ", nameAttr, " ", tableAttr, " inverse=\"true\" cascade=\"all-delete-orphan\" batch-size=\"100\" ");
            if (prop.EagerLoading)
            {
                // TODO: re-think and re-test eagerloading
                //this.WriteObjects("lazy=\"false\" fetch=\"join\" ");
            }
            this.WriteLine(">");

            this.WriteObjects("            <key ", fkThisColumnAttr, " />");
            this.WriteLine();
            this.WriteObjects("            <one-to-many ", relationEntryClassAttr, " />");
            this.WriteLine();
            this.WriteObjects("        </set>");
            this.WriteLine();
        }

        protected virtual void ApplyValueTypeProperty(string prefix, ValueTypeProperty prop)
        {
            ValueTypePropertyHbm.Call(Host, prefix, prop, null, null, false, ImplementationSuffix, needsConcurrency);
        }

        protected virtual void ApplyCompoundObjectProperty(string prefix, CompoundObjectProperty prop)
        {
            CompoundObjectPropertyHbm.Call(Host, ctx, prefix, prop, null, null, false, ImplementationSuffix);
        }
    }
}
