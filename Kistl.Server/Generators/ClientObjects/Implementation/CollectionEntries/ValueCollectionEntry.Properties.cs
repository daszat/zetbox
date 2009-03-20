using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;

namespace Kistl.Server.Generators.ClientObjects.Implementation.CollectionEntries
{
    public partial class ValueCollectionEntry
    {
        protected override void ApplyParentReferencePropertyTemplate(ValueTypeProperty prop, string propertyName)
        {
            this.CallTemplate("Implementation.CollectionEntries.ValueCollectionEntryParentReference",
                ctx, MembersToSerialize, prop.ObjectClass.ClassName, propertyName);
        }

    }
}
