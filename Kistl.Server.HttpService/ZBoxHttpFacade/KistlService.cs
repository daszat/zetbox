using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

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
                    throw new NotImplementedException();
            }
        }
    }
}