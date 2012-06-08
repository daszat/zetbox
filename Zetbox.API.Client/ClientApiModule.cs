
namespace Kistl.API.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Autofac;
    using Kistl.API.Client.PerfCounter;

    public sealed class ClientApiModule
        : Autofac.Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder
                .Register<ProxyImplementation>(c => new ProxyImplementation(
                    c.Resolve<InterfaceType.Factory>(),
                    c.Resolve<Kistl.API.Client.KistlService.IKistlService>(),
                    c.Resolve<IPerfCounter>(),
                    c.Resolve<KistlStreamReader.Factory>(),
                    c.Resolve<KistlStreamWriter.Factory>()
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

            moduleBuilder.RegisterType<KistlService.KistlServiceClient>()
                .AsImplementedInterfaces()
                .OnActivated(args =>
                {
                    args.Context.Resolve<ICredentialsResolver>().InitCredentials(args.Instance.ClientCredentials);
                })
                .InstancePerLifetimeScope();
        }
    }
}
