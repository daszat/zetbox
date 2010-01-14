
namespace Kistl.DalProvider.EF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Autofac.Builder;
    using Kistl.API;
    using Kistl.API.Server;
    using Kistl.Server.Generators;

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
                .As<IKistlServerContext>()
                .As<IKistlContext>()
                .FactoryScoped();

            moduleBuilder
                .RegisterGeneratedFactory<Func<IKistlContext>>()
                .ContainerScoped();

            moduleBuilder
                .Register<MemoryContext>(c =>
                {
                    // defer loading the assemblies until a memory context is needed to avoid 
                    // initialisation troubles

                    // TODO: decide whether to load this from configuration too?
                    System.Reflection.Assembly interfaces = System.Reflection.Assembly.Load("Kistl.Objects");
                    System.Reflection.Assembly implementation = System.Reflection.Assembly.Load("Kistl.Objects.Server");
                    return new MemoryContext(interfaces, implementation);
                })
                .FactoryScoped();

            moduleBuilder
                .RegisterGeneratedFactory<Func<MemoryContext>>()
                .ContainerScoped();

            moduleBuilder
                .Register<Generator.EntityFrameworkGenerator>()
                .As<BaseDataObjectGenerator>()
                .MemberOf<IEnumerable<BaseDataObjectGenerator>>();
        }
    }
}
