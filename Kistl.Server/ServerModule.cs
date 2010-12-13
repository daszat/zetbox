
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
                    ISchemaProvider schemaProvider = c.ResolveNamed<ISchemaProvider>(cfg.Server.SchemaProvider);
                    schemaProvider.Open(cfg.Server.ConnectionString);
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

            //moduleBuilder
            //    .RegisterType<Generators.Interfaces.InterfaceGenerator>()
            //    .As<Generators.BaseDataObjectGenerator>()
            //    .SingleInstance();
            //moduleBuilder
            //    .RegisterType<Generators.ClientObjects.ClientObjectGenerator>()
            //    .As<Generators.BaseDataObjectGenerator>()
            //    .SingleInstance();

            //moduleBuilder
            //    .RegisterType<Generators.Generator>()
            //    .SingleInstance();

            moduleBuilder
                .Register(c => new AutofacServiceHostFactory())
                .As<ServiceHostFactoryBase>()
                .SingleInstance();

            moduleBuilder
                .RegisterType<KistlService>()
                .SingleInstance();

            moduleBuilder
                .RegisterType<BootstrapperService>()
                .SingleInstance();

            moduleBuilder
                .RegisterType<CachingMetaDataResolver>()
                .As<CachingMetaDataResolver>()
                .As<IMetaDataResolver>()
                .SingleInstance();

            moduleBuilder
                .RegisterType<ThreadPrincipalResolver>()
                .As<IIdentityResolver>()
                .InstancePerLifetimeScope();

            // TODO: move to separate SchemaProvider-specific assembly, since the SQL-Schema should be independent of the DalProvider
            moduleBuilder
                .Register(c => new SchemaManagement.LoggingSchemaProviderAdapter(new SchemaManagement.NpgsqlProvider.Postgresql()))
                .As<ISchemaProvider>()
                .Named<ISchemaProvider>("POSTGRESQL")
                .InstancePerDependency();
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
        }
    }
}
