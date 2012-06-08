
namespace Zetbox.DalProvider.Ef.Generator.Templates.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator.Extensions;
    using Templates = Zetbox.Generator.Templates;

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
                null,
                interfaceType,
                implementationType,
                prop.GetAssociationName(),
                prop.ObjectClass.Name,
                null,
                prop.Name,
                true,
                false, 
                false, // value collection entries are always streamed/exported in-place
                false,
                prop.IsCalculated());

            Templates.Properties.DelegatingProperty.Call(
                Host, ctx,
                "ParentObject", "Zetbox.API.IDataObject",
                "Parent", implementationType);
        }
    }
}
