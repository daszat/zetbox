// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.Server.HttpService
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Xml.Serialization;
    using Autofac;
    using Autofac.Integration.Web;
    using Zetbox.API;
    using Zetbox.API.Configuration;

    [XmlRoot("ArrayOfFileInfo", Namespace = "http://dasz.at/Zetbox/Bootstrapper")]
    public class FileInfoArray
    {
        [XmlElement("FileInfo")]
        public Zetbox.Server.FileInfo[] Files { get; set; }
    }

    public class BootstrapperFacade : IHttpHandler
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Server.Service.BootstrapperFacade");

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
                        Log.DebugFormat("Sending [{0}] file infos", fileInfoArray.Files.Length);
                        fileInfoArray.ToXmlStream(context.Response.OutputStream);
                        break;
                    case "GetFile":
                        var probe = _service.GetFilePath(context.Request["path"]);
                        if (File.Exists(probe))
                        {
                            Log.DebugFormat("Sending file [{0}]", probe);
                            context.Response.TransmitFile(probe);
                        }
                        else
                        {
                            Log.DebugFormat("file [{0}] not found", probe);
                            context.Response.StatusCode = 404;
                        }
                        break;
                    default:
                        Log.DebugFormat("Unknown action [{0}]", action);
                        context.Response.StatusCode = 400;
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error while processing request for [{0}]", context.Request.Url), ex);
                throw;
            }
        }
    }
}