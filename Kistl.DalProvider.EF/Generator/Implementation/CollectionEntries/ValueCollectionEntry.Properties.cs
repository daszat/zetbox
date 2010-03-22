using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators.Extensions;

namespace Kistl.DalProvider.EF.Generator.Implementation.CollectionEntries
{
    public partial class ValueCollectionEntry
    {
        protected override void ApplyParentReferencePropertyTemplate(Property prop, string propertyName)
        {
            Implementation.ObjectClasses.ObjectReferencePropertyTemplate.Call(Host, ctx,
                MembersToSerialize,
                propertyName, prop.GetAssociationName(), prop.ObjectClass.Name,
                prop.ObjectClass.Name, prop.ObjectClass.Name + Kistl.API.Helper.ImplementationSuffix,
                false, String.Empty, false, prop.Module.Namespace,
                false, false, false);

        }

        protected override void ApplyReloadReferenceBody()
        {
            base.ApplyReloadReferenceBody();

            this.WriteObjects("\t\t\tif (_fk_A.HasValue)");
            this.WriteLine();
            this.WriteObjects("\t\t\t\tA__Implementation__ = (",
                    prop.ObjectClass.Name + Kistl.API.Helper.ImplementationSuffix,
                    ")Context.Find<",
                    prop.ObjectClass.Name,
                    ">(_fk_A.Value);");
            this.WriteLine();
            this.WriteObjects("\t\t\telse");
            this.WriteLine();
            this.WriteObjects("\t\t\t\tA__Implementation__ = null;");
            this.WriteLine();
        }

        protected override void ApplyCompoundObjectPropertyTemplate(CompoundObjectProperty prop, string propertyName)
        {
            Implementation.ObjectClasses.CompoundObjectPropertyTemplate.Call(Host, ctx, MembersToSerialize, prop, propertyName);
        }
    }
}
