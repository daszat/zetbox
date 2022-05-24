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
using System.Diagnostics;
using System.Linq;
using System.Text;

using Zetbox.API;
using Zetbox.API.Client;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.API.Utils;
using Zetbox.Client.Presentables;
using Autofac;
using Zetbox.App.GUI;
using System.Threading.Tasks;

namespace Zetbox.Client
{
    public static class ClientExtensions
    {
        /// <summary>
        /// Insert a range of items into an IList at a specified index
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <param name="items"></param>
        /// This implementation is quite lazy, but IList doesn't have any better methods.
        /// A more sophisticated implementation could test for specific IList implementations to do better.
        public static void InsertRange<T>(this IList<T> list, int index, System.Collections.IList items)
        {
            if (list == null) { throw new ArgumentNullException("list"); }
            if (items == null) { return; }

            foreach (object i in items)
            {
                list.Insert(index++, (T)i);
            }
        }

        public static async Task<WidthHint> GetDisplayWidth(this Property p)
        {
            if(p == null) throw new ArgumentNullException("p");

            if (p.RequestedWidth != null && p.RequestedWidth != Zetbox.App.GUI.WidthHint.Default)
            {
                return p.RequestedWidth.Value;
            }

            if (p is StringProperty)
            {
                var sp = (StringProperty)p;
                var length = await sp.GetMaxLength();
                if (length >= 1000) return WidthHint.Huge;
                if (length >= 500) return WidthHint.Large;
                if (length >= 200) return WidthHint.Medium;
                return WidthHint.Small;
            }
            else if (p is ObjectReferenceProperty)
            {
                return WidthHint.Medium;
            }
            else if (p is DateTimeProperty)
            {
                var dtp = (DateTimeProperty)p;
                switch (dtp.DateTimeStyle)
                {
                    case DateTimeStyles.Date:
                    case DateTimeStyles.Time:
                        return WidthHint.Small;
                    default:
                        return WidthHint.Medium;
                }
            }
            else if (p is CalculatedObjectReferenceProperty)
            {
                return WidthHint.Medium;
            }
            else if (p is BoolProperty)
            {
                return WidthHint.Tiny;
            }
            else
            {
                return WidthHint.Small;
            }
        }

        /// <summary>
        /// Register all ViewModel Types
        /// </summary>
        /// <param name="moduleBuilder"></param>
        /// <param name="assembly"></param>
        public static void RegisterViewModels(this ContainerBuilder moduleBuilder, System.Reflection.Assembly assembly)
        {
            if (moduleBuilder == null) throw new ArgumentNullException("moduleBuilder");
            if (assembly == null) throw new ArgumentNullException("assembly");

            foreach (var t in assembly.GetTypes()
                .Where(t => typeof(ViewModel).IsAssignableFrom(t)))
            {
                if (t.IsGenericTypeDefinition)
                {
                    moduleBuilder.RegisterGeneric(t)
                        .InstancePerDependency();
                }
                else
                {
                    moduleBuilder.RegisterType(t)
                        .InstancePerDependency();
                }
            }
        }
    }
}
