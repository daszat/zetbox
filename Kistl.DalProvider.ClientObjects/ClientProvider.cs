
namespace Kistl.DalProvider.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Autofac;
    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.API.Configuration;
    using Kistl.API.Utils;

    public interface IClientActionsManager : ICustomActionsManager { }

    public sealed class ClientProvider
        : Autofac.Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder.Register(c =>
                {
                    return new KistlContextImpl(
                        c.Resolve<KistlConfig>(),
                        c.Resolve<IProxy>(),
                        Kistl.API.Helper.ClientAssembly,
                        c.Resolve<Func<IReadOnlyKistlContext>>(Kistl.API.Helper.FrozenContextServiceName),
                        c.Resolve<InterfaceType.Factory>(),
                        c.Resolve<ClientImplementationType.ClientFactory>());
                })
                .As<IKistlContext>()
                .As<IReadOnlyKistlContext>()
                .OnActivated(args =>
                {
                    var manager = args.Context.Resolve<IClientActionsManager>();
                    manager.Init(args.Context.Resolve<IReadOnlyKistlContext>(Kistl.API.Helper.FrozenContextServiceName));
                })
                .InstancePerDependency();

            moduleBuilder
                .RegisterType<ClientImplementationType>()
                .As<ClientImplementationType>()
                .As<ImplementationType>()
                .InstancePerDependency();
        }
    }
}
