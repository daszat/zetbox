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
    using System.Text;
    using System.Web;
    using Autofac;
    using Autofac.Integration.Web;
    using Zetbox.API.Server.PerfCounter;

    public class PerfMonFacade : IHttpHandler
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Server.Service.PerfMonFacade");

        public bool IsReusable
        {
            get { return true; }
        }

        private ResetOnReadAppender _appender = null;

        public void ProcessRequest(HttpContext context)
        {
            Log.DebugFormat("Processing request for [{0}]", context.Request.Url);

            FetchAppender();

            var data = _appender.ReadAndResetValues();
            context.Response.StatusCode = 200;
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";
            using (var writer = new StreamWriter(context.Response.OutputStream, Encoding.UTF8))
            {

                foreach (var kvp in data.Totals)
                {
                    writer.WriteLine("{0}: {1}", kvp.Key, kvp.Value);
                }

                foreach (var kvp in data.Objects)
                {
                    var collector = new Dictionary<string, string>();
                    kvp.Value.FormatTo(collector);

                    foreach (var obj in collector)
                    {
                        writer.WriteLine("{0}/{1}: {2}", kvp.Key, obj.Key, obj.Value);
                    }
                }
            }
        }

        private void FetchAppender()
        {

            if (_appender == null)
            {
                var cpa = (IContainerProviderAccessor)HttpContext.Current.ApplicationInstance;
                var cp = cpa.ContainerProvider;
                _appender = cp.RequestLifetime.Resolve<ResetOnReadAppender>();
            }
        }
    }
}