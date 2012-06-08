using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;
using Zetbox.App.Base;

namespace Zetbox.App.Extensions
{
    public static class ZetboxContextExtensions
    {
        public static Interface GetIExportableInterface(this IReadOnlyZetboxContext ctx)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            // TODO: use named objects
            return ctx.GetQuery<Zetbox.App.Base.Interface>().First(o => o.Name == "IExportable" && o.Module.Name == "ZetboxBase"); 
        }
    }
}
