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
    using System.Collections.Generic;
    using System.ComponentModel;
    using Autofac;
    using Zetbox.API.Client.PerfCounter;
    using Zetbox.API.Configuration;

    public sealed class ClientApiModule
        : Autofac.Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder
                .Register<ProxyImplementation>(c => new ProxyImplementation(
                    c.Resolve<InterfaceType.Factory>(),
                    c.Resolve<IImplementationTypeChecker>(),
                    c.Resolve<UnattachedObjectFactory>(),
                    c.Resolve<Zetbox.API.Client.ZetboxService.IZetboxService>(),
                    c.Resolve<IPerfCounter>(),
                    c.Resolve<ZetboxStreamReader.Factory>(),
                    c.Resolve<ZetboxStreamWriter.Factory>(),
                    c.Resolve<IEnumerable<SerializingTypeMap>>()
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
                .RegisterType<PerfCounterDispatcher>()
                .As<IPerfCounter>()
                .OnRelease(obj => obj.Dump())
                .SingleInstance();

            moduleBuilder
                .Register<TestScreenshotTool>(c => new TestScreenshotTool())
                .As<IScreenshotTool>()
                .SingleInstance();
        }
    }

    [Feature]
    [Description("HTTP Proxy implementation")]
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
}
