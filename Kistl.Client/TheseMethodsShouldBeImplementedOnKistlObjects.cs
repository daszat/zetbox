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

        private static ILookup<string, ObjectClass> _frozenClasses;
        public static ObjectClass GetObjectClass(this IDataObject obj, Kistl.API.IKistlContext ctx)
        {
            Type type = obj.GetInterfaceType().Type;
            IQueryable<ObjectClass> q;
            if (ctx == FrozenContext.Single)
            {
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

        // TODO: Move to DataType
        public static bool IsAssignableFrom(this DataType self, DataType other)
        {
            // if one or both parameters are null, it never can be assignable
            // also, this is a nice stop condition for the recursion for ObjectClasses
            if (self == null || other == null)
                return false;

            if (self == other)
                return true;

            if (!(self is ObjectClass && other is ObjectClass))
                return false;

            // self might be an ancestor of other, check here
            return IsAssignableFrom(self, (other as ObjectClass).BaseObjectClass);
        }

        public static bool IsValid(this IDataObject self)
        {
            ObjectClass oc = self.GetObjectClass(self.Context);
            return oc.Properties.Aggregate(true, (acc, prop) =>
                acc && prop.Constraints.All(c =>
                    c.IsValid(self, self.GetPropertyValue<object>(prop.PropertyName))));
        }

        public static string ToUserString(this DataObjectState state)
        {
            switch (state)
            {
                case DataObjectState.New:
                case DataObjectState.Modified:
                    return "+";
                case DataObjectState.Deleted:
                    return "//";
                case DataObjectState.Unmodified:
                default:
                    return "";
            }
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
    }
}
