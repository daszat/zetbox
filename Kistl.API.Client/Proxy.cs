using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Client
{
    public class Proxy
    {
        /// <summary>
        /// WCF Proxy für das KistService instanzieren.
        /// Konfiguration lt. app.config File
        /// </summary>
        private static KistlService.KistlServiceClient service = new KistlService.KistlServiceClient();

        /// <summary>
        /// WCF Proxy für das KistService
        /// </summary>
        public static KistlService.KistlServiceClient Service
        {
            get
            {
                return service;
            }
        }
    }
}
