
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
                    IKistlContext ctx = c.Resolve<MemoryContext>();
                    ISchemaProvider schemaProvider = c.Resolve<ISchemaProvider>();
                    SchemaManagement.SchemaManager.LoadSavedSchemaInto(schemaProvider, ctx);
                    KistlConfig cfg = c.Resolve<KistlConfig>();

                    return new SchemaManagement.SchemaManager(
                        schemaProvider,
                        p.Named<IKistlContext>("newSchema"),
                        ctx,
                        cfg);
                })
                .InstancePerDependency();

            moduleBuilder
                .RegisterType<FrozenActionsManagerServer>()
                .As<FrozenActionsManager>()
                .SingleInstance();

            moduleBuilder
                .RegisterType<Server>()
                .SingleInstance();

            moduleBuilder
                .RegisterType<WcfServer>()
                .As<IKistlAppDomain>()
                .SingleInstance();

            moduleBuilder
                .RegisterType<Generators.Interfaces.InterfaceGenerator>()
                .As<Generators.BaseDataObjectGenerator>()
                .SingleInstance();
            moduleBuilder
                .RegisterType<Generators.ClientObjects.ClientObjectGenerator>()
                .As<Generators.BaseDataObjectGenerator>()
                .SingleInstance();

            moduleBuilder
                .RegisterType<Generators.Generator>()
                .SingleInstance();

            moduleBuilder
                .Register(c => new AutofacServiceHostFactory())
                .As<ServiceHostFactoryBase>()
                .SingleInstance();

            moduleBuilder
                .RegisterType<KistlService>()
                .InstancePerDependency();

            moduleBuilder
                .Register(c =>
                {
                    var ctx = c.Resolve<IReadOnlyKistlContext>();
                    var aCfg = c.Resolve<IAssemblyConfiguration>();

                    var cams = new CustomActionsManagerServer(aCfg);
                    cams.Init(ctx);

                    return cams;
                })
                .As<BaseCustomActionsManager>()
                .SingleInstance();

            moduleBuilder
                .RegisterType<CachingMetaDataResolver>()
                .As<IMetaDataResolver>()
                .PropertiesAutowired()
                .SingleInstance();

            moduleBuilder
                .RegisterType<ThreadPrincipalResolver>()
                .As<IIdentityResolver>()
                .InstancePerLifetimeScope();

            // TODO: move to separate MSSQL-specific assembly, since the SQL-Schema should be independent of the DalProvider
            moduleBuilder
                .RegisterType<Kistl.Server.SchemaManagement.SqlProvider.SqlServer>()
                .As<ISchemaProvider>()
                .InstancePerDependency();
        }
    }
}
