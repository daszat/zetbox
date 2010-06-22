using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Kistl.Client.Presentables;
using Kistl.API;

namespace Kistl.Client.WPF
{
    public class WPFModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register(c => new WpfModelFactory(
                    c.Resolve<ILifetimeScope>(),
                    c.Resolve<IUiThreadManager>(),
                    c.Resolve<IReadOnlyKistlContext>(Kistl.API.Helper.FrozenContextServiceName)))
                .As<IModelFactory>()
                .SingleInstance();
        }
    }
}
