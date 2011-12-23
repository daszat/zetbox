
namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.API.Utils;

    /// <summary>
    /// Client implementation
    /// </summary>
    [Implementor]
    public static class RelationActions
    {
        [Invocation]
        public static void NotifyCreated(Relation obj)
        {
            obj.A = obj.Context.Create<RelationEnd>();
            obj.B = obj.Context.Create<RelationEnd>();
        }

        /// <summary>
        /// Workaround delete cascade troubles
        /// </summary>
        /// <param name="obj"></param>
        [Invocation]
        public static void NotifyDeleting(Relation obj)
        {
            var ctx = obj.Context;
            if (obj.A != null)
            {
                obj.A.Type = null;
                if (obj.A.Navigator != null) ctx.Delete(obj.A.Navigator);
                ctx.Delete(obj.A);
            }
            if (obj.B != null)
            {
                obj.B.Type = null;
                if (obj.B.Navigator != null) ctx.Delete(obj.B.Navigator);
                ctx.Delete(obj.B);
            }
        }
    }
}
