
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

            moduleBuilder
                .Register<ProxyImplementation>(c => new ProxyImplementation(c.Resolve<InterfaceType.Factory>(), c.Resolve<ICredentialsResolver>(), c.Resolve<IToolkit>()))
                .As<IProxy>()
                .InstancePerDependency(); // No singelton!

            moduleBuilder
                .Register<ClientDeploymentRestrictor>(c => new ClientDeploymentRestrictor())
                .As<IDeploymentRestrictor>()
                .SingleInstance();
        }
    }
}
