
namespace Kistl.Generator.Templates.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.App.Base;
    using Kistl.Generator.Extensions;

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
            string interfaceType = prop.GetElementTypeString();
            string implementationType = interfaceType;

            var cop = prop as CompoundObjectProperty;
            if (cop != null)
            {
                Properties.CompoundObjectPropertyTemplate.Call(
                    Host, ctx, MembersToSerialize,
                    prop as CompoundObjectProperty, "Value",
                    false, false);
                implementationType = interfaceType + ImplementationSuffix;
            }
            var vtp = prop as ValueTypeProperty;
            if (vtp != null)
            {
                Properties.NotifyingValueProperty.Call(
                    Host, ctx, MembersToSerialize,
                    interfaceType, "Value", vtp.Module.Namespace);
            }

            Properties.DelegatingProperty.Call(Host, ctx, "ValueObject", "object", "Value", implementationType);
        }

        protected override sealed void ApplyAIndexPropertyTemplate()
        {
            // never used
        }

        protected override void ApplyBIndexPropertyTemplate()
        {
            Properties.NotifyingValueProperty.Call(
                Host, ctx, MembersToSerialize,
                "int?", "Index", prop.GetCollectionEntryNamespace());
        }
    }
}
