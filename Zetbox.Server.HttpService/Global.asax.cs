
namespace Zetbox.Server.HttpService
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
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using System.Text.RegularExpressions;

    public class Global : System.Web.HttpApplication, IContainerProviderAccessor
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Server.Service");

        // Provider that holds the application container.
        static IContainerProvider _containerProvider;

        // Instance property that will be used by Autofac HttpModules
        // to resolve and inject dependencies.
        public IContainerProvider ContainerProvider
        {
            get { return _containerProvider; }
        }

        void CreateMasterContainer(ZetboxConfig config)
        {
            var builder = Zetbox.API.Utils.AutoFacBuilder.CreateContainerBuilder(config, config.Server.Modules);

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

            Log.Info("Starting HttpService Application");

            var cfgFile = System.Configuration.ConfigurationManager.AppSettings["ConfigFile"];

            var appBasePath = Server.MapPath("~/");
            var zbBasePath = Path.Combine(appBasePath, "..");
            var configsPath = Path.Combine(zbBasePath, "Configs");

            var config = ZetboxConfig.FromFile(
                string.IsNullOrEmpty(cfgFile) ? string.Empty : Server.MapPath(cfgFile),
                ZetboxConfig.GetDefaultConfigName("Zetbox.Server.HttpService.xml", configsPath));

            // Make DocumentStore relative to HttpService
            config.Server.DocumentStore = Path.Combine(appBasePath, config.Server.DocumentStore);

            AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);
            CreateMasterContainer(config);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        #region url rewrite for bootstrappper
        private class Rewrite
        {
            public Rewrite(string url, string to)
            {
                this.Url = new Regex(url, RegexOptions.IgnoreCase);
                this.To = to;
            }
            public Regex Url { get; private set; }
            public string To { get; private set; }

            public bool IsMatch(Uri uri)
            {
                return Url.IsMatch(uri.AbsoluteUri);
            }

            public string GetRewriteUrl(Uri uri)
            {
                return Url.Replace(uri.AbsolutePath, To);
            }
        }

        private static readonly List<Rewrite> _rewrite = new List<Rewrite>() {
            new Rewrite("^(.*)/Bootstrapper.svc/([^/]+)$", "$1/Bootstrapper.facade?action=$2"),
            new Rewrite("^(.*)/Bootstrapper.svc/([^/]+)/(.+)", "$1/Bootstrapper.facade?action=$2&path=$3") 
        };

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // In a web application the http facade is responsible to handling
            // bootstrapper requests. In a WCF selfhostetd scenario not - wcf itself is responsible
            // this limitation is only for simpler configuration
            foreach (var r in _rewrite)
            {
                if (r.IsMatch(Request.Url))
                {
                    Context.RewritePath(r.GetRewriteUrl(Request.Url));
                    break;
                }
            }
        }
        #endregion

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