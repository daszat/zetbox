
namespace Kistl.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    
    /// <summary>
    /// Mainly for testing; generates classes without any hooks filled
    /// </summary>
    public sealed class BaseGeneratorModule
        : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder
                .RegisterType<BaseGenerator>()
                .As<AbstractBaseGenerator>()
                .SingleInstance();
        }
    }
}
