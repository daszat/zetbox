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
        private readonly static object _lock = new object();

        private static ILookup<string, ObjectClass> _frozenClasses;
        private static bool isInitializing = false;

        public static ObjectClass GetObjectClass(this IDataObject obj, IReadOnlyKistlContext ctx)
        {
            if (obj == null) { throw new ArgumentNullException("obj"); }
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            return GetObjectClass(obj.GetInterfaceType(), ctx);
        }

        public static ObjectClass GetObjectClass(this InterfaceType ifType, IReadOnlyKistlContext ctx)
        {
            if (ifType == null) { throw new ArgumentNullException("ifType"); }
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            Type type = ifType.Type;
            ObjectClass result;
            if (ctx == FrozenContext.Single)
            {
                // cache frozen classes by class name
                InitializeFrozenCache(ctx);
                if (_frozenClasses == null) return null; // Case #1363: GetObjectClass can be called by: InitializeFrozenCache -> QueryTranslator -> OfType Visit -> ApplySecurityFilter ->GetObjectClass
                result = _frozenClasses[type.Name].First(o => o.Module.Namespace == type.Namespace && o.ClassName == type.Name);
            }
            else
            {
                result = ctx.GetQuery<ObjectClass>().First(o => o.Module.Namespace == type.Namespace && o.ClassName == type.Name);
            }

            return result;
        }

        private static void InitializeFrozenCache(IReadOnlyKistlContext ctx)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }
            lock (_lock)
            {
                if (_frozenClasses == null && !isInitializing)
                {
                    isInitializing = true;
                    // Case #1363: GetQuery may call ObjectQueryTranslator which calls GetObjectClass ....
                    _frozenClasses = ctx.GetQuery<ObjectClass>().ToLookup(cls => cls.ClassName);
                    isInitializing = false;
                }
            }
        }

        public static ICollection<ObjectClass> GetObjectHierarchie(this ObjectClass cls)
        {
            List<ObjectClass> result = new List<ObjectClass>();
            while (cls != null)
            {
                result.Add(cls);
                cls = cls.BaseObjectClass;
            }

            result.Reverse();
            return result;
        }

        public static ObjectClass GetRootClass(this ObjectClass cls)
        {
            if (cls == null) { throw new ArgumentNullException("cls"); }

            while (cls.BaseObjectClass != null)
            {
                cls = cls.BaseObjectClass;
            }
            return cls;
        }

        public static Property GetProperty(this ObjectClass cls, string property)
        {
            if (cls == null) { throw new ArgumentNullException("cls"); }

            while (cls != null)
            {
                Property prop = cls.Properties.SingleOrDefault(p => p.PropertyName == property);
                if (prop != null)
                {
                    return prop;
                }
                cls = cls.BaseObjectClass;
            }

            return null;
        }

        public static bool IsFrozen(this ObjectClass cls)
        {
            if (cls == null) { throw new ArgumentNullException("cls"); }

            while (cls != null)
            {
                if (cls.IsFrozenObject)
                    return true;
                cls = cls.BaseObjectClass;
            }
            return false;
        }

        public static bool HasSecurityRules(this ObjectClass cls)
        {
            // TODO: Enable this when security is implemented correctly
            return false;
            //return HasSecurityRules(cls, true);
        }

        public static bool HasSecurityRules(this ObjectClass cls, bool lookupInBase)
        {
            // TODO: Enable this when security is implemented correctly
            //while (cls != null)
            //{
            //    if (cls.SecurityRules.Count > 0)
            //        return true;
            //    if (!lookupInBase) return false;
            //    cls = cls.BaseObjectClass;
            //}
            return false;
        }

        public static InterfaceType GetDescribedInterfaceType(this ObjectClass cls)
        {
            if (cls == null) { throw new ArgumentNullException("cls"); }

            // TODO: During export schema, while creating a new Database, no custom actions are attached (Database is empty)
            // return new InterfaceType(cls.GetDataType());
            return new InterfaceType(Type.GetType(cls.Module.Namespace + "." + cls.ClassName + ", Kistl.Objects", true));
        }

        public static bool ImplementsIExportable(this ObjectClass cls)
        {
            return ImplementsIExportable(cls, true);
        }

        public static bool ImplementsIExportable(this ObjectClass cls, bool lookupInBase)
        {
            if (cls == null) { throw new ArgumentNullException("cls"); }

            while (cls != null)
            {
                if (cls.ImplementsInterfaces.Count(o => o.ClassName == "IExportable" && o.Module.ModuleName == "KistlBase") == 1)
                    return true;
                if (!lookupInBase) return false;
                cls = cls.BaseObjectClass;
            }
            return false;
        }

        public static IList<Property> GetAllProperties(this ObjectClass cls)
        {
            if (cls == null) { throw new ArgumentNullException("cls"); }

            return cls.GetInheritedProperties().Concat(cls.Properties).ToList();
        }

        public static IList<Property> GetInheritedProperties(this ObjectClass cls)
        {
            if (cls == null) { throw new ArgumentNullException("cls"); }

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
