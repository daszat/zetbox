
namespace Kistl.DALProvider.EF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac.Builder;
    using Kistl.API;
    using Kistl.API.Server;

    public class EfProvider : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder
                .Register(c => new SqlServerSchemaProvider())
                .As<ISchemaProvider>()
                .ContainerScoped();

            moduleBuilder
                .Register(c => new KistlDataContext())
                .As<IKistlContext>()
                .FactoryScoped();

            moduleBuilder
                .RegisterGeneratedFactory<Func<IKistlContext>>()
                .ContainerScoped();

            moduleBuilder
                .Register<MemoryContext.ConfiguringFactory>(c => () =>
                {
                    // defer loading the assemblies until a memory context is needed to avoid 
                    // initialisation troubles

                    // TODO: decide whether to load this from configuration too?
                    System.Reflection.Assembly interfaces = System.Reflection.Assembly.Load("Kistl.Objects");
                    System.Reflection.Assembly implementation = System.Reflection.Assembly.Load("Kistl.Objects.Server");
                    return new MemoryContext(interfaces, implementation);
                })
                .ContainerScoped();
        }
    }
}
