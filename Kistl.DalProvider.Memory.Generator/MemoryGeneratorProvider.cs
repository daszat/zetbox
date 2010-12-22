
namespace Kistl.DalProvider.Memory.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.Generator;

    public class MemoryGeneratorProvider
        : Autofac.Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder
                .RegisterType<MemoryGenerator>()
                .As<AbstractBaseGenerator>()
                .SingleInstance();
        }
    }
}
