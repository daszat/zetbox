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
        /// <summary>
        /// Auch das könnte man besser implementieren
        /// </summary>
        /// <param name="ex"></param>
        public static void HandleError(Exception ex)
        {
            //TODO: put exception into DB/Logfile
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

        // TODO: Das muss in "statische" Objekte, oder auch Immutable Objects genannt, umgewandelt werden.
        private static Dictionary<InterfaceType, ObjectClass> _ObjectClasses = null;
        //private static Dictionary<string, Kistl.App.Base.Module> _Modules = null;

        // TODO: Arthur: Der Context da ist mir ein Dorn im Auge.
        //private static IKistlContext _fetchContext = KistlContext.GetContext();

        //public static void CleanCaches()
        //{
        //    lock (typeof(Helper))
        //    {
        //        _ObjectClasses = null;
        //        _Modules = null;
        //        if (_fetchContext != null)
        //        {
        //            _fetchContext.Dispose();
        //        }
        //        _fetchContext = KistlContext.GetContext();
        //    }
        //}

        private static void FetchObjectClasses()
        {
            lock (typeof(Helper))
            {
                if (_ObjectClasses == null)
                {
                    using (Logging.Log.TraceMethodCall("Getting Object Classes"))
                    {
                        // Prefetch Modules
                        //_ObjectClasses = _fetchContext.GetQuery<Kistl.App.Base.ObjectClass>()
                        //    .ToDictionary(o => o.GetDataCLRType());
                        _ObjectClasses = new Dictionary<InterfaceType, ObjectClass>();
                        foreach (var o in FrozenContext.Single.GetQuery<ObjectClass>())
                        {
                            try
                            {
                                _ObjectClasses[o.GetDescribedInterfaceType()] = o;
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex.ToString());
                            }
                        }
                    }
                }
            }
        }

        public static Dictionary<InterfaceType, ObjectClass> ObjectClasses
        {
            get
            {
                FetchObjectClasses();
                return _ObjectClasses;
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
            foreach (object i in items)
            {
                list.Insert(index++, (T)i);
            }
        }
    }
}
