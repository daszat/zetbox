using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;

namespace Kistl.Generator.ClickOnce
{
    public class ClickOnceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder
                .RegisterType<ClickOnceGenerator>()
                .As<AbstractBaseGenerator>()
                .SingleInstance();
        }
    }
}
