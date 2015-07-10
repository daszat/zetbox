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
    public static class ZetboxContextExtensions
    {
        public static Interface GetIExportableInterface(this IReadOnlyZetboxContext ctx)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            // TODO: use named objects
            return ctx.GetQuery<Zetbox.App.Base.Interface>().First(o => o.Name == "IExportable" && o.Module.Name == "ZetboxBase");
        }

        /// <summary>
        /// Return a singelton of the given type. Returns null, if no instance was found. Throws an exception, when more than one object was found.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ctx"></param>
        /// <param name="throwIfNotFound"></param>
        /// /// <returns></returns>
        public static T Singleton<T>(this IReadOnlyZetboxContext ctx, bool throwIfNotFound = true)
            where T : class, IDataObject
        {
            if (ctx == null) throw new ArgumentNullException("ctx");

            var obj = ctx.GetQuery<T>().SingleOrDefault();
            if (throwIfNotFound && obj == null)
            {
                throw new InvalidOperationException(string.Format("The requested object of type {0} was not created yet.", typeof(T).FullName));
            }
            return obj;
        }

        /// <summary>
        /// Checks, if only 0 or 1 instances of the given type are present in the given context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ctx"></param>
        /// <param name="singleton"></param>
        /// <returns></returns>
        public static bool IsSingleton<T>(this IReadOnlyZetboxContext ctx, T singleton = null)
            where T : class, IDataObject
        {
            if (ctx == null) throw new ArgumentNullException("ctx");

            var all = ctx.GetQuery<T>().ToList();
            var isSingle = true;
            if (singleton != null)
            {
                all = all.Where(i => i != singleton).ToList();
                isSingle = all.Count == 0;
            }
            else
            {
                isSingle = all.Count <= 1;
            }

            return isSingle;
        }

        /// <summary>
        /// Checks, if only 0 or 1 instances of the given type are present in the given context. Throws an InvalidOperationException if there are more than one instances.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ctx"></param>
        /// <param name="singleton"></param>
        public static void EnsureSingleton<T>(this IReadOnlyZetboxContext ctx, T singleton = null)
            where T : class, IDataObject
        {
            if (!IsSingleton<T>(ctx, singleton))
                throw new InvalidOperationException(string.Format("The Type {0} is not a singletion", typeof(T).FullName));
        }
    }
}
