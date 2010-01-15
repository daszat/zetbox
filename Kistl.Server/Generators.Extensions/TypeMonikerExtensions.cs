
namespace Kistl.Server.Generators.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;

    public static class TypeMonikerExtensions
    {
        public static TypeMoniker GetTypeMoniker(this DataType objClass)
        {
            if (objClass == null) { throw new ArgumentNullException("objClass"); }
            
            return new TypeMoniker(objClass.Module.Namespace, objClass.ClassName);
        }
    }
}
