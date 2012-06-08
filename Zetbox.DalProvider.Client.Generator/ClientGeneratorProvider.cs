
namespace Zetbox.DalProvider.Client.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.Generator;

    public sealed class ClientGeneratorProvider
        : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder
                .RegisterType<ClientGenerator>()
                .As<AbstractBaseGenerator>()
                .SingleInstance();
        }
    }
}
