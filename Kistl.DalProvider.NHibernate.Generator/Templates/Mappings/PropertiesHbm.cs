
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
        //public static void Call(IGenerationHost host, IKistlContext ctx, string prefix, IEnumerable<Property> properties)
        //{
        //    if (host == null) { throw new ArgumentNullException("host"); }
        //    if (ctx == null) { throw new ArgumentNullException("ctx"); }
        //    if (prefix == null) { throw new ArgumentNullException("prefix"); }
        //    if (properties == null) { throw new ArgumentNullException("properties"); }

        //    host.CallTemplate("Mappings.PropertiesHbm", ctx, prefix, properties);
        //}

        protected virtual void ApplyObjectReferenceProperty(string prefix, ObjectReferenceProperty prop)
        {
            this.WriteLine("<!-- ObjectReferenceProperty -->");

            var rel = Kistl.App.Extensions.RelationExtensions.Lookup(ctx, prop);
            var relEnd = rel.GetEnd(prop);
            var otherEnd = rel.GetOtherEnd(relEnd);
            bool inverse = false;

            // mark this relation mapping as inverse if there are both navigators defined
            // and we are currently working on the second
            // TODO: actually we'd probably want to choose the n-side for setting inverse
            //       except for lists, which are not supported as inverse by NHibernate
            if (relEnd.Navigator != null && otherEnd.Navigator != null && relEnd == rel.B)
            {
                inverse = true;
            }

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
                            this.WriteObjects("fetch=\"join\" ");
                        }
                        this.WriteLine("/>");
                    }
                    else
                    {
                        this.WriteObjects("        <one-to-one ", nameAttr, " ", classAttr,
                            " constrained=\"false\" ", // constrained must be false, because else the reference is not optional(!)
                            prop.EagerLoading ? "fetch=\"join\" " : String.Empty,
                            "property-ref=\"" + (otherEnd.Navigator != null ? otherEnd.Navigator.Name : "(no nav)") + "\" />");
                    }
                    break;
                case RelationType.one_n:
                    if (otherEnd.Multiplicity.UpperBound() > 1) // we are 1-side
                    {
                        // always map as set, the wrapper has to translate/order the elements
                        this.WriteObjects("        <set ", nameAttr, " ");
                        if (prop.EagerLoading)
                        {
                            this.WriteObjects("lazy=\"false\" fetch=\"join\" ");
                        }
                        else
                        {
                            this.WriteObjects("batch-size=\"100\" ");
                        }
                        // TODO: always mark this side inverse. See p57 in the reference
                        if (inverse)
                        {
                            this.WriteObjects("inverse=\"true\" ");
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
                            this.WriteObjects("fetch=\"join\" ");
                        }
                        if (inverse)
                        {
                            // invalid: this.WriteObjects("inverse=\"true\" ");
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
                    ApplyNMProperty(rel, relEnd, otherEnd, prop, inverse);
                    break;
                default:
                    throw new NotImplementedException(String.Format("Unknown RelationType {0} found", rel.GetRelationType()));
            }

            this.WriteLine();

        }

        private void ApplyNMProperty(
            Relation rel, RelationEnd relEnd, RelationEnd otherEnd,
            ObjectReferenceProperty prop,
            bool inverse)
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
            this.WriteObjects("        <set ", nameAttr, " ", tableAttr, " inverse=\"true\" cascade=\"all-delete-orphan\" ");
            if (prop.EagerLoading)
            {
                this.WriteObjects("lazy=\"false\" fetch=\"join\" ");
            }
            else
            {
                this.WriteObjects("batch-size=\"100\" ");
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
            this.WriteLine("        <!-- ValueTypeProperty -->");
            string nameAttr = String.Format("name=\"{0}\"", prop.Name);
            string tableName = prop.GetCollectionEntryTable();
            string tableAttr = String.Format("table=\"`{0}`\"", tableName);
            string typeAttr;
            if (prop.IsList)
            {
                // set the proper type for relation entries
                typeAttr = String.Format("type=\"{0}\"", prop.GetPropertyTypeString());
            }
            else
            {
                // let NHibernate work it out via reflection
                typeAttr = String.Empty; 
            }
            string mappingType = prop.HasPersistentOrder ? "list" : "idbag";

            if (prop.IsList)
            {
                this.WriteObjects("        <", mappingType, " ", nameAttr, " ", tableAttr, ">");
                this.WriteLine();

                if (!prop.HasPersistentOrder)
                {
                    this.WriteObjects("            <collection-id column=\"`ID`\" type=\"Int32\">");
                    this.WriteLine();
                    DefineIdGenerator(tableName);
                    this.WriteObjects("            </collection-id>");
                    this.WriteLine();
                }

                this.WriteObjects("            <key column=\"`", prop.GetCollectionEntryReverseKeyColumnName(), "`\" />");
                this.WriteLine();

                if (prop.HasPersistentOrder)
                {
                    this.WriteObjects("            <index column=\"`", Construct.ListPositionColumnName(prop), "`\" />");
                    this.WriteLine();
                }

                this.WriteObjects("            <element column=\"`", prop.Name, "`\" ", typeAttr, " />");
                this.WriteLine();
                this.WriteObjects("        </", mappingType, ">");
                this.WriteLine();
            }
            else
            {
                this.WriteObjects("        <property ", nameAttr);
                this.WriteLine();
                this.WriteObjects("                  column=\"`", prefix, prop.Name, "`\"");
                this.WriteLine();
                this.WriteObjects("                  ", typeAttr, " />");
                this.WriteLine();
            }
        }

        private void DefineIdGenerator(string tableName)
        {
            var sequenceName = tableName + "_ID_seq";
            this.WriteObjects("                <generator class=\"native\">");
            this.WriteLine();
            this.WriteObjects("                    <param name=\"sequence\">`", sequenceName, "`</param>");
            this.WriteLine();
            this.WriteObjects("                </generator>");
            this.WriteLine();
        }

        protected virtual void ApplyCompoundObjectProperty(string prefix, CompoundObjectProperty prop)
        {
            this.WriteLine("        <!-- CompoundObjectProperty -->");
            string nameAttr = String.Format("name=\"{0}\"", prop.Name);
            string tableName = prop.GetCollectionEntryTable();
            string tableAttr = String.Format("table=\"`{0}`\"", tableName);
            string valueClassAttr = String.Format("class=\"{0}.{1}{2},Kistl.Objects.NHibernateImpl\"",
                prop.CompoundObjectDefinition.Module.Namespace,
                prop.CompoundObjectDefinition.Name,
                ImplementationSuffix);
            string isNullColumnAttr = String.Format("column=\"`{0}`\"", prop.Name);

            string mappingType = prop.HasPersistentOrder ? "list" : "idbag";

            if (prop.IsList)
            {
                string ceClassAttr = String.Format("class=\"{0}.{1}{2}+{1}Proxy,Kistl.Objects.NHibernateImpl\"",
                    prop.Module.Namespace,
                    prop.GetCollectionEntryClassName(),
                    ImplementationSuffix);

                this.WriteObjects("        <", mappingType, " ", nameAttr, " ", tableAttr, ">");
                this.WriteLine();

                if (!prop.HasPersistentOrder)
                {
                    this.WriteObjects("            <collection-id column=\"`ID`\" type=\"Int32\">");
                    this.WriteLine();
                    DefineIdGenerator(tableName);
                    this.WriteObjects("            </collection-id>");
                    this.WriteLine();
                }

                this.WriteObjects("            <key column=\"`", prop.GetCollectionEntryReverseKeyColumnName(), "`\" />");
                this.WriteLine();

                if (prop.HasPersistentOrder)
                {
                    this.WriteObjects("            <index column=\"`", Construct.ListPositionColumnName(prop), "`\" />");
                    this.WriteLine();
                }

                this.WriteObjects("            <composite-element ", ceClassAttr, ">");
                this.WriteLine();
                this.WriteObjects("                <property name=\"ValueIsNull\" ", isNullColumnAttr, " type=\"bool\" />");
                this.WriteLine();
                this.WriteObjects("                <nested-composite-element name=\"Value\" ", valueClassAttr, ">");
                this.WriteLine();

                Call(Host, ctx, prefix + prop.Name + "_", prop.CompoundObjectDefinition.Properties);

                this.WriteObjects("                </nested-composite-element>");
                this.WriteLine();
                this.WriteObjects("            </composite-element>");
                this.WriteLine();
                this.WriteObjects("</", mappingType, ">");
                this.WriteLine();
            }
            else
            {
                this.WriteObjects("        <component ", nameAttr, " ", valueClassAttr, " >");
                this.WriteLine();

                this.WriteObjects("            <property name=\"CompoundObject_IsNull\" ", isNullColumnAttr, " type=\"bool\" />");
                this.WriteLine();

                Call(Host, ctx, prefix + prop.Name + "_", prop.CompoundObjectDefinition.Properties);

                this.WriteObjects("        </component>");
                this.WriteLine();
            }
        }
    }
}
