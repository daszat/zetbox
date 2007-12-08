using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Client
{
    public class Proxy : IKistlService
    {
        /// <summary>
        /// WCF Proxy für das KistlService instanzieren.
        /// Konfiguration lt. app.config File
        /// </summary>
        private KistlService.KistlServiceClient service = new KistlService.KistlServiceClient();
        private static Proxy current = new Proxy();

        /// <summary>
        /// WCF Proxy für das KistlService
        /// </summary>
        public static IKistlService Service
        {
            get
            {
                return current;
            }
        }

        #region IKistlService Members

        public string GetList(ObjectType type)
        {
            using (TraceClient.TraceHelper.TraceMethodCall(type.ToString()))
            {
                return service.GetList(type);
            }
        }

        public string GetListOf(ObjectType type, int ID, string property)
        {
            using (TraceClient.TraceHelper.TraceMethodCall("{0} [{1}].{2}", type, ID, property))
            {
                return service.GetListOf(type, ID, property);
            }
        }

        public string GetObject(ObjectType type, int ID)
        {
            using (TraceClient.TraceHelper.TraceMethodCall("{0} [{1}]", type, ID))
            {
                string result = CacheController<string>.Current.Get(type, ID);
                if (string.IsNullOrEmpty(result))
                {
                    result = service.GetObject(type, ID);
                    CacheController<string>.Current.Set(type, ID, result);
                }
                return result;
            }
        }

        public string SetObject(ObjectType type, string obj)
        {
            using (TraceClient.TraceHelper.TraceMethodCall("{0}", type))
            {
                CacheController<string>.Current.Clear();
                return service.SetObject(type, obj);
            }
        }

        public string HelloWorld(string name)
        {
            using (TraceClient.TraceHelper.TraceMethodCall(name))
            {
                return service.HelloWorld(name);
            }
        }

        #endregion
    }
}
