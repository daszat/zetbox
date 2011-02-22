
namespace Kistl.Tests.Utilities.PostgreSql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API.Server;
    using Kistl.API.Configuration;

    public sealed class UtilityModule
        : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder
                .RegisterAssemblyTypes(typeof(UtilityModule).Assembly)
                .AsImplementedInterfaces();

            moduleBuilder
                .Register<PostgreSqlResetter>(c => new PostgreSqlResetter(c.Resolve<KistlConfig>(), c.ResolveNamed<ISchemaProvider>("POSTGRESQL")))
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }
    }
}
