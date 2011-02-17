namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class TypeRefActions
    {
        [Invocation]
        public static void ToString(TypeRef obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("{0}{1}",
                obj.Deleted == true ? "(DELETED) " : string.Empty,
                obj.FullName);

            ToStringHelper.FixupFloatingObjectsToString(obj, e);
        }
        [Invocation]
        public static void UpdateToStringCache(Kistl.App.Base.TypeRef obj)
        {
            //obj.ToStringCache = String.Format("{0}{1}, {2}",
            //    obj.FullName,
            //    obj.GenericArguments.Count > 0
            //        ? "<" + String.Join(", ", obj.GenericArguments.Select(tr => tr.FullName).ToArray()) + ">"
            //        : String.Empty,
            //        obj.Assembly == null ? "(no assembly)" : obj.Assembly.Name);
        }

        [Invocation]
        public static void postSet_FullName(Kistl.App.Base.TypeRef obj, PropertyPostSetterEventArgs<System.String> e)
        {
            obj.UpdateToStringCache();
        }

        [Invocation]
        public static void postSet_Assembly(Kistl.App.Base.TypeRef obj, PropertyPostSetterEventArgs<Kistl.App.Base.Assembly> e)
        {
            obj.UpdateToStringCache();
        }

        // Not supported yet
        //public static void OnGenericArguments_PostSetter_TypeRef(Kistl.App.Base.TypeRef obj, PropertyPostSetterEventArgs<Kistl.App.Base.TypeRef> e)
        //{
        //    obj.UpdateToStringCache();
        //}

        private const string transientTypeTypeRefCacheKey = "__TypeTypeRefCache__";
        [Invocation]
        public static void AsType(TypeRef obj, MethodReturnEventArgs<Type> e, bool throwOnError)
        {
            Dictionary<TypeRef, Type> cache;
            if (obj.Context.TransientState.ContainsKey(transientTypeTypeRefCacheKey))
            {
                cache = (Dictionary<TypeRef, Type>)obj.Context.TransientState[transientTypeTypeRefCacheKey];
            }
            else
            {
                cache = new Dictionary<TypeRef, Type>();
                obj.Context.TransientState[transientTypeTypeRefCacheKey] = cache;
            }

            if (cache.ContainsKey(obj))
            {
                e.Result = cache[obj];
                return;
            }

            e.Result = Type.GetType(String.Format("{0}, {1}", obj.FullName, obj.Assembly.Name), false);
            if (e.Result == null)
            {
                // Try ReflectionOnly
                System.Reflection.Assembly a = null;
                try
                {
                    a = System.Reflection.Assembly.ReflectionOnlyLoad(obj.Assembly.Name);
                }
                catch
                {
                    if (throwOnError) throw;
                    cache[obj] = e.Result;
                    return;
                }
                e.Result = a.GetType(obj.FullName, throwOnError);
            }

            if (e.Result == null)
            {
                cache[obj] = e.Result;
                return;
            }
            if (obj.GenericArguments.Count > 0)
            {
                var args = obj.GenericArguments.Select(tRef => tRef.AsType(throwOnError)).ToArray();
                if (args.Contains(null))
                {
                    e.Result = null;
                    if (throwOnError)
                    {
                        throw new InvalidOperationException("Cannot create Type: missing generic argument");
                    }
                    else
                    {
                        cache[obj] = e.Result;
                        return;
                    }
                }
                e.Result = e.Result.MakeGenericType(args);
            }
            cache[obj] = e.Result;
        }
    }
}
