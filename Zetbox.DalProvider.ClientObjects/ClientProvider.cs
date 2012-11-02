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

namespace Zetbox.DalProvider.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using Zetbox.API.Client.PerfCounter;
    using Zetbox.API.Common;

    public interface IClientActionsManager : ICustomActionsManager { }

    public sealed class ClientProvider
        : Autofac.Module
    {
        private const string CLIENT_ASSEMBLY_NAME = "Zetbox.Objects.ClientImpl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7b69192d05046fdf";

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder.Register((c, p) =>
                {
                    var ilp = p.FirstOrDefault() as TypedParameter;
                    var il = ilp != null ? (ClientIsolationLevel)ilp.Value : ClientIsolationLevel.PrefereClientData;

                    return new ZetboxContextImpl(
                        il,
                        c.Resolve<ZetboxConfig>(),
                        c.Resolve<IProxy>(),
                        CLIENT_ASSEMBLY_NAME,
                        c.Resolve<Func<IFrozenContext>>(),
                        c.Resolve<InterfaceType.Factory>(),
                        c.Resolve<ClientImplementationType.ClientFactory>(),
                        c.Resolve<UnattachedObjectFactory>(),
                        c.Resolve<IPerfCounter>(),
                        c.Resolve<IIdentityResolver>());
                })
                .AsSelf()
                .As<IZetboxContext>()
                .As<IReadOnlyZetboxContext>()
                .OnActivated(args =>
                {
                    var manager = args.Context.Resolve<IClientActionsManager>();
                    manager.Init(args.Context.Resolve<IFrozenContext>());
                })
                .InstancePerDependency();

            moduleBuilder
                .RegisterType<ClientImplementationType>()
                .As<ClientImplementationType>()
                .As<ImplementationType>()
                .InstancePerDependency();

            // the following function has to be thread-independent of any context to allow the proxy to be async
            moduleBuilder.Register<UnattachedObjectFactory>(c =>
                {
                    var clientFactory = c.Resolve<ClientImplementationType.ClientFactory>();
                    var lazyContext = c.Resolve<Func<IFrozenContext>>();

                    return (UnattachedObjectFactory)(ifType =>
                    {
                        return (IPersistenceObject)Activator.CreateInstance(clientFactory(Type.GetType(ifType.Type.FullName + "Client" + Zetbox.API.Helper.ImplementationSuffix + "," + CLIENT_ASSEMBLY_NAME, true)).Type, lazyContext);
                    });
                })
                .SingleInstance();
        }
    }
}
