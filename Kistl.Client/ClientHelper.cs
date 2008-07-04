using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Client;
using System.ServiceModel;

namespace Kistl.Client
{
    /// <summary>
    /// Client Helper Methods
    /// </summary>
    public class ClientHelper
    {
        /// <summary>
        /// Auch das könnte man besser implementieren
        /// </summary>
        /// <param name="ex"></param>
        public static void HandleError(Exception ex)
        {
            //TODO: put exception into DB
            if (ex is FaultException<ApplicationException>)
            {
                Manager.Renderer.ShowMessage((ex as ApplicationException).Message);
            }
            else if (ex is FaultException)
            {
                Manager.Renderer.ShowMessage((ex as FaultException).Message);
            }
            else
            {
                Manager.Renderer.ShowMessage(ex.ToString());
            }

            if (Kistl.API.Configuration.KistlConfig.Current.Client.ThrowErrors)
            {
                throw ex;
            }
        }

        private static Dictionary<Type, Kistl.App.Base.ObjectClass> _ObjectClasses = null;
        private static Dictionary<string, Kistl.App.Base.Module> _Modules = null;

        public static void CleanCaches()
        {
            lock (typeof(Helper))
            {
                _ObjectClasses = null;
                _Modules = null;
            }
        }

        private static void FetchObjectClasses()
        {
            lock (typeof(Helper))
            {
                if (_ObjectClasses == null)
                {
                    FetchModules();
                    using (TraceClient.TraceHelper.TraceMethodCall("Getting Object Classes"))
                    {
                        // Prefetch Modules
                        using (IKistlContext ctx = KistlContext.GetContext())
                        {
                            _ObjectClasses = ctx.GetQuery<Kistl.App.Base.ObjectClass>()
                                .ToDictionary(o => o.GetDataCLRType());
                        }
                    }
                }
            }
        }

        public static Dictionary<Type, Kistl.App.Base.ObjectClass> ObjectClasses
        {
            get
            {
                FetchObjectClasses();
                return _ObjectClasses;
            }
        }

        private static void FetchModules()
        {
            lock (typeof(Helper))
            {
                if (_Modules == null)
                {
                    using (TraceClient.TraceHelper.TraceMethodCall("Getting Modules"))
                    {
                        using (IKistlContext ctx = KistlContext.GetContext())
                        {
                            _Modules = ctx.GetQuery<Kistl.App.Base.Module>().ToDictionary(m => m.ModuleName);
                        }
                    }
                }
            }
        }

        public static Dictionary<string, Kistl.App.Base.Module> Modules
        {
            get
            {
                FetchModules();
                return _Modules;
            }
        }
    }

    public static class ClientExtensions
    {

        /// <summary>
        /// joins the string representations of all items in list together, separated by joiner
        /// </summary>
        /// <param name="list"></param>
        /// <param name="joiner"></param>
        /// <returns></returns>
        public static string JoinStrings(this System.Collections.IList list, string joiner)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                sb.Append(item);
                sb.Append(joiner);
            }
            return sb.ToString();
        }

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
            foreach (object i in items)
            {
                list.Insert(index++, (T)i);
            }
        }
    }
}
