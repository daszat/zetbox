
namespace Zetbox.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.Text;
    using Autofac;
    using Autofac.Integration.Wcf;
    using Zetbox.API;
    using Zetbox.API.Common;
    using Zetbox.API.Configuration;
    using Zetbox.API.Server;
    using Zetbox.App.Extensions;

    public class ServerModule : Module
    {
        public static object NoWcfKey { get { return "nowcf"; } }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register((c, p) =>
                {
                    ZetboxConfig cfg = c.Resolve<ZetboxConfig>();
                    IZetboxContext ctx = c.Resolve<BaseMemoryContext>();
                    var connectionString = cfg.Server.GetConnectionString(Zetbox.API.Helper.ZetboxConnectionStringKey);
                    ISchemaProvider schemaProvider = c.ResolveNamed<ISchemaProvider>(connectionString.SchemaProvider);
                    schemaProvider.Open(connectionString.ConnectionString);
                    SchemaManagement.SchemaManager.LoadSavedSchemaInto(schemaProvider, ctx);

                    return new SchemaManagement.SchemaManager(
                        schemaProvider,
                        p.Named<IZetboxContext>("newSchema"),
                        ctx,
                        cfg);
                })
                .InstancePerDependency();

            builder
                .RegisterType<Server>()
                .As<IServer>()
                .SingleInstance();

            builder
                .RegisterType<WcfServer>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .RegisterCmdLineFlag("nowcf", "Do not run the embedded WCF Server; running it is the default when no action parameter is specified", NoWcfKey);

            builder
                .Register(c => new AutofacServiceHostFactory())
                .As<AutofacServiceHostFactory>()
                .SingleInstance();

            builder
                .Register(c => new AutofacWebServiceHostFactory())
                .As<AutofacWebServiceHostFactory>()
                .SingleInstance();

            builder
                .RegisterType<ZetboxService>()
                .As<ZetboxService>() // registration for WCF
                .As<IZetboxService>() // registration for ZetboxServiceFacade
                .InstancePerLifetimeScope();

            builder
                .RegisterType<BootstrapperService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<ThreadPrincipalResolver>()
                .As<IIdentityResolver>()
                .InstancePerLifetimeScope();

            // TODO: move to separate SchemaProvider-specific assembly, since the SQL-Schema should be independent of the DalProvider
            builder
                .Register(c => new SchemaManagement.LoggingSchemaProviderAdapter(new SchemaManagement.OleDbProvider.OleDb()))
                .As<ISchemaProvider>()
                .Named<ISchemaProvider>("OLEDB")
                .InstancePerDependency();
            builder
                .Register(c => new SchemaManagement.LoggingSchemaProviderAdapter(new SchemaManagement.SqlProvider.SqlServer()))
                .As<ISchemaProvider>()
                .Named<ISchemaProvider>("MSSQL")
                .InstancePerDependency();
            builder
                .Register(c => new SchemaManagement.LoggingSchemaProviderAdapter(new SchemaManagement.NpgsqlProvider.Postgresql()))
                .As<ISchemaProvider>()
                .Named<ISchemaProvider>("POSTGRESQL")
                .InstancePerDependency();

#if !MONO
            builder
                .Register(c => new ActiveDirectoryIdentitySource())
                .As<IIdentitySource>()
                .InstancePerLifetimeScope();
#endif
        }
    }
}
