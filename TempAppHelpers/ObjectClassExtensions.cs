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

        private static ILookup<string, ObjectClass> _frozenClasses;
        public static ObjectClass GetObjectClass(this IDataObject obj, Kistl.API.IKistlContext ctx)
        {
            Type type = obj.GetInterfaceType().Type;
            IQueryable<ObjectClass> q;
            if (ctx == FrozenContext.Single)
            {
                // cache frozen classes by class name
                if (_frozenClasses == null)
                {
                    _frozenClasses = ctx.GetQuery<ObjectClass>().ToLookup(cls => cls.ClassName);
                }
                q = _frozenClasses[type.Name].AsQueryable();
            }
            else
            {
                q = ctx.GetQuery<ObjectClass>();
            }

            return q.First(o => o.Module.Namespace == type.Namespace && o.ClassName == type.Name);
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
            // TODO: During export schema, while creating a new Database, no custom actions are attached (Database is empty)
            // return new InterfaceType(cls.GetDataType());
            return new InterfaceType(Type.GetType(cls.Module.Namespace + "." + cls.ClassName + ", Kistl.Objects", true));
        }

        public static bool ImplementsIExportable(this ObjectClass cls, IKistlContext ctx, bool lookupInBase)
        {
            if (!lookupInBase) return cls.ImplementsInterfaces.Contains(ctx.GetIExportableInterface());
            return ImplementsIExportable(cls, ctx);
        }

        public static bool ImplementsIExportable(this ObjectClass cls, IKistlContext ctx)
        {
            while (cls != null)
            {
                if (cls.ImplementsInterfaces.Contains(ctx.GetIExportableInterface()))
                    return true;
                cls = cls.BaseObjectClass;
            }
            return false;
        }

        public static IList<Property> GetAllProperties(this ObjectClass cls)
        {
            return cls.GetInheritedProperties().Concat(cls.Properties).ToList();
        }

        public static IList<Property> GetInheritedProperties(this ObjectClass cls)
        {
            var result = new List<Property>().AsEnumerable();
            while (cls.BaseObjectClass != null)
            {
                result = cls.BaseObjectClass.Properties.Concat(result);
                cls = cls.BaseObjectClass;
            }
            return result.ToList();
        }
    }

}
