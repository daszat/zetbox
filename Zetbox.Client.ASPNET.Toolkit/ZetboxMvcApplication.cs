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
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Autofac;
    using Autofac.Configuration;
    using Autofac.Core;
    using Autofac.Integration.Mvc;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.API.Client.PerfCounter;
    using Zetbox.API.Utils;
    using Zetbox.Client.Presentables;

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
                HostType.AspNetClient,
                string.IsNullOrEmpty(cfgFile) ? string.Empty : cfgFile,
                ZetboxConfig.GetDefaultConfigName("Zetbox.Client.AspNet.xml", configsPath));

            // Make DocumentStore relative to HttpService
            config.Server.DocumentStore = Path.Combine(appBasePath, config.Server.DocumentStore);

            AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);

            CreateMasterContainer(config);

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        private void CreateMasterContainer(ZetboxConfig config)
        {
            var allModules = config.Server.Modules
                     .Concat(config.Client.Modules);

            var builder = Zetbox.API.Utils.AutoFacBuilder.CreateContainerBuilder(config, allModules);
            ConfigureContainerBuilder(builder);

            var container = builder.Build();
            container.ApplyPerfCounterTracker();

            SetupModelBinder(container);
            SetupValidation(container);

            API.AppDomainInitializer.InitializeFrom(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        protected virtual void SetupValidation(IContainer container)
        {
            // Remove DataErrorInfoModelValidatorProvider
            // Our IZetboxViewModelBinder will register
            // HTML Fields at the IValidationManager
            // ZetboxController.UpdateModelState() will 
            // write those errors to the ModelStateDictionary
            var toRemove = ModelValidatorProviders.Providers.OfType<DataErrorInfoModelValidatorProvider>().ToList();
            foreach(var r in toRemove)
            {
                ModelValidatorProviders.Providers.Remove(r);
            }
        }

        protected virtual void SetupModelBinder(IContainer container)
        {
            ModelBinderProviders.BinderProviders.Add(container.Resolve<IZetboxViewModelBinderProvider>());
            ModelBinderProviders.BinderProviders.Add(container.Resolve<ILookupDictionaryModelBinderProvider>());
        }

        protected virtual void ConfigureContainerBuilder(ContainerBuilder builder)
        {
            builder.RegisterControllers(typeof(ZetboxMvcApplication).Assembly);
            builder.RegisterModelBinders(typeof(ZetboxMvcApplication).Assembly);
            builder.RegisterModelBinderProvider();

            // Register our model binder manual, as they also deal with derived impelementations
            builder
                .RegisterType<ZetboxViewModelBinderProvider>()
                .As<IZetboxViewModelBinderProvider>()
                .SingleInstance();

            builder
                .RegisterType<ZetboxViewModelBinder>()
                .As<IZetboxViewModelBinder>()
                .InstancePerHttpRequest(); // ZetboxViewModelBinder has some http request dependencies

            builder
                .RegisterType<LookupDictionaryModelBinderProvider>()
                .As<ILookupDictionaryModelBinderProvider>()
                .SingleInstance();

            builder
                .RegisterType<LookupDictionaryModelBinder>()
                .As<ILookupDictionaryModelBinder>()
                .SingleInstance();

            builder.RegisterModule<AspNetClientModule>();

            // Register zetbox specific ViewModels
            builder.RegisterViewModels(typeof(ZetboxMvcApplication).Assembly);

            // Register target applications specific Controller and ViewModels
            builder.RegisterControllers(this.GetType().Assembly);
            builder.RegisterViewModels(this.GetType().Assembly);
        }
    }
}
