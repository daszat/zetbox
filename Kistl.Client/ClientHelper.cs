using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

using Kistl.API;
using Kistl.API.Client;
using Kistl.App.Base;

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
        private static Dictionary<Type, Kistl.App.Base.ObjectClass> _ObjectClasses = null;
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
                    using (TraceClient.TraceHelper.TraceMethodCall("Getting Object Classes"))
                    {
                        // Prefetch Modules
                        //_ObjectClasses = _fetchContext.GetQuery<Kistl.App.Base.ObjectClass>()
                        //    .ToDictionary(o => o.GetDataCLRType());
                        _ObjectClasses = new Dictionary<Type, Kistl.App.Base.ObjectClass>();
                        foreach (var o in FrozenContext.Single.GetQuery<ObjectClass>())
                        {
                            try
                            {
                                _ObjectClasses[o.GetDataType()] = o;
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine(ex.ToString());
                            }
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

        //private static void FetchModules()
        //{
        //    lock (typeof(Helper))
        //    {
        //        if (_Modules == null)
        //        {
        //            using (TraceClient.TraceHelper.TraceMethodCall("Getting Modules"))
        //            {
        //                _Modules = _fetchContext.GetQuery<Kistl.App.Base.Module>().ToDictionary(m => m.ModuleName);
        //            }
        //        }
        //    }
        //}

        //public static Dictionary<string, Kistl.App.Base.Module> Modules
        //{
        //    get
        //    {
        //        FetchModules();
        //        return _Modules;
        //    }
        //}
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
