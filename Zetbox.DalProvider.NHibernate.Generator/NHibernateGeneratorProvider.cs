
namespace Zetbox.DalProvider.NHibernate.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.Generator;

    public sealed class NHibernateGeneratorProvider
        : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<NHibernateGenerator>()
                .As<AbstractBaseGenerator>()
                .SingleInstance();
        }
    }
}
