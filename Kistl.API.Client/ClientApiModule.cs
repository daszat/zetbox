
namespace Kistl.API.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Autofac;

    public sealed class ClientApiModule
        : Autofac.Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            // TODO: should be moved to a WcfClient module
            moduleBuilder
                .Register<ProxyImplementation>(c => new ProxyImplementation(c.Resolve<InterfaceType.Factory>(), c.Resolve<ICredentialsResolver>(), c.Resolve<IToolkit>()))
                .Named<IProxy>("implementor")
                .InstancePerDependency(); // No singelton!

            moduleBuilder
                .RegisterDecorator<IProxy>(
                    (c, inner) => new InfoLoggingProxyDecorator(inner),
                    "implementor");

            moduleBuilder
                .Register<ClientDeploymentRestrictor>(c => new ClientDeploymentRestrictor())
                .As<IDeploymentRestrictor>()
                .SingleInstance();
        }
    }
}
