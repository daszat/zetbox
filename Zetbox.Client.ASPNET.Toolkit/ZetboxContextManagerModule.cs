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
