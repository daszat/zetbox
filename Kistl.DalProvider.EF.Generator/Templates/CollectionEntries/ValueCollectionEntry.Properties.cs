
namespace Kistl.DalProvider.Ef.Generator.Templates.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Generator.Extensions;
    using Templates = Kistl.Generator.Templates;

    public partial class ValueCollectionEntry
    {
        protected override void ApplyAPropertyTemplate()
        {
            var interfaceType = prop.ObjectClass.Module.Namespace + "." + prop.ObjectClass.Name;
            var implementationType = interfaceType + ImplementationSuffix;

            Templates.Properties.ObjectReferencePropertyTemplate.Call(
                Host,
                ctx,
                MembersToSerialize,
                prop.ObjectClass.Module.Namespace,
                this.GetCeInterface(),
                "Parent",
                "Parent" + ImplementationPropertySuffix,
                "OnParent",
                "_fk_Parent",
                "_fk_Parent_guid",
                interfaceType,
                implementationType,
                prop.GetAssociationName(),
                prop.ObjectClass.Name,
                null,
                prop.Name,
                true,
                false, 
                IsExportable(),
                false);

            Templates.Properties.DelegatingProperty.Call(
                Host, ctx,
                "ParentObject", "Kistl.API.IDataObject",
                "Parent", implementationType);
        }
    }
}
