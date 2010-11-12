
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

            moduleBuilder.Register((c,p) =>
                {
                    var ilp = p.FirstOrDefault() as TypedParameter;
                    var il = ilp != null ? (ClientIsolationLevel)ilp.Value : ClientIsolationLevel.PrefereClientData;
                    
                    return new KistlContextImpl(
                        il,
                        c.Resolve<KistlConfig>(),
                        c.Resolve<IProxy>(),
                        "Kistl.Objects.ClientImpl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7b69192d05046fdf",
                        c.Resolve<Func<IFrozenContext>>(),
                        c.Resolve<InterfaceType.Factory>(),
                        c.Resolve<ClientImplementationType.ClientFactory>());
                })
                .As<IKistlContext>()
                .As<IReadOnlyKistlContext>()
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
        }
    }
}
