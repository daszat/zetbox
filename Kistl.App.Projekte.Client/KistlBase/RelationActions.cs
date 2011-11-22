
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
            if (obj.A != null)
            {
                obj.A.Type = null;
                obj.A.Navigator = null;
                obj.Context.Delete(obj.A);
            }
            if (obj.B != null)
            {
                obj.B.Type = null;
                obj.B.Navigator = null;
                obj.Context.Delete(obj.B);
            }
        }
    }
}
