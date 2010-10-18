
namespace Kistl.DalProvider.EF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.Server.Generators;

    public sealed class EfGeneratorProvider
        : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder
                .RegisterType<Generator.EntityFrameworkGenerator>()
                .As<BaseDataObjectGenerator>()
                .SingleInstance();
        }
    }
}
