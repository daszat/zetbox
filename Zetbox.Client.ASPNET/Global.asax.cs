﻿// This file is part of zetbox.
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

namespace Zetbox.Client.ASPNET
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Autofac.Integration.Mvc;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Zetbox.Client;
    using Autofac.Integration.WebApi;
    using System.Web.Http;
    using Autofac;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : ZetboxMvcApplication
    {
        public override void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            filters.Add(new HandleErrorAttribute());
        }

        public override void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected override void ConfigureContainerBuilder(Autofac.ContainerBuilder builder)
        {
            base.ConfigureContainerBuilder(builder);

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);
            builder.RegisterViewModels(typeof(MvcApplication).Assembly);
        }

        protected override void SetupResolver(IContainer container)
        {
            base.SetupResolver(container);

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container); //Set the WebApi DependencyResolver
        }
    }
}