
namespace Kistl.Server.HttpService
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Security;
    using System.Web.SessionState;
    using Autofac;
    using Autofac.Configuration;
    using Autofac.Integration.Wcf;
    using Autofac.Integration.Web;
    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.Utils;

    public class Global : System.Web.HttpApplication, IContainerProviderAccessor
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server.Service");

        // Provider that holds the application container.
        static IContainerProvider _containerProvider;

        private static string useHttpFacade;

        // Instance property that will be used by Autofac HttpModules
        // to resolve and inject dependencies.
        public IContainerProvider ContainerProvider
        {
            get { return _containerProvider; }
        }

        void CreateMasterContainer(KistlConfig config)
        {
            var builder = Kistl.API.Utils.AutoFacBuilder.CreateContainerBuilder(config, config.Server.Modules);

            // register deployment-specific components
            builder.RegisterModule(new ConfigurationSettingsReader("servercomponents"));

            // Store root container for WCF & ASP.NET
            var container = builder.Build();
            AutofacHostFactory.Container = container;
            _containerProvider = new ContainerProvider(container);
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            Logging.Configure();

            Log.Info("Starting Kistl Server");

            useHttpFacade = System.Configuration.ConfigurationManager.AppSettings["UseHttpFacade"];
            var cfgFile = System.Configuration.ConfigurationManager.AppSettings["ConfigFile"];

            var config = KistlConfig.FromFile(
                string.IsNullOrEmpty(cfgFile) ? string.Empty : Server.MapPath(cfgFile), 
                KistlConfig.GetDefaultConfigName("Kistl.Server.HttpService.xml", Path.Combine(Path.Combine(Server.MapPath("~/"), ".."), "Configs")));
            
            AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);
            CreateMasterContainer(config);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (string.Equals(useHttpFacade, "true", StringComparison.CurrentCultureIgnoreCase))
            {
                if (HttpContext.Current.Request.Url.PathAndQuery.ToLower().Contains("bootstrapper.svc"))
                {
                    var newUrl = HttpContext.Current.Request.Url.PathAndQuery.Replace("Bootstrapper.svc", "Bootstrapper.facade");
                    HttpContext.Current.Response.Redirect(newUrl);
                }
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}