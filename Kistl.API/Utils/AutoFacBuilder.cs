
namespace Kistl.API.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    using Autofac;
    using Autofac.Core;
    using Kistl.API.Configuration;
    
    public static class AutoFacBuilder
    {
        public static ContainerBuilder CreateContainerBuilder(KistlConfig config)
        {
            return CreateContainerBuilder(config, null);
        }

        public static ContainerBuilder CreateContainerBuilder(KistlConfig config, string[] modules)
        {
            if (config == null) throw new ArgumentNullException("config");

            var builder = new ContainerBuilder();

            // register the configuration
            builder
                .RegisterInstance(config)
                .ExternallyOwned()
                .SingleInstance();

            foreach (var m in modules ?? new string[] { })
            {
                try
                {
                    Logging.Log.InfoFormat("Adding module [{0}]", m);
                    builder.RegisterModule((IModule)Activator.CreateInstance(Type.GetType(m, true)));
                }
                catch (Exception ex)
                {
                    Logging.Log.Error(String.Format("Unable to register Module [{0}] from Config", m), ex);
                    throw;
                }
            }
            return builder;
        }
    }
}
