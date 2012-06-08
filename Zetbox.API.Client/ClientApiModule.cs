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

namespace Zetbox.API.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Autofac;
    using Zetbox.API.Client.PerfCounter;

    public sealed class ClientApiModule
        : Autofac.Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder
                .Register<ProxyImplementation>(c => new ProxyImplementation(
                    c.Resolve<InterfaceType.Factory>(),
                    c.Resolve<Zetbox.API.Client.ZetboxService.IZetboxService>(),
                    c.Resolve<IPerfCounter>(),
                    c.Resolve<ZetboxStreamReader.Factory>(),
                    c.Resolve<ZetboxStreamWriter.Factory>()
                    ))
                .Named<IProxy>("implementor")
                .InstancePerDependency(); // No singleton!

            moduleBuilder
                .RegisterDecorator<IProxy>(
                    (c, inner) => new InfoLoggingProxyDecorator(inner),
                    "implementor");

            moduleBuilder
                .Register<ClientDeploymentRestrictor>(c => new ClientDeploymentRestrictor())
                .As<IDeploymentRestrictor>()
                .SingleInstance();

            moduleBuilder
               .RegisterModule<Log4NetAppender.Module>();

            moduleBuilder
                .RegisterType<PerfCounterDispatcher>()
                .As<IPerfCounter>()
                .OnActivated(args => args.Instance.Initialize(args.Context.Resolve<IFrozenContext>()))
                .OnRelease(obj => obj.Dump())
                .SingleInstance();
        }
    }

    public sealed class HttpClientModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder.RegisterType<HttpServiceClient>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }

    public sealed class WcfClientModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder.RegisterType<ZetboxService.ZetboxServiceClient>()
                .AsImplementedInterfaces()
                .OnActivated(args =>
                {
                    args.Context.Resolve<ICredentialsResolver>().InitCredentials(args.Instance.ClientCredentials);
                })
                .InstancePerLifetimeScope();
        }
    }
}
