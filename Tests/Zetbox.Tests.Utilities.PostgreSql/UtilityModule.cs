
namespace Kistl.Tests.Utilities.PostgreSql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API.Server;
    using Kistl.API.Configuration;
    using Kistl.API.AbstractConsumerTests;

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
                .Register<PostgreSqlResetter>(c => new PostgreSqlResetter(c.Resolve<KistlConfig>(), c.ResolveNamed<ISchemaProvider>("POSTGRESQL"), c.Resolve<Kistl.API.ITempFileService>()))
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }
    }
}
