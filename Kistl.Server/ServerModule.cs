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
                    return new SchemaManagement.SchemaManager(
                        c.Resolve<ISchemaProvider>(),
                        p.Named<IKistlContext>("ctx"),
                        c.Resolve<MemoryContext.ConfiguringFactory>());
                })
                .FactoryScoped();

            moduleBuilder
                .Register<Server>()
                .ContainerScoped();
        }
    }
}
