
namespace Zetbox.Client.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.Client.Presentables;

    public class WPFModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<WpfModelFactory>()
                .As<IViewModelFactory>()
                .As<IToolkit>()
                .SingleInstance();
        }
    }
}
