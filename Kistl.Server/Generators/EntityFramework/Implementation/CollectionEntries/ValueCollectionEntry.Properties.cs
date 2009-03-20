using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.EntityFramework.Implementation.CollectionEntries
{
    public partial class ValueCollectionEntry
    {
        protected override void ApplyParentReferencePropertyTemplate(ValueTypeProperty prop, string propertyName)
        {
            CallTemplate("Implementation.ObjectClasses.ObjectReferencePropertyTemplate", ctx,
                null,
                propertyName, prop.GetAssociationName(), prop.ObjectClass.ClassName,
                prop.ObjectClass.ClassName, prop.ObjectClass.ClassName + Kistl.API.Helper.ImplementationSuffix,
                false);

        }

        protected override void ApplyReloadReferenceBody()
        {
            base.ApplyReloadReferenceBody();

            this.WriteObjects("\t\t\tif (_fk_A.HasValue)");
            this.WriteLine();
            this.WriteObjects("\t\t\t\tA__Implementation__ = (",
                    prop.ObjectClass.ClassName + Kistl.API.Helper.ImplementationSuffix,
                    ")Context.Find<",
                    prop.ObjectClass.ClassName,
                    ">(_fk_A.Value);");
            this.WriteLine();
            this.WriteObjects("\t\t\telse");
            this.WriteLine();
            this.WriteObjects("\t\t\t\tA__Implementation__ = null;");
            this.WriteLine();
        }

    }
}
