using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;

namespace Kistl.App.Extensions
{
    public static class KistlContextExtensions
    {
        public static Interface GetIExportableInterface(this IReadOnlyKistlContext ctx)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            return ctx.GetQuery<Kistl.App.Base.Interface>().First(o => o.Name == "IExportable" && o.Module.Name == "KistlBase"); 
        }
    }
}
