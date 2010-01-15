using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;

namespace Kistl.DalProvider.Frozen.Generator.Implementation.CollectionEntries
{
    public partial class ValueCollectionEntry
    {

        protected override void ApplyIdPropertyTemplate()
        {
            // nothing todo, inherited from BaseFrozenObject
        }

        protected override void ApplyParentReferencePropertyTemplate(ValueTypeProperty prop, string propertyName)
        {
            Kistl.Server.Generators.Templates.Implementation.ObjectClasses.NotifyingValueProperty.Call(Host, ctx,
                    this.MembersToSerialize,
                    prop.ObjectClass.ClassName,
                    propertyName, this.prop.Module.Namespace);

            // HACK
            // TODO clean this up
            if (propertyName == "A")
                this.WriteLine("        public int fk_A { get { return A.ID; } set { throw new ReadOnlyException(); } }");
        }
    }
}
