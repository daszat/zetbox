using System;
using System.Collections.Generic;
using System.Linq;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.App.Extensions
{
    /// <summary>
    /// Temp. Kist Objects Extensions
    /// </summary>
    public static partial class CompoundObjectExtensions
    {
        public static CompoundObject GetCompoundObjectDefinition(this ICompoundObject obj, IReadOnlyKistlContext ctx)
        {
            if (obj == null) { throw new ArgumentNullException("obj"); }
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            return GetCompoundObjectDefinition(((BaseCompoundObject)obj).ParentObject.ReadOnlyContext.GetInterfaceType(obj), ctx);
        }

        public static CompoundObject GetCompoundObjectDefinition(this InterfaceType ifType, IReadOnlyKistlContext ctx)
        {
            // if (ifType == null) { throw new ArgumentNullException("ifType"); }
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            Type type = ifType.Type;
            CompoundObject result;
            result = ctx.GetQuery<CompoundObject>().First(o => o.Module.Namespace == type.Namespace && o.Name == type.Name);

            return result;
        }
    }
}
