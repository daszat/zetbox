using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Kistl.Server.HttpService
{
    public class KistlServiceFacade : IHttpHandler
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server.Service.KistlServiceFacade");

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            Log.DebugFormat("Processing request for [{0}]", context.Request.Url);
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
                        outStream.WriteLine("Authenticated User: [{0}]", context.Request.Headers.Get("Authorization"));
                    }
                    break;
            }
            Log.DebugFormat("Sending response [{0}]", context.Response.StatusCode);
        }
    }
}