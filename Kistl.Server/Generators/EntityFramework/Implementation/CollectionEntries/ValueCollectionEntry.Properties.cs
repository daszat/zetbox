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
 

    }
}
