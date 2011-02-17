
namespace Kistl.App.Projekte.Common.KistlBase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;

    [Implementor]
    public static class RelationEndActions
    {
        [Invocation]
        public static void get_Parent(RelationEnd relEnd, PropertyGetterEventArgs<Relation> e)
        {
            e.Result = relEnd.AParent ?? relEnd.BParent;
        }
    }
}
