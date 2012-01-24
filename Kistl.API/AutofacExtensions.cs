
namespace Kistl.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Autofac;
    using Autofac.Builder;

    public static class AutofacExtensions
    {
        public static void RegisterZBoxImplementors(this ContainerBuilder builder, Assembly source)
        {
            if (builder == null) { throw new ArgumentNullException("builder"); }
            if (source == null) { throw new ArgumentNullException("source"); }

            // Register all non static ActionClasses
            foreach (var t in source.GetTypes()
                                    .Where(t => !t.IsStatic()
                                        && t.GetCustomAttributes(typeof(Implementor), false).Length > 0))
            {
                builder
                    .RegisterType(t)
                    .SingleInstance();
            }
        }

        public static void RegisterCmdLineDataOption(this ContainerBuilder builder, string prototype, string description, object dataKey)
        {
            builder
               .RegisterInstance<CmdLineData>(new SimpleCmdLineData(prototype, description, dataKey))
               .ExternallyOwned();
        }

        public static void RegisterCmdLineAction(this ContainerBuilder builder, string prototype, string description, Action<ILifetimeScope, string> action)
        {
            builder
               .RegisterInstance<CmdLineAction>(new SimpleCmdLineAction(prototype, description, action))
               .ExternallyOwned();
        }
    }
}
