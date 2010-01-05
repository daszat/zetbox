
namespace Kistl.App.Projekte.Common.KistlBase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    
    public static class RelationEndActions
    {
        public static void OnGetParent(RelationEnd relEnd, PropertyGetterEventArgs<Relation> e)
        {
            e.Result = relEnd.AParent ?? relEnd.BParent;
        }
    }
}
