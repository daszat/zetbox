
namespace Kistl.Generator.Templates.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.App.Base;

    public partial class ValueCollectionEntry
    {
        protected override void ApplyAPropertyTemplate()
        {
            var interfaceType = prop.ObjectClass.Module.Namespace + "." + prop.ObjectClass.Name;

            CollectionEntries.ValueCollectionEntryParentReference.Call(Host, ctx,
                MembersToSerialize, interfaceType, "Parent", prop.Module.Namespace);

            Properties.DelegatingProperty.Call(
                Host, ctx, 
                "ParentObject", "Kistl.API.IDataObject", 
                "Parent", interfaceType + ImplementationSuffix);
        }

        protected override void ApplyBPropertyTemplate()
        {
            string interfaceType = prop.GetPropertyTypeString();
            string implementationType = interfaceType;

            var cop = prop as CompoundObjectProperty;
            if (cop != null)
            {
                Properties.CompoundObjectPropertyTemplate.Call(
                    Host, ctx, MembersToSerialize,
                    prop as CompoundObjectProperty, "Value");
                implementationType = interfaceType + ImplementationSuffix;
            }
            var vtp = prop as ValueTypeProperty;
            if (vtp != null)
            {
                Properties.NotifyingValueProperty.Call(
                    Host, ctx, MembersToSerialize, 
                    vtp.GetPropertyTypeString(), "Value", vtp.Module.Namespace);
            }

            Properties.DelegatingProperty.Call(Host, ctx, "ValueObject", "object", "Value", implementationType);
        }

        protected override void ApplyAIndexPropertyTemplate()
        {
            this.WriteLine("        // always ignored because the other side (a value) cannot have a navigator and therefore no order");
            this.WriteObjects("        int? ", GetCeInterface(), ".AIndex { get { return null; } set { } }");
            this.WriteLine();
        }

        protected override void ApplyBIndexPropertyTemplate()
        {
            throw new NotImplementedException();
        }
    }
}
