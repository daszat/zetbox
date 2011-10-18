using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;

using Kistl.API;
using Kistl.API.Client;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.API.Utils;
using Kistl.Client.Presentables;
using Autofac;

namespace Kistl.Client
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

        public static readonly int WIDTH_TINY = 60;
        public static readonly int WIDTH_SMALL = 120; // MinWidth for most controls is 100 plus some margins
        public static readonly int WIDTH_NORMAL = 200;
        public static readonly int WIDTH_LARGE = 300;
        public static readonly int WIDTH_HUGE = 500;

        public static int GetDisplayWidth(this Property p)
        {
            if (p is StringProperty)
            {
                var sp = (StringProperty)p;
                var length = sp.GetMaxLength();
                if (length > 1000) return WIDTH_HUGE;
                if (length > 500) return WIDTH_LARGE;
                if (length > 200) return WIDTH_NORMAL;
                return WIDTH_SMALL;
            }
            else if (p is ObjectReferenceProperty)
            {
                return WIDTH_NORMAL;
            }
            else if (p is DateTimeProperty)
            {
                return WIDTH_NORMAL;
            }
            else if (p is CalculatedObjectReferenceProperty)
            {
                return WIDTH_NORMAL;
            }
            else if (p is BoolProperty)
            {
                return WIDTH_TINY;
            }
            else
            {
                return WIDTH_SMALL;
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
