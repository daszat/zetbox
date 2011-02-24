
namespace Kistl.Server.HttpService
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using Autofac;
    using Autofac.Integration.Web;
    using Kistl.API.Common;

    public class KistlServiceFacade : IHttpHandler
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server.Service.KistlServiceFacade");

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var cpa = (IContainerProviderAccessor)HttpContext.Current.ApplicationInstance;
            var scope = cpa.ContainerProvider.RequestLifetime;

            Log.DebugFormat("Processing request for [url={0}], as [user={1}]",
                context.Request.Url,
                scope.Resolve<IIdentityResolver>().GetCurrent().DisplayName);

            switch (context.Request.Url.Segments.Last())
            {
                case "SetObjects":
                case "GetList":
                case "GetListOf":
                case "FetchRelation":
                case "GetBlobStream":
                case "SetBlobStream":
                case "InvokeServerMethod":
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "text/plain";
                    using (var outStream = new StreamWriter(context.Response.OutputStream))
                    {
                        outStream.WriteLine("haha!");
                    }
                    break;
                default:
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "text/plain";
                    using (var outStream = new StreamWriter(context.Response.OutputStream))
                    {
                        outStream.WriteLine("haha!");
                        outStream.WriteLine("Authenticated User: [{0}]", scope.Resolve<IIdentityResolver>().GetCurrent().DisplayName);
                    }
                    break;
            }
            Log.DebugFormat("Sending response [{0}]", context.Response.StatusCode);
        }
    }
}