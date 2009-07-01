using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.App.Extensions
{
    public static class KistlContextExtensions
    {
        public static Kistl.App.Base.Interface GetIExportableInterface(this Kistl.API.IKistlContext ctx)
        {
            return ctx.GetQuery<Kistl.App.Base.Interface>().First(o => o.ClassName == "IExportable" && o.Module.ModuleName == "KistlBase"); 
        }
    }
}
