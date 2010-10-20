
namespace Kistl.DalProvider.NHibernate.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.Server.Generators;

    public sealed class NhGeneratorProvider
        : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<NhGenerator>()
                .As<BaseDataObjectGenerator>()
                .SingleInstance();
        }
    }
}
