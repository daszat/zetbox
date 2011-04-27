
namespace Kistl.Tests.Utilities.MsSql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API.Server;

    public sealed class UtilityModule
        : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            // Do not register ALL types as the registration below does NOT override types properly
            //moduleBuilder
            //    .RegisterAssemblyTypes(typeof(UtilityModule).Assembly)
            //    .AsImplementedInterfaces();

            moduleBuilder
                .RegisterType<MsSqlResetter>()
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }
    }
}
