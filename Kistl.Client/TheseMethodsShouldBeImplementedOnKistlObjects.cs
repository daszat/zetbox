using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Linq;
using Kistl.API.Client;
using Kistl.App.Base;
using Kistl.API;

namespace Kistl.Client
{
    /// <summary>
    /// Temp. Kist Objects Extensions
    /// </summary>
    public static class TheseMethodsShouldBeImplementedOnKistlObjects
    {
        public static ICollection<ObjectClass> GetObjectHierarchie(this ObjectClass objClass)
        {
            List<ObjectClass> result = new List<ObjectClass>();
            while (objClass != null)
            {
                result.Add(objClass);
                objClass = objClass.BaseObjectClass;
            }

            result.Reverse();
            return result;
        }

        public static ObjectClass GetObjectClass(this IDataObject obj, Kistl.API.IKistlContext ctx)
        {
            Type type = obj.GetType();
            return ctx.GetQuery<ObjectClass>().First(o => o.Module.Namespace == type.Namespace && o.ClassName == type.Name);
        }

        public static BaseProperty GetProperty(this ObjectClass c, string property)
        {
            ObjectClass objClass = c;
            while (objClass != null)
            {
                BaseProperty prop = objClass.Properties.SingleOrDefault(p => p.PropertyName == property);
                if (prop != null)
                {
                    return prop;
                }
                objClass = objClass.BaseObjectClass;
            }

            return null;
        }

        //public static Type GetDataCLRType(this DataType type)
        //{
        //    // TODO: remove this bad test-hack
        //    string fullname = type.Module.Namespace + "." + type.ClassName;
        //    string assembly = fullname == "Kistl.Client.Mocks.TestObject" ? "Kistl.Client.Tests" : "Kistl.Objects.Client";
        //    return Type.GetType(fullname + ", " + assembly, true);
        //}
    }
}
