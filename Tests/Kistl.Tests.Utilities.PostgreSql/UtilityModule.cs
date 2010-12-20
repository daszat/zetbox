
namespace Kistl.Tests.Utilities.PostgreSql
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

            moduleBuilder
                .RegisterAssemblyTypes(this.GetType().Assembly)
                .AsImplementedInterfaces();
        }
    }
}
