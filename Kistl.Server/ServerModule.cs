using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

using Autofac;
using Autofac.Builder;
using Autofac.Integration.Wcf;
using Kistl.API;
using Kistl.API.Server;

namespace Kistl.Server
{
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

                    return new SchemaManagement.SchemaManager(
                        schemaProvider,
                        p.Named<IKistlContext>("newSchema"),
                        ctx);
                })
                .FactoryScoped();

            moduleBuilder
                .Register<Server>()
                .SingletonScoped();

            moduleBuilder
                .Register<WcfServer>()
                .As<IKistlAppDomain>()
                .SingletonScoped();

            moduleBuilder
                .RegisterCollection<Generators.BaseDataObjectGenerator>()
                .SingletonScoped();

            moduleBuilder
                .Register<Generators.Interfaces.InterfaceGenerator>()
                .As<Generators.BaseDataObjectGenerator>()
                .MemberOf<IEnumerable<Generators.BaseDataObjectGenerator>>()
                .SingletonScoped();
            moduleBuilder
                .Register<Generators.ClientObjects.ClientObjectGenerator>()
                .As<Generators.BaseDataObjectGenerator>()
                .MemberOf<IEnumerable<Generators.BaseDataObjectGenerator>>()
                .SingletonScoped();

            moduleBuilder
                .Register<Generators.Generator>()
                .SingletonScoped();

            moduleBuilder
                .Register(c => new AutofacServiceHostFactory())
                .SingletonScoped();

            moduleBuilder
                .Register<KistlService>()
                .FactoryScoped();
        }
    }
}
