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

namespace Zetbox.Client.ASPNET
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Autofac;
    using Autofac.Configuration;
    using Autofac.Integration.Mvc;
    using Autofac.Core;
    using Zetbox.Client.Presentables;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using System.IO;
    using Zetbox.API;

    public abstract class ZetboxMvcApplication : System.Web.HttpApplication
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Client.ASPNET");

        public abstract void RegisterGlobalFilters(GlobalFilterCollection filters);
        public abstract void RegisterRoutes(RouteCollection routes);

        protected void Application_Start()
        {
            Logging.Configure();
            Log.Info("Starting Zetbox Web Application");

            var cfgFile = System.Configuration.ConfigurationManager.AppSettings["ConfigFile"];

            var appBasePath = Server.MapPath("~/");
            var zbBasePath = Path.Combine(appBasePath, "..");
            var configsPath = Path.Combine(zbBasePath, "Configs");

            var config = ZetboxConfig.FromFile(
                HostType.AspNet,
                string.IsNullOrEmpty(cfgFile) ? string.Empty : Server.MapPath(cfgFile),
                ZetboxConfig.GetDefaultConfigName("Zetbox.Client.AspNet.xml", configsPath));

            // Make DocumentStore relative to HttpService
            config.Server.DocumentStore = Path.Combine(appBasePath, config.Server.DocumentStore);

            AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);
            var container = CreateMasterContainer(config);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        private IContainer CreateMasterContainer(ZetboxConfig config)
        {
            var allModules = config.Server.Modules
                     .Concat(config.Client.Modules);

            var builder = Zetbox.API.Utils.AutoFacBuilder.CreateContainerBuilder(config, allModules);
            ConfigureContainerBuilder(builder);
            var container = builder.Build();
            API.AppDomainInitializer.InitializeFrom(container);

            SetupModelBinder(container);
            return container;
        }

        protected virtual void SetupModelBinder(IContainer container)
        {
            foreach (var vmType in container
                .ComponentRegistry
                .Registrations
                .SelectMany(r => r.Services.OfType<TypedService>())
                .Where(s => typeof(ViewModel).IsAssignableFrom(s.ServiceType))
                .Where(s => s.ServiceType.IsAbstract == false)
                .Select(s => s.ServiceType))
            {
                ModelBinders.Binders.Add(vmType, container.Resolve<IZetboxModelBinder>());
            }
        }

        protected virtual void ConfigureContainerBuilder(ContainerBuilder builder)
        {
            builder.RegisterControllers(typeof(ZetboxMvcApplication).Assembly);
            builder.RegisterModelBinders(typeof(ZetboxMvcApplication).Assembly);
            builder.RegisterModelBinderProvider();

            builder
                .RegisterType<ZetboxModelBinder>()
                .As<IZetboxModelBinder>()
                .SingleInstance();

            builder
                .RegisterModule<AspNetClientModule>();
        }
    }
}
