
namespace Zetbox.Tests.Utilities.PostgreSql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API.Server;
    using Zetbox.API.Configuration;
    using Zetbox.API.AbstractConsumerTests;

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
                .Register<PostgreSqlResetter>(c => new PostgreSqlResetter(c.Resolve<ZetboxConfig>(), c.ResolveNamed<ISchemaProvider>("POSTGRESQL"), c.Resolve<Zetbox.API.ITempFileService>()))
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }
    }
}
