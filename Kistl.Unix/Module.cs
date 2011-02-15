
namespace Kistl.Unix
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Autofac;

    public class Module
        : Autofac.Module
    {
        protected override void Load(Autofac.ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<PosixIdentitySource>()
                .AsImplementedInterfaces().
                .InstancePerLifetimeScope();
        }
    }
}
