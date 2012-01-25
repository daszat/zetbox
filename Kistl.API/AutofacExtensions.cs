
namespace Kistl.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Autofac;
    using Autofac.Builder;
    using Kistl.API.Configuration;
    using Kistl.API.Utils;

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
               .Register<CmdLineData>(c => new SimpleCmdLineData(c.Resolve<KistlConfig>(), prototype, description, dataKey))
               .As<Option>()
               .Named<CmdLineData>(prototype)
               .SingleInstance();
        }

        public static void RegisterCmdLineAction(this ContainerBuilder builder, string prototype, string description, Action<ILifetimeScope, string> action)
        {
            builder
               .Register<CmdLineAction>(c => new SimpleCmdLineAction(prototype, description, action))
               .As<Option>()
               .Named<CmdLineAction>(prototype)
               .SingleInstance();
        }
    }
}
