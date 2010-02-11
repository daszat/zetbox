
namespace Kistl.App.Projekte.Common.KistlBase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.API.Utils;
    
    public static class ObjectReferencePropertyActions
    {
        public static void OnGetIsList(ObjectReferenceProperty prop, MethodReturnEventArgs<bool> e)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            RelationEnd relEnd = prop.RelationEnd;
            Relation rel = relEnd.GetParent();
            RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

            e.Result = otherEnd.Multiplicity.UpperBound() > 1;
        }
    }
}
