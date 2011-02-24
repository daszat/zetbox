using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kistl.Server.HttpService
{
    public class KistlServiceFacade : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            // Add the shit
        }
    }
}