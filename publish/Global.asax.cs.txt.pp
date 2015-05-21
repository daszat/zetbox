// 
// Replace your global.asax.cs with this example
// and make sure you keep your own changes
//////////////////////////////////////////////////////////////////////

namespace $rootnamespace$
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using Autofac;
    using Autofac.Integration.Mvc;
    using Zetbox.API;
    using Zetbox.API.Utils;
    using Zetbox.Client;
    using Zetbox.Client.ASPNET;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : ZetboxMvcApplication
    {
        public override void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }

        public override void RegisterRoutes(RouteCollection routes)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        //protected override void ConfigureContainerBuilder(Autofac.ContainerBuilder builder)
        //{
        //    base.ConfigureContainerBuilder(builder);
		//
        //    builder.RegisterModule<YOURMODULE.Client.ClientModule>();
        //}
    }
}