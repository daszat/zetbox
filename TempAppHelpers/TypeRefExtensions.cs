using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.App.Extensions
{
    public static class TypeRefExtensions
    {

        public static object Create(this TypeRef t, params object[] parameter)
        {
            return Activator.CreateInstance(t.AsType(true), parameter);
        }

        /// <returns>a Kistl TypeRef for a given System.Type</returns>
        public static TypeRef ToRef(this Type t, IKistlContext ctx)
        {
            if (t == null) throw new ArgumentNullException("t");

            if (ctx == FrozenContext.Single)
            {
                return ToFrozenRef(t);
            }

            var result = LookupByType(ctx.GetQuery<TypeRef>(), t);
            if (result == null)
            {
                result = ctx.Create<TypeRef>();
                result.FullName = t.FullName;
                var a = t.Assembly.ToRefOrDefault(ctx);
                result.Assembly = a;
            }
            return result;
        }

        private static TypeRef ToFrozenRef(this Type t)
        {
            PrimeRefCache();
            return LookupByType(_typeRefsByFullName[t.FullName].AsQueryable(), t);
        }

        private static ILookup<string, TypeRef> _typeRefsByFullName;

        private static void PrimeRefCache()
        {
            if (_typeRefsByFullName == null)
            {
                _typeRefsByFullName = FrozenContext.Single.GetQuery<TypeRef>().ToLookup(obj => obj.FullName);
            }
        }

        private static TypeRef LookupByType(IQueryable<TypeRef> source, Type t)
        {
            return source.SingleOrDefault(tRef => tRef.Assembly.AssemblyName == t.Assembly.FullName && tRef.FullName == t.FullName && tRef.GenericArguments.Count == 0);
        }


        /// <summary>
        /// returns a kistl Assembly for a given CLR-Assembly
        /// </summary>
        public static Assembly ToRefOrDefault(this System.Reflection.Assembly ass, IKistlContext ctx)
        {
            return ctx.GetQuery<Assembly>().SingleOrDefault(a => a.AssemblyName == ass.FullName);
        }

    }
}
