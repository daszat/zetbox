using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autofac;
using Autofac.Builder;
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
                .ContainerScoped();
        }
    }
}
