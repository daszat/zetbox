
namespace Kistl.Server.HttpService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Autofac;
    using Autofac.Integration.Web;
    using Kistl.API;
    using Kistl.API.Configuration;

    public class BootstrapperFacade : IHttpHandler
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server.Service.BootstrapperFacade");

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            Log.DebugFormat("Processing request for [{0}]", context.Request.Url);

            var cpa = (IContainerProviderAccessor)HttpContext.Current.ApplicationInstance;
            var cp = cpa.ContainerProvider;
            BootstrapperService _service = cp.RequestLifetime.Resolve<BootstrapperService>();

            if (context.Request.Url.Segments.Last() == "GetFileInfos")
            {
                var fis = _service.GetFileInfos();
                context.Response.StatusCode = 200;
                context.Response.ContentType = "text/xml";
                fis.ToXmlStream(context.Response.OutputStream);
            }
            Log.DebugFormat("Sending response [{0}]", context.Response.StatusCode);
        }
    }
}