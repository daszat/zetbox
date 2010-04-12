using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Kistl.API.Configuration;
using Autofac.Core;

namespace Kistl.API.Utils
{
    public static class AutoFacBuilder
    {
        public static ContainerBuilder CreateContainerBuilder(KistlConfig config, string[] modules)
        {
            if (config == null) throw new ArgumentNullException("config");

            var builder = new ContainerBuilder();

            // register the configuration
            builder
                .RegisterInstance(config)
                .ExternallyOwned()
                .SingleInstance();

            var empty = new string[] { };

            foreach (var m in modules)
            {
                try
                {
                    builder.RegisterModule((IModule)Activator.CreateInstance(Type.GetType(m, true)));
                }
                catch (Exception ex)
                {
                    Logging.Log.Error("Unable to register Module from Config", ex);
                }
            }
            return builder;
        }
    }
}
