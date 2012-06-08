// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Zetbox.API;
using Zetbox.App.Base;

namespace Zetbox.App.Extensions
{
    public static class TypeRefExtensions
    {
        // private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.TypeRef");
        private const string transientCacheKey = "__TypeRefExtensionsCache__";

        public static object Create(this TypeRef t, params object[] parameter)
        {
            if (t == null) { throw new ArgumentNullException("t"); }

            return Activator.CreateInstance(t.AsType(true), parameter);
        }

        /// <returns>a Zetbox TypeRef for a given System.Type</returns>
        public static TypeRef ToRef(this Type t, IReadOnlyZetboxContext ctx)
        {
            if (t == null) { throw new ArgumentNullException("t"); }
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            // TODO: think about and implement naked types (i.e. without arguments)
            if (t.IsGenericTypeDefinition) { throw new ArgumentOutOfRangeException("t"); }

            var result = GetFromCache(t, ctx);
            if (result != null) return result;

            result = LookupByType(ctx, ctx.GetQuery<TypeRef>(), t);
            if (result != null)  AddToCache(t, result, ctx);

            return result;
        }

        /// <returns>a Zetbox TypeRef for a given System.Type</returns>
        public static TypeRef ToRef(this Type t, IZetboxContext ctx)
        {
            if (t == null) { throw new ArgumentNullException("t"); }
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            // TODO: think about and implement naked types (i.e. without arguments)
            if (t.IsGenericTypeDefinition) { throw new ArgumentOutOfRangeException("t"); }

            var result = GetFromCache(t, ctx);
            if (result != null) return result;

            result = LookupByType(ctx, ctx.GetQuery<TypeRef>(), t);
            if (result == null)
            {
                result = CreateTypeRef(t, ctx);
            }
            AddToCache(t, result, ctx);
            return result;
        }

        private static TypeRef CreateTypeRef(Type t, IZetboxContext ctx)
        {
            var result = ctx.Create<TypeRef>();
            var a = t.Assembly.ToRef(ctx);
            result.Assembly = a;

            if (t.IsGenericType)
            {
                var genericDefinition = t.GetGenericTypeDefinition();
                result.FullName = genericDefinition.FullName;
                result.GenericArguments.Clear();
                foreach (var arg in t.GetGenericArguments())
                {
                    result.GenericArguments.Add(arg.ToRef(ctx));
                }
            }
            else
            {
                result.FullName = t.FullName;
            }

            if (t.BaseType != null)
            {
                result.Parent = t.BaseType.ToRef(ctx);
            }
            return result;
        }

        private static TypeRef LookupByType(IReadOnlyZetboxContext ctx, IQueryable<TypeRef> source, Type t)
        {
            // TODO: think about and implement naked types (i.e. without arguments)
            if (t.IsGenericTypeDefinition) throw new ArgumentOutOfRangeException("t");

            if (t.IsGenericType)
            {
                string fullName = t.GetGenericTypeDefinition().FullName;
                var args = t.GetGenericArguments().Select(arg => arg.ToRef(ctx)).ToArray();
                var argsCount = args.Count();
                foreach (var tRef in source.Where(tRef
                    => tRef.Assembly.Name == t.Assembly.FullName
                    && tRef.FullName == fullName
                    && tRef.GenericArguments.Count == argsCount))
                {
                    bool equal = true;
                    for (int i = 0; i < tRef.GenericArguments.Count; i++)
                    {
                        equal &= args[i] == tRef.GenericArguments[i];
                        if (!equal)
                            break;
                    }
                    if (equal)
                        return tRef;
                }
                return null;
            }
            else
            {
                // ToList: Workaround Case 1212
                return source.FirstOrDefault(tRef
                    => tRef.Assembly.Name == t.Assembly.FullName
                    && tRef.FullName == t.FullName
                    && tRef.GenericArguments.Count == 0);
            }
        }

        /// <summary>
        /// returns a zetbox Assembly for a given CLR-Assembly or null if it is not stored
        /// </summary>
        public static Assembly ToRefOrDefault(this System.Reflection.Assembly ass, IZetboxContext ctx)
        {
            if (ass == null) { throw new ArgumentNullException("ass"); }
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            // ToList: Workaround Case 1212
            return ctx.GetQuery<Assembly>().ToList().SingleOrDefault(a => a.Name == ass.FullName);
        }

        /// <summary>
        /// returns a zetbox Assembly for a given CLR-Assembly, creating it if necessary
        /// </summary>
        public static Assembly ToRef(this System.Reflection.Assembly ass, IZetboxContext ctx)
        {
            if (ass == null) { throw new ArgumentNullException("ass"); }
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            Assembly result = ass.ToRefOrDefault(ctx);
            if (result == null)
            {
                result = ctx.Create<Assembly>();
                result.Name = ass.FullName;
                // Leave module empty. Must be set external
                // result.Module = ctx.GetQuery<Module>().Single(m => m.Name == "ZetboxBase");
                result.DeploymentRestrictions = DeploymentRestriction.ClientOnly;
            }
            return result;
        }

        #region Cache Part
        private static TypeRef GetFromCache(Type t, IReadOnlyZetboxContext ctx)
        {
            if (!ctx.TransientState.ContainsKey(transientCacheKey))
            {
                ctx.TransientState[transientCacheKey] = new Dictionary<Type, TypeRef>();
            }
            var cache = (Dictionary<Type, TypeRef>)ctx.TransientState[transientCacheKey];
            if (cache.ContainsKey(t)) return cache[t];
            return null;
        }

        private static void AddToCache(Type t, TypeRef tr, IReadOnlyZetboxContext ctx)
        {
            if (!ctx.TransientState.ContainsKey(transientCacheKey))
            {
                ctx.TransientState[transientCacheKey] = new Dictionary<Type, TypeRef>();
            }
            var cache = (Dictionary<Type, TypeRef>)ctx.TransientState[transientCacheKey];
            cache[t] = tr;
        }
        #endregion
    }

}
