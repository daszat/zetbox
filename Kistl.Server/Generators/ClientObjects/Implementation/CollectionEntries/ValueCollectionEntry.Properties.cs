using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;

namespace Kistl.Server.Generators.ClientObjects.Implementation.CollectionEntries
{
    public partial class ValueCollectionEntry
    {
        protected override void ApplyParentReferencePropertyTemplate(Property prop, string propertyName)
        {
            Implementation.CollectionEntries.ValueCollectionEntryParentReference.Call(Host, ctx, 
                MembersToSerialize, prop.ObjectClass.Name, propertyName);
        }

        protected override void ApplyCompoundObjectPropertyTemplate(CompoundObjectProperty prop, string propertyName)
        {
            Implementation.ObjectClasses.CompoundObjectPropertyTemplate.Call(Host, ctx, MembersToSerialize, prop, propertyName);
        }
    }
}
