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
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

using Autofac;
using Zetbox.API;
using Zetbox.API.Configuration;
using Zetbox.API.Utils;
using Zetbox.App.Extensions;
using Zetbox.Client.Presentables;

namespace Zetbox.Client.ASPNET.Toolkit
{
    public class ZetboxContextManagerModule : IHttpModule, IDisposable
    {
        public static IZetboxContext ZetboxContext
        {
            get
            {
                return (IZetboxContext)HttpContext.Current.Items["__Current_ZetboxContextManagerModule_ZetboxContext"];
            }
            private set
            {
                HttpContext.Current.Items["__Current_ZetboxContextManagerModule_ZetboxContext"] = value;
            }
        }
        public static IViewModelFactory ViewModelFactory { get; private set; }
        public static InterfaceType.Factory IftFactory { get; private set; }

        private static IContainer container;

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            if (context == null) { throw new ArgumentNullException("context"); }

            context.BeginRequest += new EventHandler(context_BeginRequest);
            context.EndRequest += new EventHandler(context_EndRequest);
        }

        void context_EndRequest(object sender, EventArgs e)
        {
            if (ZetboxContext != null)
            {
                ZetboxContext.Dispose();
                ZetboxContext = null;
            }
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            if (container == null)
            {
                Logging.Configure();

                var config = ZetboxConfig.FromFile(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["configFile"]), "AspNet.xml");
                AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);

                var builder = Zetbox.API.Utils.AutoFacBuilder.CreateContainerBuilder(config, config.Client.Modules);
                container = builder.Build();

                ViewModelFactory = container.Resolve<IViewModelFactory>();
                IftFactory = container.Resolve<InterfaceType.Factory>();

            }
            ZetboxContext = container.Resolve<IZetboxContext>();
        }
    }
}
