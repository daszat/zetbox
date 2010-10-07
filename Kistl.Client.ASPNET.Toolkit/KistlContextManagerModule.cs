using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

using Autofac;
using Kistl.API;
using Kistl.API.Configuration;
using Kistl.API.Utils;
using Kistl.App.Extensions;
using Kistl.Client.Presentables;

namespace Kistl.Client.ASPNET.Toolkit
{
    public class KistlContextManagerModule : IHttpModule, IDisposable
    {
        public static IKistlContext KistlContext
        {
            get
            {
                return (IKistlContext)HttpContext.Current.Items["__Current_KistlContextManagerModule_KistlContext"];
            }
            private set
            {
                HttpContext.Current.Items["__Current_KistlContextManagerModule_KistlContext"] = value;
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
            if (KistlContext != null)
            {
                KistlContext.Dispose();
                KistlContext = null;
            }
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            if (container == null)
            {
                Logging.Configure();

                var config = KistlConfig.FromFile(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["configFile"]));
                AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);

                var builder = Kistl.API.Utils.AutoFacBuilder.CreateContainerBuilder(config, config.Client.Modules);
                container = builder.Build();

                ViewModelFactory = container.Resolve<IViewModelFactory>();
                IftFactory = container.Resolve<InterfaceType.Factory>();

            }
            KistlContext = container.Resolve<IKistlContext>();
        }
    }
}
