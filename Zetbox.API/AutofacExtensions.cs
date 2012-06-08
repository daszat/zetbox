
namespace Zetbox.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Autofac;
    using Autofac.Builder;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;

    public static class AutofacExtensions
    {
        public static void RegisterZetboxImplementors(this ContainerBuilder builder, Assembly source)
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
               .Register<CmdLineData>(c => new SimpleCmdLineData(c.Resolve<ZetboxConfig>(), prototype, description, dataKey))
               .As<Option>()
               .Named<CmdLineData>(prototype)
               .SingleInstance();
        }

        public static void RegisterCmdLineFlag(this ContainerBuilder builder, string prototype, string description, object dataKey)
        {
            builder
               .Register<CmdLineData>(c => new SimpleCmdLineFlag(c.Resolve<ZetboxConfig>(), prototype, description, dataKey))
               .As<Option>()
               .Named<CmdLineData>(prototype)
               .SingleInstance();
        }

        public static void RegisterCmdLineAction(this ContainerBuilder builder, string prototype, string description, Action<ILifetimeScope> action)
        {
            builder
               .Register<CmdLineAction>(c => new SimpleCmdLineAction(c.Resolve<ZetboxConfig>(), prototype, description, action))
               .As<Option>()
               .Named<CmdLineAction>(prototype)
               .SingleInstance();
        }

        public static void RegisterCmdLineAction(this ContainerBuilder builder, string prototype, string description, Action<ILifetimeScope, string> action)
        {
            builder
               .Register<CmdLineAction>(c => new SimpleCmdLineAction(c.Resolve<ZetboxConfig>(), prototype, description, action))
               .As<Option>()
               .Named<CmdLineAction>(prototype)
               .SingleInstance();
        }

        public static void RegisterCmdLineListAction(this ContainerBuilder builder, string prototype, string description, Action<ILifetimeScope, string[]> listAction)
        {
            builder
               .Register<CmdLineAction>(c => new SimpleCmdLineAction(c.Resolve<ZetboxConfig>(), prototype, description, listAction))
               .As<Option>()
               .Named<CmdLineAction>(prototype)
               .SingleInstance();
        }
    }
}
