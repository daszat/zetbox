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

namespace Kistl.Client
{
    /// <summary>
    /// Client Helper Methods
    /// </summary>
    public static class ClientHelper
    {
        private readonly static object _lock = new object();

        public static void HandleError(Exception ex)
        {
            //TODO: put exception into DB/Logfile
            if (ex == null) { throw new ArgumentNullException("ex"); }

            if (ex is FaultException)
            {
                GuiApplicationContext.Current.Renderer.ShowMessage((ex as FaultException).Message);
            }
            else
            {
                GuiApplicationContext.Current.Renderer.ShowMessage(ex.ToString());
            }

            if (GuiApplicationContext.Current.Configuration.Client.ThrowErrors)
            {
                throw ex;
            }
        }
    }

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
    }
}
