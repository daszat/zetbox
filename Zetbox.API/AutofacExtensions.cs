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
