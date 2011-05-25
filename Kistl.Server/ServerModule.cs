
namespace Kistl.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.Text;
    using Autofac;
    using Autofac.Integration.Wcf;
    using Kistl.API;
    using Kistl.API.Common;
    using Kistl.API.Configuration;
    using Kistl.API.Server;
    using Kistl.App.Extensions;

    public class ServerModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder
                .Register((c, p) =>
                {
                    KistlConfig cfg = c.Resolve<KistlConfig>();
                    IKistlContext ctx = c.Resolve<BaseMemoryContext>();
                    var connectionString = cfg.Server.GetConnectionString(Kistl.API.Helper.KistlConnectionStringKey);
                    ISchemaProvider schemaProvider = c.ResolveNamed<ISchemaProvider>(connectionString.SchemaProvider);
                    schemaProvider.Open(connectionString.ConnectionString);
                    SchemaManagement.SchemaManager.LoadSavedSchemaInto(schemaProvider, ctx);

                    return new SchemaManagement.SchemaManager(
                        schemaProvider,
                        p.Named<IKistlContext>("newSchema"),
                        ctx,
                        cfg);
                })
                .InstancePerDependency();

            moduleBuilder
                .RegisterType<Server>()
                .As<IServer>()
                .SingleInstance();

            moduleBuilder
                .RegisterType<WcfServer>()
                .As<IKistlAppDomain>()
                .SingleInstance();

            moduleBuilder
                .Register(c => new AutofacServiceHostFactory())
                .As<AutofacServiceHostFactory>()
                .SingleInstance();

            moduleBuilder
                .Register(c => new AutofacWebServiceHostFactory())
                .As<AutofacWebServiceHostFactory>()
                .SingleInstance();

            moduleBuilder
                .RegisterType<KistlService>()
                .As<KistlService>() // registration for WCF
                .As<IKistlService>() // registration for KistlServiceFacade
                .SingleInstance();

            moduleBuilder
                .RegisterType<BootstrapperService>()
                .SingleInstance();

            moduleBuilder
                .RegisterType<ThreadPrincipalResolver>()
                .As<IIdentityResolver>()
                .InstancePerLifetimeScope();

            // TODO: move to separate SchemaProvider-specific assembly, since the SQL-Schema should be independent of the DalProvider
            moduleBuilder
                .Register(c => new SchemaManagement.LoggingSchemaProviderAdapter(new SchemaManagement.OleDbProvider.OleDb()))
                .As<ISchemaProvider>()
                .Named<ISchemaProvider>("OLEDB")
                .InstancePerDependency();
            moduleBuilder
                .Register(c => new SchemaManagement.LoggingSchemaProviderAdapter(new SchemaManagement.SqlProvider.SqlServer()))
                .As<ISchemaProvider>()
                .Named<ISchemaProvider>("MSSQL")
                .InstancePerDependency();
            moduleBuilder
                .Register(c => new SchemaManagement.LoggingSchemaProviderAdapter(new SchemaManagement.NpgsqlProvider.Postgresql()))
                .As<ISchemaProvider>()
                .Named<ISchemaProvider>("POSTGRESQL")
                .InstancePerDependency();

#if !MONO
            moduleBuilder
                .Register(c => new ActiveDirectoryIdentitySource())
                .As<IIdentitySource>()
                .InstancePerLifetimeScope();
#endif
        }
    }
}
