
namespace Kistl.App.Projekte.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API;
    using Kistl.API.Server;

    public class CustomServerActionsModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            // Register all non static ActionClasses
            moduleBuilder
                .RegisterAssemblyTypes(this.GetType().Assembly)
                .AsSelf()
                .SingleInstance();

            // Register types explicit
            moduleBuilder.Register(c => new ZBox.App.SchemaMigration.CustomServerActions_SchemaMigration(
                c.Resolve<SchemaProviderFactory>("MSSQL"),
                c.Resolve<SchemaProviderFactory>("POSTGRESQL"),
                c.Resolve<SchemaProviderFactory>("OLEDB")
            ));
        }
    }
}
