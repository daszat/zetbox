
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
    }
}
