using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Server.Generators.Extensions
{
    public static class TypeMonikerExtensions
    {
        public static TypeMoniker GetTypeMoniker(this DataType objClass)
        {
            return new TypeMoniker(objClass.Module.Namespace, objClass.ClassName);
        }

        //public static TypeMoniker GetTypeMonikerImplementation(this DataType objClass)
        //{
        //    return new TypeMoniker(objClass.Module.Namespace, objClass.ClassName + Kistl.API.Helper.ImplementationSuffix);
        //}

        //public static ObjectClass ToObjectClass(this TypeMoniker tm, IKistlContext ctx)
        //{
        //    return ctx.GetQuery<ObjectClass>().ToList().SingleOrDefault(oc => oc.GetTypeMoniker().Equals(tm));
        //}

        //public static Type ToSystemType(this TypeMoniker tm)
        //{
        //    return Type.GetType(tm.NameDataObject);
        //}

    }
}
