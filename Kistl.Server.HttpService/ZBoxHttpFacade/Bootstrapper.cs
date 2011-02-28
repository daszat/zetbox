
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
    using System.IO;
    using System.Xml.Serialization;

    [XmlRoot("ArrayOfFileInfo", Namespace = "http://dasz.at/ZBox/Bootstrapper")]
    public class FileInfoArray
    {
        [XmlElement("FileInfo")]
        public Kistl.Server.FileInfo[] Files { get; set; }
    }

    public class BootstrapperFacade : IHttpHandler
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server.Service.BootstrapperFacade");

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                Log.DebugFormat("Processing request for [{0}]", context.Request.Url);

                var cpa = (IContainerProviderAccessor)HttpContext.Current.ApplicationInstance;
                var cp = cpa.ContainerProvider;
                BootstrapperService _service = cp.RequestLifetime.Resolve<BootstrapperService>();

                var action = context.Request["action"];

                Log.DebugFormat("Processing action [{0}]", action);
                switch (action)
                {
                    case "GetFileInfos":
                        var fileInfoArray = new FileInfoArray() { Files = _service.GetFileInfos() };
                        context.Response.StatusCode = 200;
                        context.Response.ContentType = "text/xml";
                        fileInfoArray.ToXmlStream(context.Response.OutputStream);
                        break;
                    case "GetFile":
                        var probe = _service.GetFilePath(context.Request["directory"], context.Request["file"]);
                        if (File.Exists(probe))
                        {
                            context.Response.TransmitFile(probe);
                        }
                        else
                        {
                            context.Response.StatusCode = 404;
                        }
                        break;
                    default:
                        context.Response.StatusCode = 400;
                        break;
                }
                Log.DebugFormat("Sending response [{0}]", context.Response.StatusCode);
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error while processing request for [{0}]", context.Request.Url), ex);
                throw;
            }
        }
    }
}