
namespace Kistl.Server.HttpService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Autofac.Integration.Web;
    using Autofac;
    using Kistl.API.Configuration;
    using Kistl.API;

    public class BootstrapperFacade : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
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
        }
    }
}