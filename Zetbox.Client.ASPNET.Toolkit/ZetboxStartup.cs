using Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Zetbox.API;
using Zetbox.API.Configuration;
using Zetbox.API.Utils;

namespace Zetbox.Client.ASPNET.Toolkit
{
    public abstract class ZetboxStartup
    {
        public ZetboxStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public ILifetimeScope AutofacContainer { get; protected set; }
        public abstract string ContentRootPath { get; }

        public virtual void ConfigureContainer(ContainerBuilder builder)
        {
            // Prepare zetbox
            Logging.Configure();

            var appBasePath = ContentRootPath;
            var zbBasePath = Path.Combine(appBasePath, "..");
            var configsPath = Path.Combine(zbBasePath, "Configs");

            var config = ZetboxConfig.FromFile(
                HostType.AspNetClient,
                string.Empty, // cfgFile
                ZetboxConfig.GetDefaultConfigName("Zetbox.Client.AspNet.xml", configsPath));

            // Make DocumentStore relative to HttpService
            config.Server.DocumentStore = Path.Combine(appBasePath, config.Server.DocumentStore);

            AssemblyLoader.Bootstrap(config);

            var allModules = config.Server.Modules.Concat(config.Client.Modules);
            AutoFacBuilder.CreateContainerBuilder(builder, config, allModules);

            builder.RegisterModule<AspNetClientModule>();
            builder.RegisterViewModels(typeof(ZetboxStartup).Assembly);
            builder.RegisterViewModels(this.GetType().Assembly);
        }
    }
}
