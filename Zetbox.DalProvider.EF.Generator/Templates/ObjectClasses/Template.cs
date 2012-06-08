
namespace Zetbox.DalProvider.Ef.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator;
    using Zetbox.Generator.Extensions;
    using Templates = Zetbox.Generator.Templates;

    public class Template
        : Templates.ObjectClasses.Template
    {

        public Template(Arebis.CodeGeneration.IGenerationHost _host, Zetbox.API.IZetboxContext ctx, Zetbox.App.Base.ObjectClass cls)
            : base(_host, ctx, cls)
        {
        }

        protected override void ApplyClassAttributeTemplate()
        {
            WriteLine("    [EdmEntityType(NamespaceName=\"Model\", Name=\"{0}\")]", this.DataType.Name);
        }

        protected override string GetBaseClass()
        {
            if (this.ObjectClass.BaseObjectClass != null)
            {
                return base.GetBaseClass();
            }
            else
            {
                return "BaseServerDataObject_EntityFramework";
            }
        }

        protected override void ApplyEnumerationPropertyTemplate(EnumerationProperty prop)
        {
            this.WriteLine("        // enumeration property");
            Properties.EnumerationPropertyTemplate.Call(Host, ctx,
                this.MembersToSerialize,
                prop, true);
        }

        protected override void ApplyCompoundObjectPropertyTemplate(CompoundObjectProperty prop)
        {
            this.WriteLine("        // CompoundObject property");
            Templates.Properties.CompoundObjectPropertyTemplate.Call(Host, ctx,
                this.MembersToSerialize,
                prop);
        }

        protected override void ApplyCompoundObjectListTemplate(CompoundObjectProperty prop)
        {
            this.WriteLine("        // CompoundObject list property");
            Properties.ValueCollectionProperty.Call(Host, ctx,
                this.MembersToSerialize,
                prop);
        }

        protected override void ApplyValueTypeListTemplate(ValueTypeProperty prop)
        {
            this.WriteLine("        // value list property");
            Properties.ValueCollectionProperty.Call(Host, ctx,
                this.MembersToSerialize,
                prop);
        }

        protected override void ApplyObjectReferencePropertyTemplate(ObjectReferenceProperty prop)
        {
            var rel = Zetbox.App.Extensions.RelationExtensions.Lookup(ctx, prop);

            // Navigator can be NULL
            // Debug.Assert(rel.A.Navigator.ID == prop.ID || rel.B.Navigator.ID == prop.ID);
            RelationEnd relEnd = rel.GetEnd(prop);
            RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

            if (rel.Storage == StorageType.Separate)
            {
                throw new InvalidOperationException("Separate Storage not implemented for ObjectReferenceProperty in 1:N");
            }

            this.WriteLine("    /*");
            RelationDebugTemplate.Call(Host, ctx, rel);
            this.WriteLine("    */");

            this.WriteLine("        // object reference property");
            Templates.Properties.ObjectReferencePropertyTemplate.Call(
                Host,
                ctx,
                this.MembersToSerialize,
                prop, true, true);
        }

        protected override void ApplyObjectReferenceListTemplate(ObjectReferenceProperty prop)
        {
            // TODO: move debugging output into Templates
            this.WriteLine("    /*");
            RelationDebugTemplate.Call(Host, ctx, Zetbox.App.Extensions.RelationExtensions.Lookup(ctx, prop));
            this.WriteLine("    */");

            var rel = RelationExtensions.Lookup(ctx, prop);

            var relEnd = rel.GetEnd(prop);
            var otherEnd = rel.GetOtherEnd(relEnd);

            // without navigator, there should be no property
            if (relEnd.Navigator == null)
                return;

            switch ((StorageType)rel.Storage)
            {
                case StorageType.MergeIntoA:
                case StorageType.MergeIntoB:
                case StorageType.Replicate:
                    // simple and direct reference
                    this.WriteLine("        // object list property");
                    base.ApplyObjectReferenceListTemplate(relEnd.Navigator as ObjectReferenceProperty);
                    break;
                case StorageType.Separate:
                    this.WriteLine("        // collection reference property");
                    ApplyCollectionEntryListTemplate(rel, relEnd.GetRole());
                    break;
                default:
                    throw new NotImplementedException("unknown StorageHint for ObjectReferenceProperty[IsList == true]");
            }
        }

        protected virtual void ApplyCollectionEntryListTemplate(Relation rel, RelationEndRole endRole)
        {
            Properties.CollectionEntryListProperty.Call(Host, ctx,
                this.MembersToSerialize,
                rel, endRole);
        }

        protected override void ApplyUpdateParentTemplate()
        {
            // Do not implement/override UpdateParent, since EF keeps the tabs for us.
            //base.ApplyUpdateParentTemplate();
        }

        protected override void ApplyClassTailTemplate()
        {
            base.ApplyClassTailTemplate();

            var oc = this.DataType as ObjectClass;
            if (oc == null
                || (oc != null && oc.BaseObjectClass == null))
            {
                Templates.Properties.IdProperty.Call(Host, ctx);
            }

            if (NeedsRightsTable())
            {
                Properties.SecurityRulesProperties.Call(Host, ctx, (ObjectClass)this.DataType);
            }
        }

        protected override void ApplyNamespaceTailTemplate()
        {
            base.ApplyNamespaceTailTemplate();

            if (NeedsRightsTable())
            {
                SecurityRulesClass.Call(Host, ctx, (ObjectClass)this.DataType);
            }
        }
    }
}
