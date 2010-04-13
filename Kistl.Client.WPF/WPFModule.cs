using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Kistl.Client.Presentables;

namespace Kistl.Client.WPF
{
    public class WPFModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<WpfModelFactory>()
                .As<IModelFactory>()
                .SingleInstance();
        }
    }
}
