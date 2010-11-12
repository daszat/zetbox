
namespace Kistl.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;

    public sealed class GeneratorModule
        : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
               .RegisterType<InterfaceGenerator>()
               .As<AbstractBaseGenerator>()
               .SingleInstance();

            builder
                .RegisterType<Compiler>()
                .SingleInstance();
        }
    }
}
