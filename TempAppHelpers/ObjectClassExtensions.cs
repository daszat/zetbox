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
    public static partial class ObjectClassExtensions
    {

        public static ObjectClass GetObjectClass(this IDataObject obj, Kistl.API.IKistlContext ctx)
        {
            Type type = obj.GetInterfaceType().Type;
            return ctx.GetQuery<ObjectClass>().First(o => o.Module.Namespace == type.Namespace && o.ClassName == type.Name);
        }


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

        public static ObjectClass GetRootClass(this ObjectClass objClass)
        {
            while (objClass.BaseObjectClass != null)
            {
                objClass = objClass.BaseObjectClass;
            }
            return objClass;
        }

        public static Property GetProperty(this ObjectClass c, string property)
        {
            ObjectClass objClass = c;
            while (objClass != null)
            {
                Property prop = objClass.Properties.SingleOrDefault(p => p.PropertyName == property);
                if (prop != null)
                {
                    return prop;
                }
                objClass = objClass.BaseObjectClass;
            }

            return null;
        }

        public static bool IsFrozen(this ObjectClass cls)
        {
            while (cls != null)
            {
                if (cls.IsFrozenObject)
                    return true;
                cls = cls.BaseObjectClass;
            }
            return false;
        }

        public static InterfaceType GetDescribedInterfaceType(this ObjectClass cls)
        {
            return new InterfaceType(cls.GetDataType());
        }
    }

}
