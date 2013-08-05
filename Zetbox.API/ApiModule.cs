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
    using Zetbox.API.Utils;

    public sealed class ApiModule
        : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<InterfaceType.FactoryImpl>()
                .InstancePerLifetimeScope();

            // register the FactoryImpl's method as factory
            // this way the ABI doesn't change
            builder
                .Register<InterfaceType.Factory>(c => c.Resolve<InterfaceType.FactoryImpl>().Invoke)
                .InstancePerLifetimeScope();

            builder
                .Register<LoggingProblemReporter>(c => new LoggingProblemReporter())
                .As<IProblemReporter>()
                .SingleInstance();

            builder
                .RegisterModule(new SmtpMailSender.Module());

            builder
                .RegisterAssemblyTypes(typeof(ApiModule).Assembly)
                .Except<SimpleCmdLineAction>()
                .AssignableTo<CmdLineAction>()
                .As<Option>()
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(typeof(ApiModule).Assembly)
                .Except<SimpleCmdLineData>()
                .Except<SimpleCmdLineFlag>()
                .AssignableTo<CmdLineData>()
                .As<Option>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<ServiceControlManager>()
                .As<IServiceControlManager>()
                .SingleInstance();

            builder
                .RegisterType<DefaultFileOpener>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .RegisterType<ZetboxStreamReader.Factory>()
                .AsSelf()
                .InstancePerDependency();

            builder
                .RegisterType<ZetboxStreamWriter.Factory>()
                .AsSelf()
                .InstancePerDependency();

            builder
                .RegisterType<TypeMap>()
                .AsSelf()
                .SingleInstance();

            builder
                .RegisterType<TempFileService>()
                .As<ITempFileService>()
                .SingleInstance();
        }
    }
}
